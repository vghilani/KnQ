using GameSparks.Api.Requests;
using GameSparks.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class New_CreateMenu : MonoBehaviour
{

    public static List<string> ThesePlayerNames = new List<string>();
    public static List<string> PlayerIDs = new List<string>();
	public static List<playersData> playersData = new List<playersData>();
    public List<Text> NamesDisplayed = new List<Text>();
    public string GameName;
    public Text Validation;
    public Button Create;
    public Button Reset;
    public static bool UpdateList = false;
    public Dropdown GameNameDropdown;
    private bool isGameReady;
    public Text Information;
    private float Timer = 0;
    private bool shouldJoin = false;
    private string ChallangeToJoin;
    public GameObject PupUpPanel;
    public GameObject PanelSpawnPoint;
	public Dropdown TurnLimitDropDown;
	public int TurnLimitValue = 0;

    void Start()
    {
        HideCreateButton();
        HideReset();
    }
    void Update()
    {

        if (Information.text != "" || Information.text != null)
        {
            if (Timer > 0)
            {
                Timer--;
                if (Timer <= 0)
                {
                    Information.text = "";
                    Timer = 0;
                }
            }
        }

        if (UpdateList)
        {
            if (ThesePlayerNames.Count == 0 && PlayerIDs.Count == 0)
            {
                PlayerIDs.Add(mainMenu.myID);
                ThesePlayerNames.Add(mainMenu.myName);
                NamesDisplayed[0].text = ThesePlayerNames[0];
            }

            if (ThesePlayerNames.Count > 1)
            {
                ShowReset();
                int i = 1;
                foreach (string s in ThesePlayerNames)
                {
                    if (s == mainMenu.myName)
                    {
                        continue;
                    }
                    else
                    {
                        NamesDisplayed[i].text = s;
                        i++;
                    }
                }
            }
            if (ThesePlayerNames.Count <= 1 || GameName != null && GameName != "")
            {
                HideReset();
            }
            if (GameName != null && GameName != "")
            {
                ShowReset();
            }

            if (PlayerIDs.Count >= 1 && GameName != null && GameName != "")
            {
                ShowCreateButton();
            }
            else
            {
                HideCreateButton();
            }
            UpdateList = false;
        }

        if (shouldJoin)
        {
            shouldJoin = false;
            JoinPublicGame(ChallangeToJoin);
        }

    }

    //turn limit dropdown
    public void TurnLimitDropdownChange()
	{
		if(TurnLimitDropDown.value > 0)
		{
			if(TurnLimitDropDown.options[TurnLimitDropDown.value].text == "5 minutes")
			{
				TurnLimitValue = 300; //5 minutes is 300 seconds
			}
			if (TurnLimitDropDown.options[TurnLimitDropDown.value].text == "1 hour")
            {
				TurnLimitValue = 3600;
            }
			if (TurnLimitDropDown.options[TurnLimitDropDown.value].text == "3 hours")
            {
				TurnLimitValue = 10800;
            }
			if (TurnLimitDropDown.options[TurnLimitDropDown.value].text == "30 seconds")
            {
                TurnLimitValue = 30;
            }
		}
		else
		{
			TurnLimitValue = 0;
		}

		TurnLimitDropDown.Hide();
	}

    //gamename dropdown
    public void DropdownChange()
    {
        if (GameNameDropdown.value > 0)
        {
            GameName = GameNameDropdown.options[GameNameDropdown.value].text;
        }
        else
        {
            GameName = "";
        }
        UpdateList = true;
        GameNameDropdown.Hide();
    }
    public void HideCreateButton()
    {
        Create.gameObject.SetActive(false);
        Validation.gameObject.SetActive(true);
    }
    public void ShowCreateButton()
    {
        Create.gameObject.SetActive(true);
        Validation.gameObject.SetActive(false);
    }
    public void HideReset()
    {
        Reset.gameObject.SetActive(false);
    }
    public void ShowReset()
    {
        Reset.gameObject.SetActive(true);
    }
    public void CreateGame()
    {
        if (PlayerIDs.Count >= 1 && PlayerIDs.Count <= 4) //make it public, include eligibility criteria (I changed this to include FOUR PLAYERS, so a player who declines turns their seat into a public seat...
        {
            new CreateChallengeRequest()
            .SetAutoStartJoinedChallengeOnMaxPlayers(true)
            .SetChallengeMessage(GameName)
            .SetChallengeShortCode("4v4")
            .SetEndTime(System.DateTime.Now.AddHours(72))
            .SetMinPlayers(1)
            .SetAccessType("PUBLIC")
            .SetMaxPlayers(4)
            .Send((response) =>
            {

                if (!response.HasErrors)
                {
                    Debug.Log("Game Created");

                    Information.text = "Game created!";
                    Timer = 500;

                    GameObject game2 = Instantiate(GameObject.Find("Menu Manager").GetComponent<mainMenu>().GameListTag, GameObject.Find("Menu Manager").GetComponent<mainMenu>().spawnpoint4.transform);
                    game2.GetComponent<GameInviteMessage>().challengeId = response.ChallengeInstanceId;
                    GameObject.Find("Menu Manager").GetComponent<mainMenu>().ListOfGames.Add(game2);
                    game2.GetComponent<GameInviteMessage>().Message.text = "The Battle of " + GameName + " is about to begin! (players must accept invites)";
                    game2.GetComponent<GameInviteMessage>().GreenButton.gameObject.SetActive(false);

                    GameObject.Find("Menu Manager").GetComponent<mainMenu>().BackFromPublicCreate();
                    InvitePlayersToPublicGame(response.ChallengeInstanceId);
					game2.GetComponent<GameInviteMessage>().PopulateGameList(response.ChallengeInstanceId);
                }
                else
                {
                    Debug.Log("Game Creation Failed. Players: " + PlayerIDs.Count);
                    Information.text = "An Error Occured, please try again!";
                    Timer = 500;
                }
            });

        }
    }
    public void InvitePlayersToPublicGame(string ChalID)
    {
            List<string> DupePlayerIDs = new List<string>();
        foreach (string s in PlayerIDs)
        {
            if (s != mainMenu.myID)
            {
                DupePlayerIDs.Add(s);
            }
        }

		List<GSData> PlayesDataGSObjectList = new List<GSData>();
		foreach(var item in playersData)
		{
			string json = JsonUtility.ToJson(item);
			GSObject data = GSObject.FromJson(json);
			PlayesDataGSObjectList.Add(data);
		}


        new LogEventRequest().SetEventKey("pubgame")
            .SetEventAttribute("players", DupePlayerIDs)
		    .SetEventAttribute("playersData", PlayesDataGSObjectList)
            .SetEventAttribute("Chalid", ChalID)
            .SetEventAttribute("GameName", GameName)
		    .SetEventAttribute("TurnLimit", TurnLimitValue)
            .Send((response) =>
        {
            if (response.HasErrors)
            {
                Debug.Log("error");
            }
            else
            {

                ThesePlayerNames.Clear();
                PlayerIDs.Clear();

                foreach (Text t in NamesDisplayed)
                {
                    t.text = "";
                }
                NamesDisplayed[1].text = "Open to Public";
                NamesDisplayed[2].text = "Open to Public";
                NamesDisplayed[3].text = "Open to Public";
                GameNameDropdown.value = 0;
				GameNameDropdown.value = 0;
                TurnLimitDropDown.value = 0;
            }
        });
    }
    public void ResetMenu()
    {
        ThesePlayerNames.Clear();
        PlayerIDs.Clear();
        PlayerIDs.Add(mainMenu.myID);
        ThesePlayerNames.Add(mainMenu.myName);

        foreach (Text t in NamesDisplayed)
        {
            t.text = "";
        }

        NamesDisplayed[0].text = ThesePlayerNames[0];
        NamesDisplayed[1].text = "Open to Public";
        NamesDisplayed[2].text = "Open to Public";
        NamesDisplayed[3].text = "Open to Public";

        GameNameDropdown.value = 0;
		TurnLimitDropDown.value = 0;
		TurnLimitValue = 0;
        UpdateList = true;
        HideReset();
    }

    public void PublicGameSearch()
    {
        int gameFound = 0;
        Information.text = "Searching....";
        new FindChallengeRequest()
       .Send((response) =>
       {
           if (!response.HasErrors)
           {
               GSData data = response.ScriptData;
               List<GSData> GameList = data.GetGSDataList("challengeInstance");

               foreach (GSData level in GameList)
               {
                   GameInfo gi = GameInfo.CreateFromJSON(level.JSON); //call the class and method to parse the json data

                   if (gi.state == "ISSUED") // if its a public game which has not begun...
                   {
                       int MatchedMe = 0;
                       int NumberOfInvitees = 0;
                       int ResponsesFromInvitees = 0;
                       int DeclinedInvites = 0;
                       bool isJoinable = false;

                       if (gi.scriptData.players != null) //these are players that have been invited.
                       {
                           NumberOfInvitees = gi.scriptData.players.Length;

                           foreach (var item in gi.scriptData.players)
                           {
                               if (gi.accepted != null)
                               {
                                   foreach (var acceptPlayer in gi.accepted)
                                   {
                                       if (item == acceptPlayer)
                                       {
                                           ResponsesFromInvitees++;
                                       }
                                   }
                               }
                               if (gi.scriptData.declined != null)
                               {
                                   for (int i = 0; i < gi.scriptData.declined.Length; i++)
                                   {
                                       Debug.Log("Decline Id " + i + ": " + gi.scriptData.declined[i].id);
                                       if (item == gi.scriptData.declined[i].id)
                                       {
                                           ResponsesFromInvitees++;
                                           DeclinedInvites++;
                                       }
                                   }
                               }
                           }
                       }
                       else
                       {
                           NumberOfInvitees = 0;
                       }

                       int OpenSeats = 3 - NumberOfInvitees; //an open seat is taken by an invited player who has yet to accept, or has already accepted.
                       OpenSeats = OpenSeats + DeclinedInvites; //an open seat is added by a player who has declined

                       if (NumberOfInvitees == ResponsesFromInvitees || OpenSeats > 0) // if the people that were invited have all responded.
                       {
                           isJoinable = true;
                       }

                       if (gi.accepted.Length != 0)
                       {
                           foreach (string s in gi.accepted)
                           {
                               if (s == mainMenu.myID) //see if i've already accepted.
                               {
                                   MatchedMe++;
                               }
                           }
                       }

                       if (gi.scriptData != null)
                       {
                           if (gi.scriptData.players != null)
                           {
                               //see if ive been invited
                               foreach (string ss in gi.scriptData.players)
                               {
                                   if (ss == mainMenu.myID) //see if i've been invited
                                   {
                                       MatchedMe++;
                                   }
                               }
                           }

                           if (gi.scriptData.declined != null)
                           {
                               foreach (var s in gi.scriptData.declined)
                               {
                                   if (s.id == mainMenu.myID) //see if i've already declined.
                                   {
                                       MatchedMe++;
                                   }
                               }
                           }
                       }

                       if (MatchedMe == 0 && OpenSeats > 0 && isJoinable) //if i was not invited, and im not in the challenge, and those who were invited have already accepted.
                       {
                           Debug.Log("gi._id.oid");
                           ChallangeToJoin = gi._id.oid;
                           gameFound++;

                           GameObject PubGame = Instantiate(PupUpPanel, PanelSpawnPoint.transform);
                           PubGame.GetComponent<GameInviteMessage>().challengeId = gi._id.oid;
                           PubGame.GetComponent<GameInviteMessage>().isPublicGame = true;
                           PubGame.GetComponentInChildren<Text>().text = "Game Found: The Battle of " + gi.challengeMessage;

                           string Limit = "limit";
                           if (gi.scriptData.TurnLimit == 0)
                           {
                               Limit = "Unlimited";
                           }
                           if (gi.scriptData.TurnLimit == 30)
                           {
                               Limit = "30 seconds";
                           }
                           if (gi.scriptData.TurnLimit == 300)
                           {
                               Limit = "5 minutes";
                           }
                           if (gi.scriptData.TurnLimit == 3600)
                           {
                               Limit = "1 hour";
                           }
                           if (gi.scriptData.TurnLimit == 10800)
                           {
                               Limit = "3 hours";
                           }

                           Information.text = "Turn Limit = " + Limit + ".";
                           return;
                       }
                   }
               }

               if (gameFound == 0)
               {
                   Information.text = "There are no Public Games available at this time.";
                   Timer = 500;
               }
           }
           else
           {
               Information.text = "Error occured while searching for games.";
               Timer = 500;
           }
       });
    }

    public void JoinPublicGame(string ChalId)
    {
           new AcceptChallengeRequest()
            .SetChallengeInstanceId(ChalId)
            .Send((response) =>
            {
                if (response.HasErrors)
                {
                    Information.text = "You have entered the battle.  You can join from the Active Game List when all players are ready.";
                    Timer = 500;
                }
                else if (!response.HasErrors)
                {
                    Information.text = "Unable to join the Public Game, please try again.";
                    Timer = 500;
                }
            });
    }
}
