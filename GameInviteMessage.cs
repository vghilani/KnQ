using GameSparks.Api.Requests;
using GameSparks.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInviteMessage : MonoBehaviour {

    [SerializeField] public string challengeId;
    public string playerid;
    public Button GreenButton;
    public Button RedButton;
    public Text Message;
    public Text Player1;
    public Text Player2;
    public Text Player3;
    public Text Player4;
	public string PlayerTwoPendingId, PlayerThreePendingId, PlayerFourPendingId;
	public Button PlayerTwoButton, PlayerThreeButton, PlayerFourButton;
	public String MyFriendsId;
    public InputField Input;
    public GameObject TurnMarker;
    List<GSData> GameoverList = new List<GSData>();
    public bool isPublicGame = false;
    private static GameInviteMessage instance = null; //why i do this?
    private int Attempts = 0;

    private void Update()
    {

    }

    public void Surrender()
    {
        GameObject.Find("Chessboard").GetComponent<boardmanager>().PlayerSurrender();
    }
    public void QuitToMain()
    {
        GameMenu gm = new GameMenu();
        gm.MainMenu();
    }
    public void AcceptInvite()
    {
        new JoinChallengeRequest()
            .SetChallengeInstanceId(challengeId)
            .Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log("Error joining public challenge");
                }
                else
                {
                    Debug.Log("Joined Public Challenge");
                    GameObject.Find("Menu Manager").GetComponent<mainMenu>().ListOfGameInvites.Remove(this.gameObject);

                    new GetChallengeRequest()
                    .SetChallengeInstanceId(challengeId).Send((response2) =>
                    {

                        if (!response2.HasErrors)
                        {
                            GameInfo gi = GameInfo.CreateFromJSON(response2.Challenge.JSONString);
                            GameObject[] GameList = GameObject.FindGameObjectsWithTag("game");
                            foreach (GameObject go in GameList)
                            {
                                if (go.GetComponent<GameInviteMessage>().challengeId == challengeId)
                                {
                                    Destroy(go.gameObject);
                                }
                            }
                            GameObject game2 = Instantiate(GameObject.Find("Menu Manager").GetComponent<mainMenu>().GameListTag, GameObject.Find("Menu Manager").GetComponent<mainMenu>().spawnpoint4.transform);
                            game2.GetComponent<GameInviteMessage>().challengeId = challengeId;
                            GameObject.Find("Menu Manager").GetComponent<mainMenu>().ListOfGames.Add(game2);

                            if (gi.accepted.Length != 4)
                            {
                                game2.GetComponent<GameInviteMessage>().Message.text = "The Battle of " + gi.challengeMessage + " is about to begin! (players must accept invites)";
                                game2.GetComponent<GameInviteMessage>().GreenButton.gameObject.SetActive(false);
                            }
                            if(gi.accepted.Length == 4)
                            {
                                //send all the players a notification to let them know the game started.
                                WinConditions wc = new WinConditions();
                                wc.playerT = 1;
                                wc.prevT = 0;
                                wc.Bout = false;
                                wc.Yout = false;
                                wc.Rout = false;
                                wc.Gout = false;
                                string wcJson = JsonUtility.ToJson(wc);
                                GSObject obj = GSObject.FromJson(wcJson);

                                Experiment ex = Experiment.CreateFromJSON(response2.Challenge.JSONString);
                                List<GSData> AllPlayers = new List<GSData>();
                                foreach (Player p in ex.accepted)
                                {
                                    Debug.Log("p: " + p.id);
                                    string json = JsonUtility.ToJson(p.id); //turn the ChessmanClass into a JSON string
                                    GSObject player = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
                                    AllPlayers.Add(player);
                                }

                                List<string> newplayers = new List<string>();
                                foreach (Player p in ex.accepted)
                                {
                                    newplayers.Add(p.id);
                                }

                                // call new event which will send texts to the players since the game has begun.
                                new LogEventRequest()
                                .SetEventKey("GameStart") //this is for board data - there needs to be one for every list
                                .SetEventAttribute("cid", challengeId)
                                .SetEventAttribute("wincon", obj)
                                .SetEventAttribute("GameName", gi.challengeMessage)
                                .SetEventAttribute("TurnLimit", gi.scriptData.TurnLimit)
                                .SetEventAttribute("nextPlayer", gi.nextPlayer)
                                .SetEventAttribute("PlayerList", newplayers)
                                .Send((response3) =>
                                {
                                    Debug.Log("Sent gamesparks GameStart Event");
                                });

                            }
                            game2.GetComponent<GameInviteMessage>().PopulateGameList(challengeId);
                            Destroy(this.gameObject);
                        }
                    });
                }
            });
        GameObject.Find("Menu Manager").GetComponent<New_CreateMenu>().Information.text = "";
    }

    public void EnterGame()
    {
        this.gameObject.transform.parent = null;
        DontDestroyOnLoad(this.gameObject); // and make this object persistent as we load new scenes

        SceneManager.LoadScene("KingsAndQueens");              
    }
    public void DontJoin()
    {
        //cant modify a systemCollection - only a playerCollection...           
        new LogEventRequest().SetEventKey("DeclineGame").SetEventAttribute("chalid", challengeId).SetEventAttribute("myid", mainMenu.myID).Send((response) =>
         {
             if (response.HasErrors)
             {
                 Debug.Log("error decline game");
             }
             else
             {
                 Debug.Log("Game Invite Declined");
                 GameObject.Find("Menu Manager").GetComponent<mainMenu>().ListOfGameInvites.Remove(this.gameObject);
				 GameObject.Find("Menu Manager").GetComponent<New_CreateMenu>().Information.text = "";
                 Destroy(this.gameObject);
             }
         });
    }
	public void RevokeInvite()
    {
		
        //cant modify a systemCollection - only a playerCollection...           
		new LogEventRequest().SetEventKey("DeclineGame").SetEventAttribute("chalid", challengeId).SetEventAttribute("myid", MyFriendsId).Send((response) =>
        {
            if (response.HasErrors)
            {
                Debug.Log("error");
            }
            else
            {
                Debug.Log("Game Invite Declined");
                GameObject.Find("Menu Manager").GetComponent<mainMenu>().ListOfGameInvites.Remove(this.gameObject);
                GameObject.Find("Menu Manager").GetComponent<New_CreateMenu>().Information.text = "";
            }
        });
    }

    // NO LONGER IN USE - DELETE WHEN YOU KNOW YOU DONT NEED IT
    public void PopulateGameList(String challengeId)
    {
        int OpenSeats = 0;
        List<string> PendingSeats = new List<string>();

        new GetChallengeRequest()
          .SetChallengeInstanceId(challengeId)
          .Send((game_response) =>
          {
              if (!game_response.HasErrors)
              {
                  PlayerTwoButton.gameObject.SetActive(false);
                  PlayerThreeButton.gameObject.SetActive(false);
                  PlayerFourButton.gameObject.SetActive(false);

                  GameInfo ChallengeData = GameInfo.CreateFromJSON(game_response.Challenge.JSONString);
                  Player challenger = Player.CreateFromJSON(game_response.Challenge.Challenger.JSONString);

                  //populated all the accepted people
                  foreach (var s in game_response.Challenge.Accepted)
                  {
                      Player p2 = Player.CreateFromJSON(s.JSONString);
                      if (p2.id == challenger.id)
                      {
                          Player1.text = p2.name;
                          Player1.color = Color.yellow;
                      }
                      else if (Player2 == null || Player2.text == "")
                      {
                          Player2.text = p2.name;
                          Player2.color = Color.yellow;
                          PlayerTwoButton.gameObject.SetActive(false);
                      }
                      else if (Player3 == null || Player3.text == "")
                      {
                          Player3.text = p2.name;
                          Player3.color = Color.yellow;
                          PlayerThreeButton.gameObject.SetActive(false);
                      }
                      else if (Player4 == null || Player4.text == "")
                      {
                          Player4.text = p2.name;
                          Player4.color = Color.yellow;
                          PlayerFourButton.gameObject.SetActive(false);
                      }
                  }

                  //see if there is anybody that has been invited that hasn't answered.
                  if (ChallengeData.scriptData.players != null || ChallengeData.scriptData.players.Length > 0)
                  {
                      if (ChallengeData.scriptData.declined.Length > 0 && ChallengeData.scriptData.declined.Length < 4) //if ppl have declined
                      {
                          foreach (var InvitedPlayer in ChallengeData.scriptData.players) //see if any of them match the players that were invited
                          {
                              bool HasAccepted = false;
                              bool HasDeclined = false;

                              foreach (var DeclinedPlayer in ChallengeData.scriptData.declined)
                              {
                                  if (DeclinedPlayer.id == InvitedPlayer)
                                  {
                                      HasDeclined = true;
                                  }
                              }
                              foreach (var v in game_response.Challenge.Accepted)
                              {
                                  Player p = Player.CreateFromJSON(v.JSONString);

                                  if (p.id == InvitedPlayer)
                                  {
                                      HasAccepted = true;
                                  }
                              }

                              if (!HasAccepted && !HasDeclined)
                              {
                                  PendingSeats.Add(InvitedPlayer);
                              }
                              if (!HasAccepted && HasDeclined)
                              {
                                  OpenSeats++;
                              }
                          }
                      }
                  }
                  else
                  {
                      OpenSeats = 3;
                  }


                  //based off how many are pending - populate the list with the pending players.
                  if (PendingSeats.Count > 0)
                  {
                      foreach (string s in PendingSeats)
                      {
                          foreach (var item in ChallengeData.scriptData.playersData)
                          {
                              if (s == item.id)
                              {
                                  if (Player2 == null || Player2.text == "")
                                  {
                                      Player2.text = item.playername + " (Pending)";
                                      PlayerTwoPendingId = s;

                                      if (challenger.id == mainMenu.myID)
                                      {
                                          PlayerTwoButton.gameObject.SetActive(true);
                                      }
                                      else
                                      {
                                          PlayerTwoButton.gameObject.SetActive(false);
                                      }
                                  }
                                  else if (Player3 == null || Player3.text == "")
                                  {
                                      Player3.text = item.playername + " (Pending)";
                                      PlayerThreePendingId = s;

                                      if (challenger.id == mainMenu.myID)
                                      {
                                          PlayerThreeButton.gameObject.SetActive(true);
                                      }
                                      else
                                      {
                                          PlayerThreeButton.gameObject.SetActive(false);
                                      }
                                  }
                                  else if (Player4 == null || Player4.text == "")
                                  {
                                      Player4.text = item.playername + " (Pending)";
                                      PlayerFourPendingId = s;

                                      if (challenger.id == mainMenu.myID)
                                      {
                                          PlayerFourButton.gameObject.SetActive(true);
                                      }
                                      else
                                      {
                                          PlayerFourButton.gameObject.SetActive(false);
                                      }
                                  }
                              }
                          }
                      }
                  }

                  //last - populate the open seats for the public - if there is still nothing to put in the player fields by now, then they're public seats.
                  if (Player2 == null || Player2.text == "")
                  {
                      Player2.text = "Public";
                      PlayerTwoButton.gameObject.SetActive(false);
                  }
                  if (Player3 == null || Player3.text == "")
                  {
                      Player3.text = "Public";
                      PlayerThreeButton.gameObject.SetActive(false);
                  }
                  if (Player4 == null || Player4.text == "")
                  {
                      Player4.text = "Public";
                      PlayerFourButton.gameObject.SetActive(false);
                  }

                  if (ChallengeData.accepted.Length == 4)
                  {
                      Message.text = "The Battle of " + ChallengeData.challengeMessage + " is underway!";
                      GreenButton.gameObject.SetActive(true);
                  }
              }
          });
    }

    public void RevokePlayerTwoInvite()
	{
		MyFriendsId = PlayerTwoPendingId;
		RevokeInvite();
		PlayerTwoButton.gameObject.SetActive(false);
		Player2.text = "Public";
	}
    public void RevokePlayerThreeInvite()
	{
		MyFriendsId = PlayerThreePendingId;
		RevokeInvite();
		PlayerThreeButton.gameObject.SetActive(false);
		Player3.text = "Public";
	}
	public void RevokePlayerFourInvite()
	{
		MyFriendsId = PlayerFourPendingId;
		RevokeInvite();
		PlayerFourButton.gameObject.SetActive(false);
		Player4.text = "Public";
	}


    public void Quit()
    {
        GameSparks.Core.GS.Disconnect();
        Application.Quit();
    }
    public void ClosePublicGameResult()
    {
		GameObject.Find("Menu Manager").GetComponent<New_CreateMenu>().Information.text = "";
        Destroy(this.gameObject);
    }
    public void SubmitForgotPassword()
    {
        if (Input.text == "")
        {
            GameObject.Find("Menu Manager").GetComponent<mainMenu>().Validation.text = "Please enter the email associated with your account.";
            GameObject.Find("Menu Manager").GetComponent<mainMenu>().TriggerTimer = true;
            Destroy(this.gameObject);
        }
        else
        {
            mainMenu mm = GameObject.Find("Menu Manager").GetComponent<mainMenu>();

            mm.StartHere();
            mm.AuthStatus.gameObject.SetActive(false);
            mm.AccountButton.gameObject.SetActive(false);



            //sendGrid un|pw = vinnyghilani|Vin800111
            new AuthenticationRequest().SetUserName("Support").SetPassword("Password11").Send((response) =>
            {
                if(!response.HasErrors)
                {
                    new LogEventRequest().SetEventKey("ForgotPasswordSearch").SetEventAttribute("email", Input.text).Send((response2) =>
                    {
                        if(!response2.HasErrors)
                        {
                            GSData data = response2.ScriptData;
                            List<GSData> PlayerList = data.GetGSDataList("playerList");
                            int index = -1;
                            for (int i = 0; i < PlayerList.Count; i++)
                            {
                                MessageInfo mii = MessageInfo.CreateFromJSON(PlayerList[i].JSON);
                                if (mii.email == Input.text)
                                {
                                    index = i;
                                }
                            }

                            if (index > -1)
                            {
                                MessageInfo mi = MessageInfo.CreateFromJSON(PlayerList[index].JSON);


                                new LogEventRequest().SetEventKey("ForgotPasswordSend").SetEventAttribute("userid", mi.playerId).SetEventAttribute("email", Input.text).Send((response3) =>
                                {
                                    if (!response3.HasErrors)
                                    {
                                        Debug.Log("you did it");
                                        mm.Validation.text = "Profile information has been emailed to you.";
                                        mm.TriggerTimer = true;
                                        mm.AuthStatus.gameObject.SetActive(true);
                                        mm.AccountButton.gameObject.SetActive(true);
                                        mm.SignOut();
                                        Destroy(this.gameObject);
                                    }
                                    else
                                    {
                                        mm.Validation.text = "Error resetting password.  Please contact support.";
                                        mm.TriggerTimer = true;
                                        mm.AuthStatus.gameObject.SetActive(true);
                                        mm.AccountButton.gameObject.SetActive(true);
                                        mm.SignOut();
                                        Destroy(this.gameObject);
                                        Debug.Log("you didn't do it");
                                    }
                                });
                            }
                            else
                            {
                                mm.Validation.text = "The Email entered is not associated with an Account.";
                                mm.TriggerTimer = true;
                                mm.AuthStatus.gameObject.SetActive(true);
                                mm.AccountButton.gameObject.SetActive(true);
                                mm.SignOut();
                                Destroy(this.gameObject);
                                Debug.Log("error fetching email - search not returned");
                            }
                          
                        }
                        else
                        {
                            mm.Validation.text = "The Email entered is not associated with an Account.";
                            mm.TriggerTimer = true;
                            mm.AuthStatus.gameObject.SetActive(true);
                            mm.AccountButton.gameObject.SetActive(true);
                            mm.SignOut();
                            Destroy(this.gameObject);
                            Debug.Log("error fetching email - something broke");
                        }
                    });
                }
                else
                {
                    mm.Validation.text = "Connection error, please try again.";
                    mm.TriggerTimer = true;
                    mm.AuthStatus.gameObject.SetActive(true);
                    mm.AccountButton.gameObject.SetActive(true);
                    mm.SignOut();
                    Destroy(this.gameObject);
                    Debug.Log("error signing in as support");
                }
            });
        }
    }
    public void SendEmail(string email)
    {
        new LogEventRequest().SetEventKey("ForgotPasswordSend").SetEventAttribute("email", email).Send((response) => {
            if(!response.HasErrors)
            {
                           GameObject.Find("Menu Manager").GetComponent<mainMenu>().Validation.text = "A password has been emailed.";
                           GameObject.Find("Menu Manager").GetComponent<mainMenu>().TriggerTimer = true;
            }
        });
    }
    public void CancelForgotPassword()
    {
        if(this.gameObject.name == "PopUpPanel_AreYouSure(Clone)" && SceneManager.GetActiveScene().name == "MainMenu")
        {
            GameObject.Find("Menu Manager").GetComponent<mainMenu>().FriendPanel.GetComponent<Canvas>().enabled = false;
        }

        Destroy(this.gameObject);
    }
}
