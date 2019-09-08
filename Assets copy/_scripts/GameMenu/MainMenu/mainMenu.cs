using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Api.Messages;
using GameSparks.Core;
using System.Collections.Generic;
using System;
using System.Collections;

public class mainMenu : MonoBehaviour {

    public Canvas AccountManagement;
    public Canvas MultiplayerMenu;
    public Canvas PublicCreation;
    public Canvas Register;
    public Canvas Background;
    public Canvas FriendPanel;
    public Canvas MessageList;
    public Canvas FriendList;
	public Canvas FriendMenu;
    public Canvas GameInviteList;
    public Canvas GameList;
    public Text GameName;
    public Text AuthStatus;
    public Text FriendPanel_FriendName;
    public Button AccountButton;
	public Button FriendButton;

    public Button MessageCheckButton;
    public Button GameInviteButton;
    public Button GameListButton;
    public int MessageCount = 0;
    public int FriendCount = 0;
    public static string GameNameToSend;
    public static string JoinNameToSend;
    public GameObject NetworkingManager;
    public GameObject spawnpoint;
    public GameObject spawnpoint2;
    public GameObject spawnpoint3;
    public GameObject spawnpoint4;
    public GameObject spawnpoint5;
	public GameObject spawnpoint6;
    public GameObject FriendMessage;
    public GameObject ForgotPasswordPopUp;
    public List<GameObject> ListOfFriendMessages = new List<GameObject>();
    public List<GameObject> ListOfFriends = new List<GameObject>();
    public List<GameObject> ListOfGameInvites = new List<GameObject>();
    public List<GameObject> ListOfGames = new List<GameObject>();
    public GameObject DogTag;
    public GameObject GameInviteTag;
    public GameObject GameListTag;
    public GameObject PopUpMessage;
    public static string myID;
    public static string myName;
    public static bool onCreateGameMenu = false;
    public Scene KingsAndQueens = new Scene();
    public GameObject LoadingSpinner;

    private string friendName; //used in the myfriendsname function
    private bool friendNameCheck = false;
    private bool nameCheck = true;
    private bool friendsDisplayed = false;
    public float time = 0.0f;
    public float WaitPeriod = 30.0f;
    public float ServerConnectionTimer = 0;
    public float SignInConnectionTimer = 0;
    public float GetMessagesTimer = 0;
    public bool ShouldGetMessages = false;
    private bool InitialCheck = true; // trigger a one-time only GetMessages in Update
    private int failCount_messages = 0; //tracks if the a request in GetMessages fails..
    private int failCount_games = 0;
    public int check = 0;
    public Text LoadingText;

    //account management fields
    public InputField NewDisplayName;
    public InputField NewPassword;
    public InputField OldPassword;
    public InputField NewEmail;
    public Text AccountValidationText;

    //formerly controlled by GameSpark Experiment .cs - but has to be done here...
    public InputField REGuserName;
    public InputField REGpassword;
    public InputField REGemail;
    public InputField LOGpassword;
 //   public InputField LOGemail;
    public InputField LOGuserName;
    public Text FriendNameSearch;
    public Text SearchResultsStatus;
    public Text Validation;
    public float Timer = 0.0f;
    public bool TriggerTimer = false;
    public bool TriggerAutoSignInTimer = false;
    public float AutoSignInTimer = 0.0f;
    public string friendid;
    public GameObject QuitSpawn;
    public GameObject QuitPopUp;

    private void Start()
    {
        StartHere();
    }
    private void Update()
    {
        //KEEP THIS?
        //if(!GS.Available) //if there's no connection to the Server
        //{
        //    ServerConnectionTimer++;
        //    AuthStatus.text = "Connecting to Multiplayer Servers....";
        //    LoadingText.text = "Loading Player Data. Please Wait.";

        //    // If there's no connection after a time, go back to the Splash.
        //    if (ServerConnectionTimer >= 250)
        //    {
        //        AuthStatus.text = "Trouble connecting to the server.  Reloading Main Menu.";
        //        StartHere();
        //    }
        //    if(ServerConnectionTimer >= 300) //wait for 30 seconds and try to connect - if you we can't - tell the User.
        //    {
        //        BackToSplash();
        //    }
        //}
        //else
        //{
        //    ServerConnectionTimer = 0;
        //}

        // Keep everything below here
        if (MessageCount > 0)
        {
            MessageCheckButton.gameObject.SetActive(true);
        }
        else
        {
            MessageCheckButton.gameObject.SetActive(false);
        }
        if (ListOfGameInvites.Count > 0)
        {
            GameInviteButton.gameObject.SetActive(true);
        }
        else
        {
            GameInviteButton.gameObject.SetActive(false);
        }
        if(ListOfGames.Count > 0)
        {
            GameListButton.gameObject.SetActive(true);
        }
        else
        {
            GameListButton.gameObject.SetActive(false);
        }

        //Trigger timer starts if the Support User is signed in, OR to see if the
        //user can authenticate automatically.
        if (TriggerTimer)
        {
            Timer += Time.deltaTime;
        }
        if (Timer >= 8.0f)
        {
            Timer = 0.0f;
            TriggerTimer = false;

            if (myName == "Support")
            {
                SearchResultsStatus.text = "";
                Validation.text = "";
                AccountValidationText.text = "";
                SignOut();
            }
        }
        if(TriggerAutoSignInTimer)
        {
            AutoSignInTimer += Time.deltaTime;
        }
        if(AutoSignInTimer >= 8.0f)
        {
            TriggerAutoSignInTimer = false;
            AutoSignInTimer = 0.0f;
            ThrottlelessAuthentication();
        }
    }

    //shuts off all menus, except the HUD and the LOADING screen
    public void StartHere()
    {
        Debug.Log("Starting Here");
        StartingMenuState();

        TriggerAutoSignInTimer = true;
    }

    public void PurgeFriendLists()
    {

        FriendCount = 0;
        MessageCount = 0;
        ListOfFriendMessages.Clear();
        ListOfFriends.Clear();


        //delete message that were already loaded so we dont get dupes each time this is called:
        GameObject[] messagedisplays = GameObject.FindGameObjectsWithTag("display");
        foreach (GameObject g in messagedisplays)
        {
            Destroy(g.gameObject);
        }

        GameObject[] tags = GameObject.FindGameObjectsWithTag("dogtag");
        foreach (GameObject t in tags)
        {
            Destroy(t.gameObject);
        }
        friendsDisplayed = false;

    }
    public void PurgeGameLists()
    {
        ListOfGameInvites.Clear();
        ListOfGames.Clear();

        GameObject[] gameInvites = GameObject.FindGameObjectsWithTag("invite");
        foreach (GameObject g in gameInvites)
        {
            Destroy(g.gameObject);
        }

        GameObject[] gameList = GameObject.FindGameObjectsWithTag("game");
        foreach (GameObject g in gameList)
        {
            Destroy(g.gameObject);
        }
    }

    public void ThrottlelessAuthentication()
    {
        if (GS.Available) //if we're conencted to the server
        {
            if (GS.Authenticated)//if we're signed in.
            {
                new AccountDetailsRequest().Send((response) =>
                {
                    if (response.DisplayName != null)
                    {
                        Debug.Log("My ID: " + response.UserId);
                        myID = response.UserId;
                        myName = response.DisplayName;
                        AuthStatus.text = "Signed in as " + response.DisplayName;
                        AccountButton.gameObject.SetActive(true);
                        FriendButton.gameObject.SetActive(true);

                        new LogEventRequest().SetEventKey("PlayerStatus")
                                             .SetEventAttribute("Status", "OnMainMenu")
                                             .Send((response_two) =>
                                             {
                                                 if (!response_two.HasErrors)
                                                 {
                                                     if (myName != "Support" && AuthStatus.text == "Signed in as " + response.DisplayName)
                                                     {
                                                         // Get messages
                                                         LoadingText.text = "Fetching Game Data.  Please Wait";
                                                         GSRequestData jsonData = new GSRequestData();
                                                         jsonData.AddString("friendId", myID); //the cloud code isn't even using this because it doesn't matter....?

                                                         new LogEventRequest().SetEventKey("GetMyMessages")
                                                                              .SetEventAttribute("query", jsonData)
                                                                              .Send((response_three) =>
                                                         {
                                                             if (!response_three.HasErrors)
                                                             {
                                                                 GSData data = response_three.ScriptData;
                                                                 List<GSData> MessageListData = data.GetGSDataList("playerFriends");
                                                                 LoadingText.text = "";
                                                                 //make sure these variables are reset so they're not continually added to each time this is called:
                                                                 PurgeFriendLists();

                                                                 int numberOfMessage = 1;

                                                                 foreach (GSData level in MessageListData)
                                                                 {
                                                                     MessageInfo mi = MessageInfo.CreateFromJSON(level.JSON); //call the class and method to parse the json data

                                                                     //CHECK FOR MESSAGES/FRIENDS WHERE I WAS THE RECEIVER
                                                                     if (mi.friendId == myID)
                                                                     {
                                                                         ///IF THERE ARE ANY PENDING FRIEND REQUESTS THAT WERE SENT TO ME, DISPLAY THEM.....
                                                                         if (mi.status == "pending")
                                                                         {
                                                                             MessageCount++;

                                                                             GameObject go = Instantiate(FriendMessage, spawnpoint.transform);

                                                                             go.GetComponent<FriendMessageID>().friendDisplayName = mi.displayName;
                                                                             go.GetComponent<FriendMessageID>().MessageID = mi._id.oid; //get the message id, so we can accept/reject
                                                                             go.GetComponent<FriendMessageID>().Message.text = mi.displayName + " would like to be your friend.";
                                                                             go.GetComponent<FriendMessageID>().friendID = mi.friendId;
                                                                             go.name = "Message " + numberOfMessage; //increment the number so we can apply different names to each GO.

                                                                             ListOfFriendMessages.Add(go);
                                                                             numberOfMessage++;
                                                                         }

                                                                         ///IF THERE ARE ANY REQUESTS THAT I ACCEPTED - COUNT THEM AMOUNG MY FRIENDS
                                                                         if (mi.status == "accepted")
                                                                         {
                                                                             FriendCount++;

                                                                             GameObject go = Instantiate(DogTag, spawnpoint2.transform);
                                                                             go.GetComponent<Dogtag>().displayName = mi.displayName;
                                                                             go.GetComponent<Dogtag>().buttonText.text = mi.displayName;
                                                                             go.GetComponent<Dogtag>().playerId = mi.playerId;

                                                                             GameObject myobject = Instantiate(go, spawnpoint6.transform);
                                                                             ListOfFriends.Add(go);
                                                                         }
                                                                     }
                                                                     //CHECK FOR FRIENDS THAT ACCEPTED INVITES SENT BY ME
                                                                     if (mi.playerId == myID && mi.status == "accepted")
                                                                     {
                                                                         //we have the senders name and id.  but if WE were the sender, then we only have the reciever's id...and we need their name.

                                                                         //create a dogtag gameobject and leave the tag unassigned.  
                                                                         //then when we summod the friend list, we can gather the unassigned dogtags and called the 
                                                                         //MyFriendName function using their playerId variable as the Method's arguement...
                                                                         FriendCount++;
                                                                         GameObject go = Instantiate(DogTag, spawnpoint2.transform);
                                                                         go.GetComponent<Dogtag>().playerId = mi.friendId;
                                                                         go.GetComponent<Dogtag>().displayName = mi.friendName;

                                                                         GameObject myobject = Instantiate(go, spawnpoint6.transform);
                                                                         ListOfFriends.Add(go);
                                                                     }

                                                                 }

                                                                 // Now fetch any Game Invites
                                                                 new FindChallengeRequest().Send((response_four) =>
                                                                 {
                                                                     if (!response_four.HasErrors)
                                                                     {
                                                                         check++; //tick up for success
                                                                         int inviteCount = 0;
                                                                         int gameCount = 0;

                                                                         PurgeGameLists();

                                                                         GSData data_two = response_four.ScriptData;
                                                                         List<GSData> Challenge = new List<GSData>();
                                                                         Challenge = data_two.GetGSDataList("challengeInstance");

                                                                         foreach (GSData level in Challenge)
                                                                         {

                                                                             GameInfo gi = GameInfo.CreateFromJSON(level.JSON); //call the class and method to parse the json data
                                                                             bool wedeclined = false;
                                                                             bool weaccepted = false;
                                                                             bool wequit = false;

                                                                             #region See If We Declined This Game Already, or if we're already Accepted.
                                                                             if (gi.scriptData.declined != null)
                                                                             {
                                                                                 //see if we accepted or declined already
                                                                                 if (gi.scriptData.declined.Length > 0)
                                                                                 {
                                                                                     for (int i = 0; i < gi.scriptData.declined.Length; i++)
                                                                                     {
                                                                                         if (gi.scriptData.declined[i].id == myID)
                                                                                         {
                                                                                             wedeclined = true;
                                                                                             Debug.Log("THIS decline check worked");
                                                                                         }
                                                                                     }
                                                                                 }
                                                                             }
                                                                             //see if we accepted the invite
                                                                             if (gi.accepted.Length > 0)
                                                                             {

                                                                                 foreach (string s in gi.accepted)
                                                                                 {
                                                                                     if (s == myID)
                                                                                     {
                                                                                         weaccepted = true;
                                                                                     }
                                                                                 }
                                                                             }
                                                                             #endregion


                                                                             #region See if we've LOST this game already
                                                                             WinConditions wincon = gi.scriptData.WinConditions;
                                                                             if (wincon != null)
                                                                             {
                                                                                 if (gi.scriptData.PlayerList != null) //if this game has a player list
                                                                                 {
                                                                                     Debug.Log("found playerlist");
                                                                                     string mycolor = "color";
                                                                                     foreach (var p in gi.scriptData.PlayerList)
                                                                                     {
                                                                                         if (p.playerName == myName)
                                                                                         {
                                                                                             mycolor = p.color;
                                                                                             Debug.Log("found your color");
                                                                                         }
                                                                                     }

                                                                                     if (wincon.Bout && mycolor == "Blue")
                                                                                     {
                                                                                         wequit = true;
                                                                                         Debug.Log("you lost this game");
                                                                                     }
                                                                                     if (wincon.Yout && mycolor == "Yellow")
                                                                                     {
                                                                                         wequit = true;
                                                                                     }
                                                                                     if (wincon.Rout && mycolor == "Red")
                                                                                     {
                                                                                         wequit = true;
                                                                                     }
                                                                                     if (wincon.Gout && mycolor == "Green")
                                                                                     {
                                                                                         wequit = true;
                                                                                     }
                                                                                 }
                                                                             }

                                                                             #endregion

                                                                             string id = "";
                                                                             GameObject message = new GameObject();

                                                                             if (gi.state == "ISSUED")
                                                                             {
                                                                                 //populate the game list if we've been accepted
                                                                                 if (weaccepted)
                                                                                 {
                                                                                     if (!wequit) //if we have not quit/lost this challenge
                                                                                     {
                                                                                         gameCount++;
                                                                                         //show the Challenge and make it enterable...
                                                                                         GameObject game2 = Instantiate(GameListTag, spawnpoint4.transform);
                                                                                         game2.GetComponent<GameInviteMessage>().challengeId = gi._id.oid;
                                                                                         message = game2;
                                                                                         id = gi._id.oid;
                                                                                         ListOfGames.Add(game2);

                                                                                         if (gi.accepted.Length != 4)
                                                                                         {
                                                                                             game2.GetComponent<GameInviteMessage>().Message.text = "The Battle of " + gi.challengeMessage + " is about to begin! (four players must join)";
                                                                                             game2.GetComponent<GameInviteMessage>().GreenButton.gameObject.SetActive(false);
                                                                                         }
                                                                                     }
                                                                                 }



                                                                                 if (!weaccepted && !wedeclined && !wequit) //if i wasn't found within the accepted player list, or the declined player list, and if i have not quit.
                                                                                 {
                                                                                     bool wasinvited = false;
                                                                                     if (gi.scriptData.players != null && gi.scriptData.players.Length > 0)
                                                                                     {
                                                                                         foreach (string item in gi.scriptData.players)
                                                                                         {
                                                                                             if (item == myID)
                                                                                             {
                                                                                                 wasinvited = true;
                                                                                             }
                                                                                         }
                                                                                     }
                                                                                     if (wasinvited)
                                                                                     {
                                                                                         inviteCount++;
                                                                                         //show us the invite
                                                                                         GameObject invite = Instantiate(GameInviteTag, spawnpoint3.transform);
                                                                                         invite.GetComponent<GameInviteMessage>().challengeId = gi._id.oid;
                                                                                         id = gi._id.oid;
                                                                                         message = invite;
                                                                                         invite.GetComponent<GameInviteMessage>().Message.text = "You've been summoned to fight in The Battle Of " + gi.challengeMessage;
                                                                                         ListOfGameInvites.Add(invite);
                                                                                         invite.GetComponent<GameInviteMessage>().isPublicGame = true;
                                                                                     }
                                                                                 }
                                                                             }

                                                                             if (gi.state == "RUNNING")
                                                                             {
                                                                                 if (weaccepted)
                                                                                 {
                                                                                     if (!wequit)
                                                                                     {


                                                                                         gameCount++;
                                                                                         //show the Challenge and make it enterable...
                                                                                         GameObject game2 = Instantiate(GameListTag, spawnpoint4.transform);
                                                                                         game2.GetComponent<GameInviteMessage>().challengeId = gi._id.oid;
                                                                                         id = gi._id.oid;
                                                                                         game2.GetComponent<GameInviteMessage>().Message.text = "The Battle of " + gi.challengeMessage + " is underway!";
                                                                                         message = game2;
                                                                                         ListOfGames.Add(game2);
                                                                                     }
                                                                                 }
                                                                             }

                                                                             //Populate Game Data within the Tile.
                                                                             if (id != "")
                                                                             {
                                                                                 int OpenSeats = 0;
                                                                                 List<string> PendingSeats = new List<string>();

                                                                                 new GetChallengeRequest()
                                                                                   .SetChallengeInstanceId(id)
                                                                                   .Send((game_response) =>
                                                                                   {
                                                                                       if (!game_response.HasErrors)
                                                                                       {
                                                                                           message.GetComponent<GameInviteMessage>().PlayerTwoButton.gameObject.SetActive(false);
                                                                                           message.GetComponent<GameInviteMessage>().PlayerThreeButton.gameObject.SetActive(false);
                                                                                           message.GetComponent<GameInviteMessage>().PlayerFourButton.gameObject.SetActive(false);

                                                                                           GameInfo ChallengeData = GameInfo.CreateFromJSON(game_response.Challenge.JSONString);
                                                                                           Player challenger = Player.CreateFromJSON(game_response.Challenge.Challenger.JSONString);

                                                                                           //populated all the accepted people
                                                                                           foreach (var s in game_response.Challenge.Accepted)
                                                                                           {
                                                                                               Player p2 = Player.CreateFromJSON(s.JSONString);
                                                                                               if (p2.id == challenger.id)
                                                                                               {
                                                                                                   message.GetComponent<GameInviteMessage>().Player1.text = p2.name;
                                                                                                   message.GetComponent<GameInviteMessage>().Player1.color = Color.yellow;
                                                                                               }
                                                                                               else if (message.GetComponent<GameInviteMessage>().Player2 == null || message.GetComponent<GameInviteMessage>().Player2.text == "")
                                                                                               {
                                                                                                   message.GetComponent<GameInviteMessage>().Player2.text = p2.name;
                                                                                                   message.GetComponent<GameInviteMessage>().Player2.color = Color.yellow;
                                                                                                   message.GetComponent<GameInviteMessage>().PlayerTwoButton.gameObject.SetActive(false);
                                                                                               }
                                                                                               else if (message.GetComponent<GameInviteMessage>().Player3 == null || message.GetComponent<GameInviteMessage>().Player3.text == "")
                                                                                               {
                                                                                                   message.GetComponent<GameInviteMessage>().Player3.text = p2.name;
                                                                                                   message.GetComponent<GameInviteMessage>().Player3.color = Color.yellow;
                                                                                                   message.GetComponent<GameInviteMessage>().PlayerThreeButton.gameObject.SetActive(false);

                                                                                               }
                                                                                               else if (message.GetComponent<GameInviteMessage>().Player4 == null || message.GetComponent<GameInviteMessage>().Player4.text == "")
                                                                                               {
                                                                                                   message.GetComponent<GameInviteMessage>().Player4.text = p2.name;
                                                                                                   message.GetComponent<GameInviteMessage>().Player4.color = Color.yellow;
                                                                                                   message.GetComponent<GameInviteMessage>().PlayerFourButton.gameObject.SetActive(false);
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
                                                                                                           if (message.GetComponent<GameInviteMessage>().Player2 == null || message.GetComponent<GameInviteMessage>().Player2.text == "")
                                                                                                           {
                                                                                                               message.GetComponent<GameInviteMessage>().Player2.text = item.playername + " (Pending)";
                                                                                                               message.GetComponent<GameInviteMessage>().PlayerTwoPendingId = s;

                                                                                                               if (challenger.id == myID)
                                                                                                               {
                                                                                                                   message.GetComponent<GameInviteMessage>().PlayerTwoButton.gameObject.SetActive(true);
                                                                                                               }
                                                                                                               else
                                                                                                               {
                                                                                                                   message.GetComponent<GameInviteMessage>().PlayerTwoButton.gameObject.SetActive(false);
                                                                                                               }
                                                                                                           }
                                                                                                           else if (message.GetComponent<GameInviteMessage>().Player3 == null || message.GetComponent<GameInviteMessage>().Player3.text == "")
                                                                                                           {
                                                                                                               message.GetComponent<GameInviteMessage>().Player3.text = item.playername + " (Pending)";
                                                                                                               message.GetComponent<GameInviteMessage>().PlayerThreePendingId = s;

                                                                                                               if (challenger.id == myID)
                                                                                                               {
                                                                                                                   message.GetComponent<GameInviteMessage>().PlayerThreeButton.gameObject.SetActive(true);
                                                                                                               }
                                                                                                               else
                                                                                                               {
                                                                                                                   message.GetComponent<GameInviteMessage>().PlayerThreeButton.gameObject.SetActive(false);
                                                                                                               }
                                                                                                           }
                                                                                                           else if (message.GetComponent<GameInviteMessage>().Player4 == null || message.GetComponent<GameInviteMessage>().Player4.text == "")
                                                                                                           {
                                                                                                               message.GetComponent<GameInviteMessage>().Player4.text = item.playername + " (Pending)";
                                                                                                               message.GetComponent<GameInviteMessage>().PlayerFourPendingId = s;

                                                                                                               if (challenger.id == myID)
                                                                                                               {
                                                                                                                   message.GetComponent<GameInviteMessage>().PlayerFourButton.gameObject.SetActive(true);
                                                                                                               }
                                                                                                               else
                                                                                                               {
                                                                                                                   message.GetComponent<GameInviteMessage>().PlayerFourButton.gameObject.SetActive(false);
                                                                                                               }
                                                                                                           }
                                                                                                       }
                                                                                                   }
                                                                                               }
                                                                                           }

                                                                                           //last - populate the open seats for the public - if there is still nothing to put in the player fields by now, then they're public seats.
                                                                                           if (message.GetComponent<GameInviteMessage>().Player2 == null || message.GetComponent<GameInviteMessage>().Player2.text == "")
                                                                                           {
                                                                                               message.GetComponent<GameInviteMessage>().Player2.text = "Public";
                                                                                               message.GetComponent<GameInviteMessage>().PlayerTwoButton.gameObject.SetActive(false);
                                                                                           }
                                                                                           if (message.GetComponent<GameInviteMessage>().Player3 == null || message.GetComponent<GameInviteMessage>().Player3.text == "")
                                                                                           {
                                                                                               message.GetComponent<GameInviteMessage>().Player3.text = "Public";
                                                                                               message.GetComponent<GameInviteMessage>().PlayerThreeButton.gameObject.SetActive(false);
                                                                                           }
                                                                                           if (message.GetComponent<GameInviteMessage>().Player4 == null || message.GetComponent<GameInviteMessage>().Player4.text == "")
                                                                                           {
                                                                                               message.GetComponent<GameInviteMessage>().Player4.text = "Public";
                                                                                               message.GetComponent<GameInviteMessage>().PlayerFourButton.gameObject.SetActive(false);
                                                                                           }

                                                                                           if (ChallengeData.accepted.Length == 4)
                                                                                           {
                                                                                               message.GetComponent<GameInviteMessage>().Message.text = "The Battle of " + ChallengeData.challengeMessage + " is underway!";
                                                                                               message.GetComponent<GameInviteMessage>().GreenButton.gameObject.SetActive(true);
                                                                                           }
                                                                                       }
                                                                                   });
                                                                             }
                                                                         }
                                                                     }
                                                                     else
                                                                     {
                                                                         Debug.Log("Game Invites method response Error");
                                                                         failCount_games++;
                                                                     }
                                                                 });

                                                                 if (failCount_messages > 0)
                                                                 {
                                                                     failCount_games++;
                                                                 }
                                                             }
                                                             else
                                                             {
                                                                 Debug.Log("there were errors fetching messages.");
                                                             }
                                                         });
                                                     }
                                                     else
                                                     {
                                                         // This sets a time to auto sign out the Support User.
                                                         // It gives the Support User enough time to Reset the 
                                                         // password.
                                                         AuthStatus.text = "";
                                                         TriggerTimer = true;
                                                     }
                                                 }
                                                 else
                                                 {
                                                     Debug.Log("Error Updating Player Online Status");
                                                 }
                                             });
                    }
                    else
                    {
                        AuthStatus.text = "Authenticated but not signed in";
                        StartHere();
                    }
                });

                EnterMainMenuAsAuthenticatedUser();
            }
            else //we're connected to the server but not signed in.
            {
                LoadingSpinner.SetActive(false);
                AuthStatus.text = "User is not signed in";
                EnterMainMenuAsUnauthenticatedUser();
            }
        }
        else //we're not connected to the server
        {
            LoadingSpinner.SetActive(false);
            AuthStatus.text = "User is not signed in";
            EnterMainMenuAsUnauthenticatedUser();
        }
    }

    public void StartingMenuState()
    {
        LoadingSpinner.SetActive(true);
        LoadingText.text = "Retrieving game data, please wait.";
        AuthStatus.text = "";
        Register.GetComponent<Canvas>().enabled = false;
        MultiplayerMenu.GetComponent<Canvas>().enabled = false;
        PublicCreation.GetComponent<Canvas>().enabled = false;
        FriendPanel.GetComponent<Canvas>().enabled = false;
        MessageList.GetComponent<Canvas>().enabled = false;
        FriendList.GetComponent<Canvas>().enabled = false;
        GameInviteList.GetComponent<Canvas>().enabled = false;
        GameList.GetComponent<Canvas>().enabled = false;
        AccountManagement.GetComponent<Canvas>().enabled = false;
        FriendMenu.GetComponent<Canvas>().enabled = false;
        AccountButton.gameObject.SetActive(false);
        FriendButton.gameObject.SetActive(false);
    }

    public static void UpdatePlayerStatus(string Status)
	{
		new LogEventRequest().SetEventKey("PlayerStatus").SetEventAttribute("Status", Status).Send((response) => { 
			if(!response.HasErrors)
			{
				Debug.Log("Player Online Status Updated");
			}
			else
			{
				Debug.Log("Error Updating Player Online Status");
			}
		});
	}

    public void EnterMainMenuAsAuthenticatedUser()
    {
        //Menu Handling.
        LoadingSpinner.SetActive(false);
        Register.GetComponent<Canvas>().enabled = false;
        MultiplayerMenu.GetComponent<Canvas>().enabled = true;
        AccountButton.gameObject.SetActive(true);
		FriendButton.gameObject.SetActive(true);
        MessageCheckButton.interactable = true;
        GameInviteButton.interactable = true;
        GameListButton.interactable = true;
        LoadingText.text = "";
        UpdatePlayerStatus("OnMainMenu");

        //Initialize notification services.
        GetComponent<Notifications>().InitializeFirebase();
    }
    public void EnterMainMenuAsUnauthenticatedUser()
    {
        LoadingSpinner.SetActive(false);

        Register.GetComponent<Canvas>().enabled = true;
        MultiplayerMenu.GetComponent<Canvas>().enabled = false;
        AccountButton.gameObject.SetActive(false);
		FriendButton.gameObject.SetActive(false);
        MessageCheckButton.interactable = false;
        GameInviteButton.interactable = false;
        GameListButton.interactable = false;
        LoadingText.text = "";
        AuthStatus.text = "";
    }

    public void OpenFriendMenu()
	{
		FriendMenu.GetComponent<Canvas>().enabled = true;
	}
	public void CloseFriendMenu()
    {
        FriendMenu.GetComponent<Canvas>().enabled = false;
    }


    public void OpenAccountMenu()
    {
        AccountManagement.GetComponent<Canvas>().enabled = true;
    }
    public void CloseAccountMenu()
    {
        AccountManagement.GetComponent<Canvas>().enabled = false;
    }
    public void SubmitNewProfileInformation()
    {
        if(NewDisplayName.text != "" ||  NewPassword.text != "" || NewEmail.text != "")
        {
            if (NewPassword.text != "" && OldPassword.text == "")
            {
                AccountValidationText.text = "Please enter your old password to update your password.";
                TriggerTimer = true;
            }
            else if (NewPassword.text != "" && OldPassword.text != "")
            {
                new ChangeUserDetailsRequest().SetNewPassword(NewPassword.text).SetOldPassword(OldPassword.text).Send((response) =>
                {
                    if (!response.HasErrors)
                    {
                        AccountValidationText.text = "Your profile has been updated.";
                        TriggerTimer = true;
                        NewPassword.text = "";
                        OldPassword.text = "";
                    }
                    else
                    {
                        AccountValidationText.text = "An error occured updating your password. Please make sure your Old Password is correct.";
                        TriggerTimer = true;
                        NewPassword.text = "";
                        OldPassword.text = "";
                    }
                });
            }

            if (NewDisplayName.text != "")
            {
                new LogEventRequest().SetEventKey("UpdateName").SetEventAttribute("stringOne", myID).SetEventAttribute("stringTwo", NewDisplayName.text).Send((response) =>
                {
                    if (!response.HasErrors)
                    {
                        new ChangeUserDetailsRequest().SetUserName(NewDisplayName.text).SetDisplayName(NewDisplayName.text).Send((response2) =>
                        {
                            if(!response2.HasErrors)
                            {
                                AccountValidationText.text = "Your profile has been updated.";
                                TriggerTimer = true;
                                AuthStatus.text = "Signed in as " + NewDisplayName.text;
                                NewDisplayName.text = "";
                            }
                            else
                            {
                                AccountValidationText.text = "An error occured updating your name.";
                                TriggerTimer = true;
                                NewDisplayName.text = "";
                            }
                        });                  
                    }
                    else
                    {
                        AccountValidationText.text = "An error occured updating your name.";
                        TriggerTimer = true;
                        NewDisplayName.text = "";
                    }
                });
            }

            if(NewEmail.text != "")
            {
                new LogEventRequest().SetEventKey("UpdateEmail").SetEventAttribute("stringOne", myID).SetEventAttribute("stringTwo", NewEmail.text).Send((response) =>
                {
                    if (!response.HasErrors)
                    {
                        AccountValidationText.text = "Your profile has been updated.";
                        TriggerTimer = true;
                        NewEmail.text = "";
                    }
                    else
                    {
                        AccountValidationText.text = "An error occured updating your email.";
                        TriggerTimer = true;
                        NewEmail.text = "";
                    }
                });
            }
        }
        else
        {
            AccountValidationText.text = "Enter a new Username, Email, or Password to update.";
            TriggerTimer = true;
        }
    }

    //this will register and sign in a new user
    public void RegisterPlayerBtn()
    {

        if (REGuserName.text != "" && REGpassword.text != "" && REGemail.text != "")
        {
            GSRequestData jsonData = new GSRequestData();
            jsonData.AddString("email", REGemail.text);
            new GameSparks.Api.Requests.RegistrationRequest()
              .SetUserName(REGuserName.text)
              .SetDisplayName(REGuserName.text)
              .SetPassword(REGpassword.text)
              .SetScriptData(jsonData)
              .Send((response) =>
              {
                  if (!response.HasErrors)
                  {
                      Validation.text = "Player Registered";
                      TriggerTimer = true;
                      ShouldGetMessages = true;


                      new LogEventRequest().SetEventKey("InsertEmail").SetEventAttribute("stringOne", response.UserId).SetEventAttribute("stringTwo", REGemail.text).Send((response2) =>
                      {
                          if(!response2.HasErrors)
                          {
                              REGuserName.text = "";
                              REGpassword.text = "";
                              REGemail.text = "";
                              StartHere();
                          }
                      });

                  }
                  else
                  {
                      GSData Errors = response.Errors;
                      if (Errors.ContainsKey("USERNAME"))
                      {
                          Validation.text = "Username is taken..";
                          TriggerTimer = true;
                      }
                      else
                      {
                          Validation.text = "Email is already associated with another account.";
                          TriggerTimer = true;
                      }
                  }
              }
            );
        }
        else
        {
            Validation.text = "Please enter an Email, Username, and Password.";
            TriggerTimer = true;
            Debug.Log("Please enter a Username and Password.");
        }
    }
    //this will sign in a current user if they're not signed in when their device boots the main menu.
    public void SignInCurrentUserBtn()
    {
        new GameSparks.Api.Requests.AuthenticationRequest()
            .SetUserName(LOGuserName.text)
            .SetPassword(LOGpassword.text)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Player Authenticated...");
                    StartHere();
                    LOGpassword.text = "";
                    LOGuserName.text = "";
                  //  LOGemail.text = "";

                }
                else
                {
                    TriggerTimer = true;
                    Validation.text = "Log In information is incorrect.  Error authenticating...";
                }
            });
    }
    //Searches for a friend based off User input on Create Canvas
    public void SearchFriend()
    {

        GSRequestData jsonData = new GSRequestData();
        jsonData.AddString("displayName", FriendNameSearch.text);

        new LogEventRequest().SetEventKey("findPlayers").SetEventAttribute("query", jsonData).Send((response) => {
            if (!response.HasErrors)
            {
                if (response.JSONString.Contains(FriendNameSearch.text) && FriendNameSearch.text != "")
                {
                    FriendInvitePanel();
                }
                else
                {
                    SearchResultsStatus.text = "User name not found.  Search again.";
                    TriggerTimer = true;
                }

                GSData data = response.ScriptData;
                List<GSData> PlayerList = data.GetGSDataList("playerList");

                foreach (GSData level in PlayerList)
                {
                    if (level.GetString("displayName") == FriendNameSearch.text)
                    {
                        friendid = level.GetString("playerId");
                    }
                }
                Debug.Log(friendid);
            }
        });
    }
    public void InviteFriendRequest()
    {
        int count = 0;

        GSRequestData jData = new GSRequestData();
        new LogEventRequest().SetEventKey("GetMyMessages").SetEventAttribute("query", jData).Send((response) =>
        {
            if (!response.HasErrors)
            {
                List<GSData> MessageListData = response.ScriptData.GetGSDataList("playerFriends");
                foreach (GSData level in MessageListData)
                {
                    MessageInfo mi = MessageInfo.CreateFromJSON(level.JSON); //call the class and method to parse the json data

                    //CHECK FOR MESSAGES/FRIENDS WHERE I WAS THE SEND OR RECEIVER
                    if (mi.friendId == mainMenu.myID || mi.playerId == mainMenu.myID)
                    {
                        if (mi.friendId == friendid || mi.playerId == friendid)
                        {
                            count++;

                            if (mi.status == "pending")
                            {
                                SearchResultsStatus.text = "There is a Friend Request already pending.";
                                TriggerTimer = true;
                            }
                            else if (mi.status == "accepted")
                            {
                                SearchResultsStatus.text = "You are already Friends with this person.";
                                TriggerTimer = true;
                            }
                            else if (mi.status == "declined")
                            {
                                if (mi.playerId == mainMenu.myID) // this means i sent a request and they declined it.
                                {
                                    SearchResultsStatus.text = "This User has declined to be your friend.";
                                    TriggerTimer = true;
                                }
                                else //this means i declined THEM, in whcih case, i should be able to send them an invite still..
                                {
                                    count--;
                                }
                            }
                        }
                    }
                }

                if (count == 0)
                {
                    SendIt();
                }
            }
            else
            {
                SearchResultsStatus.text = "An Error Occurred.";
                TriggerTimer = true;
            }
        });
    }
    private void SendIt()
    {
        new LogEventRequest().SetEventKey("friendRequest").SetEventAttribute("player_id", friendid).Send((newresponse) => {
            if (!newresponse.HasErrors)
            {
                SearchResultsStatus.text = "Invite Sent!";
                TriggerTimer = true;
            }
        });
    }

    public void ForgotPasswordButton()
    {
        GameObject PopUp = Instantiate(ForgotPasswordPopUp, spawnpoint5.transform);
    }

    public void BackFromPublicCreate() 
    {
        MultiplayerMenu.GetComponent<Canvas>().enabled = true;
        PublicCreation.GetComponent<Canvas>().enabled = false;
    }
    public void ToPublicCreate() 
    {
        MultiplayerMenu.GetComponent<Canvas>().enabled = false;
        GameObject.Find("Menu Manager").GetComponent<New_CreateMenu>().ResetMenu();
        PublicCreation.GetComponent<Canvas>().enabled = true;
        New_CreateMenu.UpdateList = true;
    }
    public void DisplayMessageList()
    {
        MessageList.GetComponent<Canvas>().enabled = true;
    }
    public void DisplayFriendList()
    {
        FriendList.GetComponent<Canvas>().enabled = true;
    }
    public void DisplayGameInviteList()
    {
        GameInviteList.GetComponent<Canvas>().enabled = true;
    }
    public void DisplayGameList()
    {
        GameList.GetComponent<Canvas>().enabled = true;
    }
    public void CloseMessageList()
    {
        MessageList.GetComponent<Canvas>().enabled = false;
        FriendList.GetComponent<Canvas>().enabled = false;
        GameInviteList.GetComponent<Canvas>().enabled = false;
        GameList.GetComponent<Canvas>().enabled = false;
    }


   
    public void FriendInvitePanel()
    {
        FriendPanel.GetComponent<Canvas>().enabled = true;

        FriendPanel_FriendName.text = FriendNameSearch.text;
    }
    public void InviteFriend()
    {
        InviteFriendRequest();
        FriendPanel.GetComponent<Canvas>().enabled = false;
    }
    public void CancelInviteFriend()
    {
        FriendPanel.GetComponent<Canvas>().enabled = false;
    }
    public void SignOut()
    {

        new LogEventRequest().SetEventKey("SignOut").Send((response) =>
        {
            if (!response.HasErrors)
            {
                myID = "";
                myName = "";
                AuthStatus.text = "";
                nameCheck = true;
                EnterMainMenuAsUnauthenticatedUser();
                PurgeGameLists();
                PurgeFriendLists();

                GetComponent<Notifications>().KillListeners();
            }
        });
      //  BackToSplash();
    }
    public void Quit()
    {
        //GameSparks.Core.GS.Disconnect();
        //Application.Quit();
        if (GameObject.Find("PopUpPanel_AreYouSure(Clone)") == null)
        {
            GameObject go = Instantiate(QuitPopUp, QuitSpawn.transform);
            FriendPanel.GetComponent<Canvas>().enabled = true;
			GameObject.Find("FriendFoundFrame").SetActive(false);
        }
    }
    public void BackToSplash()
    {
        Destroy(GameObject.Find("GameSparks Manager"));
  
        GameSparks.Core.GS.Disconnect();
        SceneManager.LoadScene("SplashMenu");
    }
}






////JSON Parsing
[System.Serializable]
public class MessageInfo
{
    public string playerId;
    public string displayName;
    public ID _id;
    public string email;
    public string friendId;
    public string friendName;
    public string group;
    public string status;
    public string requestId;
    public string senderId;
    //used for public invites only
    public string challenge;
    public string chalName;

    public static MessageInfo CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<MessageInfo>(jsonString);
    }
}

[System.Serializable]
public class EmailString
{
    public string email;

    public static EmailString CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<EmailString>(jsonString);
    }
}

//Used in Game Message Invite - for some reason teh challenge.accepted variables are formatted differently here? idgi, but this works..
[System.Serializable]
public class Experiment
{
    // public challenged challenged;
    public string[] challenged;
    public string[] declined;
    public ID _id;
    public string challengeMessage;
    public string state;
    public MyScriptData scriptData;
    public string nextPlayer;
    public string challenger;
    public string accessType;
    public Player[] accepted; //trying to get the actual playerDetail Object....
    public string color; //for the PlayerQuit handler.
    public string challenge; //for the PlayerQuit handler.


    public static Experiment CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<Experiment>(jsonString);
    }
}

[System.Serializable]
public class GameInfo
{
    // public challenged challenged;
    public string[] challenged;
    public string[] accepted; 
    public string[] declined;
    public ID _id;
    public string challengeMessage;
    public string state;
    public MyScriptData scriptData;
    public string nextPlayer;
    public string challenger;
    public string accessType;
    public Player[] Accepted; //trying to get the actual playerDetail Object....
    public string color; //for the PlayerQuit handler.
    public string challenge; //for the PlayerQuit handler.


    public static GameInfo CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<GameInfo>(jsonString);
    }
}
[System.Serializable]
public class ID
{
    public string oid;
}
[System.Serializable]
public class friendId
{
    public string playerId;
}

[System.Serializable]
public class MyScriptData
{
    public string[] Gameover;
    public string WinnerName;
    public string Winner;
    public LastTiles LastBlue;
    public LastTiles LastYellow;
    public LastTiles LastRed;
    public LastTiles LastGreen;
    public Player[] PlayerList;
    public PieceData[] LastPieces;
    public string LastPlayer;
    public WinConditions WinConditions;
    public string[] players;
    public declined[] declined;
	public playersData[] playersData;
	public string[] ChatLog, BlueYellowChatLog, BlueRedChatLog, BlueGreenChatLog, YellowRedChatLog, YellowGreenChatLog, RedGreenChatLog;
	public string[] PlayersToSkip;
	public string NextPlayerColor;
	public int TurnLimit;
	public int DateSeconds;
}
[System.Serializable]
public class declined
{
    public string id;
    public string playername;

    public static declined CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<declined>(jsonString);
    }
}
[System.Serializable]
public class playersData
{
	public string id;
	public string playername;

	public static playersData CreateFromJSON(string jsonString)
	{
		jsonString = jsonString.Replace("$", "");
		return JsonUtility.FromJson<playersData>(jsonString);
	}
}
//not in use/not working as intended...make sure cloud code doesnt rely on this before deleting it - im busy right now
[System.Serializable]
public class FriendInfo
{
    public string displayName;
    public string playerId;
    public friendId friendId;

    public static FriendInfo CreateFromJSON(string jsonString)
    {
        jsonString = jsonString.Replace("$", "");
        return JsonUtility.FromJson<FriendInfo>(jsonString);
    }
}

[System.Serializable]
public class ChatHistory
{
    public string[] ChatLog;

	public static ChatHistory CreateFromJSON(string jsonString)
    {
		return JsonUtility.FromJson<ChatHistory>(jsonString);
    }
}

