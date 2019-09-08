using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using GameSparks.Core;
using UnityEngine.SceneManagement;

public class boardmanager : MonoBehaviour
{

    //public static boardmanager Instance { set; get; }
    public boardmanager mainManager;
    private bool[,] allowedMoves { set; get; }
    public bool[,] tempAllowedMoves { set; get; } //used in king.cs to determine legal Castel moves
    //used in Checkmate code to compare teams;
    public bool IsPreviousPiece = false;
    public bool[,] mateAllowedMoves { set; get; }
    public Chessman[,] Chessmans { set; get; } //a list of all the pieces in the game
    private Chessman selectedChessman; //the piece that a player has selected.
    private Chessman automoveChessman;
    public int playerTurn = 1; //track whose turn it is. blue is 1, yellow is 2, red is 3, green is 4.
    public int prevTurn = 0;
    private const float TILE_SIZE = 1.0f; //declare a variable to apply to tile size
    private const float TILE_OFFSET = 0.5f; //declare a variable for offset; to place it correctly in the gameworld
    private int selectionX = -1; //for tracking mouse selection along the x
    private int selectionY = -1; //for tracking mouse selecction along the y
    private Quaternion orientation = Quaternion.Euler(-90, 0, 0); //used in spawnchessman to determine what direction theyre facing - needs work though
    // public List<GameObject> chessmanPrefabs; //prefabs gameobjects
    public List<GameObject> New_Chessman_Prefabs; // in trying to reduce the apk size, i'm only going to have 1 peice, and then apply team based shit through code.
    public List<Material> Team_Materials;
    public static List<GameObject> activeChessman; // a list of all chessmen in the game
    public bool menuDisplayed = false;
    //for selecting pieces
    public Material previousMat;
    public Material selectedMat;
    //for pawns and enpassants
    public int[] EnPassantMoveBlue = new int[2] { -1, -1 };
    public int[] EnPassantMoveYellow = new int[2] { -1, -1 };
    public int[] EnPassantMoveRed = new int[2] { -1, -1 };
    public int[] EnPassantMoveGreen = new int[2] { -1, -1 };
    private int oldX = -1;
    private int oldY = -1;
    //Checked - and illegalmovement variables
    public GameObject CheckTile;
    public GameObject RetreatTile;
    public List<GameObject> NoMoveTiles = new List<GameObject>();
    public Chessman k1;
    public Chessman k2;
    public Chessman k3;
    public Chessman k4;
    public Chessman q1;
    public Chessman q2;
    public Chessman q3;
    public Chessman q4;
    public static bool moved = false;
    private string capColor;
    private string capName;
    private int capX;
    private int capY;
    private int capDir;
    private bool capCheck;
    private bool capMated;
    private bool prevCheck;
    private bool prevMated;
    private int prevDir;
    public Chessman previous;
    private int lastBlueX = -1;
    private int lastBlueY = -1;
    private int lastYellowX = -1;
    private int lastYellowY = -1;
    private int lastRedX = -1;
    private int lastRedY = -1;
    private int lastGreenX = -1;
    private int lastGreenY = -1;
    //The last piece each team moved.
    private Chessman resetManBlue;
    private Chessman resetManYellow;
    private Chessman resetManRed;
    private Chessman resetManGreen;
    //CHECKMATE
    EvaluationBoard eval; // fake, null, board for brute forcething.
    EvaluationBoard boardcopy; // an experiment to see if this shit will work...
    string matecapColor = null;
    string matecapName = null;
    int matecapX;
    int matecapY;
    int matecapDir = 0;
    bool matecapCheck;
    bool matecapMated;
    Chessman mateprevious;
    int mateprevDir;
    bool mateprevCheck;
    private Chessman mateselectedChessman;
    private int mateoldX = -1;
    private int mateoldY = -1;
    public static List<GameObject> myTeam = new List<GameObject>();
    public static List<GameObject> otherTeams = new List<GameObject>();
    //to see if anybody has lost
    public bool blueout = false;
    public bool yelout = false;
    public bool redout = false;
    public bool greenout = false;
    public bool IsStalemate = false;
    public bool IsDraw = false;
    //LISTS that keep track of the pieces that are being captured by color - lists are added to when pieces are captured within the moveChessman function.
    public List<string> bluePrisonersYellow;
    public List<string> bluePrisonersRed;
    public List<string> bluePrisonersGreen;
    //
    public List<string> yelPrisonersBlue;
    public List<string> yelPrisonersRed;
    public List<string> yelPrisonersGreen;
    //
    public List<string> redPrisonersBlue;
    public List<string> redPrisonersYellow;
    public List<string> redPrisonersGreen;
    //
    public List<string> greenPrisonersBlue;
    public List<string> greenPrisonersYellow;
    public List<string> greenPrisonersRed;
    //
    //UI Elements and Variables for Prisoner Exchange.
    public List<string> prisonerList;
    public bool isDisplayed = false;
    public Canvas myCanvas;
    public Canvas okCanvas;
    public int id = 0;
    public Chessman man; //used in prisoner exchange
    public int listid = 0;
    public static bool valid = true;
    //for online game
    public bool isOnline = false;
    public GameObject OnlineGame;
    public List<string> players = new List<string>();
    public string onlinePlayerTurn;
    public string LastOnlinePlayerTurn;
    public string myID;
    public Text GameText;
    public List<Player> playerList = new List<Player>();
    Player FirstNewPlayer = new Player();
    Player SecondNewPlayer = new Player();
    Player ThirdNewPlayer = new Player();
    Player FourthNewPlayer = new Player();
    public bool OnlinePlayerMoved = false;
    public float UpdateTimer = 0;
    public List<GSData> BeforeBoard = new List<GSData>();
    public List<GameObject> AfterBoard = new List<GameObject>();
    private string GameWinner = "null";
    private string WinnerName = "null";
    public List<GameObject> before;
    public List<GameObject> after;
    public bool hasBlueTurn = false;
    public bool hasYellowTurn = false;
    public bool hasRedTurn = false;
    public bool hasGreenTurn = false;
    public bool isUnderReview = false;
    public GameObject PopUp;
    public GameObject spawner;
    public int LiveUpdateX;
    public int LiveUpdateY;
    public float LiveUpdateTimer = 0;
    public bool ShouldLiveUpdate = false;
    public string GameName;
    public bool IsAutoMove = false;
    public int autox;
    public int autoy;
    //Pris Exhcnage Menu FIxes
    public GameObject PrisListingSpawn;
    public GameObject PrisonerListItem;
    //AI Variables
    public bool isBlueAI = false;
    public bool isYellowAI = false;
    public bool isRedAI = false;
    public bool isGreenAI = false;
    public AIControl BlueAI;
    public AIControl YellowAI;
    public AIControl RedAI;
    public AIControl GreenAI;
    public GameObject AIGame;
    public float timer;
    public float timerMax;
    public bool ShouldAIMove = false;
    private bool isAIMove = false;
    public int TriggerOnlineUpdate = 1; //changed from 0 to 1 - i want this to trigger through code, not from the start...
    public bool DidPlayerTurnExpire = false; //used to control the AI in an Online Match when a player takes too long to move.
    public List<string> ExpiredColors = new List<string>();
    public float TurnLimitCountDown = 0.0f;
    public Text TurnLimitTextDisplay;
    public int TurnLimitCountDownAsInt = 0;
    public string AIAction = "action"; //this is used to figure out which team's AI moved for them in an Online Game - then, in send data, this is used to set the onlineplayerturn correctly.  afterwards, it is switched back to action.
    public int activeDudes;
    //Forced Game State
    public bool ShouldRigBoardState = false;
    public float AItimer = 0.0f;

    //DEBUG
    public Text DebugText;
    /*
     * 
     * 
     * 
     */
    /*GAME METHODS*/
    private void Awake()
    {
        myCanvas.GetComponent<Canvas>().enabled = false;
        okCanvas.GetComponent<Canvas>().enabled = false;
    } //set the dropdown to invisible
    private void Start() //called at the beginning of the game
    {
        timer = 0.0f;
        timerMax = 0.1f;

        //Instance = this;
        mainManager = GameObject.Find("Chessboard").GetComponent<boardmanager>();

        OnlineGame = GameObject.Find("ListItem_Game(Clone)");
        if (OnlineGame != null)
        {
            GetGameDataAndSpawnStuff();
        }
        else
        {
            AIGame = GameObject.Find("Splash Menu Manager");
            SpawnAllChessmans();
        }

        if (AIGame != null)
        {
            if (AIGame.GetComponent<SplashMenu>().BlueIsAI)
            {
                BlueAI = new AIControl(Chessmans, 1);
                isBlueAI = true;
                Debug.Log("Blue AI On");
            }
            if (AIGame.GetComponent<SplashMenu>().YellowIsAI)
            {
                YellowAI = new AIControl(Chessmans, 2);
                isYellowAI = true;
                Debug.Log("Yellow AI On");
            }
            if (AIGame.GetComponent<SplashMenu>().RedIsAI)
            {
                RedAI = new AIControl(Chessmans, 3);
                isRedAI = true;
                Debug.Log("Red AI On");
            }
            if (AIGame.GetComponent<SplashMenu>().GreenIsAI)
            {
                GreenAI = new AIControl(Chessmans, 4);
                isGreenAI = true;
                Debug.Log("Green AI On");
            }
            if (AIGame != null)
            {
                Destroy(GameObject.Find("Splash Menu Manager").gameObject);
            }
        }

        if (playerTurn == 1 && isBlueAI)
        {
            ShouldAIMove = true;
        }
        else if (playerTurn == 2 && isYellowAI)
        {
            ShouldAIMove = true;
        }
        else if (playerTurn == 3 && isRedAI)
        {
            ShouldAIMove = true;
        }
        else if (playerTurn == 4 && isGreenAI)
        {
            ShouldAIMove = true;
        }




        Debug.Log("It is Player " + playerTurn + "'s turn.");
        if (!isOnline)
        {
            GameText.text = "It is Player " + playerTurn + "'s turn.";

            GameObject[] Panels = GameObject.FindGameObjectsWithTag("playerbox");
            foreach (GameObject go in Panels)
            {
                Destroy(go.gameObject);
            }
        }
    }
    private void Update() //this is called every frame
    {
        //DebugText.text = "Active: " + activeChessman.Count;
        activeDudes = activeChessman.Count;
        UpdateSelection();
        DrawChessboard();

        #region After Moved
        if (moved && !IsAutoMove) //this triggers to True at the end of a successuflly run Movement(). it switches back to false at the end of this IF.
        {
            turnLoop();
            turnControl();
            //See if anybody was put in check due to the previous move
            CheckMoves();
            //see if anybody is no longer in check anymore due to the previous move
            notChecked();
            //if the user ignored or moved into check then go back////////////////////////////////////////////////////////////////
            // illegalMovement();

            if (valid) //valid will change to false int he illegal move method when it has to reset shit.
            {
                //if there's a pawn at the end of the board then do the prisoner exchange
                if (previous)
                {
                    PrisonExchange(previous.CurrentX, previous.CurrentY);
                }

                if (myCanvas.GetComponent<Canvas>().enabled == false)
                {
                    //if the king is capured, promote the queen, otherwise, if there's not a queen, you lose.
                    promoteQueen();
                    //see if anybody lost as a result of the last move.
                    Lose();
                    //see if we have a winner yet
                    Win();
                    //this keeps track of each team's last moved piece and last postition that moved from - this way, we can check if they move back there when the illegalmoves function is called.         
                    storeLastMove();

                    // Make sure the global stalemate bool is false - it'l snap to true the logic below if it needs to be.

                    New_Checkmate();
                    Stalemate();

                    //if your king is in the corner, jailbreak
                    Jailbreak();
                     
                    // turnControl();

                    //SEND A BOARD AND TURN UPDATE TO THE SEVER< HAVE THE SERVER SEND THE NEXT PLAYER A MESSAGE ABOUT THEIR TURN.
                    if (isOnline)
                    {
                        SendGameDataAndAdvanceTurn();
                    }

                    if (playerTurn == 1 && isBlueAI)
                    {
                        ShouldAIMove = true;
                    }
                    else if (playerTurn == 2 && isYellowAI)
                    {
                        ShouldAIMove = true;
                    }
                    else if (playerTurn == 3 && isRedAI)
                    {
                        ShouldAIMove = true;
                    }
                    else if (playerTurn == 4 && isGreenAI)
                    {
                        ShouldAIMove = true;
                    }

                    //trigger the ad timer if we're offline, and a person just
                    //made a legal move.
                    if (!isOnline)
                    {
                        AdTest.should_countdown = true;
                    }
                }
            }
            else if (!valid)
            {
                valid = true;
                moved = false;
            }

            moved = false;
        }
        #endregion

        #region Input
        if (!isUnderReview)
        {
            if (!isDisplayed && !menuDisplayed)
            {
                //timer += Time.deltaTime;
                //if (timer >= timerMax){


                if (Input.GetMouseButtonDown(0)) //if the User clicks the left mouse button...
                {
                    if (selectionX >= 0 && selectionY >= 0) // ...and theyre actually clicking on the board...
                    {
                        if (!isOnline)
                        {
                            if (selectedChessman == null) //...and they have nothing selected
                            {
                                Chessman c = Chessmans[selectionX, selectionY];

                                if (playerTurn == 1 && c.isBlue && !isBlueAI)
                                {
                                    SelectChessman(selectionX, selectionY); //...then select this peice.
                                }
                                if (playerTurn == 2 && c.isYellow && !isYellowAI)
                                {
                                    SelectChessman(selectionX, selectionY); //...then select this peice.
                                }
                                if (playerTurn == 3 && c.isRed && !isRedAI)
                                {
                                    SelectChessman(selectionX, selectionY); //...then select this peice.
                                }
                                if (playerTurn == 4 && c.isGreen && !isGreenAI)
                                {
                                    SelectChessman(selectionX, selectionY); //...then select this peice.
                                }
                            }
                            else // ...or else, they must have something selected already
                            {
                                MoveChessman(selectionX, selectionY); //...in which case their selected piece shoudl move to where they clicked.          

                            }
                        }
                        else
                        {
                            if (selectedChessman == null) //...and they have nothing selected
                            {
                                Chessman c = Chessmans[selectionX, selectionY];

                                if (myID == onlinePlayerTurn && c.isBlue)
                                {
                                    SelectChessman(selectionX, selectionY); //...then select this peice.
                                }
                                if (myID == onlinePlayerTurn && c.isYellow)
                                {
                                    SelectChessman(selectionX, selectionY); //...then select this peice.
                                }
                                if (myID == onlinePlayerTurn && c.isRed)
                                {
                                    SelectChessman(selectionX, selectionY); //...then select this peice.
                                }
                                if (myID == onlinePlayerTurn && c.isGreen)
                                {
                                    SelectChessman(selectionX, selectionY); //...then select this peice.
                                }
                            }
                            else // ...or else, they must have something selected already
                            {
                                MoveChessman(selectionX, selectionY); //...in which case their selected piece shoudl move to where they clicked
                            }
                        }
                    }
                }
            }
        }
        #endregion

        if (ShouldAIMove)
        {
            AItimer++;
        }
        if (AItimer > 60.0)
        {
            ShouldAIMove = false;
            AItimer = 0.0f;
            AIMove();
        }


        //makes sure the board will correct itself if a player is skipped, and the local data is not in synce with the server data as far as turn control.
        if (isOnline && TriggerOnlineUpdate == 0)
        {
            Debug.Log("comparing the playerturn/playerid to the onlineplayerturn");
            if (playerList != null || playerList.Count > 0)
            {
                if (playerTurn == 1 && onlinePlayerTurn != playerList[0].id ||
                    playerTurn == 2 && onlinePlayerTurn != playerList[1].id ||
                    playerTurn == 3 && onlinePlayerTurn != playerList[2].id ||
                    playerTurn == 4 && onlinePlayerTurn != playerList[3].id)
                {
                    Debug.Log("onlinePlayerTurn: " + onlinePlayerTurn);
                    Debug.Log("player turn: " + playerTurn);
                    Debug.Log("player 1: " + playerList[0].id);
                    Debug.Log("player 2: " + playerList[1].id);
                    Debug.Log("player 3: " + playerList[2].id);
                    Debug.Log("player 4: " + playerList[3].id);
                    if (!DidPlayerTurnExpire)
                    {
                        TriggerOnlineUpdate++;
                        OnlinePlayerSkipped();
                    }
                }
            }
            TriggerOnlineUpdate++;
        }

        if (TurnLimitCountDown > 0)
        {
            TurnLimitCountDown -= Time.deltaTime;
            TurnLimitTextDisplay.text = "Turn Limit: " + TurnLimitCountDown;
        }
        else
        {
            TurnLimitCountDown = 0.0f;
        }
    }

    //if we're online the start method calls this: 
    public void GetGameDataAndSpawnStuff()
    {
        isOnline = true;
        if (myID == null || myID == "")
        {
            new AccountDetailsRequest().Send((response) =>
            {
                myID = response.UserId;
            });
        }
        //get challenge data from the server

        new GetChallengeRequest()
          .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
          .Send((response) =>
          {
              if (!response.HasErrors)
              {
                  Challenge challengeData = Challenge.CreateFromJSON(response.Challenge.JSONString); //deserialize the incoming data
                  GameInfo GameInfo = GameInfo.CreateFromJSON(response.Challenge.JSONString); //i tried again....so...leave me alone.
                  GameName = GameInfo.challengeMessage;

                  #region Collect Board States
                  try
                  {
                      List<GSData> bb = response.Challenge.ScriptData.GetGSDataList("bluebefore");
                      if (bb.Count > 0)
                      {
                          hasBlueTurn = true;
                      }

                      List<GSData> yb = response.Challenge.ScriptData.GetGSDataList("yellowbefore");
                      if (yb.Count > 0)
                      {
                          hasYellowTurn = true;
                      }

                      List<GSData> rb = response.Challenge.ScriptData.GetGSDataList("redbefore");
                      if (rb.Count > 0)
                      {
                          hasRedTurn = true;
                      }

                      List<GSData> gb = response.Challenge.ScriptData.GetGSDataList("greenbefore");
                      if (gb.Count > 0)
                      {
                          hasGreenTurn = true;
                      }

                  }
                  catch
                  {

                  }
                  #endregion
                  #region ASSIGN WINCONDITION VARIABLES
                  if (response.Challenge.ScriptData.GetGSData("WinConditions") != null)
                  {
                      Debug.Log("setting wincon");
                      GSData wincon = response.Challenge.ScriptData.GetGSData("WinConditions");
                      WinConditions wc = WinConditions.CreateFromJSON(wincon.JSON);
                      playerTurn = wc.playerT;
                      prevTurn = wc.prevT;
                      blueout = wc.Bout;
                      yelout = wc.Yout;
                      redout = wc.Rout;
                      greenout = wc.Gout;

                      LastOnlinePlayerTurn = response.Challenge.ScriptData.GetString("LastPlayer");
                  }
                  #endregion
                  #region Get the players in the match and assign their ids into a list
                  onlinePlayerTurn = challengeData.nextPlayer;
                  Debug.Log("Player Turn =  " + onlinePlayerTurn);

                  if (playerList != null)
                  {
                      if (playerList.Count > 0)
                      {
                          playerList.Clear();
                      }
                  }

                  try
                  {
                      Debug.Log("Assign the colors");
                      List<GSData> GSmyplayerlist = response.Challenge.ScriptData.GetGSDataList("PlayerList");

                      foreach (GSData d in GSmyplayerlist)
                      {
                          Player newplayer = Player.CreateFromJSON(d.JSON);
                          playerList.Add(newplayer);
                      }

                      foreach (Player item in playerList)
                      {
                          if (onlinePlayerTurn == item.id) //if we dont know the color, we need to assign it the first time they make a move
                          {
                              if (item.color == null || item.color == "")
                              {
                                  if (playerTurn == 1)
                                  {
                                      item.color = "Blue";
                                      FirstNewPlayer = item;
                                  }
                                  if (playerTurn == 2)
                                  {
                                      item.color = "Yellow";
                                      SecondNewPlayer = item;
                                  }
                                  if (playerTurn == 3)
                                  {
                                      item.color = "Red";
                                      ThirdNewPlayer = item;
                                  }
                                  if (playerTurn == 4)
                                  {
                                      item.color = "Green";
                                      FourthNewPlayer = item;
                                  }
                              }
                              else //if we know the color assign them to the right player order
                              {
                                  if (item.color == "Blue")
                                  {
                                      FirstNewPlayer = item;
                                  }
                                  if (item.color == "Yellow")
                                  {
                                      SecondNewPlayer = item;
                                  }
                                  if (item.color == "Red")
                                  {
                                      ThirdNewPlayer = item;
                                  }
                                  if (item.color == "Green")
                                  {
                                      FourthNewPlayer = item;
                                  }
                              }
                          }
                          else
                          {
                              continue;
                          }
                      }
                  }
                  catch //if the TRY throughs an execption, we need to start the player turn order list completely from scratch based off the ACCEPTED LIST.
                  {
                      Debug.Log("Creating the list");
                      FirstNewPlayer.id = challengeData.accepted[0].id;
                      FirstNewPlayer.playerName = challengeData.accepted[0].name;
                      FirstNewPlayer.color = "Blue";
                      playerList.Add(FirstNewPlayer);

                      SecondNewPlayer.id = challengeData.accepted[1].id;
                      SecondNewPlayer.playerName = challengeData.accepted[1].name;
                      SecondNewPlayer.color = "Yellow";
                      playerList.Add(SecondNewPlayer);

                      ThirdNewPlayer.id = challengeData.accepted[2].id;
                      ThirdNewPlayer.playerName = challengeData.accepted[2].name;
                      ThirdNewPlayer.color = "Red";
                      playerList.Add(ThirdNewPlayer);

                      FourthNewPlayer.id = challengeData.accepted[3].id;
                      FourthNewPlayer.playerName = challengeData.accepted[3].name;
                      FourthNewPlayer.color = "Green";
                      playerList.Add(FourthNewPlayer);
                  }
                  #endregion
                  #region collect chat logs
                  GameObject[] allChatListings = GameObject.FindGameObjectsWithTag("chatMessage");

                  if (allChatListings.Length > 0)
                  {
                      foreach (GameObject go in allChatListings) // for each peice on the board that is active...
                      {
                          Destroy(go.gameObject);
                      }
                  }

                  string myColor = "color";
                  foreach (Player p in playerList)
                  {
                      if (p.id == myID)
                      {
                          myColor = p.color;
                      }
                  }

                  if (GameInfo.scriptData.ChatLog != null && GameInfo.scriptData.ChatLog.Length != 0)
                  {
                      foreach (var log in GameInfo.scriptData.ChatLog)
                      {
                          Debug.Log(log);
                          GameObject.Find("GameSparks Manager").GetComponent<GameSparksManager>().BuildChatLog(log);
                      }
                  }
                  if (GameInfo.scriptData.BlueYellowChatLog != null && GameInfo.scriptData.BlueYellowChatLog.Length != 0)
                  {
                      if (myColor == "Blue") //if I'm blue, spawn the convo on yellow's chat screen.
                      {
                          foreach (var log in GameInfo.scriptData.BlueYellowChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToYellowMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("yellowchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToYellowMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                      if (myColor == "Yellow") //if I'm yellow, spawn the convo on the chatToBlue Screen.
                      {
                          foreach (var log in GameInfo.scriptData.BlueYellowChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToBlueMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("bluechatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToBlueMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                  }
                  if (GameInfo.scriptData.BlueRedChatLog != null && GameInfo.scriptData.BlueRedChatLog.Length != 0)
                  {
                      if (myColor == "Blue")
                      {
                          foreach (var log in GameInfo.scriptData.BlueRedChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToRedMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("redchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToRedMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                      if (myColor == "Red")
                      {
                          foreach (var log in GameInfo.scriptData.BlueRedChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToBlueMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("bluechatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToBlueMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                  }
                  if (GameInfo.scriptData.BlueGreenChatLog != null && GameInfo.scriptData.BlueGreenChatLog.Length != 0)
                  {
                      if (myColor == "Blue")
                      {
                          foreach (var log in GameInfo.scriptData.BlueGreenChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToGreenMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("greenchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToGreenMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                      if (myColor == "Green")
                      {
                          foreach (var log in GameInfo.scriptData.BlueGreenChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToBlueMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("bluechatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToBlueMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                  }

                  if (GameInfo.scriptData.YellowRedChatLog != null && GameInfo.scriptData.YellowRedChatLog.Length != 0)
                  {
                      if (myColor == "Yellow")
                      {
                          foreach (var log in GameInfo.scriptData.YellowRedChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToRedMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("redchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToRedMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                      if (myColor == "Red")
                      {
                          foreach (var log in GameInfo.scriptData.YellowRedChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToYellowMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("yellowchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToYellowMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                  }
                  if (GameInfo.scriptData.YellowGreenChatLog != null && GameInfo.scriptData.YellowGreenChatLog.Length != 0)
                  {
                      if (myColor == "Yellow")
                      {
                          foreach (var log in GameInfo.scriptData.YellowGreenChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToGreenMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("greenchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToGreenMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                      if (myColor == "Green")
                      {
                          foreach (var log in GameInfo.scriptData.YellowGreenChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToYellowMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("yellowchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToYellowMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                  }
                  if (GameInfo.scriptData.RedGreenChatLog != null && GameInfo.scriptData.RedGreenChatLog.Length != 0)
                  {
                      if (myColor == "Red")
                      {
                          foreach (var log in GameInfo.scriptData.RedGreenChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToGreenMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("greenchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToGreenMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                      if (myColor == "Green")
                      {
                          foreach (var log in GameInfo.scriptData.RedGreenChatLog)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToRedMenu.SetActive(true);
                              Chat ChatVariable = GameObject.FindGameObjectWithTag("redchatmenu").GetComponent<Chat>();
                              GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToRedMenu.SetActive(false);
                              GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
                              ChatListing.GetComponentInChildren<Text>().text = log;
                          }
                      }
                  }
                  #endregion
                  #region Get Turn Expire time for the countdown.
                  if (GameInfo.scriptData.TurnLimit > 0)
                  {
                      if (GameInfo.scriptData.DateSeconds != null || GameInfo.scriptData.DateSeconds != 0 && GameInfo.scriptData.TurnLimit != 0)
                      {
                          System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                          int CurrentTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
                          int ElapsedTime = CurrentTime - GameInfo.scriptData.DateSeconds;

                          TurnLimitCountDown = GameInfo.scriptData.TurnLimit - ElapsedTime;

                          Debug.Log("TurnLimitCountDown: " + TurnLimitCountDown);
                      }
                      if (GameInfo.scriptData.TurnLimit == 0)
                      {
                          TurnLimitTextDisplay.text = "Turn Limit: Unlimited";
                      }
                  }
                  #endregion

                  if (response.Challenge.ScriptData.GetGSDataList("gameState") != null)
                  {
                      #region spawn pieces
                      if (activeChessman != null)
                      {
                          foreach (GameObject item in activeChessman)
                          {
                              Destroy(item.gameObject);
                          }
                      }

                      //SPAWN THE PIECES ONTO THE CHESSBOARD ACCORDING TO THE GAME DATA FROM THE SERVER
                      BeforeBoard = response.Challenge.ScriptData.GetGSDataList("gameState");
                      foreach (GSData item in BeforeBoard)
                      {
                          PieceData piece = PieceData.CreateFromJSON(item.JSON);
                          SpawnChessman(piece.index, piece.SaveX, piece.SaveY);
                          Chessmans[piece.SaveX, piece.SaveY].isChecked = piece.isChecked;
                          Chessmans[piece.SaveX, piece.SaveY].isMated = piece.isMated;
                          Chessmans[piece.SaveX, piece.SaveY].pawnDirection = piece.pawnDirection;
                      }
                      #endregion
                      #region assign enpassant tiles if there are any...
                      //ASSIGN THE ENPASSANT VALUES
                      List<GSData> enpassData = response.Challenge.ScriptData.GetGSDataList("Enpass");
                      Enpass e0 = Enpass.CreateFromJSON(enpassData[0].JSON);
                      EnPassantMoveBlue[0] = e0.number;
                      Enpass e1 = Enpass.CreateFromJSON(enpassData[1].JSON);
                      EnPassantMoveBlue[1] = e1.number;
                      Enpass e2 = Enpass.CreateFromJSON(enpassData[2].JSON);
                      EnPassantMoveYellow[0] = e2.number;
                      Enpass e3 = Enpass.CreateFromJSON(enpassData[3].JSON);
                      EnPassantMoveYellow[1] = e3.number;
                      Enpass e4 = Enpass.CreateFromJSON(enpassData[4].JSON);
                      EnPassantMoveRed[0] = e4.number;
                      Enpass e5 = Enpass.CreateFromJSON(enpassData[5].JSON);
                      EnPassantMoveRed[1] = e5.number;
                      Enpass e6 = Enpass.CreateFromJSON(enpassData[6].JSON);
                      EnPassantMoveGreen[0] = e6.number;
                      Enpass e7 = Enpass.CreateFromJSON(enpassData[7].JSON);
                      EnPassantMoveGreen[1] = e7.number;
                      #endregion
                      #region ASSIGN ALL THE PRISONER LISTS

                      bluePrisonersYellow.Clear();
                      bluePrisonersRed.Clear();
                      bluePrisonersGreen.Clear();
                      yelPrisonersBlue.Clear();
                      yelPrisonersRed.Clear();
                      yelPrisonersGreen.Clear();
                      redPrisonersBlue.Clear();
                      redPrisonersYellow.Clear();
                      redPrisonersGreen.Clear();
                      greenPrisonersBlue.Clear();
                      greenPrisonersYellow.Clear();
                      greenPrisonersRed.Clear();

                      List<GSData> bpyList = response.Challenge.ScriptData.GetGSDataList("bpy");
                      foreach (GSData item in bpyList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              bluePrisonersYellow.Add(pi.prisName);
                          }
                      }
                      List<GSData> bprList = response.Challenge.ScriptData.GetGSDataList("bpr");
                      foreach (GSData item in bprList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              bluePrisonersRed.Add(pi.prisName);
                          }
                      }
                      List<GSData> bpgList = response.Challenge.ScriptData.GetGSDataList("bpg");
                      foreach (GSData item in bpgList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              bluePrisonersGreen.Add(pi.prisName);
                          }
                      }
                      List<GSData> ypbList = response.Challenge.ScriptData.GetGSDataList("ypb");
                      foreach (GSData item in ypbList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              yelPrisonersBlue.Add(pi.prisName);
                          }
                      }
                      List<GSData> yprList = response.Challenge.ScriptData.GetGSDataList("ypr");
                      foreach (GSData item in yprList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              yelPrisonersRed.Add(pi.prisName);
                          }
                      }
                      List<GSData> ypgList = response.Challenge.ScriptData.GetGSDataList("ypg");
                      foreach (GSData item in ypgList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              yelPrisonersGreen.Add(pi.prisName);
                          }
                      }
                      List<GSData> rpbList = response.Challenge.ScriptData.GetGSDataList("rpb");
                      foreach (GSData item in rpbList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              redPrisonersBlue.Add(pi.prisName);
                          }
                      }
                      List<GSData> rpyList = response.Challenge.ScriptData.GetGSDataList("rpy");
                      foreach (GSData item in rpyList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              redPrisonersYellow.Add(pi.prisName);
                          }
                      }
                      List<GSData> rpgList = response.Challenge.ScriptData.GetGSDataList("rpg");
                      foreach (GSData item in rpgList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              redPrisonersGreen.Add(pi.prisName);
                          }
                      }
                      List<GSData> gpbList = response.Challenge.ScriptData.GetGSDataList("gpb");
                      foreach (GSData item in gpbList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              greenPrisonersBlue.Add(pi.prisName);
                          }
                      }
                      List<GSData> gpyList = response.Challenge.ScriptData.GetGSDataList("gpy");
                      foreach (GSData item in gpyList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              greenPrisonersYellow.Add(pi.prisName);
                          }
                      }
                      List<GSData> gprList = response.Challenge.ScriptData.GetGSDataList("gpr");
                      foreach (GSData item in gprList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              greenPrisonersRed.Add(pi.prisName);
                          }
                      }
                      #endregion
                      #region TRACK EACH TEAMS LAST PIECE AND LAST TILE

                      List<GSData> LastPieces = response.Challenge.ScriptData.GetGSDataList("LastPieces");
                      if (LastPieces != null)
                      {
                          for (int i = 0; i < LastPieces.Count; i++)
                          {
                              PieceData piece = PieceData.CreateFromJSON(LastPieces[i].JSON);
                              if (piece.isBlue) { resetManBlue = Chessmans[piece.SaveX, piece.SaveY]; }
                              if (piece.isYellow) { resetManYellow = Chessmans[piece.SaveX, piece.SaveY]; }
                              if (piece.isRed) { resetManRed = Chessmans[piece.SaveX, piece.SaveY]; }
                              if (piece.isGreen) { resetManGreen = Chessmans[piece.SaveX, piece.SaveY]; }
                          }
                      }

                      GSData lastBlueTiles = response.Challenge.ScriptData.GetGSData("LastBlue");
                      if (lastBlueTiles != null)
                      {
                          LastTiles b = LastTiles.CreateFromJSON(lastBlueTiles.JSON);
                          lastBlueX = b.LastX;
                          lastBlueY = b.LastY;
                      }

                      GSData lastYellowTiles = response.Challenge.ScriptData.GetGSData("LastYellow");
                      if (lastYellowTiles != null)
                      {
                          LastTiles y = LastTiles.CreateFromJSON(lastYellowTiles.JSON);
                          lastYellowX = y.LastX;
                          lastYellowY = y.LastY;
                      }

                      GSData lastRedTiles = response.Challenge.ScriptData.GetGSData("LastRed");
                      if (lastRedTiles != null)
                      {
                          LastTiles r = LastTiles.CreateFromJSON(lastRedTiles.JSON);
                          lastRedX = r.LastX;
                          lastRedY = r.LastY;
                      }

                      GSData lastGreenTiles = response.Challenge.ScriptData.GetGSData("LastGreen");
                      if (lastGreenTiles != null)
                      {
                          LastTiles g = LastTiles.CreateFromJSON(lastGreenTiles.JSON);
                          lastGreenX = g.LastX;
                          lastGreenY = g.LastY;
                      }
                      #endregion
                  }
                  else
                  {
                      SpawnAllChessmans();
                      BeforeBoardCollection();
                  }
                  Lose();
                  FindPlayerTurn();

                  if (response.Challenge.ScriptData.GetGSData("VotingPlayers") != null)
                  {
                      GSData data = response.Challenge.ScriptData.GetGSData("VotingPlayers");
                      Vote voteData = Vote.CreateFromJSON(data.JSON);

                      foreach (var item in voteData.WaitingVotes)
                      {
                          GameObject.Find("MenuController").GetComponent<GameMenu>().WaitingVoteList.Add(item);
                      }
                      foreach (var item in voteData.NoVotes)
                      {
                          GameObject.Find("MenuController").GetComponent<GameMenu>().NoVoteList.Add(item);
                      }
                      foreach (var item in voteData.YesVotes)
                      {
                          GameObject.Find("MenuController").GetComponent<GameMenu>().YesVoteList.Add(item);
                      }

                      if (voteData.isActiveVote)
                      {
                          Debug.Log("make it active in game menu");
                          GameObject.Find("MenuController").GetComponent<GameMenu>().isVoteActive = true;

                          if (GameObject.Find("MenuController").GetComponent<GameMenu>().WaitingVoteList.Count != 0)
                          {
                              Debug.Log("there are pending votes");
                              foreach (var item in GameObject.Find("MenuController").GetComponent<GameMenu>().WaitingVoteList)
                              {
                                  Debug.Log("searching through pending votes");
                                  if (item == myID)
                                  {
                                      Debug.Log("your vote is pending");
                                      GameObject.Find("MenuController").GetComponent<GameMenu>().DemandVoteCast();
                                  }
                              }
                          }
                          else
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().isVoteActive = false;
                          }

                          if (GameObject.Find("MenuController").GetComponent<GameMenu>().YesVoteList.Count != 0)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().YayCount = GameObject.Find("MenuController").GetComponent<GameMenu>().YesVoteList.Count;
                          }
                          if (GameObject.Find("MenuController").GetComponent<GameMenu>().NoVoteList.Count != 0)
                          {
                              GameObject.Find("MenuController").GetComponent<GameMenu>().NayCount = GameObject.Find("MenuController").GetComponent<GameMenu>().NoVoteList.Count;
                          }

                      }
                  }

                  if (GameInfo.scriptData.PlayersToSkip != null)
                  {
                      if (GameInfo.scriptData.PlayersToSkip.Length > 0)
                      {
                          foreach (string s in GameInfo.scriptData.PlayersToSkip)
                          {
                              ExpiredColors.Add(s);
                          }

                          OnlineTurnExpired();
                      }

                  }

                  // when it is my turn in an online game, i have to watch an 
                  // unskippable ad prior to taking my turn.
                  if (onlinePlayerTurn == myID)
                  {
                      GameObject.Find("AdManager").GetComponent<AdTest>().PlayRewardedAd();
                  }

                  // TriggerOnlineUpdate = 0; //now that we have data collected - we can change this to 0, which enables Update to evaluate the server data with the local dat and try to fix it..
              }
              else
              {
                  Debug.Log("fetching challenge data messed up.");

                  SceneManager.LoadScene("MainMenu");

              }
          });

        if (isOnline)
        {
            mainMenu.UpdatePlayerStatus(OnlineGame.GetComponent<GameInviteMessage>().challengeId);
        }
        // Call this - because this data is not passed ot the server.
        Stalemate();
    }
    public void SendGameDataAndAdvanceTurn()
    {
        GameText.text = "Sending game data, please wait....";
        //COLLECT ALL THE DATA
        #region Collect the board state at the Start and End of the Turn so we can revist the previous turns.
        //collect the after board state for each player so we can revist the moves later.
        AfterBoard.Clear();
        foreach (GameObject go in activeChessman)
        {
            AfterBoard.Add(go);
        }

        List<GSData> AfterList = new List<GSData>();
        foreach (GameObject go in AfterBoard)
        {
            if (go != null)
            {
                go.GetComponent<Chessman>().SaveX = go.GetComponent<Chessman>().CurrentX;
                go.GetComponent<Chessman>().SaveY = go.GetComponent<Chessman>().CurrentY;
                go.GetComponent<Chessman>().Name = go.name;

                string json = JsonUtility.ToJson(go.GetComponent<Chessman>()); //turn the ChessmanClass into a JSON string
                GSObject piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
                AfterList.Add(piece);
            }
        }


        if (prevTurn == 1)
        {
            new LogChallengeEventRequest()
                .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                .SetEventKey("TurnState_Blue")
                .SetEventAttribute("before", BeforeBoard)
                .SetEventAttribute("after", AfterList)
                                        .Send((response) =>
                                        {
                                            if (!response.HasErrors)
                                            {
                                                Debug.Log("turn data saved");
                                            }
                                            else
                                            {
                                                Debug.Log("error saving p1 turn data");
                                            }
                                        });
        }
        if (prevTurn == 2)
        {
            new LogChallengeEventRequest()
               .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
               .SetEventKey("TurnState_Yellow")
               .SetEventAttribute("before", BeforeBoard)
               .SetEventAttribute("after", AfterList)
                                       .Send((response) =>
                                       {
                                           if (!response.HasErrors)
                                           {
                                               Debug.Log("turn data saved");
                                           }
                                           else
                                           {
                                               Debug.Log("error saving p2 turn data");
                                           }
                                       });
        }
        if (prevTurn == 3)
        {
            new LogChallengeEventRequest()
            .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            .SetEventKey("TurnState_Red")
            .SetEventAttribute("before", BeforeBoard)
            .SetEventAttribute("after", AfterList)
                            .Send((response) =>
                            {
                                if (!response.HasErrors)
                                {
                                    Debug.Log("turn data saved");
                                }
                                else
                                {
                                    Debug.Log("error saving p3 turn data");
                                }
                            });
        }
        if (prevTurn == 4)
        {
            new LogChallengeEventRequest()
            .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            .SetEventKey("TurnState_Green")
            .SetEventAttribute("before", BeforeBoard)
            .SetEventAttribute("after", AfterList)
                            .Send((response) =>
                            {
                                if (!response.HasErrors)
                                {
                                    Debug.Log("turn data saved");
                                }
                                else
                                {
                                    Debug.Log("error saving p4 turn data");
                                }
                            });
        }

        #endregion
        #region      COLLECT GAME DATA FROM EVERY PIECE ON THE BOARD
        List<GSData> objectList = new List<GSData>();
        foreach (GameObject go in activeChessman) //send all the pieces on the board.
        {
            if (go != null)
            {
                go.GetComponent<Chessman>().SaveX = go.GetComponent<Chessman>().CurrentX;
                go.GetComponent<Chessman>().SaveY = go.GetComponent<Chessman>().CurrentY;
                go.GetComponent<Chessman>().Name = go.name;

                string json = JsonUtility.ToJson(go.GetComponent<Chessman>()); //turn the ChessmanClass into a JSON string
                GSObject piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
                objectList.Add(piece);
            }
        }
        #endregion
        #region   COLLECT ALL ENPASSANT DATA
        List<GSData> enpassData = new List<GSData>();
        List<int> enpassInts = new List<int>();
        enpassInts.Add(EnPassantMoveBlue[0]);
        enpassInts.Add(EnPassantMoveBlue[1]);
        enpassInts.Add(EnPassantMoveYellow[0]);
        enpassInts.Add(EnPassantMoveYellow[1]);
        enpassInts.Add(EnPassantMoveRed[0]);
        enpassInts.Add(EnPassantMoveRed[1]);
        enpassInts.Add(EnPassantMoveGreen[0]);
        enpassInts.Add(EnPassantMoveGreen[1]);
        foreach (int i in enpassInts)
        {
            Enpass number = new Enpass();
            number.number = i;
            string json = JsonUtility.ToJson(number);
            GSObject o = GSObject.FromJson(json);
            enpassData.Add(o);
        }
        #endregion
        #region        COLLECT ALL PRISONER LISTS
        List<GSData> bpyList = new List<GSData>();
        List<GSData> bprList = new List<GSData>();
        List<GSData> bpgList = new List<GSData>();
        List<GSData> ypbList = new List<GSData>();
        List<GSData> yprList = new List<GSData>();
        List<GSData> ypgList = new List<GSData>();
        List<GSData> rpbList = new List<GSData>();
        List<GSData> rpyList = new List<GSData>();
        List<GSData> rpgList = new List<GSData>();
        List<GSData> gpbList = new List<GSData>();
        List<GSData> gpyList = new List<GSData>();
        List<GSData> gprList = new List<GSData>();
        //bluePrisonersYellow;
        foreach (string item in bluePrisonersYellow)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            bpyList.Add(prisoner);
        }
        //bluePrisonersRed;
        foreach (string item in bluePrisonersRed)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            bprList.Add(prisoner);
        }
        //bluePrisonersGreen;
        foreach (string item in bluePrisonersGreen)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            bpgList.Add(prisoner);
        }
        ////
        //yelPrisonersBlue;
        foreach (string item in yelPrisonersBlue)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            ypbList.Add(prisoner);
        }
        //yelPrisonersRed;
        foreach (string item in yelPrisonersRed)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            yprList.Add(prisoner);
        }
        //yelPrisonersGreen;
        foreach (string item in yelPrisonersGreen)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            ypgList.Add(prisoner);
        }
        ////
        //redPrisonersBlue;
        foreach (string item in redPrisonersBlue)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            rpbList.Add(prisoner);
        }
        //redPrisonersYellow;
        foreach (string item in redPrisonersYellow)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            rpyList.Add(prisoner);
        }
        //redPrisonersGreen;
        foreach (string item in redPrisonersGreen)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            rpgList.Add(prisoner);
        }
        ////
        //greenPrisonersBlue;
        foreach (string item in greenPrisonersBlue)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            gpbList.Add(prisoner);
        }
        //greenPrisonersYellow;
        foreach (string item in greenPrisonersYellow)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            gpyList.Add(prisoner);
        }
        //greenPrisonersRed;
        foreach (string item in greenPrisonersRed)
        {
            PrisonerItem pi = new PrisonerItem();
            pi.prisName = item;
            string json = JsonUtility.ToJson(pi);
            GSObject prisoner = GSObject.FromJson(json);
            gprList.Add(prisoner);
        }
        #endregion
        #region COLLECT ALL PREVIOUS PEICES AND THEIR PREVIOUS TILE
        List<GSData> LastPieces = new List<GSData>();
        List<GameObject> resetMen = new List<GameObject>();
        if (resetManBlue != null)
        {
            resetMen.Add(resetManBlue.gameObject);
        }
        if (resetManYellow != null)
        {
            resetMen.Add(resetManYellow.gameObject);
        }
        if (resetManRed != null)
        {
            resetMen.Add(resetManRed.gameObject);
        }
        if (resetManGreen != null)
        {
            resetMen.Add(resetManGreen.gameObject);
        }

        foreach (GameObject go in resetMen)
        {
            go.GetComponent<Chessman>().SaveX = go.GetComponent<Chessman>().CurrentX;
            go.GetComponent<Chessman>().SaveY = go.GetComponent<Chessman>().CurrentY;
            go.GetComponent<Chessman>().Name = go.name;

            string json = JsonUtility.ToJson(go.GetComponent<Chessman>()); //turn the ChessmanClass into a JSON string
            GSObject piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
            LastPieces.Add(piece);
        }

        LastTiles LastBlue = new LastTiles();
        LastBlue.LastX = lastBlueX;
        LastBlue.LastY = lastBlueY;
        string lastbluejson = JsonUtility.ToJson(LastBlue);
        GSObject GSLastBlue = GSObject.FromJson(lastbluejson);

        LastTiles LastYellow = new LastTiles();
        LastYellow.LastX = lastYellowX;
        LastYellow.LastY = lastYellowY;
        string lastyeljson = JsonUtility.ToJson(LastYellow);
        GSObject GSLastYellow = GSObject.FromJson(lastyeljson);

        LastTiles LastRed = new LastTiles();
        LastRed.LastX = lastRedX;
        LastRed.LastY = lastRedY;
        string lastredjson = JsonUtility.ToJson(LastRed);
        GSObject GSLastRed = GSObject.FromJson(lastredjson);

        LastTiles LastGreen = new LastTiles();
        LastGreen.LastX = lastGreenX;
        LastGreen.LastY = lastGreenY;
        string lastgreenjson = JsonUtility.ToJson(LastGreen);
        GSObject GSLastGreen = GSObject.FromJson(lastgreenjson);
        #endregion
        #region      Win Conditions Variables
        WinConditions wc = new WinConditions();
        wc.playerT = playerTurn;
        wc.prevT = prevTurn;
        wc.Bout = blueout;
        wc.Yout = yelout;
        wc.Rout = redout;
        wc.Gout = greenout;
        string wcJson = JsonUtility.ToJson(wc);
        GSObject obj = GSObject.FromJson(wcJson);
        #endregion
        #region Collect Player info to figure out the turn order.

        List<GSData> GSPL = new List<GSData>();
        foreach (Player p in playerList)
        {
            string p5 = JsonUtility.ToJson(p);
            GSObject gsplayer = GSObject.FromJson(p5);
            GSPL.Add(gsplayer);
        }
        #endregion
        //SEND ALL THE DATA

        if (GameName == null)
        {
            GameName = "notset";
        }

        if (DidPlayerTurnExpire)
        {
            //make it so 'onlinePlayerTurn is set to the Expired Player's ID.  That way, when multiple live viewers are viewing, SmoothMoves has the correct 'LastPlayer' ID and they don't attempt to move a Null pices from the wrong team...
            if (AIAction != "action")
            {
                if (AIAction == "Blue")
                {
                    onlinePlayerTurn = playerList[0].id;
                }
                if (AIAction == "Yellow")
                {
                    onlinePlayerTurn = playerList[1].id;
                }
                if (AIAction == "Red")
                {
                    onlinePlayerTurn = playerList[2].id;
                }
                if (AIAction == "Green")
                {
                    onlinePlayerTurn = playerList[3].id;
                }

                AIAction = "action"; //reset the variable to teh default value.  when the AI is turned on, it will set it to one of the above.
            }
        }
        new LogChallengeEventRequest()
            .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            .SetEventKey("GameState") //this is for board data - there needs to be one for every list
            .SetEventAttribute("GameState", objectList)
            .SetEventAttribute("Enpass", enpassData)
            .SetEventAttribute("bpYel", bpyList)
            .SetEventAttribute("bpRed", bprList)
            .SetEventAttribute("bpGre", bpgList)
            .SetEventAttribute("ypBlue", ypbList)
            .SetEventAttribute("ypRed", yprList)
            .SetEventAttribute("ypGre", ypgList)
            .SetEventAttribute("rpBlue", rpbList)
            .SetEventAttribute("rpYel", rpyList)
            .SetEventAttribute("rpGre", rpgList)
            .SetEventAttribute("gpBlue", gpbList)
            .SetEventAttribute("gpYel", gpyList)
            .SetEventAttribute("gpRed", gprList)
            .SetEventAttribute("LastPieces", LastPieces)
            .SetEventAttribute("LastBlue", GSLastBlue)
            .SetEventAttribute("LastYellow", GSLastYellow)
            .SetEventAttribute("LastRed", GSLastRed)
            .SetEventAttribute("LastGreen", GSLastGreen)
            .SetEventAttribute("winCondition", obj)
            .SetEventAttribute("LastPlayer", onlinePlayerTurn)
            .SetEventAttribute("PlayerList", GSPL)
            .SetEventAttribute("Winner", GameWinner)
            .SetEventAttribute("WinnerName", WinnerName)
            .SetEventAttribute("GameName", GameName)
            .Send((response) =>
                {
                    if (!response.HasErrors)
                    {
                        Debug.Log("game data sent");

                        new GetChallengeRequest()
                          .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                          .Send((response2) =>
                          {
                              if (!response2.HasErrors)
                              {
                                  Challenge challengeData = Challenge.CreateFromJSON(response2.Challenge.JSONString); //deserialize the incoming data
                                  GameInfo GameInfo = GameInfo.CreateFromJSON(response2.Challenge.JSONString);
                                  onlinePlayerTurn = challengeData.nextPlayer;
                                  Debug.Log("onlineplayerturn from send data: " + onlinePlayerTurn);
                                  FindPlayerTurn();


                                  #region Get Turn Expire time for the countdown.
                                  if (GameInfo.scriptData.TurnLimit > 0)
                                  {
                                      if (GameInfo.scriptData.DateSeconds != null || GameInfo.scriptData.DateSeconds != 0 && GameInfo.scriptData.TurnLimit != 0)
                                      {
                                          System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                                          int CurrentTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
                                          int ElapsedTime = CurrentTime - GameInfo.scriptData.DateSeconds;

                                          TurnLimitCountDown = GameInfo.scriptData.TurnLimit - ElapsedTime;

                                          Debug.Log("TurnLimitCountDown: " + TurnLimitCountDown);
                                      }
                                      if (GameInfo.scriptData.TurnLimit == 0)
                                      {
                                          TurnLimitTextDisplay.text = "Turn Limit: Unlimited";
                                      }
                                  }
                                  #endregion


                                  if (DidPlayerTurnExpire)
                                  {
                                      isBlueAI = false;
                                      isYellowAI = false;
                                      isRedAI = false;
                                      isGreenAI = false;

                                      // CheckForMore();

                                      Debug.Log("checking for more from SendGameData(), no checkformore()");
                                      if (ExpiredColors != null && ExpiredColors.Count != 0)
                                      {
                                          Debug.Log("more found");
                                          //MoveExpiredPlayerWithAI();
                                          OnlineTurnExpired();
                                      }
                                      else
                                      {
                                          string skipto = "skipto";
                                          Debug.Log("more not found");
                                          new GetChallengeRequest()
                                             .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                                             .Send((response3) =>
                                             {
                                                 if (!response3.HasErrors)
                                                 {

                                                     GameInfo NewGameInfo = GameInfo.CreateFromJSON(response3.Challenge.JSONString);
                                                     Debug.Log("CheckForMore - looking at server stuff - START nextplayercolor = " + NewGameInfo.scriptData.NextPlayerColor);
                                                     onlinePlayerTurn = NewGameInfo.nextPlayer;

                                                     Debug.Log("Next Player Color: " + NewGameInfo.scriptData.NextPlayerColor);

                                                     if (NewGameInfo.scriptData.NextPlayerColor == "Blue")
                                                     {
                                                         skipto = playerList[0].id;
                                                     }
                                                     if (NewGameInfo.scriptData.NextPlayerColor == "Yellow")
                                                     {
                                                         skipto = playerList[1].id;
                                                     }
                                                     if (NewGameInfo.scriptData.NextPlayerColor == "Red")
                                                     {
                                                         skipto = playerList[2].id;
                                                     }
                                                     if (NewGameInfo.scriptData.NextPlayerColor == "Green")
                                                     {
                                                         skipto = playerList[3].id;
                                                     }


                                                     //since ExpireColors.Count is zero, remove it from the game's scriptData
                                                     new LogEventRequest().SetEventKey("RemoveScriptData")
                                                              .SetEventAttribute("cid", OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                                                              .SetEventAttribute("data", "PlayersToSkip")
                                                              .Send((response4) =>
                                                              {
                                                                  if (!response4.HasErrors)
                                                                  {
                                                                      Debug.Log("Delete Data Success");
                                                                      Debug.Log("Skipto: " + skipto);
                                                                      Debug.Log("Skipfrom: " + onlinePlayerTurn);
                                                                      #region      Win Conditions Variables
                                                                      WinConditions wc1 = new WinConditions();
                                                                      wc1.playerT = playerTurn;
                                                                      wc1.prevT = prevTurn;
                                                                      wc1.Bout = blueout;
                                                                      wc1.Yout = yelout;
                                                                      wc1.Rout = redout;
                                                                      wc1.Gout = greenout;
                                                                      string wc1Json = JsonUtility.ToJson(wc1);
                                                                      GSObject obj1 = GSObject.FromJson(wc1Json);
                                                                      #endregion
                                                                      #region Collect Player info to figure out the turn order.

                                                                      List<GSData> GSPL1 = new List<GSData>();
                                                                      foreach (Player p in playerList)
                                                                      {
                                                                          string p5 = JsonUtility.ToJson(p);
                                                                          GSObject gsplayer = GSObject.FromJson(p5);
                                                                          GSPL1.Add(gsplayer);
                                                                      }
                                                                      #endregion

                                                                      new LogEventRequest().SetEventKey("MakeRightTurn")
                                                                   .SetEventAttribute("cid", OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                                                                   .SetEventAttribute("wincon", obj1)
                                                                   .SetEventAttribute("players", GSPL1)
                                                                   .SetEventAttribute("skipto", skipto)
                                                                   .SetEventAttribute("skipfrom", onlinePlayerTurn)
                                                                   .Send((response5) =>
                                                                   {
                                                                       if (!response5.HasErrors)
                                                                       {
                                                                           Debug.Log("CheckForMore - trying to fix turn state...");
                                                                           new GetChallengeRequest()
                                                                                  .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                                                                                  .Send((response6) =>
                                                                                  {
                                                                                      if (!response6.HasErrors)
                                                                                      {
                                                                                          Challenge challengeD = Challenge.CreateFromJSON(response6.Challenge.JSONString); //deserialize the incoming data
                                                                                          onlinePlayerTurn = challengeD.nextPlayer;
                                                                                          DidPlayerTurnExpire = false;
                                                                                          FindPlayerTurn();

                                                                                          Debug.Log("CheckForMore() reset the player turn to normal. COMPLETE: " + onlinePlayerTurn);
                                                                                      }
                                                                                      else
                                                                                      {
                                                                                          Debug.Log("Repsonse 6 has errors when resetting the online turn");
                                                                                      }
                                                                                  });
                                                                       }
                                                                       else
                                                                       {
                                                                           Debug.Log("SkipToTurn() Messed Up");
                                                                       }
                                                                   });

                                                                  }
                                                                  else
                                                                  {
                                                                      Debug.Log("CheckForMore Delete Data Fucked Up");
                                                                  }
                                                              });
                                                 }
                                                 else
                                                 {
                                                     Debug.Log("CheckForMore Get Data Fucked Up");
                                                 }
                                             });
                                      }
                                  }
                                  TriggerOnlineUpdate = 0;
                              }
                          });
                    }
                    else
                    {
                        Debug.Log("sending game data messed up");
                        GameText.text = "Something went wrong...";
                    }
                });
    }
    public void FindPlayerTurn()
    {
        foreach (Player p in playerList)
        {
            if (p.id == onlinePlayerTurn)
            {
                GameText.text = "It is " + p.playerName + "'s turn!";



                if (playerTurn == 1)
                {
                    //       p.color = "Blue";
                    GameText.color = Color.blue;
                }
                if (playerTurn == 2)
                {
                    //     p.color = "Yellow";
                    GameText.color = Color.yellow;
                }
                if (playerTurn == 3)
                {
                    //   p.color = "Red";
                    GameText.color = Color.red;
                }
                if (playerTurn == 4)
                {
                    // p.color = "Green";
                    GameText.color = Color.green;
                }
            }
        }
        if (onlinePlayerTurn == myID)
        {
            GameText.text = "It's your turn!";
        }
    }
    private void BeforeBoardCollection()
    {
        BeforeBoard.Clear();
        foreach (var item in activeChessman)
        {
            item.GetComponent<Chessman>().SaveX = item.GetComponent<Chessman>().CurrentX;
            item.GetComponent<Chessman>().SaveY = item.GetComponent<Chessman>().CurrentY;
            item.GetComponent<Chessman>().Name = item.name;

            string json = JsonUtility.ToJson(item.GetComponent<Chessman>()); //turn the ChessmanClass into a JSON string
            GSObject piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
            BeforeBoard.Add(piece);
        }
    }
    public void GameLose(string _message)
    {
        Debug.Log("LOST HANDLER");
        GameObject o = Instantiate(PopUp, spawner.transform);
        o.GetComponentInChildren<Text>().text = _message + " has won the battle!";
        menuDisplayed = true;
    }
    public void GameWin()
    {
        Debug.Log("WIN HANDLER");
        GameObject o = Instantiate(PopUp, spawner.transform);
        o.GetComponentInChildren<Text>().text = "Victory is yours!";
        menuDisplayed = true;
        if (IsDraw)
        {
            o.GetComponentInChildren<Text>().text = "The remaining players have voted to end the game in a draw.";
        }
    }
    public void AIMove()
    {
        int[,] move = new int[2, 2];
        if (!isOnline)
        {
            if (playerTurn == 1 && isBlueAI && !isAIMove)
            {
                Debug.Log("Blue AI Moved");
                move = BlueAI.getMove(Chessmans);
                SelectChessman(move[0, 0], move[0, 1]);
                MoveChessman(move[1, 0], move[1, 1]); //MoveChessman calls Move() which sets 'moved' to true.  its possible that called it a second time in the next line is what's causing it to skip the next turn.
                isAIMove = true;
                //          moved = true;
            }
            else if (playerTurn == 2 && isYellowAI && !isAIMove)
            {

                move = YellowAI.getMove(Chessmans);
                SelectChessman(move[0, 0], move[0, 1]);
                MoveChessman(move[1, 0], move[1, 1]);
                isAIMove = true;
                //        moved = true;
                Debug.Log("Yellow AI Moved");
            }
            else if (playerTurn == 3 && isRedAI && !isAIMove)
            {

                move = RedAI.getMove(Chessmans);
                SelectChessman(move[0, 0], move[0, 1]);
                MoveChessman(move[1, 0], move[1, 1]);
                isAIMove = true;
                //      moved = true;
                Debug.Log("Red AI Moved");
            }
            else if (playerTurn == 4 && isGreenAI && !isAIMove)
            {

                move = GreenAI.getMove(Chessmans);
                SelectChessman(move[0, 0], move[0, 1]);
                MoveChessman(move[1, 0], move[1, 1]);
                isAIMove = true;
                //    moved = true;
                Debug.Log("Green AI Moved");
            }
        }


        //for when a player's turn expires.
        if (isOnline && DidPlayerTurnExpire)
        {
            Debug.Log("Trying to move online AI");
            if (isBlueAI && !isAIMove)
            {
                move = BlueAI.getMove(Chessmans);
                SelectChessman(move[0, 0], move[0, 1]);
                MoveChessman(move[1, 0], move[1, 1]);
                isAIMove = true;
            }
            if (isYellowAI && !isAIMove)
            {
                move = YellowAI.getMove(Chessmans);
                SelectChessman(move[0, 0], move[0, 1]);
                MoveChessman(move[1, 0], move[1, 1]);
                isAIMove = true;
            }
            if (isRedAI && !isAIMove)
            {
                move = RedAI.getMove(Chessmans);
                SelectChessman(move[0, 0], move[0, 1]);
                MoveChessman(move[1, 0], move[1, 1]);
                isAIMove = true;
            }
            if (isGreenAI && !isAIMove)
            {
                move = GreenAI.getMove(Chessmans);
                SelectChessman(move[0, 0], move[0, 1]);
                MoveChessman(move[1, 0], move[1, 1]);
                isAIMove = true;
            }
        }
    }
    public void PlayerSurrender()
    {
        string color = null;

        if (myID == onlinePlayerTurn)
        {
            if (playerTurn == 1)
            {
                blueout = true;

                if (!yelout && redout && greenout)
                {
                    GameWinner = playerList[1].id;
                }
                if (yelout && !redout && greenout)
                {
                    GameWinner = playerList[2].id;
                }
                if (yelout && redout && !greenout)
                {
                    GameWinner = playerList[3].id;
                }
            }
            if (playerTurn == 2)
            {
                yelout = true;

                if (!blueout && redout && greenout)
                {
                    GameWinner = playerList[0].id;
                }
                if (blueout && !redout && greenout)
                {
                    GameWinner = playerList[2].id;
                }
                if (blueout && redout && !greenout)
                {
                    GameWinner = playerList[3].id;
                }
            }
            if (playerTurn == 3)
            {
                redout = true;
                if (!yelout && blueout && greenout)
                {
                    GameWinner = playerList[1].id;
                }
                if (yelout && !blueout && greenout)
                {
                    GameWinner = playerList[0].id;
                }
                if (yelout && blueout && !greenout)
                {
                    GameWinner = playerList[3].id;
                }
            }
            if (playerTurn == 4)
            {
                greenout = true;

                if (!yelout && redout && blueout)
                {
                    GameWinner = playerList[1].id;
                }
                if (yelout && !redout && blueout)
                {
                    GameWinner = playerList[2].id;
                }
                if (yelout && redout && !blueout)
                {
                    GameWinner = playerList[0].id;
                }
            }

            Lose();
            turnLoop();
            turnControl();
            SendGameDataAndAdvanceTurn();

            //Quit to Main Menu
            if (GameObject.Find("ListItem_Game(Clone)"))
            {
                SceneManager.LoadScene("MainMenu");
                Destroy(GameObject.Find("ListItem_Game(Clone)").gameObject);
            }
            else
            {
                SceneManager.LoadScene("SplashMenu");
            }
        }
        else
        {
            foreach (var player in playerList)
            {
                if (player.id == myID)
                {
                    color = player.color;
                }
            }
            #region      Win Conditions Variables
            WinConditions wc = new WinConditions();
            wc.playerT = playerTurn;
            wc.prevT = prevTurn;
            wc.Bout = blueout;
            wc.Yout = yelout;
            wc.Rout = redout;
            wc.Gout = greenout;
            string wcJson = JsonUtility.ToJson(wc);
            GSObject obj = GSObject.FromJson(wcJson);
            #endregion
            #region Collect Player info to figure out the turn order.

            List<GSData> GSPL = new List<GSData>();
            foreach (Player p in playerList)
            {
                string p5 = JsonUtility.ToJson(p);
                GSObject gsplayer = GSObject.FromJson(p5);
                GSPL.Add(gsplayer);
            }
            #endregion
            new LogEventRequest().SetEventKey("PlayerQuit")
                .SetEventAttribute("color", color)
                .SetEventAttribute("cid", OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                .SetEventAttribute("winCondition", obj)
                .SetEventAttribute("PlayerList", GSPL)
                .Send((response) =>
                {
                    if (!response.HasErrors)
                    {
                        //Quit to Main Menu
                        if (GameObject.Find("ListItem_Game(Clone)"))
                        {
                            SceneManager.LoadScene("MainMenu");
                            Destroy(GameObject.Find("ListItem_Game(Clone)").gameObject);
                        }
                        else
                        {
                            SceneManager.LoadScene("SplashMenu");
                        }
                    }
                });
        }
    }

    public void SmoothMove()
    {
        // Called in an online game to see an opponent's piece move smoothly to where they should be.
        new GetChallengeRequest()
                      .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                      .Send((response) =>
                      {
                          if (!response.HasErrors)
                          {
                              Challenge challengeData = Challenge.CreateFromJSON(response.Challenge.JSONString); //deserialize the incoming data
                              GameInfo GameInfo = GameInfo.CreateFromJSON(response.Challenge.JSONString); //i tried again....so...leave me alone.
                              GameName = GameInfo.challengeMessage;
                              onlinePlayerTurn = challengeData.nextPlayer;
                              TurnLimitCountDown = GameInfo.scriptData.TurnLimit;
                              bool DidLastTurnQuit = false;
                              Player LastPlayer = new Player();
                              Player p1 = new Player();
                              Player p2 = new Player();
                              Player p3 = new Player();
                              Player p4 = new Player();

                              string myColor = "color";
                              string turnColor = "turn";

                              #region Get Turn Expire time for the countdown.
                              if (GameInfo.scriptData.TurnLimit > 0)
                              {
                                  if (GameInfo.scriptData.DateSeconds != null || GameInfo.scriptData.DateSeconds != 0 && GameInfo.scriptData.TurnLimit != 0)
                                  {
                                      System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                                      int CurrentTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
                                      int ElapsedTime = CurrentTime - GameInfo.scriptData.DateSeconds;

                                      TurnLimitCountDown = GameInfo.scriptData.TurnLimit - ElapsedTime;

                                      Debug.Log("TurnLimitCountDown: " + TurnLimitCountDown);
                                  }
                                  if (GameInfo.scriptData.TurnLimit == 0)
                                  {
                                      TurnLimitTextDisplay.text = "Turn Limit: Unlimited";
                                  }
                              }
                              #endregion

                              if (response.Challenge.ScriptData.GetGSData("WinConditions") != null)
                              {
                                  #region ASSIGN WINCONDITION VARIABLES
                                  Debug.Log("setting wincon");
                                  GSData wincon = response.Challenge.ScriptData.GetGSData("WinConditions");
                                  WinConditions wc = WinConditions.CreateFromJSON(wincon.JSON);
                                  playerTurn = wc.playerT;
                                  prevTurn = wc.prevT;
                                  blueout = wc.Bout;
                                  yelout = wc.Yout;
                                  redout = wc.Rout;
                                  greenout = wc.Gout;
                                  LastOnlinePlayerTurn = response.Challenge.ScriptData.GetString("LastPlayer");
                                  #endregion
                                  #region FIND MY COLOR - and find out which color's turn it is - and figure out the player who went last so we can find their color, so we know which 'last piece' to move.
                                  foreach (Player p in playerList)
                                  {
                                      if (p.id == myID)
                                      {
                                          myColor = p.color;
                                      }
                                      if (p.color == "Blue")
                                      {
                                          p1 = p;
                                      }
                                      if (p.color == "Yellow")
                                      {
                                          p2 = p;
                                      }
                                      if (p.color == "Red")
                                      {
                                          p3 = p;
                                      }
                                      if (p.color == "Green")
                                      {
                                          p4 = p;
                                      }

                                      if (p.id == LastOnlinePlayerTurn)
                                      {
                                          LastPlayer = p;
                                      }
                                      if (p.id == onlinePlayerTurn)
                                      {
                                          turnColor = p.color;
                                      }
                                  }

                                  //see if the previous player is out and needs skipped - we wont try to simulate their turn.
                                  if (turnColor == "Blue")
                                  {
                                      //if the last player was't p4, then a player was skipped.
                                      if (greenout)
                                      {
                                          DidLastTurnQuit = true;
                                      }
                                  }
                                  if (turnColor == "Yellow")
                                  {
                                      if (blueout)
                                      {
                                          DidLastTurnQuit = true;
                                      }
                                  }
                                  if (turnColor == "Red")
                                  {
                                      if (yelout)
                                      {
                                          DidLastTurnQuit = true;
                                      }
                                  }
                                  if (turnColor == "Green")
                                  {
                                      if (redout)
                                      {
                                          DidLastTurnQuit = true;
                                      }
                                  }

                                  if (DidLastTurnQuit)
                                  {
                                      Debug.Log("SmootMoves was Called even thoughthe last player quit.");
                                  }
                                  #endregion
                              }

                              #region TRACK EACH TEAMS LAST PIECE AND LAST TILE
                              List<GSData> LastPieces = response.Challenge.ScriptData.GetGSDataList("LastPieces");
                              if (LastPieces != null)
                              {
                                  for (int i = 0; i < LastPieces.Count; i++)
                                  {
                                      PieceData piece = PieceData.CreateFromJSON(LastPieces[i].JSON);
                                      if (piece.isBlue) { resetManBlue = Chessmans[piece.SaveX, piece.SaveY]; }
                                      if (piece.isYellow) { resetManYellow = Chessmans[piece.SaveX, piece.SaveY]; }
                                      if (piece.isRed) { resetManRed = Chessmans[piece.SaveX, piece.SaveY]; }
                                      if (piece.isGreen) { resetManGreen = Chessmans[piece.SaveX, piece.SaveY]; }
                                  }
                              }

                              GSData lastBlueTiles = response.Challenge.ScriptData.GetGSData("LastBlue");
                              if (lastBlueTiles != null)
                              {
                                  LastTiles b = LastTiles.CreateFromJSON(lastBlueTiles.JSON);
                                  lastBlueX = b.LastX;
                                  lastBlueY = b.LastY;
                              }

                              GSData lastYellowTiles = response.Challenge.ScriptData.GetGSData("LastYellow");
                              if (lastYellowTiles != null)
                              {
                                  LastTiles y = LastTiles.CreateFromJSON(lastYellowTiles.JSON);
                                  lastYellowX = y.LastX;
                                  lastYellowY = y.LastY;
                              }

                              GSData lastRedTiles = response.Challenge.ScriptData.GetGSData("LastRed");
                              if (lastRedTiles != null)
                              {
                                  LastTiles r = LastTiles.CreateFromJSON(lastRedTiles.JSON);
                                  lastRedX = r.LastX;
                                  lastRedY = r.LastY;
                              }

                              GSData lastGreenTiles = response.Challenge.ScriptData.GetGSData("LastGreen");
                              if (lastGreenTiles != null)
                              {
                                  LastTiles g = LastTiles.CreateFromJSON(lastGreenTiles.JSON);
                                  lastGreenX = g.LastX;
                                  lastGreenY = g.LastY;
                              }
                              #endregion


                              if (LastOnlinePlayerTurn != myID && !DidLastTurnQuit)
                              {
                                  Debug.Log("Last Player = " + LastOnlinePlayerTurn);
                                  #region RESET EMPASSANT MOVES
                                  if (playerTurn == 1)
                                  {
                                      EnPassantMoveBlue[0] = -1;
                                      EnPassantMoveBlue[1] = -1;
                                  }
                                  if (playerTurn == 2)
                                  {
                                      EnPassantMoveYellow[0] = -1;
                                      EnPassantMoveYellow[1] = -1;
                                  }
                                  if (playerTurn == 3)
                                  {
                                      EnPassantMoveRed[0] = -1;
                                      EnPassantMoveRed[1] = -1;
                                  }
                                  if (playerTurn == 4)
                                  {
                                      EnPassantMoveGreen[0] = -1;
                                      EnPassantMoveGreen[1] = -1;
                                  }
                                  #endregion
                                  #region Smooth Movement
                                  if (!DidLastTurnQuit) //if the last turn didn't quit, then there's a move to simulate.
                                  {
                                      //MOVE MY BOARD PIECE TO THE SERVERS LCOATION - DELETE THE PIECE THAT MAY BE IN THE WAY
                                      if (LastPlayer.color == "Blue")
                                      {
                                          List<GSData> TheseLastPieces = response.Challenge.ScriptData.GetGSDataList("LastPieces");
                                          GSData BlueTiles = response.Challenge.ScriptData.GetGSData("LastBlue");

                                          LastTiles b = LastTiles.CreateFromJSON(BlueTiles.JSON);
                                          automoveChessman = Chessmans[b.LastX, b.LastY];
                                          autox = b.LastX;
                                          autoy = b.LastY;
                                          //               Debug.Log("b.LastX: " + b.LastX + ", b.LastY: " + b.LastY);
                                          foreach (GSData data in TheseLastPieces)
                                          {
                                              PieceData p = PieceData.CreateFromJSON(data.JSON);
                                              if (p.isBlue)
                                              {
                                                  if (Chessmans[p.SaveX, p.SaveY] != null)
                                                  {
                                                      activeChessman.Remove(Chessmans[p.SaveX, p.SaveY].gameObject);
                                                      Destroy(Chessmans[p.SaveX, p.SaveY].gameObject);
                                                  }
                                                  previous = automoveChessman; //grab this piece so we can reset the board by one turn to enforce check
                                                  prevDir = automoveChessman.pawnDirection;
                                                  prevCheck = automoveChessman.isChecked;
                                                  prevMated = automoveChessman.isMated;
                                                  StartCoroutine(AutoMove(automoveChessman, GetTileCenter(p.SaveX, p.SaveY), 5, p.SaveX, p.SaveY));
                                                  //    Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY] = null; //set the current position to null because we're going to move it.
                                                  //      automoveChessman.SetPosition(p.SaveX, p.SaveY);
                                                  //        Chessmans[p.SaveX, p.SaveY] = automoveChessman; //make the intended peice BE the selected piece
                                                  //          automoveChessman = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY];

                                              }
                                          }
                                      }
                                      if (LastPlayer.color == "Yellow")
                                      {
                                          List<GSData> TheseLastPieces = response.Challenge.ScriptData.GetGSDataList("LastPieces");
                                          GSData YellowTiles = response.Challenge.ScriptData.GetGSData("LastYellow");

                                          LastTiles y = LastTiles.CreateFromJSON(YellowTiles.JSON);
                                          automoveChessman = Chessmans[y.LastX, y.LastY];
                                          autox = y.LastX;
                                          autoy = y.LastY;
                                          foreach (GSData data in TheseLastPieces)
                                          {
                                              PieceData p = PieceData.CreateFromJSON(data.JSON);
                                              if (p.isYellow)
                                              {
                                                  if (Chessmans[p.SaveX, p.SaveY] != null)
                                                  {
                                                      activeChessman.Remove(Chessmans[p.SaveX, p.SaveY].gameObject);
                                                      Destroy(Chessmans[p.SaveX, p.SaveY].gameObject);
                                                  }
                                                  previous = automoveChessman; //grab this piece so we can reset the board by one turn to enforce check
                                                  prevDir = automoveChessman.pawnDirection;
                                                  prevCheck = automoveChessman.isChecked;
                                                  prevMated = automoveChessman.isMated;
                                                  StartCoroutine(AutoMove(automoveChessman, GetTileCenter(p.SaveX, p.SaveY), 5, p.SaveX, p.SaveY));
                                                  //    Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY] = null; //set the current position to null because we're going to move it.
                                                  //      automoveChessman.SetPosition(p.SaveX, p.SaveY);
                                                  //        Chessmans[p.SaveX, p.SaveY] = automoveChessman; //make the intended peice BE the selected piece
                                                  //          automoveChessman = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY];
                                              }
                                          }
                                      }
                                      if (LastPlayer.color == "Red")
                                      {
                                          List<GSData> TheseLastPieces = response.Challenge.ScriptData.GetGSDataList("LastPieces");
                                          GSData RedTiles = response.Challenge.ScriptData.GetGSData("LastRed");

                                          LastTiles r = LastTiles.CreateFromJSON(RedTiles.JSON);
                                          automoveChessman = Chessmans[r.LastX, r.LastY];
                                          autox = r.LastX;
                                          autoy = r.LastY;
                                          foreach (GSData data in TheseLastPieces)
                                          {
                                              PieceData p = PieceData.CreateFromJSON(data.JSON);
                                              if (p.isRed)
                                              {
                                                  if (Chessmans[p.SaveX, p.SaveY] != null)
                                                  {
                                                      activeChessman.Remove(Chessmans[p.SaveX, p.SaveY].gameObject);
                                                      Destroy(Chessmans[p.SaveX, p.SaveY].gameObject);
                                                  }
                                                  previous = automoveChessman; //grab this piece so we can reset the board by one turn to enforce check
                                                  prevDir = automoveChessman.pawnDirection;
                                                  prevCheck = automoveChessman.isChecked;
                                                  prevMated = automoveChessman.isMated;
                                                  StartCoroutine(AutoMove(automoveChessman, GetTileCenter(p.SaveX, p.SaveY), 5, p.SaveX, p.SaveY));
                                                  //    Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY] = null; //set the current position to null because we're going to move it.
                                                  //      automoveChessman.SetPosition(p.SaveX, p.SaveY);
                                                  //        Chessmans[p.SaveX, p.SaveY] = automoveChessman; //make the intended peice BE the selected piece
                                                  //          automoveChessman = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY];
                                              }
                                          }
                                      }
                                      if (LastPlayer.color == "Green")
                                      {
                                          List<GSData> TheseLastPieces = response.Challenge.ScriptData.GetGSDataList("LastPieces");
                                          GSData GreenTiles = response.Challenge.ScriptData.GetGSData("LastGreen");

                                          LastTiles g = LastTiles.CreateFromJSON(GreenTiles.JSON);
                                          automoveChessman = Chessmans[g.LastX, g.LastY];
                                          autox = g.LastX;
                                          autoy = g.LastY;
                                          foreach (GSData data in TheseLastPieces)
                                          {
                                              PieceData p = PieceData.CreateFromJSON(data.JSON);
                                              if (p.isGreen)
                                              {
                                                  if (Chessmans[p.SaveX, p.SaveY] != null)
                                                  {
                                                      activeChessman.Remove(Chessmans[p.SaveX, p.SaveY].gameObject);
                                                      Destroy(Chessmans[p.SaveX, p.SaveY].gameObject);
                                                  }
                                                  previous = automoveChessman; //grab this piece so we can reset the board by one turn to enforce check
                                                  prevDir = automoveChessman.pawnDirection;
                                                  prevCheck = automoveChessman.isChecked;
                                                  prevMated = automoveChessman.isMated;
                                                  StartCoroutine(AutoMove(automoveChessman, GetTileCenter(p.SaveX, p.SaveY), 5, p.SaveX, p.SaveY));
                                                  //    Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY] = null; //set the current position to null because we're going to move it.
                                                  //      automoveChessman.SetPosition(p.SaveX, p.SaveY);
                                                  //        Chessmans[p.SaveX, p.SaveY] = automoveChessman; //make the intended peice BE the selected piece
                                                  //          automoveChessman = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY];
                                              }
                                          }
                                      }
                                      // took out castle logic from here

                                      //make sure we indicate that a piece moved from its starting position - since pawns take in considerably different logic for this, we leave them alone.                       
                                      //             if (automoveChessman.name != "pawn(Clone)")
                                      //               {
                                      //                     Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY].pawnDirection = 1; //this is a special consideration for Casetling logic, as well as a pawns direction they're faceing.
                                      //                   }

                                  }
                                  #endregion
                              }
                              if (LastOnlinePlayerTurn != myID && DidLastTurnQuit)
                              {
                                  FindPlayerTurn();
                                  Lose();
                                  Win();
                              }

                              TriggerOnlineUpdate = 0;
                          }
                      });

    }
    IEnumerator AutoMove(Chessman selected, Vector3 destination, float speed, int newx, int newy)
    {
        while (selected.transform.position != destination)
        {
            selected.transform.position = Vector3.MoveTowards(selected.transform.position, destination, speed * Time.deltaTime);
            speed++;
            yield return null;
        }
        Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY] = null; //set the current position to null because we're going to move it.
        automoveChessman.SetPosition(newx, newy);
        Chessmans[newx, newy] = automoveChessman; //make the intended peice BE the selected piece
        automoveChessman = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY];

        SmoothMovesFollowThrough();
    }
    public void OnlinePlayerSkipped()
    {
        new GetChallengeRequest()
              .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
              .Send((response) =>
              {
                  if (!response.HasErrors)
                  {
                      Challenge challengeData = Challenge.CreateFromJSON(response.Challenge.JSONString); //deserialize the incoming data
                      onlinePlayerTurn = challengeData.nextPlayer;
                      #region ASSIGN WINCONDITION VARIABLES
                      Debug.Log("setting wincon");
                      GSData wincon = response.Challenge.ScriptData.GetGSData("WinConditions");
                      WinConditions wc = WinConditions.CreateFromJSON(wincon.JSON);
                      playerTurn = wc.playerT;
                      prevTurn = wc.prevT;
                      blueout = wc.Bout;
                      yelout = wc.Yout;
                      redout = wc.Rout;
                      greenout = wc.Gout;
                      LastOnlinePlayerTurn = response.Challenge.ScriptData.GetString("LastPlayer");
                      #endregion
                      FindPlayerTurn();
                      TriggerOnlineUpdate = 0;
                  }
              });
        ReconcileBoard("OnlinePlayerSkipped()");
    }
    public void ReconcileBoard(string CalledFrom)
    {
        Debug.Log("Reconcile Called From " + CalledFrom);
        new GetChallengeRequest()
            .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            .Send((response) =>
        {
            #region BOARDRECONCILE 
            //Board reconcile
            List<GSData> GameStateFromServer = response.Challenge.ScriptData.GetGSDataList("gameState");
            List<PieceData> ServerData = new List<PieceData>();

            if (GameStateFromServer != null)
            {
                foreach (GSData item in GameStateFromServer)
                {
                    PieceData piece = PieceData.CreateFromJSON(item.JSON);

                    if (Chessmans[piece.SaveX, piece.SaveY] == null) //the server has a piece on the board that my local copy does not
                    {
                        SpawnChessman(piece.index, piece.SaveX, piece.SaveY);
                        Chessmans[piece.SaveX, piece.SaveY].isChecked = piece.isChecked;
                        Chessmans[piece.SaveX, piece.SaveY].isMated = piece.isMated;
                        Chessmans[piece.SaveX, piece.SaveY].pawnDirection = piece.pawnDirection;
                        Debug.Log("Reconcile 1: There was a piece on the Server that was not in the game.  A " + piece.Name + " was spawned at (" + piece.SaveX + ", " + piece.SaveY + ")");
                    }
                    else if (piece.isBlue && Chessmans[piece.SaveX, piece.SaveY].getColor() != 1 ||
                                piece.isYellow && Chessmans[piece.SaveX, piece.SaveY].getColor() != 2 ||
                                piece.isRed && Chessmans[piece.SaveX, piece.SaveY].getColor() != 3 ||
                                piece.isGreen && Chessmans[piece.SaveX, piece.SaveY].getColor() != 4) //if there are two pieces in the same location of different color.
                    {
                        Debug.Log("Reconcile 2: There was a piece on the Server that was in the same location as a piece in the game, and they're different (OFF TEAM) at (" + piece.SaveX + ", " + piece.SaveY + ").");

                        //Destory the Local Object
                        activeChessman.Remove(Chessmans[piece.SaveX, piece.SaveY].gameObject);
                        Destroy(Chessmans[piece.SaveX, piece.SaveY].gameObject);
                        //Spawn the Server Object
                        SpawnChessman(piece.index, piece.SaveX, piece.SaveY);
                        Chessmans[piece.SaveX, piece.SaveY].isChecked = piece.isChecked;
                        Chessmans[piece.SaveX, piece.SaveY].isMated = piece.isMated;
                        Chessmans[piece.SaveX, piece.SaveY].pawnDirection = piece.pawnDirection;
                    }
                    else if (piece.isBlue && Chessmans[piece.SaveX, piece.SaveY].getColor() == 1 ||
                            piece.isYellow && Chessmans[piece.SaveX, piece.SaveY].getColor() == 2 ||
                            piece.isRed && Chessmans[piece.SaveX, piece.SaveY].getColor() == 3 ||
                             piece.isGreen && Chessmans[piece.SaveX, piece.SaveY].getColor() == 4) //if there are aligning pieces between the server and the game...make sure they're indeed identical.
                    {
                        if (piece.Name != Chessmans[piece.SaveX, piece.SaveY].name) //then the pieces don't match - this indicates a prisoner exchange may have occured. 
                        {
                            Debug.Log("Reconcile 3: There was a piece on the Server that was in the same location as a piece in the game, and they're different (SAME TEAM) at (" + piece.SaveX + ", " + piece.SaveY + ").");

                            //Destory the Local Object
                            activeChessman.Remove(Chessmans[piece.SaveX, piece.SaveY].gameObject);
                            Destroy(Chessmans[piece.SaveX, piece.SaveY].gameObject);
                            //Spawn the Server Object
                            SpawnChessman(piece.index, piece.SaveX, piece.SaveY);
                            Chessmans[piece.SaveX, piece.SaveY].isChecked = piece.isChecked;
                            Chessmans[piece.SaveX, piece.SaveY].isMated = piece.isMated;
                            Chessmans[piece.SaveX, piece.SaveY].pawnDirection = piece.pawnDirection;
                        }
                    }

                    ServerData.Add(piece);
                }
            }

            if (ServerData.Count > 0) //do a final compare - the lists should be identical.
            {
                int wasFound = 0;

                foreach (GameObject LocalItem in activeChessman)
                {

                    foreach (PieceData ServerItem in ServerData) //compare my local piece to the pieces in the server, one at a time.
                    {
                        //we dont need to evaluate futher because we already checked if our matchign pieces matched names too above.
                        if (Chessmans[LocalItem.GetComponent<Chessman>().CurrentX, LocalItem.GetComponent<Chessman>().CurrentY].gameObject == Chessmans[ServerItem.SaveX, ServerItem.SaveY].gameObject)
                        {
                            wasFound++;
                        }
                    }
                }

                if (wasFound != activeChessman.Count && wasFound != ServerData.Count) //if, after the evaluation above, the list counts dont match than the board states are fubar.
                {
                    Debug.Log("Final Reconcile Check Failed. The board state is not to be trusted.");
                }
            }

            #endregion
        });
    }
    public void SmoothMovesFollowThrough()
    {
        new GetChallengeRequest()
              .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
              .Send((response) =>
              {
                  if (!response.HasErrors)
                  {
                      Challenge challengeData = Challenge.CreateFromJSON(response.Challenge.JSONString); //deserialize the incoming data
                      GameInfo GameInfo = GameInfo.CreateFromJSON(response.Challenge.JSONString); //i tried again....so...leave me alone.

                      #region ENPASSANT DESTROY
                      if (automoveChessman.CurrentX == EnPassantMoveBlue[0] && automoveChessman.CurrentY == EnPassantMoveBlue[1])
                      {
                          if (automoveChessman.CurrentX == 0 && automoveChessman.CurrentY == 5 || automoveChessman.CurrentX == 1 && automoveChessman.CurrentY == 4 || automoveChessman.CurrentX == 2 && automoveChessman.CurrentY == 3 || automoveChessman.CurrentX == 3 && automoveChessman.CurrentY == 2 || automoveChessman.CurrentX == 4 && automoveChessman.CurrentY == 1 || automoveChessman.CurrentX == 5 && automoveChessman.CurrentY == 0 || automoveChessman.CurrentX == 1 && automoveChessman.CurrentY == 3 || automoveChessman.CurrentX == 2 && automoveChessman.CurrentY == 2 || automoveChessman.CurrentX == 3 && automoveChessman.CurrentY == 1)
                          {
                              Chessman c = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY + 1]; // set the chessman to be deleted if they're on similar orientations

                              if (c == null)
                              {
                                  c = Chessmans[automoveChessman.CurrentX + 1, automoveChessman.CurrentY];
                              }

                              if (autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY - 1 || //up left
                                  autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY + 1 || // down left
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY - 1 || // up right
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY + 1) // down right
                              {
                                  activeChessman.Remove(c.gameObject);
                                  Destroy(c.gameObject);
                              }
                              else
                              {
                                  EnPassantMoveBlue[0] = -1;
                                  EnPassantMoveBlue[1] = -1;
                              }
                          }
                      }
                      if (automoveChessman.CurrentX == EnPassantMoveYellow[0] && automoveChessman.CurrentY == EnPassantMoveYellow[1])
                      {
                          if (automoveChessman.CurrentX == 0 && automoveChessman.CurrentY == 6 ||
                          automoveChessman.CurrentX == 1 && automoveChessman.CurrentY == 7 ||
                          automoveChessman.CurrentX == 2 && automoveChessman.CurrentY == 8 ||
                          automoveChessman.CurrentX == 3 && automoveChessman.CurrentY == 9 ||
                          automoveChessman.CurrentX == 4 && automoveChessman.CurrentY == 10 ||
                          automoveChessman.CurrentX == 5 && automoveChessman.CurrentY == 11 ||
                          automoveChessman.CurrentX == 1 && automoveChessman.CurrentY == 8 ||
                          automoveChessman.CurrentX == 2 && automoveChessman.CurrentY == 9 ||
                          automoveChessman.CurrentX == 3 && automoveChessman.CurrentY == 10)
                          {
                              Chessman c = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY - 1]; // set the chessman to be deleted if they're on similar orientations

                              if (c == null)
                              {
                                  c = Chessmans[automoveChessman.CurrentX + 1, automoveChessman.CurrentY];
                              }
                              if (autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY - 1 || //up left
                                  autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY + 1 || // down left
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY - 1 || // up right
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY + 1) // down right
                              {
                                  activeChessman.Remove(c.gameObject);
                                  Destroy(c.gameObject);
                              }
                              else
                              {
                                  EnPassantMoveYellow[0] = -1;
                                  EnPassantMoveYellow[1] = -1;
                              }
                          }
                      }
                      if (automoveChessman.CurrentX == EnPassantMoveRed[0] && automoveChessman.CurrentY == EnPassantMoveRed[1])
                      {
                          if (automoveChessman.CurrentX == 6 && automoveChessman.CurrentY == 11 ||
                          automoveChessman.CurrentX == 7 && automoveChessman.CurrentY == 10 ||
                          automoveChessman.CurrentX == 8 && automoveChessman.CurrentY == 9 ||
                          automoveChessman.CurrentX == 9 && automoveChessman.CurrentY == 8 ||
                          automoveChessman.CurrentX == 10 && automoveChessman.CurrentY == 7 ||
                          automoveChessman.CurrentX == 11 && automoveChessman.CurrentY == 6 ||
                          automoveChessman.CurrentX == 10 && automoveChessman.CurrentY == 8 ||
                          automoveChessman.CurrentX == 9 && automoveChessman.CurrentY == 9 ||
                          automoveChessman.CurrentX == 8 && automoveChessman.CurrentY == 10)
                          {
                              Chessman c = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY - 1]; // set the chessman to be deleted if they're on similar orientations
                              if (c == null)
                              {
                                  c = Chessmans[automoveChessman.CurrentX - 1, automoveChessman.CurrentY]; //if they're not on similar orietnations, and its to the right
                              }
                              if (autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY - 1 || //up left
                                  autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY + 1 || // down left
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY - 1 || // up right
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY + 1) // down right
                              {
                                  activeChessman.Remove(c.gameObject);
                                  Destroy(c.gameObject);
                              }
                              else
                              {
                                  EnPassantMoveRed[0] = -1;
                                  EnPassantMoveRed[1] = -1;
                              }
                          }
                      }
                      if (automoveChessman.CurrentX == EnPassantMoveGreen[0] && automoveChessman.CurrentY == EnPassantMoveGreen[1])
                      {
                          if (automoveChessman.CurrentX == 6 && automoveChessman.CurrentY == 0 ||
                          automoveChessman.CurrentX == 7 && automoveChessman.CurrentY == 1 ||
                          automoveChessman.CurrentX == 8 && automoveChessman.CurrentY == 2 ||
                          automoveChessman.CurrentX == 9 && automoveChessman.CurrentY == 3 ||
                          automoveChessman.CurrentX == 10 && automoveChessman.CurrentY == 4 ||
                          automoveChessman.CurrentX == 11 && automoveChessman.CurrentY == 5 ||
                          automoveChessman.CurrentX == 8 && automoveChessman.CurrentY == 1 ||
                          automoveChessman.CurrentX == 9 && automoveChessman.CurrentY == 2 ||
                          automoveChessman.CurrentX == 10 && automoveChessman.CurrentY == 3)
                          {
                              Chessman c = Chessmans[automoveChessman.CurrentX, automoveChessman.CurrentY + 1]; // set the chessman to be deleted if they're on similar orientations
                              if (c == null)
                              {
                                  c = Chessmans[automoveChessman.CurrentX - 1, automoveChessman.CurrentY];
                              }
                              if (autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY - 1 || //up left
                                  autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY + 1 || // down left
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY - 1 || // up right
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY + 1) // down right
                              {
                                  activeChessman.Remove(c.gameObject);
                                  Destroy(c.gameObject);
                              }
                              else
                              {
                                  EnPassantMoveGreen[0] = -1;
                                  EnPassantMoveGreen[1] = -1;
                              }
                          }
                      }
                      #endregion
                      #region PAWN COMMITMENT AND ENPASSANT
                      if (automoveChessman.name == "pawn(Clone)" && automoveChessman.pawnDirection == 0 ||
                      automoveChessman.name == "pawn(Clone)" && automoveChessman.pawnDirection == -1) //if a pawn moved, that did not declare a direction
                      {
                          if (automoveChessman.isBlue) //if they're a blue pawn with no direction 
                          {
                              //and they just moved left
                              if (autoy < automoveChessman.CurrentY && autox == automoveChessman.CurrentX ||
                                 autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY - 1)
                              {
                                  automoveChessman.pawnDirection = 1; //commit that pawn left
                              }
                              //and they just moved right
                              if (autox < automoveChessman.CurrentX && autoy == automoveChessman.CurrentY ||
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY + 1)
                              {
                                  automoveChessman.pawnDirection = 2; //commit that pawn right
                              }
                              //and they just moved diag mid
                              if (autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY - 1)
                              {
                                  automoveChessman.pawnDirection = -1; // dont commit to a direction, but mark -1 because your first turn happened and you can't move two space anymore..
                              }

                              //and they moved TWICE during their first move...ENPASSANT
                              //reset moves...
                              EnPassantMoveBlue[0] = -1;
                              EnPassantMoveBlue[1] = -1;

                              if (autox == automoveChessman.CurrentX - 2 && autoy == automoveChessman.CurrentY)
                              {
                                  EnPassantMoveBlue[0] = automoveChessman.CurrentX - 1;
                                  EnPassantMoveBlue[1] = automoveChessman.CurrentX;
                              }
                              if (autox == automoveChessman.CurrentX && autoy == automoveChessman.CurrentY - 2)
                              {
                                  EnPassantMoveBlue[0] = automoveChessman.CurrentX;
                                  EnPassantMoveBlue[1] = automoveChessman.CurrentY - 1;
                              }
                          }

                          if (automoveChessman.isYellow) //if they're a yellow pawn with no direction 
                          {
                              //and they went left
                              if (autox < automoveChessman.CurrentX && autoy == automoveChessman.CurrentY ||
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY - 1)
                              {
                                  automoveChessman.pawnDirection = 1; //commit that pawn left
                              }

                              //and they went right
                              if (autox == automoveChessman.CurrentX && autoy > automoveChessman.CurrentY ||
                                  autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY + 1)
                              {
                                  automoveChessman.pawnDirection = 2; //commit that pawn right
                              }

                              //and they just moved diag 
                              if (autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY + 1)
                              {
                                  automoveChessman.pawnDirection = -1; //dont commit
                              }

                              //and they moved TWICE during their first move...ENPASSANT
                              //reset moves...
                              EnPassantMoveYellow[0] = -1;
                              EnPassantMoveYellow[1] = -1;

                              if (autox == automoveChessman.CurrentX - 2 && autoy == automoveChessman.CurrentY)
                              {
                                  EnPassantMoveYellow[0] = automoveChessman.CurrentX - 1;
                                  EnPassantMoveYellow[1] = automoveChessman.CurrentY;
                              }
                              if (autox == automoveChessman.CurrentX && autoy == automoveChessman.CurrentY + 2)
                              {
                                  EnPassantMoveYellow[0] = automoveChessman.CurrentX;
                                  EnPassantMoveYellow[1] = automoveChessman.CurrentY + 1;
                              }
                          }

                          if (automoveChessman.isRed) //if they're a red pawn with no direction 
                          {
                              //and they went left
                              if (autox == automoveChessman.CurrentX && autoy > automoveChessman.CurrentY ||
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY + 1)
                              {
                                  automoveChessman.pawnDirection = 1; //commit that pawn left
                              }

                              //and they went right
                              if (autox > automoveChessman.CurrentX && autoy == automoveChessman.CurrentY ||
                                  autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY - 1)
                              {
                                  automoveChessman.pawnDirection = 2; //commit that pawn right
                              }

                              //if they moved diag
                              if (autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY - 1)
                              {
                                  automoveChessman.pawnDirection = -1; //dont commit
                              }

                              //and they moved TWICE during their first move...ENPASSANT
                              //reset moves...
                              EnPassantMoveRed[0] = -1;
                              EnPassantMoveRed[1] = -1;

                              if (autox == automoveChessman.CurrentX + 2 && autoy == automoveChessman.CurrentY)
                              {
                                  EnPassantMoveRed[0] = automoveChessman.CurrentX + 1;
                                  EnPassantMoveRed[1] = automoveChessman.CurrentY;
                              }
                              if (autox == automoveChessman.CurrentX && autoy == automoveChessman.CurrentY + 2)
                              {
                                  EnPassantMoveRed[0] = automoveChessman.CurrentX;
                                  EnPassantMoveRed[1] = automoveChessman.CurrentY + 1;
                              }

                          }

                          if (automoveChessman.isGreen) //if they're a green pawn with no direction 
                          {
                              //and went left
                              if (autox > automoveChessman.CurrentX && autoy == automoveChessman.CurrentY ||
                                  autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY + 1)
                              {
                                  automoveChessman.pawnDirection = 1;
                              }
                              //and went right
                              if (autox == automoveChessman.CurrentX && autoy < automoveChessman.CurrentY ||
                                  autox == automoveChessman.CurrentX - 1 && autoy == automoveChessman.CurrentY - 1)
                              {
                                  automoveChessman.pawnDirection = 2;
                              }
                              //if they moved diag
                              if (autox == automoveChessman.CurrentX + 1 && autoy == automoveChessman.CurrentY - 1)
                              {
                                  automoveChessman.pawnDirection = -1;  //dont commit
                              }

                              //and they moved TWICE during their first move...ENPASSANT
                              //reset moves...
                              EnPassantMoveGreen[0] = -1;
                              EnPassantMoveGreen[1] = -1;

                              if (autox == automoveChessman.CurrentX + 2 && autoy == automoveChessman.CurrentY)
                              {
                                  EnPassantMoveGreen[0] = automoveChessman.CurrentX + 1;
                                  EnPassantMoveGreen[1] = automoveChessman.CurrentY;
                              }
                              if (autox == automoveChessman.CurrentX && autoy == automoveChessman.CurrentY - 2)
                              {
                                  EnPassantMoveGreen[0] = automoveChessman.CurrentX;
                                  EnPassantMoveGreen[1] = automoveChessman.CurrentY - 1;
                              }
                          }
                      }
                      #endregion
                      #region assign enpassant tiles if there are any...
                      //ASSIGN THE ENPASSANT VALUES
                      List<GSData> enpassData = response.Challenge.ScriptData.GetGSDataList("Enpass");
                      Enpass e0 = Enpass.CreateFromJSON(enpassData[0].JSON);
                      EnPassantMoveBlue[0] = e0.number;
                      Enpass e1 = Enpass.CreateFromJSON(enpassData[1].JSON);
                      EnPassantMoveBlue[1] = e1.number;
                      Enpass e2 = Enpass.CreateFromJSON(enpassData[2].JSON);
                      EnPassantMoveYellow[0] = e2.number;
                      Enpass e3 = Enpass.CreateFromJSON(enpassData[3].JSON);
                      EnPassantMoveYellow[1] = e3.number;
                      Enpass e4 = Enpass.CreateFromJSON(enpassData[4].JSON);
                      EnPassantMoveRed[0] = e4.number;
                      Enpass e5 = Enpass.CreateFromJSON(enpassData[5].JSON);
                      EnPassantMoveRed[1] = e5.number;
                      Enpass e6 = Enpass.CreateFromJSON(enpassData[6].JSON);
                      EnPassantMoveGreen[0] = e6.number;
                      Enpass e7 = Enpass.CreateFromJSON(enpassData[7].JSON);
                      EnPassantMoveGreen[1] = e7.number;
                      #endregion
                      #region Collect Board States
                      try
                      {
                          List<GSData> bb = response.Challenge.ScriptData.GetGSDataList("bluebefore");
                          if (bb.Count > 0)
                          {
                              hasBlueTurn = true;
                          }

                          List<GSData> yb = response.Challenge.ScriptData.GetGSDataList("yellowbefore");
                          if (yb.Count > 0)
                          {
                              hasYellowTurn = true;
                          }

                          List<GSData> rb = response.Challenge.ScriptData.GetGSDataList("redbefore");
                          if (rb.Count > 0)
                          {
                              hasRedTurn = true;
                          }

                          List<GSData> gb = response.Challenge.ScriptData.GetGSDataList("greenbefore");
                          if (gb.Count > 0)
                          {
                              hasGreenTurn = true;
                          }

                      }
                      catch
                      {
                          Debug.Log("couldnt collect boardstates in smooth moves");
                      }
                      #endregion
                      #region ASSIGN ALL THE PRISONER LISTS

                      bluePrisonersYellow.Clear();
                      bluePrisonersRed.Clear();
                      bluePrisonersGreen.Clear();
                      yelPrisonersBlue.Clear();
                      yelPrisonersRed.Clear();
                      yelPrisonersGreen.Clear();
                      redPrisonersBlue.Clear();
                      redPrisonersYellow.Clear();
                      redPrisonersGreen.Clear();
                      greenPrisonersBlue.Clear();
                      greenPrisonersYellow.Clear();
                      greenPrisonersRed.Clear();

                      List<GSData> bpyList = response.Challenge.ScriptData.GetGSDataList("bpy");
                      foreach (GSData item in bpyList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              bluePrisonersYellow.Add(pi.prisName);
                          }
                      }
                      List<GSData> bprList = response.Challenge.ScriptData.GetGSDataList("bpr");
                      foreach (GSData item in bprList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              bluePrisonersRed.Add(pi.prisName);
                          }
                      }
                      List<GSData> bpgList = response.Challenge.ScriptData.GetGSDataList("bpg");
                      foreach (GSData item in bpgList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              bluePrisonersGreen.Add(pi.prisName);
                          }
                      }
                      List<GSData> ypbList = response.Challenge.ScriptData.GetGSDataList("ypb");
                      foreach (GSData item in ypbList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              yelPrisonersBlue.Add(pi.prisName);
                          }
                      }
                      List<GSData> yprList = response.Challenge.ScriptData.GetGSDataList("ypr");
                      foreach (GSData item in yprList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              yelPrisonersRed.Add(pi.prisName);
                          }
                      }
                      List<GSData> ypgList = response.Challenge.ScriptData.GetGSDataList("ypg");
                      foreach (GSData item in ypgList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              yelPrisonersGreen.Add(pi.prisName);
                          }
                      }
                      List<GSData> rpbList = response.Challenge.ScriptData.GetGSDataList("rpb");
                      foreach (GSData item in rpbList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              redPrisonersBlue.Add(pi.prisName);
                          }
                      }
                      List<GSData> rpyList = response.Challenge.ScriptData.GetGSDataList("rpy");
                      foreach (GSData item in rpyList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              redPrisonersYellow.Add(pi.prisName);
                          }
                      }
                      List<GSData> rpgList = response.Challenge.ScriptData.GetGSDataList("rpg");
                      foreach (GSData item in rpgList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              redPrisonersGreen.Add(pi.prisName);
                          }
                      }
                      List<GSData> gpbList = response.Challenge.ScriptData.GetGSDataList("gpb");
                      foreach (GSData item in gpbList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              greenPrisonersBlue.Add(pi.prisName);
                          }
                      }
                      List<GSData> gpyList = response.Challenge.ScriptData.GetGSDataList("gpy");
                      foreach (GSData item in gpyList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              greenPrisonersYellow.Add(pi.prisName);
                          }
                      }
                      List<GSData> gprList = response.Challenge.ScriptData.GetGSDataList("gpr");
                      foreach (GSData item in gprList)
                      {
                          PrisonerItem pi = PrisonerItem.CreateFromJSON(item.JSON);
                          if (pi.prisName != "Select a piece:")
                          {
                              greenPrisonersRed.Add(pi.prisName);
                          }
                      }
                      #endregion

                      FindPlayerTurn();
                      Lose();
                      Win();
                      ReconcileBoard("AutoMove() > SmoothMovesFollowThrough()");
                  }
              });
    }

    public void OnlineTurnExpired()
    {
        TriggerOnlineUpdate++; //make sure this doesnt trigger stuff in update
        DidPlayerTurnExpire = true;
        Debug.Log("OnlineTurnExpired");
        if (ExpiredColors.Count > 0)
        {
            if (onlinePlayerTurn != myID)
            {

                SkipToTurn(myID, onlinePlayerTurn, "me");
            }
            else
            {
                MoveExpiredPlayerWithAI();
            }
        }

    }
    public void SkipToTurn(string skipto, string skipfrom, string myAction) //make it my turn so i can alter the board.  OR make it the correct person's turn after i alter the board. (when an expired player is moved by the AI)
    {
        Debug.Log("Skipping To Turn()");
        bool error = false;

        #region      Win Conditions Variables
        WinConditions wc = new WinConditions();
        wc.playerT = playerTurn;
        wc.prevT = prevTurn;
        wc.Bout = blueout;
        wc.Yout = yelout;
        wc.Rout = redout;
        wc.Gout = greenout;
        string wcJson = JsonUtility.ToJson(wc);
        GSObject obj = GSObject.FromJson(wcJson);
        #endregion
        #region Collect Player info to figure out the turn order.

        List<GSData> GSPL = new List<GSData>();
        foreach (Player p in playerList)
        {
            string p5 = JsonUtility.ToJson(p);
            GSObject gsplayer = GSObject.FromJson(p5);
            GSPL.Add(gsplayer);
        }
        #endregion

        new LogEventRequest().SetEventKey("MakeRightTurn")
                 .SetEventAttribute("cid", OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                 .SetEventAttribute("wincon", obj)
                 .SetEventAttribute("players", GSPL)
                 .SetEventAttribute("skipto", skipto)
                 .SetEventAttribute("skipfrom", skipfrom)
                 .Send((response) =>
                 {
                     if (!response.HasErrors)
                     {
                         new GetChallengeRequest()
                                .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                                .Send((response2) =>
                                {
                                        //the action used to be for something ---- now i dont think it matters, but im not refactoring now,..

                                        if (myAction == "me")
                                    {
                                        if (!response2.HasErrors)
                                        {
                                            Challenge challengeData = Challenge.CreateFromJSON(response2.Challenge.JSONString); //deserialize the incoming data
                                                onlinePlayerTurn = challengeData.nextPlayer;

                                        }
                                    }

                                    if (myAction == "them")
                                    {
                                        Challenge challengeData = Challenge.CreateFromJSON(response2.Challenge.JSONString); //deserialize the incoming data
                                            onlinePlayerTurn = challengeData.nextPlayer;

                                    }
                                });
                     }
                     else
                     {
                         error = true;
                         Debug.Log("SkipToTurn() Messed Up");
                     }
                 });

        if (!error)
        {
            MoveExpiredPlayerWithAI();
        }
    }
    public void MoveExpiredPlayerWithAI()
    {
        Debug.Log("MoveExpiredPlayer()");
        if (ExpiredColors[0] == "Blue")
        {
            BlueAI = new AIControl(Chessmans, 1);
            isBlueAI = true;
            ShouldAIMove = true;
            AIAction = "Blue";
        }
        if (ExpiredColors[0] == "Yellow") //index should remain 0 no matter the color
        {
            YellowAI = new AIControl(Chessmans, 2);
            isYellowAI = true;
            ShouldAIMove = true;
            AIAction = "Yellow";
        }
        if (ExpiredColors[0] == "Red")
        {
            RedAI = new AIControl(Chessmans, 3);
            isRedAI = true;
            ShouldAIMove = true;
            AIAction = "Red";
        }
        if (ExpiredColors[0] == "Green")
        {
            GreenAI = new AIControl(Chessmans, 4);
            isGreenAI = true;
            ShouldAIMove = true;
            AIAction = "Green";
        }
        ExpiredColors.Remove(ExpiredColors[0]);
    }
    public void CheckForMore()
    {
        Debug.Log("checking for more");
        if (ExpiredColors != null && ExpiredColors.Count != 0)
        {
            Debug.Log("more found");
            //MoveExpiredPlayerWithAI();
            OnlineTurnExpired();
        }
        else
        {
            string skipto = "skipto";
            Debug.Log("more not found");
            new GetChallengeRequest()
               .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
               .Send((response) =>
               {
                   if (!response.HasErrors)
                   {
                       Debug.Log("CheckForMore - looking at server stuff - START");
                       GameInfo GameInfo = GameInfo.CreateFromJSON(response.Challenge.JSONString);
                       onlinePlayerTurn = GameInfo.nextPlayer;

                       if (GameInfo.scriptData.NextPlayerColor == "Blue")
                       {
                           skipto = playerList[0].id;
                       }
                       if (GameInfo.scriptData.NextPlayerColor == "Yellow")
                       {
                           skipto = playerList[1].id;
                       }
                       if (GameInfo.scriptData.NextPlayerColor == "Red")
                       {
                           skipto = playerList[2].id;
                       }
                       if (GameInfo.scriptData.NextPlayerColor == "Green")
                       {
                           skipto = playerList[3].id;
                       }


                       //since ExpireColors.Count is zero, remove it from the game's scriptData
                       new LogEventRequest().SetEventKey("RemoveScriptData").SetEventAttribute("cid", OnlineGame.GetComponent<GameInviteMessage>().challengeId).SetEventAttribute("data", "PlayersToSkip").Send((response2) =>
                          {
                              if (!response2.HasErrors)
                              {
                                  Debug.Log("CheckForMore(): Delete Data Success");


                                       #region      Win Conditions Variables
                                       WinConditions wc = new WinConditions();
                                  wc.playerT = playerTurn;
                                  wc.prevT = prevTurn;
                                  wc.Bout = blueout;
                                  wc.Yout = yelout;
                                  wc.Rout = redout;
                                  wc.Gout = greenout;
                                  string wcJson = JsonUtility.ToJson(wc);
                                  GSObject obj = GSObject.FromJson(wcJson);
                                       #endregion
                                       #region Collect Player info to figure out the turn order.

                                       List<GSData> GSPL = new List<GSData>();
                                  foreach (Player p in playerList)
                                  {
                                      string p5 = JsonUtility.ToJson(p);
                                      GSObject gsplayer = GSObject.FromJson(p5);
                                      GSPL.Add(gsplayer);
                                  }
                                       #endregion

                                       new LogEventRequest().SetEventKey("MakeRightTurn")
                                                .SetEventAttribute("cid", OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                                                .SetEventAttribute("wincon", obj)
                                                .SetEventAttribute("players", GSPL)
                                                .SetEventAttribute("skipto", skipto)
                                                .SetEventAttribute("skipfrom", onlinePlayerTurn)
                                                .Send((response3) =>
                                                {
                                                                           if (!response3.HasErrors)
                                                                           {
                                                                               Debug.Log("CheckForMore - trying to fix turn state...");
                                                                               new GetChallengeRequest()
                                                                                      .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                                                                                      .Send((response4) =>
                                                                                      {
                                                                                          Debug.Log("CheckForMore() reset the player turn to normal. COMPLETE");
                                                                                          Challenge challengeData = Challenge.CreateFromJSON(response4.Challenge.JSONString); //deserialize the incoming data
                                                                                               onlinePlayerTurn = challengeData.nextPlayer;
                                                                                          DidPlayerTurnExpire = false;
                                                                                          FindPlayerTurn();
                                                                                      });
                                                                           }
                                                                           else
                                                                           {
                                                                               Debug.Log("SkipToTurn() Messed Up");
                                                                           }
                                                                       });

                              }
                              else
                              {
                                  Debug.Log("CheckForMore Delete Data Fucked Up");
                              }
                          });
                   }
                   else
                   {
                       Debug.Log("CheckForMore Get Data Fucked Up");
                   }
               });
        }
    }
    public void PurgeScriptData(string data)
    {
        new LogEventRequest().SetEventKey("RemoveScriptData").SetEventAttribute("cid", OnlineGame.GetComponent<GameInviteMessage>().challengeId).SetEventAttribute("data", data).Send((response) =>
        {
            if (!response.HasErrors)
            {
                Debug.Log("CheckForMore(): Delete Data Success");
            }
            else
            {
                Debug.Log("CheckForMore Delete Data Fucked Up");
            }
        });
    }
    public void Stalemate()
    {
        IsStalemate = false;

        if (k1 != null)
        {
            if (!k1.isChecked && playerTurn == 1) //if not in check
            {
                GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                int TotalPossibleMoves = 0;
                int MovesThatResultInCheck = 0;
                List<string> ListMovesThatResultInCheck = new List<string>();

                foreach (GameObject go in allPieces)
                {
                    Chessman me = go.GetComponent<Chessman>(); 

                    if (me.isBlue) //see if i can make a move that gets me out of check - so get a piece that is on my team
                    {
                        allowedMoves = go.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen);

                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (allowedMoves[i, j])
                                {
                                    bool isRetreatTile = false;
                                    bool isRetreatPiece = false;

                                    if (lastBlueX == i && lastBlueY == j)
                                    {
                                        isRetreatTile = true;
                                    }

                                    if (resetManBlue != null)
                                    {
                                        if (Chessmans[me.CurrentX, me.CurrentY] == Chessmans[resetManBlue.CurrentX, resetManBlue.CurrentY])
                                        {
                                            isRetreatPiece = true;
                                        }
                                    }

                                    bool ShouldEvaluate = true;

                                    if (isRetreatPiece && isRetreatTile)
                                    {
                                        ShouldEvaluate = false;
                                    }

                                    if (ShouldEvaluate) 
                                    {
                                        int FoundCheck = 0;
                                        TotalPossibleMoves++;

                                        if (ThisMoveResultsInCheck(i, j, me.getColor(), me.CurrentX, me.CurrentY))
                                        {
                                            FoundCheck++;

                                            string codeword = go.name + " from " + mateoldX + ", " + mateoldY + " to " + i + ", " + j;
                                            if (ListMovesThatResultInCheck.Count == 0)
                                            {
                                                MovesThatResultInCheck++;
                                                ListMovesThatResultInCheck.Add(codeword);
                                            }
                                            else
                                            {
                                                bool MatchFound = false;
                                                foreach (string s in ListMovesThatResultInCheck)
                                                {
                                                    if (codeword == s)
                                                    {
                                                        MatchFound = true;
                                                    }
                                                }
                                                if (!MatchFound)
                                                {
                                                    MovesThatResultInCheck++;
                                                    ListMovesThatResultInCheck.Add(codeword);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (TotalPossibleMoves == MovesThatResultInCheck)////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    IsStalemate = true;
                    GameText.text = "The Blue team is in Stalemate. Make a move that results in Check.";
                    Debug.Log("The Blue team is in Stalemate.");////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
        }

        if (k2 != null)
        {
            if (!k2.isChecked && playerTurn == 2)
            {
                GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                int TotalPossibleMoves = 0;
                int MovesThatResultInCheck = 0;
                List<string> ListMovesThatResultInCheck = new List<string>();

                foreach (GameObject go in allPieces)
                {
                    Chessman me = go.GetComponent<Chessman>();

                    if (me.isYellow) //see if i can make a move that gets me out of check - so get a piece that is on my team
                    {
                        allowedMoves = me.PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen);

                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (allowedMoves[i, j])
                                {
                                    bool isRetreatTile = false;
                                    bool isRetreatPiece = false;

                                    if (lastYellowX == i && lastYellowY == j)
                                    {
                                        isRetreatTile = true;
                                    }

                                    if (resetManYellow != null)
                                    {
                                        if (Chessmans[me.CurrentX, me.CurrentY] == Chessmans[resetManYellow.CurrentX, resetManYellow.CurrentY])
                                        {
                                            isRetreatPiece = true;
                                        }
                                    }

                                    bool ShouldEvaluate = true;

                                    if (isRetreatPiece && isRetreatTile)
                                    {
                                        ShouldEvaluate = false;
                                    }

                                    if (ShouldEvaluate)
                                    {
                                        int FoundCheck = 0;
                                        TotalPossibleMoves++;

                                        if (ThisMoveResultsInCheck(i, j, me.getColor(), me.CurrentX, me.CurrentY))
                                        {
                                            FoundCheck++;

                                            string codeword = go.name + " from " + mateoldX + ", " + mateoldY + " to " + i + ", " + j;
                                            if (ListMovesThatResultInCheck.Count == 0)
                                            {
                                                MovesThatResultInCheck++;
                                                ListMovesThatResultInCheck.Add(codeword);
                                            }
                                            else
                                            {
                                                bool MatchFound = false;
                                                foreach (string s in ListMovesThatResultInCheck)
                                                {
                                                    if (codeword == s)
                                                    {
                                                        MatchFound = true;
                                                    }
                                                }
                                                if (!MatchFound)
                                                {
                                                    MovesThatResultInCheck++;
                                                    ListMovesThatResultInCheck.Add(codeword);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (TotalPossibleMoves == MovesThatResultInCheck)////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    IsStalemate = true;
                    GameText.text = "The Yellow team is in Stalemate. Make a move that results in Check.";
                    Debug.Log("The Yellow team is in Stalemate.");////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
        }

        if (k3 != null)
        {
            if (!k3.isChecked && playerTurn == 3)
            {
                GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                int TotalPossibleMoves = 0;
                int MovesThatResultInCheck = 0;
                List<string> ListMovesThatResultInCheck = new List<string>();

                foreach (GameObject go in allPieces)
                {
                    Chessman me = go.GetComponent<Chessman>();

                    if (me.isRed) //see if i can make a move that gets me out of check - so get a piece that is on my team
                    {
                        allowedMoves = go.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen);

                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (allowedMoves[i, j])
                                {
                                    bool isRetreatTile = false;
                                    bool isRetreatPiece = false;

                                    if (lastRedX == i && lastRedY == j)
                                    {
                                        isRetreatTile = true;
                                    }

                                    if (resetManRed != null)
                                    {
                                        if (Chessmans[me.CurrentX, me.CurrentY] == Chessmans[resetManRed.CurrentX, resetManRed.CurrentY])
                                        {
                                            isRetreatPiece = true;
                                        }
                                    }

                                    bool ShouldEvaluate = true;

                                    if (isRetreatPiece && isRetreatTile)
                                    {
                                        ShouldEvaluate = false;
                                    }

                                    if (ShouldEvaluate)
                                    {
                                        int FoundCheck = 0;
                                        TotalPossibleMoves++;

                                        if (ThisMoveResultsInCheck(i, j, me.getColor(), me.CurrentX, me.CurrentY))
                                        {
                                            FoundCheck++;

                                            string codeword = go.name + " from " + mateoldX + ", " + mateoldY + " to " + i + ", " + j;
                                            if (ListMovesThatResultInCheck.Count == 0)
                                            {
                                                MovesThatResultInCheck++;
                                                ListMovesThatResultInCheck.Add(codeword);
                                            }
                                            else
                                            {
                                                bool MatchFound = false;
                                                foreach (string s in ListMovesThatResultInCheck)
                                                {
                                                    if (codeword == s)
                                                    {
                                                        MatchFound = true;
                                                    }
                                                }
                                                if (!MatchFound)
                                                {
                                                    MovesThatResultInCheck++;
                                                    ListMovesThatResultInCheck.Add(codeword);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (TotalPossibleMoves == MovesThatResultInCheck)////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    IsStalemate = true;
                    GameText.text = "The Red team is in Stalemate. Make a move that results in Check.";
                    Debug.Log("The Red team is in Stalemate.");////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
        }

        if (k4 != null)
        {
            if (!k4.isChecked && playerTurn == 4) 
            {
                GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");
                int TotalPossibleMoves = 0;
                int MovesThatResultInCheck = 0;
                List<string> ListMovesThatResultInCheck = new List<string>();

                foreach (GameObject go in allPieces)
                {
                    Chessman me = go.GetComponent<Chessman>();

                    if (me.isGreen) //see if i can make a move that gets me out of check - so get a piece that is on my team
                    {
                        allowedMoves = go.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen);

                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (allowedMoves[i, j])
                                {
                                    bool isRetreatTile = false;
                                    bool isRetreatPiece = false;

                                    if (lastGreenX == i && lastGreenY == j)
                                    {
                                        isRetreatTile = true;
                                    }

                                    if (resetManGreen != null)
                                    {
                                        if (Chessmans[me.CurrentX, me.CurrentY] == Chessmans[resetManGreen.CurrentX, resetManGreen.CurrentY])
                                        {
                                            isRetreatPiece = true;
                                        }
                                    }

                                    bool ShouldEvaluate = true;

                                    if (isRetreatPiece && isRetreatTile)
                                    {
                                        ShouldEvaluate = false;
                                    }

                                    if (ShouldEvaluate)
                                    {
                                        int FoundCheck = 0;
                                        TotalPossibleMoves++;

                                        if (ThisMoveResultsInCheck(i, j, me.getColor(), me.CurrentX, me.CurrentY))
                                        {
                                            FoundCheck++;

                                            string codeword = go.name + " from " + mateoldX + ", " + mateoldY + " to " + i + ", " + j;
                                            if (ListMovesThatResultInCheck.Count == 0)
                                            {
                                                MovesThatResultInCheck++;
                                                ListMovesThatResultInCheck.Add(codeword);
                                            }
                                            else
                                            {
                                                bool MatchFound = false;
                                                foreach (string s in ListMovesThatResultInCheck)
                                                {
                                                    if (codeword == s)
                                                    {
                                                        MatchFound = true;
                                                    }
                                                }
                                                if (!MatchFound)
                                                {
                                                    MovesThatResultInCheck++;
                                                    ListMovesThatResultInCheck.Add(codeword);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (TotalPossibleMoves == MovesThatResultInCheck)////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    IsStalemate = true;
                    GameText.text = "The Green team is in Stalemate. Make a move that results in Check.";
                    Debug.Log("The Green team is in Stalemate.");////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
        }
    }
    //called when player turn message listener is called....NOW SMOOTHMOVE IS GETTING CALLED INSTEAD.    
    public void OnlineTurnTaken()
    {
        //clear and reset current gamestate
        foreach (GameObject go in activeChessman)
        {
            Destroy(go.gameObject);
        }

        bluePrisonersYellow.Clear();
        bluePrisonersRed.Clear();
        bluePrisonersGreen.Clear();
        yelPrisonersBlue.Clear();
        yelPrisonersRed.Clear();
        yelPrisonersGreen.Clear();
        redPrisonersBlue.Clear();
        redPrisonersYellow.Clear();
        redPrisonersGreen.Clear();
        greenPrisonersBlue.Clear();
        greenPrisonersYellow.Clear();
        greenPrisonersRed.Clear();

        //get the new gamestate
        GetGameDataAndSpawnStuff();
    }

    //used for fetching and displaying previous turn board states.
    public void PreviousTurn(string color)
    {


        //clear and reset current gamestate
        foreach (GameObject go in activeChessman)
        {
            Destroy(go.gameObject);
        }

        new GetChallengeRequest()
          .SetChallengeInstanceId(OnlineGame.GetComponent<GameInviteMessage>().challengeId)
          .Send((response) =>
          {
              if (!response.HasErrors)
              {
                  List<GSData> board = response.Challenge.ScriptData.GetGSDataList(color);
                  foreach (GSData item in board)
                  {
                      PieceData piece = PieceData.CreateFromJSON(item.JSON);
                      SpawnChessman(piece.index, piece.SaveX, piece.SaveY);
                      Chessmans[piece.SaveX, piece.SaveY].pawnDirection = piece.pawnDirection;
                  }
              }
          });
        isUnderReview = true;
    }
    public void BackFromPreviousTurn()
    {
        //clear and reset current gamestate
        foreach (GameObject go in activeChessman)
        {
            Destroy(go.gameObject);
        }
        bluePrisonersYellow.Clear();
        bluePrisonersRed.Clear();
        bluePrisonersGreen.Clear();
        yelPrisonersBlue.Clear();
        yelPrisonersRed.Clear();
        yelPrisonersGreen.Clear();
        redPrisonersBlue.Clear();
        redPrisonersYellow.Clear();
        redPrisonersGreen.Clear();
        greenPrisonersBlue.Clear();
        greenPrisonersYellow.Clear();
        greenPrisonersRed.Clear();

        isUnderReview = false;
        //get the new gamestate
        GetGameDataAndSpawnStuff();
    }

    //Select a peice and move it.  
    IEnumerator Move(Chessman selected, Vector3 destination, float speed)
    {
        while (selected.transform.position != destination)
        {
            selected.transform.position = Vector3.MoveTowards(selected.transform.position, destination, speed * Time.deltaTime);
            speed++;
            yield return null;
        }
        if (!isAIMove && !IsAutoMove)
        {
            moved = true;
        }

        if (isAIMove)
        {
            isAIMove = false;
            moved = true;
        }
        if (IsAutoMove)
        {
            IsAutoMove = false;
            moved = true;
        }
    }
    private void SelectChessman(int x, int y)
    {
        if (Chessmans[x, y] == null) //if there's no piece
            return; //then quit
        bool teamismated = false;
        if (playerTurn == 1 && Chessmans[x, y].isBlue) // make sure its player 1's turn and that they can only select a blue piece...same for the others.
        {
            selectedChessman = Chessmans[x, y];
            //track where the peice's starting position is
            oldX = Chessmans[x, y].CurrentX;
            oldY = Chessmans[x, y].CurrentY;

            previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
            capColor = null;
            capName = null;
            capDir = 0;
            if(k1.isMated){ teamismated = true; }
        }
        if (playerTurn == 2 && Chessmans[x, y].isYellow)
        {
            selectedChessman = Chessmans[x, y];
            //track where the peice's starting position is
            oldX = Chessmans[x, y].CurrentX;
            oldY = Chessmans[x, y].CurrentY;

            previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
            capColor = null;
            capName = null;//reset this here so it is only called in Update() if it has been set to seomthign in Movement()
            capDir = 0;
            if (k2.isMated) { teamismated = true; }
        }
        if (playerTurn == 3 && Chessmans[x, y].isRed)
        {
            selectedChessman = Chessmans[x, y];
            //track where the peice's starting position is
            oldX = Chessmans[x, y].CurrentX;
            oldY = Chessmans[x, y].CurrentY;

            previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
            capColor = null;
            capName = null;
            capDir = 0;
            if (k3.isMated) { teamismated = true; }
        }
        if (playerTurn == 4 && Chessmans[x, y].isGreen)
        {
            selectedChessman = Chessmans[x, y];
            //track where the peice's starting position is
            oldX = Chessmans[x, y].CurrentX;
            oldY = Chessmans[x, y].CurrentY;

            previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
            capColor = null;
            capName = null;
            capDir = 0;
            if (k4.isMated) { teamismated = true; }
        }

        //make sure the User cannot select a piece that cannot be moved.
        //bool hasAtleastOneMove = false;
        allowedMoves = Chessmans[x, y].PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //re-reading this, its doesnt make sense for this to be here since its declared again below?
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (allowedMoves[i, j])
                {
                    selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
                    //print(i + ", " + j);
                    if (!ThisIsARetreatMove(i, j, Chessmans[x, y].getColor(), x, y)) // Retreat Move is False, and Im not in Checkmate
                    {
                        if (!teamismated && !IsStalemate) // If im not in checkmate or stalemate, then I'm not allowed to move into check.
                        {
                            if (ThisMoveResultsInCheck(i, j, Chessmans[x, y].getColor(), x, y)) // if this move puts me in check, remove it
                            {
                                allowedMoves[i, j] = false;
                                //print("Check at " + i + ", " + j);
                                GameObject go = Instantiate(CheckTile);
                                go.transform.position = new Vector3(i + 0.5f, 0.002f, j + 0.5f);
                                NoMoveTiles.Add(go);
                            }
                        }
                    }
                    else
                    {
                        allowedMoves[i, j] = false;
                        //print("Retreat piece and tile at " + x + " ," + y + " to " + i + ", " + j);
                        GameObject go = Instantiate(RetreatTile);
                        go.transform.position = new Vector3(i + 0.5f, 0.002f, j + 0.5f);
                        NoMoveTiles.Add(go);
                    }
                }
            }
        }
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }
    private void MoveChessman(int x, int y)
    {

        if (allowedMoves[x, y] || IsAutoMove /* && Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] != Chessmans[x, y] */)
        {

            //RESET EMPASSANT MOVES
            #region
            if (playerTurn == 1)
            {
                EnPassantMoveBlue[0] = -1;
                EnPassantMoveBlue[1] = -1;
            }
            if (playerTurn == 2)
            {
                EnPassantMoveYellow[0] = -1;
                EnPassantMoveYellow[1] = -1;
            }
            if (playerTurn == 3)
            {
                EnPassantMoveRed[0] = -1;
                EnPassantMoveRed[1] = -1;
            }
            if (playerTurn == 4)
            {
                EnPassantMoveGreen[0] = -1;
                EnPassantMoveGreen[1] = -1;
            }
            #endregion

            //CAPTURE PIECES
            #region
            if (Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY].isBlue)
            {
                Chessman c = Chessmans[x, y];

                if (c != null && !c.isBlue) //if there is a piece where we're moving that isn't our color
                {
                    if (c.isYellow)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            bluePrisonersYellow.Add(c.name);
                        }
                        capColor = "Yellow";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;

                        if (c == k2)
                        {
                            k2 = null;
                        }
                        if (c == q2)
                        {
                            q2 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    if (c.isRed)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            bluePrisonersRed.Add(c.name);
                        }
                        capColor = "Red";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;

                        if (c == k3)
                        {
                            k3 = null;
                        }
                        if (c == q3)
                        {
                            q3 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    if (c.isGreen)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            bluePrisonersGreen.Add(c.name);
                        }
                        capColor = "Green";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;

                        if (c == k4)
                        {
                            k4 = null;
                        }
                        if (c == q4)
                        {
                            q4 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                }
            }
            if (Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY].isYellow)
            {
                Chessman c = Chessmans[x, y];

                if (c != null && !c.isYellow) //if there is a piece where we're moving that isn't our color
                {
                    if (c.isBlue)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            yelPrisonersBlue.Add(c.name);
                        }
                        capColor = "Blue";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k1)
                        {
                            k1 = null;
                        }
                        if (c == q1)
                        {
                            q1 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    if (c.isRed)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            yelPrisonersRed.Add(c.name);
                        }
                        capColor = "Red";
                        capName = c.name;
                        capDir = c.pawnDirection;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k3)
                        {
                            k3 = null;
                        }
                        if (c == q3)
                        {
                            q3 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    if (c.isGreen)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            yelPrisonersGreen.Add(c.name);
                        }
                        capColor = "Green";
                        capName = c.name;
                        capDir = c.pawnDirection;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k4)
                        {
                            k4 = null;
                        }
                        if (c == q4)
                        {
                            q4 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                }
            }
            if (Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY].isRed)
            {
                Chessman c = Chessmans[x, y];

                if (c != null && !c.isRed) //if there is a piece where we're moving that isn't our color
                {
                    if (c.isBlue)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            redPrisonersBlue.Add(c.name);
                        }
                        capColor = "Blue";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k1)
                        {
                            k1 = null;
                        }
                        if (c == q1)
                        {
                            q1 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    if (c.isYellow)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            redPrisonersYellow.Add(c.name);
                        }
                        capColor = "Yellow";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k2)
                        {
                            k2 = null;
                        }
                        if (c == q2)
                        {
                            q2 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    if (c.isGreen)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            redPrisonersGreen.Add(c.name);
                        }
                        capColor = "Green";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k4)
                        {
                            k4 = null;
                        }
                        if (c == q4)
                        {
                            q4 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                }
            }
            if (Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY].isGreen)
            {
                Chessman c = Chessmans[x, y];

                if (c != null && !c.isGreen) //if there is a piece where we're moving that isn't our color
                {
                    if (c.isBlue)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            greenPrisonersBlue.Add(c.name);
                        }
                        capColor = "Blue";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k1)
                        {
                            k1 = null;
                        }
                        if (c == q1)
                        {
                            q1 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    if (c.isYellow)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            greenPrisonersYellow.Add(c.name);
                        }
                        capColor = "Yellow";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k2)
                        {
                            k2 = null;
                        }
                        if (c == q2)
                        {
                            q2 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    if (c.isRed)
                    {
                        if (c.name != "pawn(Clone)" && c.name != "King")
                        {
                            greenPrisonersRed.Add(c.name);
                        }
                        capColor = "Red";
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        capCheck = c.isChecked;
                        capMated = c.isMated;
                        if (c == k3)
                        {
                            k3 = null;
                        }
                        if (c == q3)
                        {
                            q3 = null;
                        }

                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                }
            }
            #endregion

            //MOVE PIECE
            previous = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY]; //grab this piece so we can reset the board by one turn to enforce check
            prevDir = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY].pawnDirection;
            prevCheck = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY].isChecked;
            prevMated = Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY].isMated;
            StartCoroutine(Move(selectedChessman, GetTileCenter(x, y), 5));
            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null; //set the current position to null because we're going to move it.
                                                                                    //  selectedChessman.transform.position = GetTileCenter(x, y); //move to the center of the tile         
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman; //make the intended peice BE the selected piece

            //Castle Logic (resides in the kings script)
            #region
            if (Chessmans[x, y].name == "King")
            {
                if (Chessmans[x, y].pawnDirection == 0)
                {
                    Chessmans[x, y].pawnDirection = 1;
                    if (Chessmans[x, y].CurrentX == oldX + 2) //these means a blue or yellow moved right...
                    {
                        if (Chessmans[x, y].isBlue)
                        {
                            Destroy(Chessmans[3, 0].gameObject);
                            SpawnChessman(1, 1, 0);
                            Chessmans[1, 0].pawnDirection = 1;
                        }
                        if (Chessmans[x, y].isYellow)
                        {
                            Destroy(Chessmans[3, 11].gameObject);
                            SpawnChessman(19, 1, 11);
                            Chessmans[1, 11].pawnDirection = 1;
                        }
                    }
                    if (Chessmans[x, y].CurrentX == oldX - 2) //these means a green or red moved left...
                    {
                        if (Chessmans[x, y].isGreen)
                        {
                            Destroy(Chessmans[8, 0].gameObject);
                            SpawnChessman(7, 10, 0);
                            Chessmans[10, 0].pawnDirection = 1;
                        }
                        if (Chessmans[x, y].isRed)
                        {
                            Destroy(Chessmans[8, 11].gameObject);
                            SpawnChessman(13, 10, 11);
                            Chessmans[10, 11].pawnDirection = 1;
                        }
                    }
                    if (Chessmans[x, y].CurrentY == oldY + 2) //these means a blue or green moved up...
                    {
                        if (Chessmans[x, y].isBlue)
                        {
                            Destroy(Chessmans[0, 3].gameObject);
                            SpawnChessman(1, 0, 1);
                            Chessmans[0, 1].pawnDirection = 1;
                        }
                        if (Chessmans[x, y].isGreen)
                        {
                            Destroy(Chessmans[11, 3].gameObject);
                            SpawnChessman(7, 11, 1);
                            Chessmans[11, 1].pawnDirection = 1;
                        }
                    }
                    if (Chessmans[x, y].CurrentY == oldY - 2) //these means a yellow or red moved down...
                    {
                        if (Chessmans[x, y].isYellow)
                        {
                            Destroy(Chessmans[0, 8].gameObject);
                            SpawnChessman(19, 0, 10);
                            Chessmans[0, 10].pawnDirection = 1;
                        }
                        if (Chessmans[x, y].isRed)
                        {
                            Destroy(Chessmans[11, 8].gameObject);
                            SpawnChessman(13, 11, 10);
                            Chessmans[11, 10].pawnDirection = 1;
                        }
                    }
                }
            }
            //    if (Chessmans[x, y].name == "Rook") //make sure a Rook is set to 1 if they've moved. otherwise, casteling is possible.
            //    {
            //        if (Chessmans[x, y].pawnDirection == 0)
            //        {
            //            Chessmans[x, y].pawnDirection = 1;
            //        }
            //    }
            //if (Chessmans[x, y].name == "King") //make sure a King is set to 1 if they've moved. otherwise, casteling is possible.
            //{
            //    if (Chessmans[x, y].pawnDirection == 0)
            //    {
            //        Chessmans[x, y].pawnDirection = 1;
            //    }
            //}
            #endregion

            //make sure we indicate that a piece moved from its starting position - since pawns take in considerably different logic for this, we leave them alone.
            if (Chessmans[x, y].name != "pawn(Clone)")
            {
                Chessmans[x, y].pawnDirection = 1; //this is a special consideration for Casetling logic, as well as a pawns direction they're faceing.
            }

            //ENPASSANT DESTROY
            #region
            if (x == EnPassantMoveBlue[0] && y == EnPassantMoveBlue[1])
            {
                if (x == 0 && y == 5 || x == 1 && y == 4 || x == 2 && y == 3 || x == 3 && y == 2 || x == 4 && y == 1 || x == 5 && y == 0 || x == 1 && y == 3 || x == 2 && y == 2 || x == 3 && y == 1)
                {
                    Chessman c = Chessmans[x, y + 1]; // set the chessman to be deleted if they're on similar orientations

                    if (c == null)
                    {
                        c = Chessmans[x + 1, y];
                    }

                    if (oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY - 1 || //up left
                        oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY + 1 || // down left
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY - 1 || // up right
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY + 1) // down right
                    {
                        if (c.isBlue)
                        {
                            capColor = "Blue";
                        }
                        if (c.isYellow)
                        {
                            capColor = "Yellow";
                        }
                        if (c.isRed)
                        {
                            capColor = "Red";
                        }
                        if (c.isGreen)
                        {
                            capColor = "Green";
                        }
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    else
                    {
                        EnPassantMoveBlue[0] = -1;
                        EnPassantMoveBlue[1] = -1;
                    }
                }
            }
            if (x == EnPassantMoveYellow[0] && y == EnPassantMoveYellow[1])
            {
                if (x == 0 && y == 6 || x == 1 && y == 7 || x == 2 && y == 8 || x == 3 && y == 9 || x == 4 && y == 10 || x == 5 && y == 11 || x == 1 && y == 8 || x == 2 && y == 9 || x == 3 && y == 10)
                {
                    Chessman c = Chessmans[x, y - 1]; // set the chessman to be deleted if they're on similar orientations

                    if (c == null)
                    {
                        c = Chessmans[x + 1, y];
                    }
                    if (oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY - 1 || //up left
                        oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY + 1 || // down left
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY - 1 || // up right
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY + 1) // down right
                    {
                        if (c.isBlue)
                        {
                            capColor = "Blue";
                        }
                        if (c.isYellow)
                        {
                            capColor = "Yellow";
                        }
                        if (c.isRed)
                        {
                            capColor = "Red";
                        }
                        if (c.isGreen)
                        {
                            capColor = "Green";
                        }
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    else
                    {
                        EnPassantMoveYellow[0] = -1;
                        EnPassantMoveYellow[1] = -1;
                    }
                }
            }
            if (x == EnPassantMoveRed[0] && y == EnPassantMoveRed[1])
            {
                if (x == 6 && y == 11 || x == 7 && y == 10 || x == 8 && y == 9 || x == 9 && y == 8 || x == 10 && y == 7 || x == 11 && y == 6 || x == 10 && y == 8 || x == 9 && y == 9 || x == 8 && y == 10)
                {
                    Chessman c = Chessmans[x, y - 1]; // set the chessman to be deleted if they're on similar orientations
                    if (c == null)
                    {
                        c = Chessmans[x - 1, y]; //if they're not on similar orietnations, and its to the right
                    }
                    if (oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY - 1 || //up left
                        oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY + 1 || // down left
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY - 1 || // up right
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY + 1) // down right
                    {
                        if (c.isBlue)
                        {
                            capColor = "Blue";
                        }
                        if (c.isYellow)
                        {
                            capColor = "Yellow";
                        }
                        if (c.isRed)
                        {
                            capColor = "Red";
                        }
                        if (c.isGreen)
                        {
                            capColor = "Green";
                        }
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    else
                    {
                        EnPassantMoveRed[0] = -1;
                        EnPassantMoveRed[1] = -1;
                    }
                }
            }
            if (x == EnPassantMoveGreen[0] && y == EnPassantMoveGreen[1])
            {
                if (x == 6 && y == 0 || x == 7 && y == 1 || x == 8 && y == 2 || x == 9 && y == 3 || x == 10 && y == 4 || x == 11 && y == 5 || x == 8 && y == 1 || x == 9 && y == 2 || x == 10 && y == 3)
                {
                    Chessman c = Chessmans[x, y + 1]; // set the chessman to be deleted if they're on similar orientations
                    if (c == null)
                    {
                        c = Chessmans[x - 1, y];
                    }
                    if (oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY - 1 || //up left
                        oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY + 1 || // down left
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY - 1 || // up right
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY + 1) // down right
                    {
                        if (c.isBlue)
                        {
                            capColor = "Blue";
                        }
                        if (c.isYellow)
                        {
                            capColor = "Yellow";
                        }
                        if (c.isRed)
                        {
                            capColor = "Red";
                        }
                        if (c.isGreen)
                        {
                            capColor = "Green";
                        }
                        capName = c.name;
                        capX = c.CurrentX;
                        capY = c.CurrentY;
                        capDir = c.pawnDirection;
                        activeChessman.Remove(c.gameObject);
                        Destroy(c.gameObject);
                    }
                    else
                    {
                        EnPassantMoveGreen[0] = -1;
                        EnPassantMoveGreen[1] = -1;
                    }
                }
            }
            #endregion



            //PAWN COMMITMENT AND ENPASSANT
            #region
            if (Chessmans[x, y].name == "pawn(Clone)" && Chessmans[x, y].pawnDirection == 0 || Chessmans[x, y].name == "pawn(Clone)" && Chessmans[x, y].pawnDirection == -1) //if a pawn moved, that did not declare a direction
            {
                if (Chessmans[x, y].isBlue) //if they're a blue pawn with no direction 
                {
                    //and they just moved left
                    if (oldY < Chessmans[x, y].CurrentY && oldX == Chessmans[x, y].CurrentX ||
                        oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY - 1)
                    {
                        Chessmans[x, y].pawnDirection = 1; //commit that pawn left
                    }
                    //and they just moved right
                    if (oldX < Chessmans[x, y].CurrentX && oldY == Chessmans[x, y].CurrentY ||
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY + 1)
                    {
                        Chessmans[x, y].pawnDirection = 2; //commit that pawn right
                    }
                    //and they just moved diag mid
                    if (oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY - 1)
                    {
                        Chessmans[x, y].pawnDirection = -1; // dont commit to a direction, but mark -1 because your first turn happened and you can't move two space anymore..
                    }

                    //and they moved TWICE during their first move...ENPASSANT
                    //reset moves...
                    EnPassantMoveBlue[0] = -1;
                    EnPassantMoveBlue[1] = -1;

                    if (oldX == Chessmans[x, y].CurrentX - 2 && oldY == Chessmans[x, y].CurrentY)
                    {
                        EnPassantMoveBlue[0] = x - 1;
                        EnPassantMoveBlue[1] = y;
                    }
                    if (oldX == Chessmans[x, y].CurrentX && oldY == Chessmans[x, y].CurrentY - 2)
                    {
                        EnPassantMoveBlue[0] = x;
                        EnPassantMoveBlue[1] = y - 1;
                    }
                }

                if (Chessmans[x, y].isYellow) //if they're a yellow pawn with no direction 
                {
                    //and they went left
                    if (oldX < Chessmans[x, y].CurrentX && oldY == Chessmans[x, y].CurrentY ||
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY - 1)
                    {
                        Chessmans[x, y].pawnDirection = 1; //commit that pawn left
                    }

                    //and they went right
                    if (oldX == Chessmans[x, y].CurrentX && oldY > Chessmans[x, y].CurrentY ||
                        oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY + 1)
                    {
                        Chessmans[x, y].pawnDirection = 2; //commit that pawn right
                    }

                    //and they just moved diag 
                    if (oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY + 1)
                    {
                        Chessmans[x, y].pawnDirection = -1; //dont commit
                    }

                    //and they moved TWICE during their first move...ENPASSANT
                    //reset moves...
                    EnPassantMoveYellow[0] = -1;
                    EnPassantMoveYellow[1] = -1;

                    if (oldX == Chessmans[x, y].CurrentX - 2 && oldY == Chessmans[x, y].CurrentY)
                    {
                        EnPassantMoveYellow[0] = x - 1;
                        EnPassantMoveYellow[1] = y;
                    }
                    if (oldX == Chessmans[x, y].CurrentX && oldY == Chessmans[x, y].CurrentY + 2)
                    {
                        EnPassantMoveYellow[0] = x;
                        EnPassantMoveYellow[1] = y + 1;
                    }
                }

                if (Chessmans[x, y].isRed) //if they're a red pawn with no direction 
                {
                    //and they went left
                    if (oldX == Chessmans[x, y].CurrentX && oldY > Chessmans[x, y].CurrentY ||
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY + 1)
                    {
                        Chessmans[x, y].pawnDirection = 1; //commit that pawn left
                    }

                    //and they went right
                    if (oldX > Chessmans[x, y].CurrentX && oldY == Chessmans[x, y].CurrentY ||
                        oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY - 1)
                    {
                        Chessmans[x, y].pawnDirection = 2; //commit that pawn right
                    }

                    //if they moved diag
                    if (oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY - 1)
                    {
                        Chessmans[x, y].pawnDirection = -1; //dont commit
                    }

                    //and they moved TWICE during their first move...ENPASSANT
                    //reset moves...
                    EnPassantMoveRed[0] = -1;
                    EnPassantMoveRed[1] = -1;

                    if (oldX == Chessmans[x, y].CurrentX + 2 && oldY == Chessmans[x, y].CurrentY)
                    {
                        EnPassantMoveRed[0] = x + 1;
                        EnPassantMoveRed[1] = y;
                    }
                    if (oldX == Chessmans[x, y].CurrentX && oldY == Chessmans[x, y].CurrentY + 2)
                    {
                        EnPassantMoveRed[0] = x;
                        EnPassantMoveRed[1] = y + 1;
                    }

                }

                if (Chessmans[x, y].isGreen) //if they're a green pawn with no direction 
                {
                    //and went left
                    if (oldX > Chessmans[x, y].CurrentX && oldY == Chessmans[x, y].CurrentY ||
                        oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY + 1)
                    {
                        Chessmans[x, y].pawnDirection = 1;
                    }
                    //and went right
                    if (oldX == Chessmans[x, y].CurrentX && oldY < Chessmans[x, y].CurrentY ||
                        oldX == Chessmans[x, y].CurrentX - 1 && oldY == Chessmans[x, y].CurrentY - 1)
                    {
                        Chessmans[x, y].pawnDirection = 2;
                    }
                    //if they moved diag
                    if (oldX == Chessmans[x, y].CurrentX + 1 && oldY == Chessmans[x, y].CurrentY - 1)
                    {
                        Chessmans[x, y].pawnDirection = -1;  //dont commit
                    }

                    //and they moved TWICE during their first move...ENPASSANT
                    //reset moves...
                    EnPassantMoveGreen[0] = -1;
                    EnPassantMoveGreen[1] = -1;

                    if (oldX == Chessmans[x, y].CurrentX + 2 && oldY == Chessmans[x, y].CurrentY)
                    {
                        EnPassantMoveGreen[0] = x + 1;
                        EnPassantMoveGreen[1] = y;
                    }
                    if (oldX == Chessmans[x, y].CurrentX && oldY == Chessmans[x, y].CurrentY - 2)
                    {
                        EnPassantMoveGreen[0] = x;
                        EnPassantMoveGreen[1] = y - 1;
                    }
                }
            }
            #endregion
        }
        //END OF ALLOWED MOVES

        if (!IsAutoMove)
        {
            selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
        }

        BoardHighlights.Instance.Hidehighlights(); //whether you move or not, get rid of the highlights...

        selectedChessman = null; //unselect the piece
    } //END OF MOVEMENT

    private void turnLoop()
    {
        //TURN LOOP
        prevTurn = playerTurn;

        playerTurn++; //go to the next player's turn once the previous player makes a move.
        if (playerTurn > 4) //if player turn goes above player 4, then return to player 1's turn.
        {
            playerTurn = 1;
            if (blueout)
            {
                playerTurn = 2;
                if (yelout)
                {
                    playerTurn = 3;
                }
            }
        }

        // Debug.Log("It is Player " + playerTurn + "'s turn.");
        if (!isOnline)
        {
            GameText.text = "It is Player " + playerTurn + "'s turn.";
        }
    }
    private void turnControl()
    {
        //control turns
        #region
        if (playerTurn == 1 && blueout)
        {
            playerTurn = 2;
        }
        if (playerTurn == 2 && yelout)
        {
            playerTurn = 3;
        }
        if (playerTurn == 3 && redout)
        {
            playerTurn = 4;
        }
        if (playerTurn == 4 && greenout)
        {
            playerTurn = 1;
        }
        //
        if (prevTurn == 1 && blueout)
        {
            prevTurn = 4;
        }
        if (prevTurn == 2 && yelout)
        {
            prevTurn = 1;
        }
        if (prevTurn == 3 && redout)
        {
            prevTurn = 2;
        }
        if (prevTurn == 4 && greenout)
        {
            prevTurn = 3;
        }
        #endregion

        Debug.Log("It is Player " + playerTurn + "'s turn.");
        if (!isOnline)
        {
            GameText.text = "It is Player " + playerTurn + "'s turn.";
        }
    }

    private void blueResetFromCheck()
    {
        //Debug.Log("It is Player " + playerTurn + "'s turn.");
        if (previous.name == "pawn(Clone)")
        {
            SpawnChessman(0, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;

            resetManBlue = Chessmans[oldX, oldY];
        }
        if (previous.name == "Rook")
        {
            SpawnChessman(1, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;

            resetManBlue = Chessmans[oldX, oldY];
        }
        if (previous.name == "Knight")
        {
            SpawnChessman(2, oldX, oldY);

            resetManBlue = Chessmans[oldX, oldY];
        }
        if (previous.name == "Bishop")
        {
            SpawnChessman(3, oldX, oldY);

            resetManBlue = Chessmans[oldX, oldY];
        }
        if (previous.name == "Queen")
        {
            SpawnChessman(4, oldX, oldY);
            Chessmans[oldX, oldY].isChecked = prevCheck;
            Chessmans[oldX, oldY].isMated = prevMated;
            if (k1 == null)
            {
                k1 = previous;
            }
            else
            {
                q1 = previous;
            }

            resetManBlue = Chessmans[oldX, oldY];
        }
        if (previous.name == "King")
        {
            SpawnChessman(5, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;
            Chessmans[oldX, oldY].isChecked = prevCheck;
            Chessmans[oldX, oldY].isMated = prevMated;

            if (k1.name == "Queen")
            {
                q1 = k1;
                k1 = previous;
            }
            else if (k1 == null)
            {
                k1 = previous;
            }

            resetManBlue = Chessmans[oldX, oldY];
        }

        if (capName != null)
        {
            if (capColor == "Yellow")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(18, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(19, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(20, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(21, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(22, capX, capY);
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k2 == null)
                    {
                        k2 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q2 = Chessmans[capX, capY];
                    }

                }
                if (capName == "King")
                {
                    SpawnChessman(23, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k2.name == "Queen")
                    {
                        q2 = k2;
                        k2 = Chessmans[capX, capY];
                    }
                    else if (k2 == null)
                    {
                        k2 = Chessmans[capX, capY];
                    }
                }
                //take it off the prisoner list it was sent to
                foreach (string s in bluePrisonersYellow)
                {
                    if (s == capName)
                    {
                        bluePrisonersYellow.Remove(s);
                        break;
                    }
                }
            }

            if (capColor == "Red")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(12, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(13, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(14, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(15, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(16, capX, capY);
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k3 == null)
                    {
                        k3 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q3 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(17, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k3.name == "Queen")
                    {
                        q3 = k3;
                        k3 = Chessmans[capX, capY];
                    }
                    else if (k3 == null)
                    {
                        k3 = Chessmans[capX, capY];
                    }
                }

                //take it off the prisoner list it was sent to
                foreach (string s in bluePrisonersRed)
                {
                    if (s == capName)
                    {
                        bluePrisonersRed.Remove(s);
                        break;
                    }
                }
            }


            if (capColor == "Green")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(6, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(7, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(8, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(9, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(10, capX, capY);
                    Chessmans[oldX, oldY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k4 == null)
                    {
                        k4 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q4 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(11, capX, capY);
                    Chessmans[oldX, oldY].pawnDirection = capDir;
                    Chessmans[oldX, oldY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k4.name == "Queen")
                    {
                        q4 = k4;
                        k4 = Chessmans[capX, capY];
                    }
                    else if (k4 == null)
                    {
                        k4 = Chessmans[capX, capY];
                    }
                }

                //take it off the prisoner list it was sent to
                foreach (string s in bluePrisonersGreen)
                {
                    if (s == capName)
                    {
                        bluePrisonersGreen.Remove(s);
                        break;
                    }
                }
            }
        }
        activeChessman.Remove(previous.gameObject);
        Destroy(previous.gameObject);
        //previous = Chessmans[oldX, oldY];
        if (!greenout)
        {
            prevTurn = 4;
            previous = resetManGreen;
        }
        else
        {
            if (!redout)
            {
                prevTurn = 3;
                previous = resetManRed;
            }
            else
            {
                if (!yelout)
                {
                    prevTurn = 2;
                    previous = resetManYellow;
                }
            }
        }
    }
    private void yellowResetFromCheck()
    {
        //Debug.Log("It is Player " + playerTurn + "'s turn.");
        if (previous.name == "pawn(Clone)")
        {
            SpawnChessman(18, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;

            resetManYellow = Chessmans[oldX, oldY];
        }
        if (previous.name == "Rook")
        {
            SpawnChessman(19, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;

            resetManYellow = Chessmans[oldX, oldY];
        }
        if (previous.name == "Knight")
        {
            SpawnChessman(20, oldX, oldY);

            resetManYellow = Chessmans[oldX, oldY];
        }
        if (previous.name == "Bishop")
        {
            SpawnChessman(21, oldX, oldY);

            resetManYellow = Chessmans[oldX, oldY];
        }
        if (previous.name == "Queen")
        {
            SpawnChessman(22, oldX, oldY);
            Chessmans[oldX, oldY].isChecked = prevCheck;
            Chessmans[oldX, oldY].isMated = prevMated;
            if (k2 == null)
            {
                k2 = previous;
            }
            else
            {
                q2 = previous;
            }

            resetManYellow = Chessmans[oldX, oldY];
        }
        if (previous.name == "King")
        {
            SpawnChessman(23, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;
            Chessmans[oldX, oldY].isChecked = prevCheck;
            Chessmans[oldX, oldY].isMated = prevMated;
            if (k2.name == "Queen")
            {
                q2 = k2;
                k2 = previous;
            }
            else if (k2 == null)
            {
                k2 = previous;
            }

            resetManYellow = Chessmans[oldX, oldY];
        }

        if (capName != null)
        {
            if (capColor == "Blue")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(0, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(1, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(2, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(3, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(4, capX, capY);
                    Chessmans[oldX, oldY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k1 == null)
                    {
                        k1 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q1 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(5, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k1.name == "Queen")
                    {
                        q1 = k1;
                        k1 = Chessmans[capX, capY];
                    }
                    else if (k1 == null)
                    {
                        k1 = Chessmans[capX, capY];
                    }
                }

                foreach (string s in yelPrisonersBlue)
                {
                    if (s == capName)
                    {
                        yelPrisonersBlue.Remove(s);
                        break;
                    }
                }
            }

            if (capColor == "Red")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(12, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(13, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(14, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(15, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(16, capX, capY);
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k3 == null)
                    {
                        k3 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q3 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(17, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k3.name == "Queen")
                    {
                        q3 = k3;
                        k3 = Chessmans[capX, capY];
                    }
                    else if (k3 == null)
                    {
                        k3 = Chessmans[capX, capY];
                    }
                }
                //take it off the prisoner list it was sent to
                foreach (string s in yelPrisonersRed)
                {
                    if (s == capName)
                    {
                        yelPrisonersRed.Remove(s);
                        break;
                    }
                }
            }


            if (capColor == "Green")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(6, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(7, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;

                }
                if (capName == "Knight")
                {
                    SpawnChessman(8, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(9, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(10, capX, capY);
                    Chessmans[oldX, oldY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k4 == null)
                    {
                        k4 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q4 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(11, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k4.name == "Queen")
                    {
                        q4 = k4;
                        k4 = Chessmans[capX, capY];
                    }
                    else if (k4 == null)
                    {
                        k4 = Chessmans[capX, capY];
                    }
                }
                foreach (string s in yelPrisonersGreen)
                {
                    if (s == capName)
                    {
                        yelPrisonersGreen.Remove(s);
                        break;
                    }
                }
            }
        }

        activeChessman.Remove(previous.gameObject);
        Destroy(previous.gameObject);
        //previous = Chessmans[oldX, oldY];
        if (!blueout)
        {
            prevTurn = 1;
            previous = resetManBlue;
        }
        else
        {
            if (!greenout)
            {
                prevTurn = 4;
                previous = resetManGreen;
            }
            else
            {
                if (!redout)
                {
                    prevTurn = 3;
                    previous = resetManRed;
                }
            }
        }
    }
    private void redResetFromCheck()
    {
        //Debug.Log("It is Player " + playerTurn + "'s turn.");
        if (previous.name == "pawn(Clone)")
        {
            SpawnChessman(12, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;

            resetManRed = Chessmans[oldX, oldY];
        }
        if (previous.name == "Rook")
        {
            SpawnChessman(13, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;

            resetManRed = Chessmans[oldX, oldY];
        }
        if (previous.name == "Knight")
        {
            SpawnChessman(14, oldX, oldY);

            resetManRed = Chessmans[oldX, oldY];
        }
        if (previous.name == "Bishop")
        {
            SpawnChessman(15, oldX, oldY);

            resetManRed = Chessmans[oldX, oldY];
        }
        if (previous.name == "Queen")
        {
            SpawnChessman(16, oldX, oldY);
            Chessmans[oldX, oldY].isChecked = prevCheck;
            Chessmans[oldX, oldY].isMated = prevMated;
            if (k3 == null)
            {
                k3 = previous;
            }
            else
            {
                q3 = previous;
            }

            resetManRed = Chessmans[oldX, oldY];
        }
        if (previous.name == "King")
        {
            SpawnChessman(17, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;
            Chessmans[oldX, oldY].isChecked = prevCheck;
            Chessmans[oldX, oldY].isMated = prevMated;
            if (k3.name == "Queen")
            {
                q3 = k3;
                k3 = previous;
            }
            else if (k3 == null)
            {
                k3 = previous;
            }

            resetManRed = Chessmans[oldX, oldY];
        }

        if (capName != null)
        {
            if (capColor == "Yellow")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(18, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(19, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(20, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(21, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(22, capX, capY);
                    Chessmans[oldX, oldY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k2 == null)
                    {
                        k2 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q2 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(23, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k2.name == "Queen")
                    {
                        q2 = k2;
                        k2 = Chessmans[capX, capY];
                    }
                    else if (k2 == null)
                    {
                        k2 = Chessmans[capX, capY];
                    }
                }

                //take it off the prisoner list it was sent to
                foreach (string s in redPrisonersYellow)
                {
                    if (s == capName)
                    {
                        redPrisonersYellow.Remove(s);
                        break;
                    }
                }
            }

            if (capColor == "Green")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(6, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(7, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(8, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(9, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(10, capX, capY);
                    Chessmans[oldX, oldY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k4 == null)
                    {
                        k4 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q4 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(11, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k4.name == "Queen")
                    {
                        q4 = k4;
                        k4 = Chessmans[capX, capY];
                    }
                    else if (k4 == null)
                    {
                        k4 = Chessmans[capX, capY];
                    }
                }

                //take it off the prisoner list it was sent to
                foreach (string s in redPrisonersGreen)
                {
                    if (s == capName)
                    {
                        redPrisonersGreen.Remove(s);
                        break;
                    }
                }
            }


            if (capColor == "Blue")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(0, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(1, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(2, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(3, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(4, capX, capY);
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k1 == null)
                    {
                        k1 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q1 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(5, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k1.name == "Queen")
                    {
                        q1 = k1;
                        k1 = Chessmans[capX, capY];
                    }
                    else if (k1 == null)
                    {
                        k1 = Chessmans[capX, capY];
                    }
                }

                foreach (string s in redPrisonersBlue)
                {
                    if (s == capName)
                    {
                        redPrisonersBlue.Remove(s);
                        break;
                    }
                }
            }
        }

        activeChessman.Remove(previous.gameObject);
        Destroy(previous.gameObject);
        //previous = Chessmans[oldX, oldY];
        if (!yelout)
        {
            prevTurn = 2;
            previous = resetManYellow;
        }
        else
        {
            if (!blueout)
            {
                prevTurn = 1;
                previous = resetManBlue;
            }
            else
            {
                if (!greenout)
                {
                    prevTurn = 4;
                    previous = resetManGreen;
                }
            }
        }
    }
    private void greenResetFromCheck()
    {
        //Debug.Log("It is Player " + playerTurn + "'s turn.");
        if (previous.name == "pawn(Clone)")
        {
            SpawnChessman(6, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;

            resetManGreen = Chessmans[oldX, oldY];
        }
        if (previous.name == "Rook")
        {
            SpawnChessman(7, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;

            resetManGreen = Chessmans[oldX, oldY];
        }
        if (previous.name == "Knight")
        {
            SpawnChessman(8, oldX, oldY);

            resetManGreen = Chessmans[oldX, oldY];
        }
        if (previous.name == "Bishop")
        {
            SpawnChessman(9, oldX, oldY);

            resetManGreen = Chessmans[oldX, oldY];
        }
        if (previous.name == "Queen")
        {
            SpawnChessman(10, oldX, oldY);
            Chessmans[oldX, oldY].isChecked = prevCheck;
            Chessmans[oldX, oldY].isMated = prevMated;
            if (k4 == null)
            {
                k4 = previous;
            }
            else
            {
                q4 = previous;
            }

            resetManGreen = Chessmans[oldX, oldY];
        }
        if (previous.name == "King")
        {
            SpawnChessman(11, oldX, oldY);
            Chessmans[oldX, oldY].pawnDirection = prevDir;
            Chessmans[oldX, oldY].isChecked = prevCheck;
            Chessmans[oldX, oldY].isMated = prevMated;
            if (k4.name == "Queen")
            {
                q4 = k4;
                k4 = previous;
            }
            else if (k4 == null)
            {
                k4 = previous;
            }

            resetManGreen = Chessmans[oldX, oldY];
        }

        if (capName != null)
        {
            if (capColor == "Blue")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(0, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(1, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(2, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(3, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(4, capX, capY);
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k1 == null)
                    {
                        k1 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q1 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(5, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k1.name == "Queen")
                    {
                        q1 = k1;
                        k1 = Chessmans[capX, capY];
                    }
                    else if (k1 == null)
                    {
                        k1 = Chessmans[capX, capY];
                    }
                }

                foreach (string s in greenPrisonersBlue)
                {
                    if (s == capName)
                    {
                        greenPrisonersBlue.Remove(s);
                        break;
                    }
                }

            }

            if (capColor == "Red")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(12, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(13, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(14, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(15, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(16, capX, capY);
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k3 == null)
                    {
                        k3 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q3 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(17, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k3.name == "Queen")
                    {
                        q3 = k3;
                        k3 = Chessmans[capX, capY];
                    }
                    else if (k3 == null)
                    {
                        k3 = Chessmans[capX, capY];
                    }
                }

                //take it off the prisoner list it was sent to
                foreach (string s in greenPrisonersRed)
                {
                    if (s == capName)
                    {
                        greenPrisonersRed.Remove(s);
                        break;
                    }
                }
            }

            if (capColor == "Yellow")
            {
                if (capName == "pawn(Clone)")
                {
                    SpawnChessman(18, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Rook")
                {
                    SpawnChessman(19, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                }
                if (capName == "Knight")
                {
                    SpawnChessman(20, capX, capY);
                }
                if (capName == "Bishop")
                {
                    SpawnChessman(21, capX, capY);
                }
                if (capName == "Queen")
                {
                    SpawnChessman(22, capX, capY);
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k2 == null)
                    {
                        k2 = Chessmans[capX, capY];
                    }
                    else
                    {
                        q2 = Chessmans[capX, capY];
                    }
                }
                if (capName == "King")
                {
                    SpawnChessman(23, capX, capY);
                    Chessmans[capX, capY].pawnDirection = capDir;
                    Chessmans[capX, capY].isChecked = capCheck;
                    Chessmans[capX, capY].isMated = capMated;

                    if (k2.name == "Queen")
                    {
                        q2 = k2;
                        k2 = Chessmans[capX, capY];
                    }
                    else if (k2 == null)
                    {
                        k2 = Chessmans[capX, capY];
                    }
                }

                //take it off the prisoner list it was sent to
                foreach (string s in greenPrisonersYellow)
                {
                    if (s == capName)
                    {
                        greenPrisonersYellow.Remove(s);
                        break;
                    }
                }
            }
        }

        activeChessman.Remove(previous.gameObject);
        Destroy(previous.gameObject);
        //previous = Chessmans[oldX, oldY];
        if (!redout)
        {
            prevTurn = 3;
            previous = resetManRed;
        }
        else
        {
            if (!yelout)
            {
                prevTurn = 2;
                previous = resetManYellow;
            }
            else
            {
                if (!blueout)
                {
                    prevTurn = 1;
                    previous = resetManBlue;
                }
            }
        }
    }

    private void illegalMovement()
    {
        //checks to see if the PREVIOUS move 9the one just made) was legal.
        if (k1 != null)
        {
            if (prevTurn == 1)
            {
                if (!k1.isMated && !IsStalemate)
                {
                    if (k1.isChecked)
                    {
                        playerTurn = 1;
                        prevTurn = 4;
                        blueResetFromCheck();
                        //CheckMoves();
                        //notChecked();
                        Debug.Log("Player 1 cannot remain in check!!!");
                        GameText.text = "You cannot move into or remain in check!";
                        valid = false;
                        return;
                    }
                    else if (resetManBlue)
                    {
                        if (resetManBlue.CurrentX == lastBlueX && resetManBlue.CurrentY == lastBlueY)
                        {
                            playerTurn = 1;
                            prevTurn = 4;
                            blueResetFromCheck();
                            //CheckMoves();
                            //notChecked();
                            Debug.Log("Only cowards retreat! (You cannot move last turn's piece from where it came from.)");
                            GameText.text = "Only cowards retreat! (You cannot move last turn's piece from where it came from.)";
                            valid = false;
                            return;
                        }
                    }
                }
            }
        }

        if (k2 != null)
        {
            if (prevTurn == 2)
            {
                if (!k2.isMated && !IsStalemate)
                {
                    if (k2.isChecked)
                    {
                        playerTurn = 2;
                        prevTurn = 1;
                        yellowResetFromCheck();
                        //CheckMoves();
                        //notChecked();
                        Debug.Log("Player 2 cannot remain in check!!!");
                        GameText.text = "You cannot move into or remain in check!";
                        valid = false;
                        return;
                    }
                    else if (resetManYellow)
                    {
                        if (resetManYellow.CurrentX == lastYellowX && resetManYellow.CurrentY == lastYellowY)
                        {
                            playerTurn = 2;
                            prevTurn = 1;
                            yellowResetFromCheck();
                            //CheckMoves();
                            //notChecked();
                            Debug.Log("Only cowards retreat! (You cannot move last turn's piece from where it came from.)");
                            GameText.text = "Only cowards retreat! (You cannot move last turn's piece from where it came from.)";
                            valid = false;
                            return;
                        }
                    }
                }
            }
        }

        if (k3 != null)
        {
            if (prevTurn == 3)
            {
                if (!k3.isMated && !IsStalemate)
                {
                    if (k3.isChecked)
                    {
                        playerTurn = 3;
                        prevTurn = 2;
                        redResetFromCheck();
                        //CheckMoves();
                        //notChecked();
                        Debug.Log("Player 3 cannot remain in check");
                        GameText.text = "You cannot move into or remain in check!";
                        valid = false;
                        return;
                    }
                    else if (resetManRed)
                    {
                        if (resetManRed.CurrentX == lastRedX && resetManRed.CurrentY == lastRedY)
                        {
                            playerTurn = 3;
                            prevTurn = 2;
                            redResetFromCheck();
                            //CheckMoves();
                            //notChecked();
                            Debug.Log("Only cowards retreat! (You cannot move last turn's piece from where it came from.)");
                            GameText.text = "Only cowards retreat! (You cannot move last turn's piece from where it came from.)";
                            valid = false;
                            return;
                        }
                    }

                }
            }
        }

        if (k4 != null)
        {
            if (prevTurn == 4)
            {
                if (!k4.isMated && !IsStalemate)
                {
                    if (k4.isChecked)
                    {
                        playerTurn = 4;
                        prevTurn = 3;
                        greenResetFromCheck();
                        // CheckMoves();
                        //notChecked();
                        Debug.Log("Player 4 cannot remain in check");
                        GameText.text = "You cannot move into or remain in check!";
                        valid = false;
                        return;
                    }
                    else if (resetManGreen)
                    {
                        if (resetManGreen.CurrentX == lastGreenX && resetManGreen.CurrentY == lastGreenY)
                        {
                            playerTurn = 4;
                            prevTurn = 3;
                            greenResetFromCheck();
                            //CheckMoves();
                            //notChecked();
                            Debug.Log("Only cowards retreat! (You cannot move last turn's piece from where it came from.)");
                            GameText.text = "Only cowards retreat! (You cannot move last turn's piece from where it came from.)";
                            valid = false;
                            return;
                        }
                    }
                }
            }
        }

        if (playerTurn == 1 && isBlueAI)
        {
            ShouldAIMove = true;
        }
        else if (playerTurn == 2 && isYellowAI)
        {
            ShouldAIMove = true;
        }
        else if (playerTurn == 3 && isRedAI)
        {
            ShouldAIMove = true;
        }
        else if (playerTurn == 4 && isGreenAI)
        {
            ShouldAIMove = true;
        }
    }
    private void promoteQueen()
    {
        if (k1 == null)
        {
            if (q1)
            {
                k1 = q1;
                q1 = null;
            }
            else
            {
                k1 = null;
                Debug.Log("Blue is out");
                // GameText.text = "Blue is out";
                blueout = true;
            }
        }
        if (k2 == null)
        {
            if (q2)
            {
                k2 = q2;
                q2 = null;
            }
            else
            {
                k2 = null;
                Debug.Log("Yellow is out");
                //GameText.text = "Yellow is out";
                yelout = true;
            }
        }
        if (k3 == null)
        {
            if (q3)
            {
                k3 = q3;
                q3 = null;
            }
            else
            {
                k3 = null;
                Debug.Log("Red is out");
                //GameText.text = "Red is out";
                redout = true;
            }
        }
        if (k4 == null)
        {
            if (q4)
            {
                k4 = q4;
                q4 = null;
            }
            else
            {
                k4 = null;
                Debug.Log("Green is out");
                // GameText.text = "Green is out";
                greenout = true;
            }
        }
    }
    private void storeLastMove()
    {
        //get the individual piece's previous data for the reversal.
        if (prevTurn == 1)
        {
            lastBlueX = oldX;
            lastBlueY = oldY;

            resetManBlue = previous;
        }
        if (prevTurn == 2)
        {
            lastYellowX = oldX;
            lastYellowY = oldY;

            resetManYellow = previous;
        }
        if (prevTurn == 3)
        {
            lastRedX = oldX;
            lastRedY = oldY;

            resetManRed = previous;
        }
        if (prevTurn == 4)
        {
            lastGreenX = oldX;
            lastGreenY = oldY;

            resetManGreen = previous;
        }
    }

    //Board logic
    public void SpawnChessman(int index, int x, int y)
    {
        #region Set For  Online because we're skipping spawnallchessman in online:
        if (Chessmans == null)
        {
            Chessmans = new Chessman[12, 12];
        }
        if (activeChessman == null)
        {
            activeChessman = new List<GameObject>();
        }
        #endregion


        GameObject go;/* = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), orientation) as GameObject; //spawn the associated chessman, with a position and rotation

        go.transform.SetParent(transform); //make it a child of the object this script is attached to.  in this case, a piece is a child of the board.
        Chessmans[x, y] = go.GetComponent<Chessman>(); //apply the variable to the gameobject's componet
        Chessmans[x, y].SetPosition(x, y); //track/set its own position
        activeChessman.Add(go); //add it to the list of gameobject that are in the game
        go.tag = "piece";
        */

        //RANK is given and used during the Jailbreak Method.
        if (index == 5 || index == 23 || index == 17 || index == 11)
        {
            go = Instantiate(New_Chessman_Prefabs[0], GetTileCenter(x, y), orientation) as GameObject;
            if (index == 5)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[0];
                go.GetComponent<Chessman>().isBlue = true;
                k1 = go.GetComponent<Chessman>();
            }
            if (index == 23)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[1];
                go.GetComponent<Chessman>().isYellow = true;
                k2 = go.GetComponent<Chessman>();
            }
            if (index == 17)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[2];
                go.GetComponent<Chessman>().isRed = true;
                k3 = go.GetComponent<Chessman>();
            }
            if (index == 11)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[3];
                go.GetComponent<Chessman>().isGreen = true;
                k4 = go.GetComponent<Chessman>();
            }
            go.name = "King";
            go.GetComponent<Chessman>().Rank = 5;
            go.GetComponent<Chessman>().index = index; //this is important for online gameplay for tracking piece data

            go.transform.SetParent(transform); //make it a child of the object this script is attached to.  in this case, a piece is a child of the board.
            Chessmans[x, y] = go.GetComponent<Chessman>(); //apply the variable to the gameobject's componet
            Chessmans[x, y].SetPosition(x, y); //track/set its own position
            activeChessman.Add(go); //add it to the list of gameobject that are in the game
            go.tag = "piece";
        }

        if (index == 4 || index == 22 || index == 16 || index == 10)
        {
            go = Instantiate(New_Chessman_Prefabs[1], GetTileCenter(x, y), orientation) as GameObject;
            if (index == 4)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[0];
                go.GetComponent<Chessman>().isBlue = true;
                q1 = go.GetComponent<Chessman>();
            }
            if (index == 22)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[1];
                go.GetComponent<Chessman>().isYellow = true;
                q2 = go.GetComponent<Chessman>();
            }
            if (index == 16)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[2];
                go.GetComponent<Chessman>().isRed = true;
                q3 = go.GetComponent<Chessman>();
            }
            if (index == 10)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[3];
                go.GetComponent<Chessman>().isGreen = true;
                q4 = go.GetComponent<Chessman>();
            }
            go.name = "Queen";
            go.GetComponent<Chessman>().Rank = 4;
            go.GetComponent<Chessman>().index = index; //this is important for online gameplay for tracking piece data

            go.transform.SetParent(transform); //make it a child of the object this script is attached to.  in this case, a piece is a child of the board.
            Chessmans[x, y] = go.GetComponent<Chessman>(); //apply the variable to the gameobject's componet
            Chessmans[x, y].SetPosition(x, y); //track/set its own position
            activeChessman.Add(go); //add it to the list of gameobject that are in the game
            go.tag = "piece";
        }

        if (index == 3 || index == 21 || index == 15 || index == 9)
        {
            go = Instantiate(New_Chessman_Prefabs[2], GetTileCenter(x, y), orientation) as GameObject;
            if (index == 3)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[0];
                go.GetComponent<Chessman>().isBlue = true;
            }
            if (index == 21)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[1];
                go.GetComponent<Chessman>().isYellow = true;
            }
            if (index == 15)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[2];
                go.GetComponent<Chessman>().isRed = true;
            }
            if (index == 9)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[3];
                go.GetComponent<Chessman>().isGreen = true;
            }
            go.name = "Bishop";
            go.GetComponent<Chessman>().Rank = 3;
            go.GetComponent<Chessman>().index = index; //this is important for online gameplay for tracking piece data

            go.transform.SetParent(transform); //make it a child of the object this script is attached to.  in this case, a piece is a child of the board.
            Chessmans[x, y] = go.GetComponent<Chessman>(); //apply the variable to the gameobject's componet
            Chessmans[x, y].SetPosition(x, y); //track/set its own position
            activeChessman.Add(go); //add it to the list of gameobject that are in the game
            go.tag = "piece";
        }

        if (index == 2 || index == 20 || index == 14 || index == 8)
        {
            go = Instantiate(New_Chessman_Prefabs[3], GetTileCenter(x, y), orientation) as GameObject;
            if (index == 2)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[0];
                go.GetComponent<Chessman>().isBlue = true;
            }
            if (index == 20)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[1];
                go.GetComponent<Chessman>().isYellow = true;
            }
            if (index == 14)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[2];
                go.GetComponent<Chessman>().isRed = true;
            }
            if (index == 8)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[3];
                go.GetComponent<Chessman>().isGreen = true;
            }
            go.name = "Knight";
            go.GetComponent<Chessman>().Rank = 2;
            go.GetComponent<Chessman>().index = index; //this is important for online gameplay for tracking piece data

            go.transform.SetParent(transform); //make it a child of the object this script is attached to.  in this case, a piece is a child of the board.
            Chessmans[x, y] = go.GetComponent<Chessman>(); //apply the variable to the gameobject's componet
            Chessmans[x, y].SetPosition(x, y); //track/set its own position
            activeChessman.Add(go); //add it to the list of gameobject that are in the game
            go.tag = "piece";
        }

        if (index == 1 || index == 19 || index == 13 || index == 7)
        {
            go = Instantiate(New_Chessman_Prefabs[4], GetTileCenter(x, y), orientation) as GameObject;
            if (index == 1)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[0];
                go.GetComponent<Chessman>().isBlue = true;
            }
            if (index == 19)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[1];
                go.GetComponent<Chessman>().isYellow = true;
            }
            if (index == 13)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[2];
                go.GetComponent<Chessman>().isRed = true;
            }
            if (index == 7)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[3];
                go.GetComponent<Chessman>().isGreen = true;
            }
            go.name = "Rook";
            go.GetComponent<Chessman>().Rank = 1;
            go.GetComponent<Chessman>().index = index; //this is important for online gameplay for tracking piece data

            go.transform.SetParent(transform); //make it a child of the object this script is attached to.  in this case, a piece is a child of the board.
            Chessmans[x, y] = go.GetComponent<Chessman>(); //apply the variable to the gameobject's componet
            Chessmans[x, y].SetPosition(x, y); //track/set its own position
            activeChessman.Add(go); //add it to the list of gameobject that are in the game
            go.tag = "piece";
        }

        if (index == 0 || index == 18 || index == 12 || index == 6)
        {
            go = Instantiate(New_Chessman_Prefabs[5], GetTileCenter(x, y), orientation) as GameObject;
            if (index == 0)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[0];
                go.GetComponent<Chessman>().isBlue = true;
            }
            if (index == 18)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[1];
                go.GetComponent<Chessman>().isYellow = true;
            }
            if (index == 12)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[2];
                go.GetComponent<Chessman>().isRed = true;
            }
            if (index == 6)
            {
                go.GetComponent<MeshRenderer>().material = Team_Materials[3];
                go.GetComponent<Chessman>().isGreen = true;
            }
            go.name = "pawn(Clone)";
            go.GetComponent<Chessman>().Rank = 0;
            go.GetComponent<Chessman>().index = index; //this is important for online gameplay for tracking piece data

            go.transform.SetParent(transform); //make it a child of the object this script is attached to.  in this case, a piece is a child of the board.
            Chessmans[x, y] = go.GetComponent<Chessman>(); //apply the variable to the gameobject's componet
            Chessmans[x, y].SetPosition(x, y); //track/set its own position
            activeChessman.Add(go); //add it to the list of gameobject that are in the game
            go.tag = "piece";
        }
    }
    public void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[12, 12];

        if (!ShouldRigBoardState)
        {
            //METHOD(gameObjectIndex, x, y)
            //Blue Team Spawn
            SpawnChessman(0, 0, 4); //pawn
            SpawnChessman(0, 1, 3);
            SpawnChessman(0, 2, 2);
            SpawnChessman(0, 3, 1);
            SpawnChessman(0, 4, 0);
            SpawnChessman(0, 1, 2);
            SpawnChessman(0, 2, 1);
            SpawnChessman(1, 0, 3); //rooks
            SpawnChessman(1, 3, 0);
            SpawnChessman(2, 0, 2); //knights
            SpawnChessman(2, 2, 0);
            SpawnChessman(3, 0, 1); // bishops
            SpawnChessman(3, 1, 0);
            SpawnChessman(5, 0, 0);  //king
            SpawnChessman(4, 1, 1); //queen

            //Green Team Spawn
            SpawnChessman(6, 7, 0); //pawn
            SpawnChessman(6, 8, 1);
            SpawnChessman(6, 9, 2);
            SpawnChessman(6, 10, 3);
            SpawnChessman(6, 11, 4);
            SpawnChessman(6, 9, 1);
            SpawnChessman(6, 10, 2);
            SpawnChessman(7, 8, 0); //rooks
            SpawnChessman(7, 11, 3);
            SpawnChessman(8, 9, 0); //kights
            SpawnChessman(8, 11, 2);
            SpawnChessman(9, 10, 0); //bishops
            SpawnChessman(9, 11, 1);
            SpawnChessman(11, 11, 0); //king
            SpawnChessman(10, 10, 1); //queen

            //Red Team Spawn
            SpawnChessman(12, 11, 7); //pawn
            SpawnChessman(12, 10, 8);
            SpawnChessman(12, 9, 9);
            SpawnChessman(12, 8, 10);
            SpawnChessman(12, 7, 11);
            SpawnChessman(12, 10, 9);
            SpawnChessman(12, 9, 10);
            SpawnChessman(13, 8, 11); //rooks
            SpawnChessman(13, 11, 8);
            SpawnChessman(14, 9, 11); //knights
            SpawnChessman(14, 11, 9);
            SpawnChessman(15, 10, 11); //bishops
            SpawnChessman(15, 11, 10);
            SpawnChessman(17, 11, 11); //king
            SpawnChessman(16, 10, 10); //queen

            //Yellow Team Spawn
            SpawnChessman(18, 0, 7); //pawn
            SpawnChessman(18, 1, 8);
            SpawnChessman(18, 2, 9);
            SpawnChessman(18, 3, 10);
            SpawnChessman(18, 4, 11);
            SpawnChessman(18, 1, 9);
            SpawnChessman(18, 2, 10);
            SpawnChessman(19, 0, 8); //rooks
            SpawnChessman(19, 3, 11);
            SpawnChessman(20, 0, 9); //knights
            SpawnChessman(20, 2, 11);
            SpawnChessman(21, 0, 10); //bishops
            SpawnChessman(21, 1, 11);
            SpawnChessman(23, 0, 11); //king
            SpawnChessman(22, 1, 10); //queen        
        }
        else
        {
            #region rig board state for an easy STALEMATE agains the blue king
            SpawnChessman(5, 1, 0);  // blue king in his corner - move once to the right.
            SpawnChessman(22, 2, 10); //yellow queen, a space to the right of their starting position - move all the way to the left: blocking the blue king's left movements
            SpawnChessman(16, 9, 11); //red queen, a space to teh left abd up of their start so blue and yellow are not in check right away from it being here - move to block the king's right movements.
            SpawnChessman(10, 11, 3); //nqueen, a space to the right and up to avoid check everywhere - move to blok the blue king's forward moves.
            //SpawnChessman(18, 1, 3);
            #endregion

            #region Fill All Prisons
            //All prisons filled will all possible pieces, even if they're duplicates.  Just need full prisons to test.
            //bluePrisonersRed.Add("Queen");
            //bluePrisonersRed.Add("Bishop");
            //bluePrisonersRed.Add("Knight");
            //bluePrisonersRed.Add("Rook");
            //bluePrisonersYellow.Add("Queen");
            //bluePrisonersYellow.Add("Bishop");
            //bluePrisonersYellow.Add("Knight");
            //bluePrisonersYellow.Add("Rook");
            //bluePrisonersGreen.Add("Queen");
            //bluePrisonersGreen.Add("Bishop");
            //bluePrisonersGreen.Add("Knight");
            //bluePrisonersGreen.Add("Rook");

            //yelPrisonersRed.Add("Queen");
            //yelPrisonersRed.Add("Bishop");
            //yelPrisonersRed.Add("Knight");
            //yelPrisonersRed.Add("Rook");
            //yelPrisonersBlue.Add("Queen");
            //yelPrisonersBlue.Add("Bishop");
            //yelPrisonersBlue.Add("Knight");
            //yelPrisonersBlue.Add("Rook");
            //yelPrisonersGreen.Add("Queen");
            //yelPrisonersGreen.Add("Bishop");
            //yelPrisonersGreen.Add("Knight");
            //yelPrisonersGreen.Add("Rook");

            //redPrisonersYellow.Add("Queen");
            //redPrisonersYellow.Add("Bishop");
            //redPrisonersYellow.Add("Knight");
            //redPrisonersYellow.Add("Rook");
            //redPrisonersBlue.Add("Queen");
            //redPrisonersBlue.Add("Bishop");
            //redPrisonersBlue.Add("Knight");
            //redPrisonersBlue.Add("Rook");
            //redPrisonersGreen.Add("Queen");
            //redPrisonersGreen.Add("Bishop");
            //redPrisonersGreen.Add("Knight");
            //redPrisonersGreen.Add("Rook");

            //greenPrisonersYellow.Add("Queen");
            //greenPrisonersYellow.Add("Bishop");
            //greenPrisonersYellow.Add("Knight");
            //greenPrisonersYellow.Add("Rook");
            //greenPrisonersBlue.Add("Queen");
            //greenPrisonersBlue.Add("Bishop");
            //greenPrisonersBlue.Add("Knight");
            //greenPrisonersBlue.Add("Rook");
            //greenPrisonersRed.Add("Queen");
            //greenPrisonersRed.Add("Bishop");
            //greenPrisonersRed.Add("Knight");
            //greenPrisonersRed.Add("Rook");
            #endregion

            #region This is to Rig for Prison Break testing.
            //SpawnChessman(5, 0, 10); //Blue at Yellow
            //SpawnChessman(5, 10, 10); //Blue at Red
            //SpawnChessman(5, 10, 0); //Blue at Green

            //SpawnChessman(23, 10, 11); //Yellow at Red
            //SpawnChessman(23, 10, 1); //Yellow at Green
            //SpawnChessman(23, 0, 1); //Yellow at Blue

            //SpawnChessman(17, 11, 1); //Red at Green
            //SpawnChessman(17, 1, 1); //Red at Blue
            //SpawnChessman(17, 1, 11); //Red at Yellow

            //SpawnChessman(11, 1, 0); //Green at Blue
            //SpawnChessman(11, 1, 10); //Green at Yellow
            //SpawnChessman(11, 11, 10); //Green at Red

            //k1.pawnDirection = 1;
            //k2.pawnDirection = 1;
            //k3.pawnDirection = 1;
            //k4.pawnDirection = 1;
            #endregion

            #region This is to Rig for Prison Exchange testing
            //SpawnChessman(5, 0, 0);
            //SpawnChessman(23, 0, 11);
            //SpawnChessman(17, 11, 11);
            //SpawnChessman(11, 11, 0);
            //k1.pawnDirection = 1;
            //k2.pawnDirection = 1;
            //k3.pawnDirection = 1;
            //k4.pawnDirection = 1;

            //SpawnChessman(0, 2, 10); //blue at yellow
            //Chessmans[2, 10].pawnDirection = 1; // left
            //SpawnChessman(0, 9, 10); // blue at red north
            //Chessmans[9, 10].pawnDirection = 1;
            //SpawnChessman(0, 10, 9); // blue at red east
            //Chessmans[10, 9].pawnDirection = 2; //right
            //SpawnChessman(0, 10, 2); // blue at green
            //Chessmans[10, 2].pawnDirection = 2;
            #endregion

            #region This is to Rig for Check/IllegalMoves Testing
            ////Kings
            //SpawnChessman(5, 1, 0);
            //SpawnChessman(23, 0, 9);
            //SpawnChessman(17, 11, 11);
            //SpawnChessman(11, 11, 0);
            //k1.pawnDirection = 1;
            //k2.pawnDirection = 1;
            //k3.pawnDirection = 1;
            //k4.pawnDirection = 1;

            ////Others
            ////Blue
            //SpawnChessman(4, 0, 1);
            //SpawnChessman(1, 6, 0); // Move up on X (make it 7)
            //SpawnChessman(0, 0, 4);
            //SpawnChessman(0, 1, 3);
            //SpawnChessman(0, 2, 3);
            //SpawnChessman(0, 4, 2);
            ////Yellow
            //SpawnChessman(22, 7, 3); // Move up on X (make it 8)
            //SpawnChessman(21, 1, 11);
            //SpawnChessman(20, 2, 11);
            //SpawnChessman(18, 1, 8);
            //SpawnChessman(18, 2, 9);
            //SpawnChessman(18, 2, 10);
            //SpawnChessman(18, 3, 10);
            //SpawnChessman(18, 3, 8);
            ////Red
            //SpawnChessman(16, 10, 10);
            //SpawnChessman(15, 11, 10);
            //SpawnChessman(14, 11, 9);
            //SpawnChessman(13, 11, 8);
            //SpawnChessman(12, 10, 9);
            //SpawnChessman(12, 10, 8); // Move down on X (make it 9)
            ////Green
            //SpawnChessman(9, 10, 0);
            //SpawnChessman(8, 9, 0);
            //SpawnChessman(6, 11, 4);
            #endregion
        }
    }
    private Vector3 GetTileCenter(int x, int y) //figure out where a piece should be positioned with a tile; get the exact center
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        origin.y = 0.0f;
        return origin;
    }
    private void DrawChessboard() //method to draw the board
    {
        Vector3 widthLine = Vector3.right * 12; //12 tiles wide
        Vector3 heightLine = Vector3.forward * 12; //12 tiles long

        for (int i = 0; i <= 12; i++) //loop through all the width tiles
        {
            Vector3 start = Vector3.forward * i; //determine where we should start drawing
            Debug.DrawLine(start, start + widthLine); //draw the tiles from the start to the end points

            for (int j = 0; j <= 12; j++) //loop through the height tiles
            {
                start = Vector3.right * j; //determine start point
                Debug.DrawLine(start, start + heightLine); //draw from start to end point
            }
        }

        //Draw the selection as an 'X' - this is based on info from the Update Selection Method
        if (selectionX >= 0 && selectionY >= 0)
        {
            // "/"
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            // "\"
            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }
    private void UpdateSelection() // the 'X' when the mouse hovers a tile
    {
        //if there is no camera then return out of this method
        if (!Camera.main)
            return;

        //if the mouse is positioned over a tile, then get that tile's coordinates.
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            //make the coordinates an int, rather than a float.
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            //otherwise, keep the selection off the board with ints that do not apply to board position.
            selectionX = -1;
            selectionY = -1;
        }


    }

    //Called after a player moves to see if a king has been put in check.
    private void CheckMoves()
    {

        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");

        if (k1 != null)
        {
            foreach (GameObject p in allPieces) // for each peice on the board that is active...
            {
                if (p.GetComponent<Chessman>().isBlue)  //SEE IF BLUE HAS ANYBDY IN  CHECK/////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    allowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //then assign their possible moves to allowedMoves

                    //see if the piece has any movements
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (allowedMoves[i, j])
                            {
                                bool isRetreatTile = false;
                                bool isRetreatPiece = false;

                                if (lastBlueX == i && lastBlueY == j)
                                {
                                    isRetreatTile = true;
                                }

                                if (resetManBlue != null)
                                {
                                    if (Chessmans[p.GetComponent<Chessman>().CurrentX, p.GetComponent<Chessman>().CurrentY] == Chessmans[resetManBlue.CurrentX, resetManBlue.CurrentY])
                                    {
                                        isRetreatPiece = true;
                                    }
                                }

                                bool ShouldEvaluate = true;

                                if (isRetreatPiece && isRetreatTile)
                                {
                                    ShouldEvaluate = false;
                                }

                                if (ShouldEvaluate)
                                {
                                    if (k2 != null)
                                    {
                                        //compare the X,Y of a king to the X,Y of a highlight for ENEMY KING
                                        if (k2.CurrentX == i && k2.CurrentY == j)
                                        {
                                            if (!k2.isMated)
                                            {
                                                if (playerTurn == 2)
                                                {
                                                    Debug.Log("Yellow is in check by Blue's" + p.name);  //then put that king in check
                                                    GameText.text = "Yellow is in check by Blue's " + p.name;
                                                }

                                                k2.isChecked = true;
                                            }
                                        }
                                    }
                                    if (k3 != null)
                                    {
                                        if (k3.CurrentX == i && k3.CurrentY == j)
                                        {
                                            if (!k3.isMated)
                                            {
                                                if (playerTurn == 3)
                                                {
                                                    Debug.Log("Red is in check by Blue's" + p.name);  //then put that king in check    
                                                    GameText.text = "Red is in check by Blue's " + p.name;
                                                }

                                                k3.isChecked = true;
                                            }
                                        }
                                    }
                                    if (k4 != null)
                                    {
                                        if (k4.CurrentX == i && k4.CurrentY == j)
                                        {
                                            if (!k4.isMated)
                                            {
                                                if (playerTurn == 4)
                                                {
                                                    Debug.Log("Green is in check by Blue's" + p.name);  //then put that king in check  
                                                    GameText.text = "Green is in check by Blue's " + p.name;
                                                }

                                                k4.isChecked = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (k2 != null)
        {
            foreach (GameObject p in allPieces) // for each peice on the board that is active...
            {
                if (p.GetComponent<Chessman>().isYellow)  //YELLOW////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    allowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //then assign their possible moves to allowedMoves

                    //see if the piece has any movements
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (allowedMoves[i, j])
                            {
                                bool isRetreatTile = false;
                                bool isRetreatPiece = false;

                                if (lastYellowX == i && lastYellowY == j)
                                {
                                    isRetreatTile = true;
                                }

                                if (resetManYellow != null)
                                {
                                    if (Chessmans[p.GetComponent<Chessman>().CurrentX, p.GetComponent<Chessman>().CurrentY] == Chessmans[resetManYellow.CurrentX, resetManYellow.CurrentY])
                                    {
                                        isRetreatPiece = true;
                                    }
                                }

                                bool ShouldEvaluate = true;

                                if (isRetreatPiece && isRetreatTile)
                                {
                                    ShouldEvaluate = false;
                                }

                                if (ShouldEvaluate)
                                {
                                    if (k1 != null)
                                    {
                                        if (k1.CurrentX == i && k1.CurrentY == j)
                                        {
                                            if (!k1.isMated)
                                            {
                                                if (playerTurn == 1)
                                                {
                                                    GameText.text = "Blue is in check by Yellow's " + p.name;
                                                    Debug.Log("Blue is in check by Yellow's " + p.name);  //then put that king in check
                                                }

                                                k1.isChecked = true;
                                            }
                                        }
                                    }

                                    if (k3 != null)
                                    {
                                        if (k3.CurrentX == i && k3.CurrentY == j)
                                        {
                                            if (!k3.isMated)
                                            {
                                                if (playerTurn == 3)
                                                {
                                                    Debug.Log("Red is in check by Yellow's " + p.name);  //then put that king in check
                                                    GameText.text = "Red is in check by Yellow's " + p.name;
                                                }

                                                k3.isChecked = true;
                                            }
                                        }
                                    }

                                    if (k4 != null)
                                    {
                                        if (k4.CurrentX == i && k4.CurrentY == j)
                                        {
                                            if (!k4.isMated)
                                            {
                                                if (playerTurn == 4)
                                                {
                                                    Debug.Log("Green is in check by Yellow's" + p.name);  //then put that king in check
                                                    GameText.text = "Green is in check by Yellow's " + p.name;
                                                }

                                                k4.isChecked = true;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        if (k3 != null)
        {
            foreach (GameObject p in allPieces) // for each peice on the board that is active...
            {
                if (p.GetComponent<Chessman>().isRed) //RED///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    allowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //then assign their possible moves to allowedMoves

                    //see if the piece has any movements
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (allowedMoves[i, j])
                            {
                                bool isRetreatTile = false;
                                bool isRetreatPiece = false;

                                if (lastRedX == i && lastRedY == j)
                                {
                                    isRetreatTile = true;
                                }

                                if (resetManRed != null)
                                {
                                    if (Chessmans[p.GetComponent<Chessman>().CurrentX, p.GetComponent<Chessman>().CurrentY] == Chessmans[resetManRed.CurrentX, resetManRed.CurrentY])
                                    {
                                        isRetreatPiece = true;
                                    }
                                }

                                bool ShouldEvaluate = true;

                                if (isRetreatPiece && isRetreatTile)
                                {
                                    ShouldEvaluate = false;
                                }

                                if (ShouldEvaluate)
                                {
                                    if (k2 != null)
                                    {
                                        if (k2.CurrentX == i && k2.CurrentY == j)
                                        {
                                            if (!k2.isMated)
                                            {
                                                if (playerTurn == 2)
                                                {
                                                    Debug.Log("Yellow is in check by Red's " + p.name);  //then put that king in check
                                                    GameText.text = "Yellow is in check by Red's " + p.name;
                                                }
                                                k2.isChecked = true;
                                            }
                                        }
                                    }

                                    if (k1 != null)
                                    {
                                        if (k1.CurrentX == i && k1.CurrentY == j)
                                        {
                                            if (!k1.isMated)
                                            {
                                                if (playerTurn == 1)
                                                {
                                                    Debug.Log("Blue is in check by Red's" + p.name);  //then put that king in check
                                                    GameText.text = "Blue is in check by Red's " + p.name;
                                                }

                                                k1.isChecked = true;
                                            }
                                        }
                                    }

                                    if (k4 != null)
                                    {
                                        if (k4.CurrentX == i && k4.CurrentY == j)
                                        {
                                            if (!k4.isMated)
                                            {
                                                if (playerTurn == 4)
                                                {
                                                    Debug.Log("Green is in check by Red's" + p.name);  //then put that king in check
                                                    GameText.text = "Green is in check by Red's " + p.name;
                                                }

                                                k4.isChecked = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (k4 != null)
        {
            foreach (GameObject p in allPieces) // for each peice on the board that is active...
            {
                if (p.GetComponent<Chessman>().isGreen) //GREEN/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    allowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //then assign their possible moves to allowedMoves

                    //see if the piece has any movements
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (allowedMoves[i, j])
                            {
                                bool isRetreatTile = false;
                                bool isRetreatPiece = false;

                                if (lastGreenX == i && lastGreenY == j)
                                {
                                    isRetreatTile = true;
                                }

                                if (resetManGreen != null)
                                {
                                    if (Chessmans[p.GetComponent<Chessman>().CurrentX, p.GetComponent<Chessman>().CurrentY] == Chessmans[resetManGreen.CurrentX, resetManGreen.CurrentY])
                                    {
                                        isRetreatPiece = true;
                                    }
                                }

                                bool ShouldEvaluate = true;

                                if (isRetreatPiece && isRetreatTile)
                                {
                                    ShouldEvaluate = false;
                                }

                                if (ShouldEvaluate)
                                {
                                    if (k2 != null)
                                    {
                                        if (k2.CurrentX == i && k2.CurrentY == j)
                                        {
                                            if (!k2.isMated)
                                            {
                                                if (playerTurn == 2)
                                                {
                                                    Debug.Log("Yellow is in check by Green's" + p.name);  //then put that king in check
                                                    GameText.text = "Yellow is in check by Green's " + p.name;
                                                }

                                                k2.isChecked = true;
                                            }
                                        }
                                    }

                                    if (k3 != null)
                                    {
                                        if (k3.CurrentX == i && k3.CurrentY == j)
                                        {
                                            if (!k3.isMated)
                                            {
                                                if (playerTurn == 3)
                                                {
                                                    Debug.Log("Red is in check by Green's" + p.name);  //then put that king in check
                                                    GameText.text = "Red is in check by Green's " + p.name;
                                                }

                                                k3.isChecked = true;
                                            }
                                        }
                                    }

                                    if (k1 != null)
                                    {
                                        if (k1.CurrentX == i && k1.CurrentY == j)
                                        {
                                            if (!k1.isMated)
                                            {
                                                if (playerTurn == 1)
                                                {
                                                    Debug.Log("Blue is in check by Green's " + p.name);  //then put that king in check
                                                    GameText.text = "Blue is in check by Green's " + p.name;
                                                }

                                                k1.isChecked = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }

    private void notChecked() //see if a king is out of Check
    {
        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");

        bool k1stillchecked = false; //set it to false - and if one highlight equals a king, then it wont take them out of check.
        bool k2stillchecked = false; //set it to false - and if one highlight equals a king, then it wont take them out of check.
        bool k3stillchecked = false; //set it to false - and if one highlight equals a king, then it wont take them out of check.
        bool k4stillchecked = false; //set it to false - and if one highlight equals a king, then it wont take them out of check.

        if (k1 != null)
        {
            if (k1.isChecked)
            {
                foreach (GameObject p in allPieces)
                {
                    if (!p.GetComponent<Chessman>().isBlue)
                    {
                        allowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //then assign their possible moves to allowedMoves

                        //see if the piece has any movements
                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (allowedMoves[i, j])
                                {
                                    bool isRetreatTile = false;
                                    bool isRetreatPiece = false;
                                    Chessman c = p.GetComponent<Chessman>();
                                    if(c.isBlue)
                                    {
                                        if (lastBlueX == i && lastBlueY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManBlue != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] == 
                                            Chessmans[resetManBlue.CurrentX, resetManBlue.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isYellow)
                                    {
                                        if (lastYellowX == i && lastYellowY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManYellow != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManYellow.CurrentX, resetManYellow.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isRed)
                                    {
                                        if (lastRedX == i && lastRedY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManRed != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManRed.CurrentX, resetManRed.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isGreen)
                                    {
                                        if (lastGreenX == i && lastGreenY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManGreen != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManGreen.CurrentX, resetManGreen.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }


                                    bool ShouldEvaluate = true;

                                    if (isRetreatPiece && isRetreatTile)
                                    {
                                        ShouldEvaluate = false;
                                    }

                                    if (ShouldEvaluate)
                                    {
                                        if (k1.CurrentX == i && k1.CurrentY == j)
                                        {
                                            k1stillchecked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if (k2 != null)
        {
            if (k2.isChecked)
            {
                foreach (GameObject p in allPieces)
                {
                    if (!p.GetComponent<Chessman>().isYellow)
                    {
                        allowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //then assign their possible moves to allowedMoves

                        //see if the piece has any movements
                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (allowedMoves[i, j])
                                {
                                    bool isRetreatTile = false;
                                    bool isRetreatPiece = false;
                                    Chessman c = p.GetComponent<Chessman>();
                                    if (c.isBlue)
                                    {
                                        if (lastBlueX == i && lastBlueY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManBlue != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManBlue.CurrentX, resetManBlue.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isYellow)
                                    {
                                        if (lastYellowX == i && lastYellowY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManYellow != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManYellow.CurrentX, resetManYellow.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isRed)
                                    {
                                        if (lastRedX == i && lastRedY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManRed != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManRed.CurrentX, resetManRed.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isGreen)
                                    {
                                        if (lastGreenX == i && lastGreenY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManGreen != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManGreen.CurrentX, resetManGreen.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }


                                    bool ShouldEvaluate = true;

                                    if (isRetreatPiece && isRetreatTile)
                                    {
                                        ShouldEvaluate = false;
                                    }

                                    if (ShouldEvaluate)
                                    {
                                        if (k2.CurrentX == i && k2.CurrentY == j)
                                        {
                                            k2stillchecked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (k3 != null)
        {
            if (k3.isChecked)
            {
                foreach (GameObject p in allPieces)
                {
                    if (!p.GetComponent<Chessman>().isRed)
                    {
                        allowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //then assign their possible moves to allowedMoves

                        //see if the piece has any movements
                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (allowedMoves[i, j])
                                {
                                    bool isRetreatTile = false;
                                    bool isRetreatPiece = false;
                                    Chessman c = p.GetComponent<Chessman>();
                                    if (c.isBlue)
                                    {
                                        if (lastBlueX == i && lastBlueY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManBlue != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManBlue.CurrentX, resetManBlue.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isYellow)
                                    {
                                        if (lastYellowX == i && lastYellowY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManYellow != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManYellow.CurrentX, resetManYellow.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isRed)
                                    {
                                        if (lastRedX == i && lastRedY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManRed != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManRed.CurrentX, resetManRed.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isGreen)
                                    {
                                        if (lastGreenX == i && lastGreenY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManGreen != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManGreen.CurrentX, resetManGreen.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }


                                    bool ShouldEvaluate = true;

                                    if (isRetreatPiece && isRetreatTile)
                                    {
                                        ShouldEvaluate = false;
                                    }

                                    if (ShouldEvaluate)
                                    {
                                        if (k3.CurrentX == i && k3.CurrentY == j)
                                        {
                                            k3stillchecked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (k4 != null)
        {
            if (k4.isChecked)
            {
                foreach (GameObject p in allPieces)
                {
                    if (!p.GetComponent<Chessman>().isGreen)
                    {
                        allowedMoves = p.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen); //then assign their possible moves to allowedMoves

                        //see if the piece has any movements
                        for (int i = 0; i < 12; i++)
                        {
                            for (int j = 0; j < 12; j++)
                            {
                                if (allowedMoves[i, j])
                                {
                                    bool isRetreatTile = false;
                                    bool isRetreatPiece = false;
                                    Chessman c = p.GetComponent<Chessman>();
                                    if (c.isBlue)
                                    {
                                        if (lastBlueX == i && lastBlueY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManBlue != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManBlue.CurrentX, resetManBlue.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isYellow)
                                    {
                                        if (lastYellowX == i && lastYellowY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManYellow != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManYellow.CurrentX, resetManYellow.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isRed)
                                    {
                                        if (lastRedX == i && lastRedY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManRed != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManRed.CurrentX, resetManRed.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }
                                    if (c.isGreen)
                                    {
                                        if (lastGreenX == i && lastGreenY == j)
                                        {
                                            isRetreatTile = true;
                                        }
                                        if (resetManGreen != null)
                                        {
                                            if (Chessmans[c.CurrentX, c.CurrentY] ==
                                            Chessmans[resetManGreen.CurrentX, resetManGreen.CurrentY])
                                            {
                                                isRetreatPiece = true;
                                            }
                                        }
                                    }


                                    bool ShouldEvaluate = true;

                                    if (isRetreatPiece && isRetreatTile)
                                    {
                                        ShouldEvaluate = false;
                                    }

                                    if (ShouldEvaluate)
                                    {
                                        if (k4.CurrentX == i && k4.CurrentY == j)
                                        {
                                            k4stillchecked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        if (!k1stillchecked && k1 != null)
        {
            k1.GetComponent<Chessman>().isChecked = false;
            k1.GetComponent<Chessman>().isMated = false;
        }
        if (!k2stillchecked && k2 != null)
        {
            k2.GetComponent<Chessman>().isChecked = false;
            k2.GetComponent<Chessman>().isMated = false;
        }
        if (!k3stillchecked && k3 != null)
        {
            k3.GetComponent<Chessman>().isChecked = false;
            k3.GetComponent<Chessman>().isMated = false;
        }
        if (!k4stillchecked && k4 != null)
        {
            k4.GetComponent<Chessman>().isChecked = false;
            k4.GetComponent<Chessman>().isMated = false;
        }
    }

    public void Win()
    {
        if (blueout == false && yelout == true && redout == true && greenout == true)
        {
            GameText.text = "Blue Wins";
            Debug.Log("Blue Wins");
            if (isOnline)
            {
                GameWinner = onlinePlayerTurn;

                foreach (Player p in playerList)
                {
                    if (p.id == GameWinner)
                    {
                        WinnerName = p.playerName;
                    }
                }
            }
            else
            {
                foreach (Player p in playerList)
                {
                    if (p.id == GameWinner)
                    {
                        GameLose(p.playerName);
                    }
                }
            }
        }
        if (blueout == true && yelout == false && redout == true && greenout == true)
        {
            GameText.text = "Yellow Wins";
            Debug.Log("Yellow Wins");
            if (isOnline)
            {
                GameWinner = onlinePlayerTurn;
                foreach (Player p in playerList)
                {
                    if (p.id == GameWinner)
                    {
                        WinnerName = p.playerName;
                    }
                }
            }
            else
            {
                foreach (Player p in playerList)
                {
                    if (p.id == GameWinner)
                    {
                        GameLose(p.playerName);
                    }
                }
            }
        }
        if (blueout == true && yelout == true && redout == false && greenout == true)
        {
            GameText.text = "Red Wins";
            Debug.Log("Red Wins");
            if (isOnline)
            {
                GameWinner = onlinePlayerTurn;
                foreach (Player p in playerList)
                {
                    if (p.id == GameWinner)
                    {
                        WinnerName = p.playerName;
                    }
                }
            }
            else
            {
                foreach (Player p in playerList)
                {
                    if (p.id == GameWinner)
                    {
                        GameLose(p.playerName);
                    }
                }
            }
        }
        if (blueout == true && yelout == true && redout == true && greenout == false)
        {
            GameText.text = "Green Wins";
            Debug.Log("Green Wins");
            if (isOnline)
            {
                GameWinner = onlinePlayerTurn;
                foreach (Player p in playerList)
                {
                    if (p.id == GameWinner)
                    {
                        WinnerName = p.playerName;
                    }
                }
            }
            else
            {
                foreach (Player p in playerList)
                {
                    if (p.id == GameWinner)
                    {
                        GameLose(p.playerName);
                    }
                }
            }
        }
    }
    public void Lose()
    {
        if (blueout)
        {
            GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");

            foreach (GameObject p in allPieces) // for each peice on the board that is active...
            {
                if (p.GetComponent<Chessman>().isBlue)
                {
                    activeChessman.Remove(p);
                    Destroy(p.gameObject);
                }
            }
        }

        if (yelout)
        {
            GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");

            foreach (GameObject p in allPieces) // for each peice on the board that is active...
            {
                if (p.GetComponent<Chessman>().isYellow)
                {
                    activeChessman.Remove(p);
                    Destroy(p.gameObject);
                }
            }
        }
        if (redout)
        {
            GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");

            foreach (GameObject p in allPieces) // for each peice on the board that is active...
            {
                if (p.GetComponent<Chessman>().isRed)
                {
                    activeChessman.Remove(p);
                    Destroy(p.gameObject);
                }
            }
        }
        if (greenout)
        {
            GameObject[] allPieces = GameObject.FindGameObjectsWithTag("piece");

            foreach (GameObject p in allPieces) // for each peice on the board that is active...
            {
                if (p.GetComponent<Chessman>().isGreen)
                {
                    activeChessman.Remove(p);
                    Destroy(p.gameObject);
                }
            }
        }
    }
    private void Jailbreak()
    {
        if (k1 != null)//if youre not out
        {
            if (k1.name == "King")
            {
                if (previous == k1)// and we just moved there on the last turn.
                {
                    if (k1.CurrentX == 0 && k1.CurrentY == 11) //and youre in their corner
                    {
                        if (!yelout) //and theyre not out 
                        {
                            if (yelPrisonersBlue.Count > 0) // and this player has pieces of yours
                            {
                                List<string> YellowRemoveBlue = new List<string>();

                                foreach (string s in yelPrisonersBlue)
                                {
                                    if (s == "Rook") // blue rook is 1 index
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[3, 11];
                                        Chessman c2 = Chessmans[0, 8];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(1, 3, 11);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(1, 3, 11);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(1, 0, 8);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isYellow && !c2.isYellow)
                                            {
                                                SpawnChessman(1, 3, 11);
                                                bluePrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveBlue.Add(s);
                                            }
                                            if (!c.isYellow && c2.isYellow)
                                            {
                                                SpawnChessman(1, 0, 8);
                                                bluePrisonersYellow.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                YellowRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isYellow && c2.isYellow)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(1, 3, 11);
                                                    bluePrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(1, 0, 8);
                                                    bluePrisonersYellow.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(1, 3, 11);
                                                    bluePrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isYellow && !c2.isYellow)
                                            {
                                                if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(1, 3, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        bluePrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(1, 0, 8);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c2.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c2.name);
                                                    }
                                                    if (c2.isGreen)
                                                    {
                                                        bluePrisonersGreen.Add(c2.name);
                                                    }

                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    YellowRemoveBlue.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(1, 3, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        bluePrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }

                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Knight") // blue knight is 2 index
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[2, 11];
                                        Chessman c2 = Chessmans[0, 9];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(2, 2, 11);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(2, 2, 11);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(2, 0, 9);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isYellow && !c2.isYellow)
                                            {
                                                SpawnChessman(2, 2, 11);
                                                bluePrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveBlue.Add(s);
                                            }
                                            if (!c.isYellow && c2.isYellow)
                                            {
                                                SpawnChessman(2, 0, 9);
                                                bluePrisonersYellow.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                YellowRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isYellow && c2.isYellow)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(2, 2, 11);
                                                    bluePrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(2, 0, 9);
                                                    bluePrisonersYellow.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(2, 2, 11);
                                                    bluePrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }
                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isYellow && !c2.isYellow)
                                                {
                                                    if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(2, 2, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isRed)
                                                        {
                                                            bluePrisonersRed.Add(c.name);
                                                        }
                                                        if (c.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveBlue.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(2, 0, 9);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c2.isRed)
                                                        {
                                                            bluePrisonersRed.Add(c2.name);
                                                        }
                                                        if (c2.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c2.name);
                                                        }

                                                        activeChessman.Remove(c2.gameObject);
                                                        Destroy(c2.gameObject);
                                                        YellowRemoveBlue.Add(s);

                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(2, 2, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isRed)
                                                        {
                                                            bluePrisonersRed.Add(c.name);
                                                        }
                                                        if (c.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveBlue.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Bishop") // blue bishop is 3
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[1, 11];
                                        Chessman c2 = Chessmans[0, 10];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(3, 1, 11);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(3, 1, 11);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(3, 0, 10);
                                            YellowRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isYellow && !c2.isYellow)
                                            {
                                                SpawnChessman(3, 1, 11);
                                                bluePrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveBlue.Add(s);
                                            }
                                            if (!c.isYellow && c2.isYellow)
                                            {
                                                SpawnChessman(3, 0, 10);
                                                bluePrisonersYellow.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                YellowRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isYellow && c2.isYellow)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(3, 1, 11);
                                                    bluePrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(3, 0, 10);
                                                    bluePrisonersYellow.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(3, 1, 11);
                                                    bluePrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveBlue.Add(s);
                                                }
                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isYellow && !c2.isYellow)
                                                {
                                                    if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(3, 1, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isRed)
                                                        {
                                                            bluePrisonersRed.Add(c.name);
                                                        }
                                                        if (c.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveBlue.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(3, 0, 10);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c2.isRed)
                                                        {
                                                            bluePrisonersRed.Add(c2.name);
                                                        }
                                                        if (c2.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c2.name);
                                                        }

                                                        activeChessman.Remove(c2.gameObject);
                                                        Destroy(c2.gameObject);
                                                        YellowRemoveBlue.Add(s);

                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(3, 1, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isRed)
                                                        {
                                                            bluePrisonersRed.Add(c.name);
                                                        }
                                                        if (c.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveBlue.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Queen") // blue queen is 4
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[1, 10];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(4, 1, 10);
                                            YellowRemoveBlue.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....        
                                            if (c.isYellow)
                                            {
                                                SpawnChessman(4, 1, 10);
                                                bluePrisonersYellow.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                SpawnChessman(4, 1, 10);
                                                bluePrisonersRed.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(4, 1, 10);
                                                bluePrisonersGreen.Add(c.name);
                                            }
                                            if (c.isBlue)
                                            {
                                                continue;
                                            }

                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            YellowRemoveBlue.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (var s in YellowRemoveBlue)
                                {
                                    yelPrisonersBlue.Remove(s);
                                }

                                YellowRemoveBlue.Clear();
                            }
                        }
                    }

                    if (k1.CurrentX == 11 && k1.CurrentY == 11)
                    {
                        if (!redout)
                        {
                            if (redPrisonersBlue.Count > 0)
                            {
                                List<string> RedRemoveBlue = new List<string>();

                                foreach (string s in redPrisonersBlue)
                                {
                                    if (s == "Rook")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[8, 11];
                                        Chessman c2 = Chessmans[11, 8];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(1, 8, 11);
                                            RedRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(1, 8, 11);
                                            RedRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(1, 11, 8);
                                            RedRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isRed && !c2.isRed)
                                            {
                                                SpawnChessman(1, 8, 11);
                                                bluePrisonersRed.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                RedRemoveBlue.Add(s);
                                            }
                                            if (!c.isRed && c2.isRed)
                                            {
                                                SpawnChessman(1, 11, 8);
                                                bluePrisonersRed.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                RedRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isRed && c2.isRed)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(1, 8, 11);
                                                    bluePrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(1, 11, 8);
                                                    bluePrisonersRed.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(1, 8, 11);
                                                    bluePrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isRed && !c2.isRed)
                                            {
                                                if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(1, 8, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        bluePrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(1, 11, 8);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c2.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c2.name);
                                                    }
                                                    if (c2.isGreen)
                                                    {
                                                        bluePrisonersGreen.Add(c2.name);
                                                    }

                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    RedRemoveBlue.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(1, 8, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        bluePrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Knight")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[9, 11];
                                        Chessman c2 = Chessmans[11, 9];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(2, 9, 11);
                                            RedRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(2, 9, 11);
                                            RedRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(2, 11, 9);
                                            RedRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isRed && !c2.isRed)
                                            {
                                                SpawnChessman(2, 9, 11);
                                                bluePrisonersRed.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                RedRemoveBlue.Add(s);
                                            }
                                            if (!c.isRed && c2.isRed)
                                            {
                                                SpawnChessman(2, 11, 9);
                                                bluePrisonersRed.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                RedRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isRed && c2.isRed)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(2, 9, 11);
                                                    bluePrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(2, 11, 9);
                                                    bluePrisonersRed.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(2, 9, 11);
                                                    bluePrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }
                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isRed && !c2.isRed)
                                                {
                                                    if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(2, 9, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            bluePrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveBlue.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(2, 11, 9);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c2.isYellow)
                                                        {
                                                            bluePrisonersYellow.Add(c2.name);
                                                        }
                                                        if (c2.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c2.name);
                                                        }

                                                        activeChessman.Remove(c2.gameObject);
                                                        Destroy(c2.gameObject);
                                                        RedRemoveBlue.Add(s);

                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(2, 9, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            bluePrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveBlue.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Bishop")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 11];
                                        Chessman c2 = Chessmans[11, 10];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(3, 10, 11);
                                            RedRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(3, 10, 11);
                                            RedRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(3, 11, 10);
                                            RedRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isRed && !c2.isRed)
                                            {
                                                SpawnChessman(3, 10, 11);
                                                bluePrisonersRed.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                RedRemoveBlue.Add(s);
                                            }
                                            if (!c.isRed && c2.isRed)
                                            {
                                                SpawnChessman(3, 11, 10);
                                                bluePrisonersRed.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                RedRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isRed && c2.isRed)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(3, 10, 11);
                                                    bluePrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(3, 11, 10);
                                                    bluePrisonersRed.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(3, 10, 11);
                                                    bluePrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveBlue.Add(s);
                                                }
                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isRed && !c2.isRed)
                                                {
                                                    if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(3, 10, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            bluePrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveBlue.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(3, 11, 10);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c2.isYellow)
                                                        {
                                                            bluePrisonersYellow.Add(c2.name);
                                                        }
                                                        if (c2.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c2.name);
                                                        }

                                                        activeChessman.Remove(c2.gameObject);
                                                        Destroy(c2.gameObject);
                                                        RedRemoveBlue.Add(s);

                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(3, 10, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            bluePrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isGreen)
                                                        {
                                                            bluePrisonersGreen.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveBlue.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Queen")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 10];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(4, 10, 10);
                                            RedRemoveBlue.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isYellow)
                                            {
                                                SpawnChessman(4, 10, 10);
                                                bluePrisonersYellow.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                SpawnChessman(4, 10, 10);
                                                bluePrisonersRed.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(4, 10, 10);
                                                bluePrisonersGreen.Add(c.name);
                                            }
                                            if (c.isBlue)
                                            {
                                                continue;
                                            }

                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            RedRemoveBlue.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (string s in RedRemoveBlue)
                                {
                                    redPrisonersBlue.Remove(s);
                                }
                                RedRemoveBlue.Clear();
                            }
                        }
                    }

                    if (k1.CurrentX == 11 && k1.CurrentY == 0)
                    {
                        if (!greenout)
                        {
                            if (greenPrisonersBlue.Count > 0)
                            {
                                List<string> GreenRemoveBlue = new List<string>();

                                foreach (string s in greenPrisonersBlue)
                                {

                                    if (s == "Rook")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[8, 0];
                                        Chessman c2 = Chessmans[11, 3];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(1, 8, 0);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(1, 8, 0);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(1, 11, 3);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(1, 8, 0);
                                                bluePrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveBlue.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(1, 11, 3);
                                                bluePrisonersGreen.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                GreenRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(1, 8, 0);
                                                    bluePrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(1, 11, 3);
                                                    bluePrisonersGreen.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(1, 8, 0);
                                                    bluePrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(1, 8, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(1, 11, 3);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c2.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c2.name);
                                                    }
                                                    if (c2.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c2.name);
                                                    }

                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveBlue.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(1, 8, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Knight")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[9, 0];
                                        Chessman c2 = Chessmans[11, 2];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(2, 9, 0);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(2, 9, 0);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(2, 11, 2);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(2, 9, 0);
                                                bluePrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveBlue.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(2, 11, 2);
                                                bluePrisonersGreen.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                GreenRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(2, 9, 0);
                                                    bluePrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(2, 11, 2);
                                                    bluePrisonersGreen.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(2, 9, 0);
                                                    bluePrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(2, 9, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(2, 11, 2);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c2.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c2.name);
                                                    }
                                                    if (c2.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c2.name);
                                                    }

                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveBlue.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(2, 9, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Bishop")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 0];
                                        Chessman c2 = Chessmans[11, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(3, 10, 0);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(3, 10, 0);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(3, 11, 1);
                                            GreenRemoveBlue.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(3, 10, 0);
                                                bluePrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveBlue.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(3, 11, 1);
                                                bluePrisonersGreen.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                GreenRemoveBlue.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(3, 10, 0);
                                                    bluePrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(3, 11, 1);
                                                    bluePrisonersGreen.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(3, 10, 0);
                                                    bluePrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isBlue) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(3, 10, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(3, 11, 1);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c2.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c2.name);
                                                    }
                                                    if (c2.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c2.name);
                                                    }

                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveBlue.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(3, 10, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        bluePrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        bluePrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveBlue.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Queen")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(4, 10, 1);
                                            GreenRemoveBlue.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isYellow)
                                            {
                                                SpawnChessman(4, 10, 1);
                                                bluePrisonersYellow.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                SpawnChessman(4, 10, 1);
                                                bluePrisonersRed.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(4, 10, 1);
                                                bluePrisonersGreen.Add(c.name);
                                            }
                                            if (c.isBlue)
                                            {
                                                continue;
                                            }

                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            GreenRemoveBlue.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (string s in GreenRemoveBlue)
                                {
                                    greenPrisonersBlue.Remove(s);
                                }

                                GreenRemoveBlue.Clear();
                            }
                        }
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (k2 != null)
        {
            if (k2.name == "King")
            {
                if (previous == k2)
                {
                    if (k2.CurrentX == 0 && k2.CurrentY == 0)
                    {
                        if (!blueout)
                        {
                            if (bluePrisonersYellow.Count > 0)
                            {
                                List<string> BlueRemoveYellow = new List<string>();

                                foreach (string s in bluePrisonersYellow)
                                {
                                    if (s == "Rook") //19
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[3, 0];
                                        Chessman c2 = Chessmans[0, 3];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(19, 3, 0);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(19, 3, 0);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(19, 0, 3);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isBlue && !c2.isBlue)
                                            {
                                                SpawnChessman(19, 3, 0);
                                                yelPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveYellow.Add(s);
                                            }
                                            if (!c.isBlue && c2.isBlue)
                                            {
                                                SpawnChessman(19, 0, 3);
                                                yelPrisonersBlue.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                BlueRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isBlue && c2.isBlue)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(19, 3, 0);
                                                    yelPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(19, 0, 3);
                                                    yelPrisonersBlue.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(19, 3, 0);
                                                    yelPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isBlue && !c2.isBlue)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(19, 3, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(19, 0, 3);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(19, 3, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Knight") //20
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[2, 0];
                                        Chessman c2 = Chessmans[0, 2];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(20, 2, 0);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(20, 2, 0);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(20, 0, 2);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isBlue && !c2.isBlue)
                                            {
                                                SpawnChessman(20, 2, 0);
                                                yelPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveYellow.Add(s);
                                            }
                                            if (!c.isBlue && c2.isBlue)
                                            {
                                                SpawnChessman(20, 0, 2);
                                                yelPrisonersBlue.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                BlueRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isBlue && c2.isBlue)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(20, 2, 0);
                                                    yelPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(20, 0, 2);
                                                    yelPrisonersBlue.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(20, 2, 0);
                                                    yelPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isBlue && !c2.isBlue)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(20, 2, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(20, 0, 2);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(20, 2, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Bishop") //21
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[1, 0];
                                        Chessman c2 = Chessmans[0, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(21, 1, 0);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(21, 1, 0);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(21, 0, 1);
                                            BlueRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            //take the yellow peice if they're different teams, since we're attack yellow....
                                            if (c.isBlue && !c2.isBlue)
                                            {
                                                SpawnChessman(21, 1, 0);
                                                yelPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveYellow.Add(s);
                                            }
                                            if (!c.isBlue && c2.isBlue)
                                            {
                                                SpawnChessman(21, 0, 1);
                                                yelPrisonersBlue.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                BlueRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isBlue && c2.isBlue)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(21, 1, 0);
                                                    yelPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(21, 0, 1);
                                                    yelPrisonersBlue.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(21, 1, 0);
                                                    yelPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isBlue && !c2.isBlue)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(21, 1, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(21, 0, 1);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(21, 1, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Queen") //22
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[1, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(22, 1, 1);
                                            BlueRemoveYellow.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue)
                                            {
                                                SpawnChessman(22, 1, 1);
                                                yelPrisonersBlue.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                SpawnChessman(22, 1, 1);
                                                yelPrisonersRed.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(22, 1, 1);
                                                yelPrisonersGreen.Add(c.name);
                                            }
                                            if (c.isYellow)
                                            {
                                                continue;
                                            }

                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            BlueRemoveYellow.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (string s in BlueRemoveYellow)
                                {
                                    bluePrisonersYellow.Remove(s);
                                }

                                BlueRemoveYellow.Clear();
                            }
                        }
                    }

                    if (k2.CurrentX == 11 && k2.CurrentY == 11)
                    {
                        if (!redout)
                        {
                            if (redPrisonersYellow.Count > 0)
                            {
                                List<string> RedRemoveYellow = new List<string>();

                                foreach (string s in redPrisonersYellow)
                                {
                                    if (s == "Rook") //19
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[8, 11];
                                        Chessman c2 = Chessmans[11, 8];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(19, 8, 11);
                                            RedRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(19, 8, 11);
                                            RedRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(19, 11, 8);
                                            RedRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isRed && !c2.isRed)
                                            {
                                                SpawnChessman(19, 8, 11);
                                                yelPrisonersRed.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                RedRemoveYellow.Add(s);
                                            }
                                            if (!c.isRed && c2.isRed)
                                            {
                                                SpawnChessman(19, 11, 8);
                                                yelPrisonersRed.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                RedRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isRed && c2.isRed)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(19, 8, 11);
                                                    yelPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(19, 11, 8);
                                                    yelPrisonersRed.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(19, 8, 11);
                                                    yelPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isRed && !c2.isRed)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(19, 8, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(19, 11, 8);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(19, 8, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Knight") //20
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[9, 11];
                                        Chessman c2 = Chessmans[11, 9];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(20, 9, 11);
                                            RedRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(20, 9, 11);
                                            RedRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(20, 11, 9);
                                            RedRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isRed && !c2.isRed)
                                            {
                                                SpawnChessman(20, 9, 11);
                                                yelPrisonersRed.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                RedRemoveYellow.Add(s);
                                            }
                                            if (!c.isRed && c2.isRed)
                                            {
                                                SpawnChessman(20, 11, 9);
                                                yelPrisonersRed.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                RedRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isRed && c2.isRed)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(20, 9, 11);
                                                    yelPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(20, 11, 9);
                                                    yelPrisonersRed.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(20, 9, 11);
                                                    yelPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isRed && !c2.isRed)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(20, 9, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(20, 11, 9);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(20, 9, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Bishop") //21                             
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 11];
                                        Chessman c2 = Chessmans[11, 10];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(21, 10, 11);
                                            RedRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(21, 10, 11);
                                            RedRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(21, 11, 10);
                                            RedRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isRed && !c2.isRed)
                                            {
                                                SpawnChessman(21, 10, 11);
                                                yelPrisonersRed.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                RedRemoveYellow.Add(s);
                                            }
                                            if (!c.isRed && c2.isRed)
                                            {
                                                SpawnChessman(21, 11, 10);
                                                yelPrisonersRed.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                RedRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isRed && c2.isRed)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(21, 10, 11);
                                                    yelPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(21, 11, 10);
                                                    yelPrisonersRed.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(21, 10, 11);
                                                    yelPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isRed && !c2.isRed)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(21, 10, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(21, 11, 10);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(21, 10, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isGreen)
                                                    {
                                                        yelPrisonersGreen.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Queen") //22
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 10];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(22, 10, 10);
                                            RedRemoveYellow.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue)
                                            {
                                                SpawnChessman(22, 10, 10);
                                                yelPrisonersBlue.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                SpawnChessman(22, 10, 10);
                                                yelPrisonersRed.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(22, 10, 10);
                                                yelPrisonersGreen.Add(c.name);
                                            }
                                            if (c.isYellow)
                                            {
                                                continue;
                                            }

                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            RedRemoveYellow.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (string s in RedRemoveYellow)
                                {
                                    redPrisonersYellow.Remove(s);
                                }

                                RedRemoveYellow.Clear();
                            }
                        }
                    }

                    if (k2.CurrentX == 11 && k2.CurrentY == 0)
                    {
                        if (!greenout)
                        {
                            if (greenPrisonersYellow.Count > 0)
                            {
                                List<string> GreenRemoveYellow = new List<string>();

                                foreach (string s in greenPrisonersYellow)
                                {
                                    if (s == "Rook") //19
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[8, 0];
                                        Chessman c2 = Chessmans[11, 3];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(19, 8, 0);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(19, 8, 0);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(19, 11, 3);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(19, 8, 0);
                                                yelPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveYellow.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(19, 11, 3);
                                                yelPrisonersGreen.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                GreenRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(19, 8, 0);
                                                    yelPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(19, 11, 3);
                                                    yelPrisonersGreen.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(19, 8, 0);
                                                    yelPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(19, 8, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(19, 11, 3);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(19, 8, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Knight") //20
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[9, 0];
                                        Chessman c2 = Chessmans[11, 2];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(20, 9, 0);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(20, 9, 0);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(20, 11, 2);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(20, 9, 0);
                                                yelPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveYellow.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(20, 11, 2);
                                                yelPrisonersGreen.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                GreenRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(20, 9, 0);
                                                    yelPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(20, 11, 2);
                                                    yelPrisonersGreen.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(20, 9, 0);
                                                    yelPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(20, 9, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(20, 11, 2);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(20, 9, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Bishop") //21
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 0];
                                        Chessman c2 = Chessmans[11, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(21, 10, 0);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(21, 10, 0);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(21, 11, 1);
                                            GreenRemoveYellow.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(21, 10, 0);
                                                yelPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveYellow.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(21, 11, 1);
                                                yelPrisonersGreen.Add(c2.name);
                                                activeChessman.Remove(c2.gameObject);
                                                Destroy(c2.gameObject);
                                                GreenRemoveYellow.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(21, 10, 0);
                                                    yelPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(21, 11, 1);
                                                    yelPrisonersGreen.Add(c2.name);
                                                    activeChessman.Remove(c2.gameObject);
                                                    Destroy(c2.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(21, 10, 0);
                                                    yelPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isYellow) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(21, 10, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(21, 11, 1);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);

                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(21, 10, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isRed)
                                                    {
                                                        yelPrisonersRed.Add(c.name);
                                                    }
                                                    if (c.isBlue)
                                                    {
                                                        yelPrisonersBlue.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveYellow.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Queen") //22
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(22, 10, 1);
                                            GreenRemoveYellow.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue)
                                            {
                                                SpawnChessman(22, 10, 1);
                                                yelPrisonersBlue.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                SpawnChessman(22, 10, 1);
                                                yelPrisonersRed.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(22, 10, 1);
                                                yelPrisonersGreen.Add(c.name);
                                            }
                                            if (c.isYellow)
                                            {
                                                continue;
                                            }

                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            GreenRemoveYellow.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (string s in GreenRemoveYellow)
                                {
                                    greenPrisonersYellow.Remove(s);
                                }

                                GreenRemoveYellow.Clear();
                            }
                        }
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (k3 != null)
        {
            if (k3.name == "King")
            {
                if (previous == k3)
                {
                    if (k3.CurrentX == 0 && k3.CurrentY == 0)
                    {
                        if (!blueout)
                        {
                            if (bluePrisonersRed.Count > 0)
                            {
                                List<string> BlueRemoveRed = new List<string>();

                                foreach (string s in bluePrisonersRed)
                                {
                                    if (s == "Rook") //13
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[3, 0];
                                        Chessman c2 = Chessmans[0, 3];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(13, 3, 0);
                                            BlueRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(13, 3, 0);
                                            BlueRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(13, 0, 3);
                                            BlueRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue && !c2.isBlue)
                                            {
                                                SpawnChessman(13, 3, 0);
                                                redPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveRed.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(13, 0, 3);
                                                redPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(13, 3, 0);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(13, 0, 3);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(13, 3, 0);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isBlue && !c2.isBlue)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(13, 3, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(13, 0, 3);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(13, 3, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Knight") //14
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[2, 0];
                                        Chessman c2 = Chessmans[0, 2];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(14, 2, 0);
                                            BlueRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(14, 2, 0);
                                            BlueRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(14, 0, 2);
                                            BlueRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue && !c2.isBlue)
                                            {
                                                SpawnChessman(14, 2, 0);
                                                redPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveRed.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(14, 0, 2);
                                                redPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(14, 2, 0);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(14, 0, 2);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(14, 2, 0);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isBlue && !c2.isBlue)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(14, 2, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(14, 0, 2);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(14, 2, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Bishop") //15
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[1, 0];
                                        Chessman c2 = Chessmans[0, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(15, 1, 0);
                                            BlueRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(15, 1, 0);
                                            BlueRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(15, 0, 1);
                                            BlueRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue && !c2.isBlue)
                                            {
                                                SpawnChessman(15, 1, 0);
                                                redPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveRed.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(15, 0, 1);
                                                redPrisonersBlue.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(15, 1, 0);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(15, 0, 1);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(15, 1, 0);
                                                    redPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isBlue && !c2.isBlue)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(15, 1, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(15, 0, 1);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(15, 1, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Queen") //16
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[1, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(16, 1, 1);
                                            BlueRemoveRed.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue)
                                            {
                                                SpawnChessman(16, 1, 1);
                                                redPrisonersBlue.Add(c.name);
                                            }
                                            if (c.isYellow)
                                            {
                                                SpawnChessman(16, 1, 1);
                                                redPrisonersYellow.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(16, 1, 1);
                                                redPrisonersGreen.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                continue;
                                            }

                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            BlueRemoveRed.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (string s in BlueRemoveRed)
                                {
                                    bluePrisonersRed.Remove(s);
                                }

                                BlueRemoveRed.Clear();
                            }
                        }
                    }
                    if (k3.CurrentX == 0 && k3.CurrentY == 11)
                    {
                        if (!yelout)
                        {
                            if (yelPrisonersRed.Count > 0)
                            {
                                List<string> YellowRemoveRed = new List<string>();

                                foreach (string s in yelPrisonersRed)
                                {
                                    if (s == "Rook") //13
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[3, 11];
                                        Chessman c2 = Chessmans[0, 8];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(13, 3, 11);
                                            YellowRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(13, 3, 11);
                                            YellowRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(13, 0, 8);
                                            YellowRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isYellow && !c2.isYellow)
                                            {
                                                SpawnChessman(13, 3, 11);
                                                redPrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveRed.Add(s);
                                            }
                                            if (!c.isYellow && c2.isYellow)
                                            {
                                                SpawnChessman(13, 0, 8);
                                                redPrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isYellow && c2.isYellow)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(13, 3, 11);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(13, 0, 8);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(13, 3, 11);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isYellow && !c2.isYellow)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(13, 3, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(13, 0, 8);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(13, 3, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Knight")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[2, 11];
                                        Chessman c2 = Chessmans[0, 9];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(14, 2, 11);
                                            YellowRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(14, 2, 11);
                                            YellowRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(14, 0, 9);
                                            YellowRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isYellow && !c2.isYellow)
                                            {
                                                SpawnChessman(14, 2, 11);
                                                redPrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveRed.Add(s);
                                            }
                                            if (!c.isYellow && c2.isYellow)
                                            {
                                                SpawnChessman(14, 0, 9);
                                                redPrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isYellow && c2.isYellow)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(14, 2, 11);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(14, 0, 9);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(14, 2, 11);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isYellow && !c2.isYellow)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(14, 2, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(14, 0, 9);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(14, 2, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Bishop")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[1, 11];
                                        Chessman c2 = Chessmans[0, 10];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(15, 1, 11);
                                            YellowRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(15, 1, 11);
                                            YellowRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(15, 0, 10);
                                            YellowRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isYellow && !c2.isYellow)
                                            {
                                                SpawnChessman(15, 1, 11);
                                                redPrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveRed.Add(s);
                                            }
                                            if (!c.isYellow && c2.isYellow)
                                            {
                                                SpawnChessman(15, 0, 10);
                                                redPrisonersYellow.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isYellow && c2.isYellow)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(15, 1, 11);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(15, 0, 10);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(15, 1, 11);
                                                    redPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isYellow && !c2.isYellow)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(15, 1, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(15, 0, 10);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(15, 1, 11);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isGreen)
                                                    {
                                                        redPrisonersGreen.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    if (s == "Queen")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[1, 10];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(16, 1, 10);
                                            YellowRemoveRed.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue)
                                            {
                                                SpawnChessman(16, 1, 10);
                                                redPrisonersBlue.Add(c.name);
                                            }
                                            if (c.isYellow)
                                            {
                                                SpawnChessman(16, 1, 10);
                                                redPrisonersYellow.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(16, 1, 10);
                                                redPrisonersGreen.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                continue;
                                            }
                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            YellowRemoveRed.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (string s in YellowRemoveRed)
                                {
                                    yelPrisonersRed.Remove(s);
                                }

                                YellowRemoveRed.Clear();
                            }
                        }
                    }
                    if (k3.CurrentX == 11 && k3.CurrentY == 0)
                    {
                        if (!greenout)
                        {
                            if (greenPrisonersRed.Count > 0)
                            {
                                List<string> GreenRemoveRed = new List<string>();

                                foreach (string s in greenPrisonersRed)
                                {
                                    if (s == "Rook") //13
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[11, 3];
                                        Chessman c2 = Chessmans[8, 0];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(13, 8, 0);
                                            GreenRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(13, 8, 0);
                                            GreenRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(13, 11, 3);
                                            GreenRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(13, 11, 3);
                                                redPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveRed.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(13, 8, 0);
                                                redPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(13, 8, 0);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(13, 8, 0);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(13, 11, 3);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(13, 8, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(13, 8, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(13, 11, 3);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    if (s == "Knight")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[11, 2];
                                        Chessman c2 = Chessmans[9, 0];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(14, 9, 0);
                                            GreenRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(14, 9, 0);
                                            GreenRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(14, 11, 2);
                                            GreenRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(14, 11, 2);
                                                redPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveRed.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(14, 9, 0);
                                                redPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(14, 9, 0);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(14, 9, 0);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(14, 11, 2);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(14, 9, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(14, 9, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(14, 11, 2);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    if (s == "Bishop")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[11, 1];
                                        Chessman c2 = Chessmans[10, 0];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(15, 10, 0);
                                            GreenRemoveRed.Add(s);
                                        }
                                        if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                        {
                                            SpawnChessman(15, 10, 0);
                                            GreenRemoveRed.Add(s);
                                        }
                                        if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                        {
                                            SpawnChessman(15, 11, 1);
                                            GreenRemoveRed.Add(s);
                                        }
                                        else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isGreen && !c2.isGreen)
                                            {
                                                SpawnChessman(15, 11, 1);
                                                redPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveRed.Add(s);
                                            }
                                            if (!c.isGreen && c2.isGreen)
                                            {
                                                SpawnChessman(15, 10, 0);
                                                redPrisonersGreen.Add(c.name);
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                GreenRemoveRed.Add(s);
                                            }

                                            //if they're both the color we're attacking take the lower rank
                                            if (c.isGreen && c2.isGreen)
                                            {
                                                //if theyre the same rank, spawn in the back row
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(15, 10, 0);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }

                                                //if they're not the same rank, take the lower ranking piece...
                                                if (c.Rank > c2.Rank)
                                                {
                                                    SpawnChessman(15, 10, 0);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(15, 11, 1);
                                                    redPrisonersGreen.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                            }

                                            //if they're both a color we're NOT attacking.....
                                            if (!c.isGreen && !c2.isGreen)
                                            {
                                                if (c.isRed) //if they're both blue then skip this piece and continue with the next loop iteration
                                                {
                                                    continue;
                                                }

                                                //if theyre the same rank, spawn in the back row.
                                                if (c.Rank == c2.Rank)
                                                {
                                                    SpawnChessman(15, 10, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }

                                                // otherwise take the lower rank
                                                if (c.Rank > c2.Rank)
                                                {

                                                    SpawnChessman(15, 10, 0);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                                if (c.Rank < c2.Rank)
                                                {
                                                    SpawnChessman(15, 11, 1);

                                                    //since its not us, and its not our enemy, it can only be one of two teams:
                                                    if (c.isBlue)
                                                    {
                                                        redPrisonersBlue.Add(c.name);
                                                    }
                                                    if (c.isYellow)
                                                    {
                                                        redPrisonersYellow.Add(c.name);
                                                    }

                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    GreenRemoveRed.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                    if (s == "Queen")
                                    #region
                                    {
                                        //get the pieces that are in our spawn location (if there are any)
                                        Chessman c = Chessmans[10, 1];

                                        //first try to spawn in a location where you do not have to capture any of enemies
                                        if (c == null) // if nobody is in either spawn, spawn in the back
                                        {
                                            SpawnChessman(16, 10, 1);
                                            GreenRemoveRed.Add(s);
                                        }

                                        else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                        {
                                            if (c.isBlue)
                                            {
                                                SpawnChessman(16, 10, 1);
                                                redPrisonersBlue.Add(c.name);
                                            }
                                            if (c.isYellow)
                                            {
                                                SpawnChessman(16, 10, 1);
                                                redPrisonersYellow.Add(c.name);
                                            }
                                            if (c.isGreen)
                                            {
                                                SpawnChessman(16, 10, 1);
                                                redPrisonersGreen.Add(c.name);
                                            }
                                            if (c.isRed)
                                            {
                                                continue;
                                            }
                                            activeChessman.Remove(c.gameObject);
                                            Destroy(c.gameObject);
                                            GreenRemoveRed.Add(s);
                                        }
                                    }
                                    #endregion
                                }

                                foreach (string s in GreenRemoveRed)
                                {
                                    greenPrisonersRed.Remove(s);
                                }

                                GreenRemoveRed.Clear();
                            }
                        }
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (k4 != null)
        {
            if (k4.name == "King")
            {
                {
                    if (previous == k4)
                    {
                        if (k4.CurrentX == 0 && k4.CurrentY == 0)
                        {
                            if (!blueout)
                            {
                                if (bluePrisonersGreen.Count > 0)
                                {
                                    List<string> BlueRemoveGreen = new List<string>();

                                    foreach (string s in bluePrisonersGreen)
                                    {
                                        if (s == "Rook") //7
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[3, 0];
                                            Chessman c2 = Chessmans[0, 3];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(7, 3, 0);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(7, 3, 0);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(7, 0, 3);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isBlue && !c2.isBlue)
                                                {
                                                    SpawnChessman(7, 3, 0);
                                                    greenPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveGreen.Add(s);
                                                }
                                                if (!c.isGreen && c2.isGreen)
                                                {
                                                    SpawnChessman(7, 0, 3);
                                                    greenPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isBlue && c2.isBlue)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(7, 3, 0);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(7, 0, 3);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(7, 3, 0);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isBlue && !c2.isBlue)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(7, 3, 0);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(7, 0, 3);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(7, 3, 0);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Knight")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[2, 0];
                                            Chessman c2 = Chessmans[0, 2];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(8, 2, 0);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(8, 2, 0);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(8, 0, 2);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isBlue && !c2.isBlue)
                                                {
                                                    SpawnChessman(8, 2, 0);
                                                    greenPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveGreen.Add(s);
                                                }
                                                if (!c.isGreen && c2.isGreen)
                                                {
                                                    SpawnChessman(8, 0, 2);
                                                    greenPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isBlue && c2.isBlue)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(8, 2, 0);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(8, 0, 2);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(8, 2, 0);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isBlue && !c2.isBlue)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(8, 2, 0);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(8, 0, 2);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(8, 2, 0);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Bishop")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[1, 0];
                                            Chessman c2 = Chessmans[0, 1];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(9, 1, 0);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(9, 1, 0);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(9, 0, 1);
                                                BlueRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isBlue && !c2.isBlue)
                                                {
                                                    SpawnChessman(9, 1, 0);
                                                    greenPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveGreen.Add(s);
                                                }
                                                if (!c.isGreen && c2.isGreen)
                                                {
                                                    SpawnChessman(9, 0, 1);
                                                    greenPrisonersBlue.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    BlueRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isBlue && c2.isBlue)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(9, 1, 0);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(9, 0, 1);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(9, 1, 0);
                                                        greenPrisonersBlue.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isBlue && !c2.isBlue)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(9, 1, 0);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(9, 0, 1);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(9, 1, 0);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        BlueRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Queen")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[1, 1];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(10, 1, 1);
                                                BlueRemoveGreen.Add(s);
                                            }

                                            else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isBlue)
                                                {
                                                    SpawnChessman(16, 10, 1);
                                                    greenPrisonersBlue.Add(c.name);
                                                }
                                                if (c.isYellow)
                                                {
                                                    SpawnChessman(16, 10, 1);
                                                    greenPrisonersYellow.Add(c.name);
                                                }
                                                if (c.isRed)
                                                {
                                                    SpawnChessman(16, 10, 1);
                                                    greenPrisonersRed.Add(c.name);
                                                }
                                                if (c.isGreen)
                                                {
                                                    continue;
                                                }
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                BlueRemoveGreen.Add(s);
                                            }
                                        }
                                        #endregion
                                    }

                                    foreach (string s in BlueRemoveGreen)
                                    {
                                        bluePrisonersGreen.Remove(s);
                                    }

                                    BlueRemoveGreen.Clear();
                                }
                            }
                        }

                        if (k4.CurrentX == 0 && k4.CurrentY == 11)
                        {
                            if (!yelout)
                            {
                                if (yelPrisonersGreen.Count > 0)
                                {
                                    List<string> YellowRemoveGreen = new List<string>();

                                    foreach (string s in yelPrisonersGreen)
                                    {
                                        if (s == "Rook") //7
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[0, 8];
                                            Chessman c2 = Chessmans[3, 11];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(7, 3, 11);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(7, 3, 11);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(7, 0, 8);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isYellow && !c2.isYellow)
                                                {
                                                    SpawnChessman(7, 3, 11);
                                                    greenPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveGreen.Add(s);
                                                }
                                                if (!c.isYellow && c2.isYellow)
                                                {
                                                    SpawnChessman(7, 0, 8);
                                                    greenPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isYellow && c2.isYellow)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(7, 3, 11);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(7, 0, 8);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(7, 3, 11);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isYellow && !c2.isYellow)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(7, 3, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(7, 0, 8);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(7, 3, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Knight")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[0, 9];
                                            Chessman c2 = Chessmans[2, 11];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(8, 2, 11);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(8, 2, 11);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(8, 0, 9);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isYellow && !c2.isYellow)
                                                {
                                                    SpawnChessman(8, 2, 11);
                                                    greenPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveGreen.Add(s);
                                                }
                                                if (!c.isYellow && c2.isYellow)
                                                {
                                                    SpawnChessman(8, 0, 9);
                                                    greenPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isYellow && c2.isYellow)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(8, 2, 11);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(8, 0, 9);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(8, 2, 11);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isYellow && !c2.isYellow)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(8, 2, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(8, 0, 9);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(8, 2, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Bishop")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[0, 10];
                                            Chessman c2 = Chessmans[1, 11];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(9, 1, 11);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(9, 1, 11);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(9, 0, 10);
                                                YellowRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isYellow && !c2.isYellow)
                                                {
                                                    SpawnChessman(9, 1, 11);
                                                    greenPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveGreen.Add(s);
                                                }
                                                if (!c.isYellow && c2.isYellow)
                                                {
                                                    SpawnChessman(9, 0, 10);
                                                    greenPrisonersYellow.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    YellowRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isYellow && c2.isYellow)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(9, 1, 11);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(9, 0, 10);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(9, 1, 11);
                                                        greenPrisonersYellow.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isYellow && !c2.isYellow)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(9, 1, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(9, 0, 10);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(9, 1, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isRed)
                                                        {
                                                            greenPrisonersRed.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        YellowRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Queen")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[1, 10];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(10, 1, 10);
                                                YellowRemoveGreen.Add(s);
                                            }

                                            else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isBlue)
                                                {
                                                    SpawnChessman(10, 1, 10);
                                                    greenPrisonersBlue.Add(c.name);
                                                }
                                                if (c.isYellow)
                                                {
                                                    SpawnChessman(10, 1, 10);
                                                    greenPrisonersYellow.Add(c.name);
                                                }
                                                if (c.isRed)
                                                {
                                                    SpawnChessman(10, 1, 10);
                                                    greenPrisonersRed.Add(c.name);
                                                }
                                                if (c.isGreen)
                                                {
                                                    continue;
                                                }
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                YellowRemoveGreen.Add(s);
                                            }
                                        }
                                        #endregion
                                    }

                                    foreach (string s in YellowRemoveGreen)
                                    {
                                        yelPrisonersGreen.Remove(s);
                                    }

                                    YellowRemoveGreen.Clear();
                                }
                            }
                        }

                        if (k4.CurrentX == 11 && k4.CurrentY == 11)
                        {
                            if (!redout)
                            {
                                if (redPrisonersGreen.Count > 0)
                                {
                                    List<string> RedRemoveGreen = new List<string>();

                                    foreach (string s in redPrisonersGreen)
                                    {
                                        if (s == "Rook") //7
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[8, 11];
                                            Chessman c2 = Chessmans[11, 8];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(7, 8, 11);
                                                RedRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(7, 8, 11);
                                                RedRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(7, 11, 8);
                                                RedRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isRed && !c2.isRed)
                                                {
                                                    SpawnChessman(7, 8, 11);
                                                    greenPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveGreen.Add(s);
                                                }
                                                if (!c.isRed && c2.isRed)
                                                {
                                                    SpawnChessman(7, 11, 8);
                                                    greenPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isRed && c2.isRed)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(7, 8, 11);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(7, 11, 8);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(7, 8, 11);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isRed && !c2.isRed)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(7, 8, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(7, 11, 8);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(7, 8, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Knight")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[9, 11];
                                            Chessman c2 = Chessmans[11, 9];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(8, 9, 11);
                                                RedRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(8, 9, 11);
                                                RedRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(8, 11, 9);
                                                RedRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isRed && !c2.isRed)
                                                {
                                                    SpawnChessman(8, 9, 11);
                                                    greenPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveGreen.Add(s);
                                                }
                                                if (!c.isRed && c2.isRed)
                                                {
                                                    SpawnChessman(8, 11, 9);
                                                    greenPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isRed && c2.isRed)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(8, 9, 11);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(8, 11, 9);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(8, 9, 11);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isRed && !c2.isRed)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(8, 9, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(8, 11, 9);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(8, 9, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Bishop")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[10, 11];
                                            Chessman c2 = Chessmans[10, 9];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null && c2 == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(9, 10, 11);
                                                RedRemoveGreen.Add(s);
                                            }
                                            if (c == null && c2 != null) // if there is a piece on the side, but not in back, spawn in back
                                            {
                                                SpawnChessman(9, 10, 11);
                                                RedRemoveGreen.Add(s);
                                            }
                                            if (c != null && c2 == null) // if there is a peice in the back, then spawn on the side
                                            {
                                                SpawnChessman(9, 11, 10);
                                                RedRemoveGreen.Add(s);
                                            }
                                            else if (c != null && c2 != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isRed && !c2.isRed)
                                                {
                                                    SpawnChessman(9, 10, 11);
                                                    greenPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveGreen.Add(s);
                                                }
                                                if (!c.isRed && c2.isRed)
                                                {
                                                    SpawnChessman(9, 11, 10);
                                                    greenPrisonersRed.Add(c.name);
                                                    activeChessman.Remove(c.gameObject);
                                                    Destroy(c.gameObject);
                                                    RedRemoveGreen.Add(s);
                                                }

                                                //if they're both the color we're attacking take the lower rank
                                                if (c.isRed && c2.isRed)
                                                {
                                                    //if theyre the same rank, spawn in the back row
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(9, 10, 11);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }

                                                    //if they're not the same rank, take the lower ranking piece...
                                                    if (c.Rank > c2.Rank)
                                                    {
                                                        SpawnChessman(9, 11, 10);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(9, 10, 11);
                                                        greenPrisonersRed.Add(c.name);
                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                }

                                                //if they're both a color we're NOT attacking.....
                                                if (!c.isRed && !c2.isRed)
                                                {
                                                    if (c.isGreen) //if they're both blue then skip this piece and continue with the next loop iteration
                                                    {
                                                        continue;
                                                    }

                                                    //if theyre the same rank, spawn in the back row.
                                                    if (c.Rank == c2.Rank)
                                                    {
                                                        SpawnChessman(9, 10, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }

                                                    // otherwise take the lower rank
                                                    if (c.Rank > c2.Rank)
                                                    {

                                                        SpawnChessman(9, 11, 10);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                    if (c.Rank < c2.Rank)
                                                    {
                                                        SpawnChessman(9, 10, 11);

                                                        //since its not us, and its not our enemy, it can only be one of two teams:
                                                        if (c.isBlue)
                                                        {
                                                            greenPrisonersBlue.Add(c.name);
                                                        }
                                                        if (c.isYellow)
                                                        {
                                                            greenPrisonersYellow.Add(c.name);
                                                        }

                                                        activeChessman.Remove(c.gameObject);
                                                        Destroy(c.gameObject);
                                                        RedRemoveGreen.Add(s);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion
                                        if (s == "Queen")
                                        #region
                                        {
                                            //get the pieces that are in our spawn location (if there are any)
                                            Chessman c = Chessmans[10, 10];

                                            //first try to spawn in a location where you do not have to capture any of enemies
                                            if (c == null) // if nobody is in either spawn, spawn in the back
                                            {
                                                SpawnChessman(10, 10, 10);
                                                RedRemoveGreen.Add(s);
                                            }

                                            else if (c != null) //if there IS a piece that is blocking our spawn, we must determine who we capture....
                                            {
                                                if (c.isBlue)
                                                {
                                                    SpawnChessman(10, 10, 10);
                                                    greenPrisonersBlue.Add(c.name);
                                                }
                                                if (c.isYellow)
                                                {
                                                    SpawnChessman(10, 10, 10);
                                                    greenPrisonersYellow.Add(c.name);
                                                }
                                                if (c.isRed)
                                                {
                                                    SpawnChessman(10, 10, 10);
                                                    greenPrisonersRed.Add(c.name);
                                                }
                                                if (c.isGreen)
                                                {
                                                    continue;
                                                }
                                                activeChessman.Remove(c.gameObject);
                                                Destroy(c.gameObject);
                                                RedRemoveGreen.Add(s);
                                            }
                                        }
                                        #endregion
                                    }
                                    foreach (string s in RedRemoveGreen)
                                    {
                                        redPrisonersGreen.Remove(s);
                                    }

                                    RedRemoveGreen.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void WinCheatButton()
    {
        GameWinner = myID;
        foreach (Player p in playerList)
        {
            if (p.id == myID)
            {
                WinnerName = p.playerName;
            }
        }

        SendGameDataAndAdvanceTurn();
    }

    //Checkmate...
    private void ResetMateCap()
    {
        if (matecapColor == "blue")
        {
            if (matecapName == "pawn(Clone)")
            {
                SpawnChessman(0, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
            }
            if (matecapName == "Rook")
            {
                SpawnChessman(1, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
            }
            if (matecapName == "Knight")
            {
                SpawnChessman(2, matecapX, matecapY);
            }
            if (matecapName == "Bishop")
            {
                SpawnChessman(3, matecapX, matecapY);
            }
            if (matecapName == "Queen")
            {
                SpawnChessman(4, matecapX, matecapY);
                Chessmans[matecapX, matecapY].isChecked = matecapCheck;
                Chessmans[matecapX, matecapY].isMated = matecapMated;

                if (k1 == null)
                {
                    k1 = Chessmans[matecapX, matecapY];
                }
                else
                {
                    q1 = Chessmans[matecapX, matecapY];
                }
            }
            if (matecapName == "King")
            {
                SpawnChessman(5, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
                Chessmans[matecapX, matecapY].isChecked = matecapCheck;
                Chessmans[matecapX, matecapY].isMated = matecapMated;

                if (k1.name == "Queen")
                {
                    q1 = k1;
                    k1 = Chessmans[matecapX, matecapY];
                }
                else if (k1 == null)
                {
                    k1 = Chessmans[matecapX, matecapY];
                }
            }
        }

        if (matecapColor == "yellow")
        {
            if (matecapName == "pawn(Clone)")
            {
                SpawnChessman(18, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
            }
            if (matecapName == "Rook")
            {
                SpawnChessman(19, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
            }
            if (matecapName == "Knight")
            {
                SpawnChessman(20, matecapX, matecapY);
            }
            if (matecapName == "Bishop")
            {
                SpawnChessman(21, matecapX, matecapY);
            }
            if (matecapName == "Queen")
            {
                SpawnChessman(22, matecapX, matecapY);
                Chessmans[matecapX, matecapY].isChecked = matecapCheck;
                Chessmans[matecapX, matecapY].isMated = matecapMated;

                if (k2 == null)
                {
                    k2 = Chessmans[matecapX, matecapY];
                }
                else
                {
                    q2 = Chessmans[matecapX, matecapY];
                }
            }
            if (matecapName == "King")
            {
                SpawnChessman(23, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
                Chessmans[matecapX, matecapY].isChecked = matecapCheck;
                Chessmans[matecapX, matecapY].isMated = matecapMated;

                if (k2.name == "Queen")
                {
                    q2 = k2;
                    k2 = Chessmans[matecapX, matecapY];
                }
                else if (k2 == null)
                {
                    k2 = Chessmans[matecapX, matecapY];
                }
            }
        }

        if (matecapColor == "red")
        {
            if (matecapName == "pawn(Clone)")
            {
                SpawnChessman(12, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
            }
            if (matecapName == "Rook")
            {
                SpawnChessman(13, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
            }
            if (matecapName == "Knight")
            {
                SpawnChessman(14, matecapX, matecapY);
            }
            if (matecapName == "Bishop")
            {
                SpawnChessman(15, matecapX, matecapY);
            }
            if (matecapName == "Queen")
            {
                SpawnChessman(16, matecapX, matecapY);
                Chessmans[matecapX, matecapY].isChecked = matecapCheck;
                Chessmans[matecapX, matecapY].isMated = matecapMated;

                if (k3 == null)
                {
                    k3 = Chessmans[matecapX, matecapY];
                }
                else
                {
                    q3 = Chessmans[matecapX, matecapY];
                }
            }
            if (matecapName == "King")
            {
                SpawnChessman(17, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
                Chessmans[matecapX, matecapY].isChecked = matecapCheck;
                Chessmans[matecapX, matecapY].isMated = matecapMated;

                if (k3.name == "Queen")
                {
                    q3 = k3;
                    k3 = Chessmans[matecapX, matecapY];
                }
                else if (k3 == null)
                {
                    k3 = Chessmans[matecapX, matecapY];
                }
            }
        }

        if (matecapColor == "green")
        {
            if (matecapName == "pawn(Clone)")
            {
                SpawnChessman(6, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
            }
            if (matecapName == "Rook")
            {
                SpawnChessman(7, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
            }
            if (matecapName == "Knight")
            {
                SpawnChessman(8, matecapX, matecapY);
            }
            if (matecapName == "Bishop")
            {
                SpawnChessman(9, matecapX, matecapY);
            }
            if (matecapName == "Queen")
            {
                SpawnChessman(10, matecapX, matecapY);
                Chessmans[matecapX, matecapY].isChecked = matecapCheck;
                Chessmans[matecapX, matecapY].isMated = matecapMated;

                if (k4 == null)
                {
                    k4 = Chessmans[matecapX, matecapY];
                }
                else
                {
                    q4 = Chessmans[matecapX, matecapY];
                }
            }
            if (matecapName == "King")
            {
                SpawnChessman(11, matecapX, matecapY);
                Chessmans[matecapX, matecapY].pawnDirection = matecapDir;
                Chessmans[matecapX, matecapY].isChecked = matecapCheck;
                Chessmans[matecapX, matecapY].isMated = matecapMated;

                if (k4.name == "Queen")
                {
                    q4 = k4;
                    k4 = Chessmans[matecapX, matecapY];
                }
                else if (k4 == null)
                {
                    k4 = Chessmans[matecapX, matecapY];
                }
            }
        }

        if (IsPreviousPiece)
        {
            previous = Chessmans[matecapX, matecapY];
            IsPreviousPiece = false;
        }

        matecapColor = null;
        matecapName = null;
    }

    private Dictionary<string, List<GSData>> SerializeForEvaluation()
    {
        // Serialize Game Data
        #region      COLLECT GAME DATA FROM EVERY PIECE ON THE BOARD
        List<GSData> objectList = new List<GSData>();
        foreach (GameObject go in activeChessman) //send all the pieces on the board.
        {
            if (go != null)
            {
                go.GetComponent<Chessman>().SaveX = go.GetComponent<Chessman>().CurrentX;
                go.GetComponent<Chessman>().SaveY = go.GetComponent<Chessman>().CurrentY;
                go.GetComponent<Chessman>().Name = go.name;

                string json = JsonUtility.ToJson(go.GetComponent<Chessman>()); //turn the ChessmanClass into a JSON string
                GSObject piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
                objectList.Add(piece);
            }
        }
        #endregion
        #region   COLLECT ALL ENPASSANT DATA
        List<GSData> enpassData = new List<GSData>();
        List<int> enpassInts = new List<int>();
        enpassInts.Add(EnPassantMoveBlue[0]);
        enpassInts.Add(EnPassantMoveBlue[1]);
        enpassInts.Add(EnPassantMoveYellow[0]);
        enpassInts.Add(EnPassantMoveYellow[1]);
        enpassInts.Add(EnPassantMoveRed[0]);
        enpassInts.Add(EnPassantMoveRed[1]);
        enpassInts.Add(EnPassantMoveGreen[0]);
        enpassInts.Add(EnPassantMoveGreen[1]);
        foreach (int i in enpassInts)
        {
            Enpass number = new Enpass();
            number.number = i;
            string json = JsonUtility.ToJson(number);
            GSObject o = GSObject.FromJson(json);
            enpassData.Add(o);
        }
        #endregion
        #region COLLECT ALL PREVIOUS PEICES AND THEIR PREVIOUS TILE
        List<GSData> LastPieces = new List<GSData>();
        List<GameObject> resetMen = new List<GameObject>();
        if (resetManBlue != null)
        {
            resetMen.Add(resetManBlue.gameObject);
        }
        if (resetManYellow != null)
        {
            resetMen.Add(resetManYellow.gameObject);
        }
        if (resetManRed != null)
        {
            resetMen.Add(resetManRed.gameObject);
        }
        if (resetManGreen != null)
        {
            resetMen.Add(resetManGreen.gameObject);
        }

        foreach (GameObject go in resetMen)
        {
            go.GetComponent<Chessman>().SaveX = go.GetComponent<Chessman>().CurrentX;
            go.GetComponent<Chessman>().SaveY = go.GetComponent<Chessman>().CurrentY;
            go.GetComponent<Chessman>().Name = go.name;

            string json = JsonUtility.ToJson(go.GetComponent<Chessman>()); //turn the ChessmanClass into a JSON string
            GSObject piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
            LastPieces.Add(piece);
        }

        LastTiles LastBlue = new LastTiles();
        LastBlue.LastX = lastBlueX;
        LastBlue.LastY = lastBlueY;
        string lastbluejson = JsonUtility.ToJson(LastBlue);
        GSObject GSLastBlue = GSObject.FromJson(lastbluejson);

        LastTiles LastYellow = new LastTiles();
        LastYellow.LastX = lastYellowX;
        LastYellow.LastY = lastYellowY;
        string lastyeljson = JsonUtility.ToJson(LastYellow);
        GSObject GSLastYellow = GSObject.FromJson(lastyeljson);

        LastTiles LastRed = new LastTiles();
        LastRed.LastX = lastRedX;
        LastRed.LastY = lastRedY;
        string lastredjson = JsonUtility.ToJson(LastRed);
        GSObject GSLastRed = GSObject.FromJson(lastredjson);

        LastTiles LastGreen = new LastTiles();
        LastGreen.LastX = lastGreenX;
        LastGreen.LastY = lastGreenY;
        string lastgreenjson = JsonUtility.ToJson(LastGreen);
        GSObject GSLastGreen = GSObject.FromJson(lastgreenjson);

        List<GSData> PreviousTiles = new List<GSData>();
        PreviousTiles.Add(GSLastBlue);
        PreviousTiles.Add(GSLastYellow);
        PreviousTiles.Add(GSLastRed);
        PreviousTiles.Add(GSLastGreen);

        #endregion
        #region      Win Conditions Variables
        WinConditions wc = new WinConditions();
        wc.playerT = playerTurn;
        wc.prevT = prevTurn;
        wc.Bout = blueout;
        wc.Yout = yelout;
        wc.Rout = redout;
        wc.Gout = greenout;
        string wcJson = JsonUtility.ToJson(wc);
        GSObject obj = GSObject.FromJson(wcJson);
        List<GSData> wincon = new List<GSData>();
        wincon.Add(obj);
        #endregion

        #region Get the leaders
        GSObject K1Piece, K2Piece, K3Piece, K4Piece;
        List<GSData> TheKs = new List<GSData>();

        if (k1)
        {
            string json = JsonUtility.ToJson(k1); //turn the ChessmanClass into a JSON string
            K1Piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
            TheKs.Add(K1Piece);
        }
        if (k2)
        {
            string json = JsonUtility.ToJson(k2); //turn the ChessmanClass into a JSON string
            K2Piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
            TheKs.Add(K2Piece);
        }
        if (k3)
        {
            string json = JsonUtility.ToJson(k3); //turn the ChessmanClass into a JSON string
            K3Piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
            TheKs.Add(K3Piece);
        }
        if (k4)
        {
            string json = JsonUtility.ToJson(k4); //turn the ChessmanClass into a JSON string
            K4Piece = GSObject.FromJson(json);   //turn the json string into a GameSparks Object
            TheKs.Add(K4Piece);
        }
        #endregion


        Dictionary<string, List<GSData>> dict = new Dictionary<string, List<GSData>>();
        dict.Add("GameState", objectList);
        dict.Add("Enpass", enpassData);
        dict.Add("LastPieces", LastPieces);
        dict.Add("LastTiles", PreviousTiles);
        dict.Add("winCondition", wincon);
        dict.Add("ks", TheKs);

        return dict;
    }
    private EvaluationBoard CloneBoardmanager(Dictionary<string, List<GSData>> data)
    {
        GameObject BoardManagerGameObject = new GameObject();
        BoardManagerGameObject.AddComponent<EvaluationBoard>();
        boardcopy = BoardManagerGameObject.GetComponent<EvaluationBoard>();
        BoardManagerGameObject.name = "EvaluationBoard";
        boardcopy.Chessmans = new Chessman[12, 12];
        boardcopy.activeChessman = new List<GameObject>();

        // Apply win conditions to cloned class.
        List<GSData> wincon = data["winCondition"];
        WinConditions wc = WinConditions.CreateFromJSON(wincon[0].JSON);
        boardcopy.playerTurn = wc.playerT;
        boardcopy.prevTurn = wc.prevT;
        boardcopy.blueout = wc.Bout;
        boardcopy.yelout = wc.Yout;
        boardcopy.redout = wc.Rout;
        boardcopy.greenout = wc.Gout;

        List<GSData> GameState = data["GameState"];
        foreach (GSData item in GameState)
        {
            PieceData piece = PieceData.CreateFromJSON(item.JSON);
            //Shouldnt need to physically spawn - only logically...(i think)
            //boardcopy.
            //Debug.Log("Count: " + GameState.Count);
            //Debug.Log("Interation: " + x);
            //Debug.Log("Name: " + piece.Name);
            //Debug.Log("SavedXY: " + piece.SaveX + ", " + piece.SaveY);
            //Debug.Log("index: " + piece.index);
            GameObject go = new GameObject();
            //go.AddComponent<Chessman>();

            if (piece.Name == "King") { go.AddComponent<king>(); }
            if (piece.Name == "Queen") { go.AddComponent<queen>(); }
            if (piece.Name == "Bishop") { go.AddComponent<bishop>(); }
            if (piece.Name == "Knight") { go.AddComponent<knight>(); }
            if (piece.Name == "Rook") { go.AddComponent<rook>(); }
            if (piece.Name == "pawn(Clone)") { go.AddComponent<pawn>(); }

            Chessman c = go.GetComponent<Chessman>();
            boardcopy.Chessmans[piece.SaveX, piece.SaveY] = c;
            c.SetPosition(piece.SaveX, piece.SaveY); // sets c's current x and y
            c.gameObject.tag = "clone";
            c.index = piece.index;
            c.name = piece.Name;
            c.isChecked = piece.isChecked;
            c.isMated = piece.isMated;
            c.pawnDirection = piece.pawnDirection;
            c.Rank = piece.Rank;
            c.isBlue = piece.isBlue;
            c.isYellow = piece.isYellow;
            c.isRed = piece.isRed;
            c.isGreen = piece.isGreen;

            c.transform.SetParent(boardcopy.transform); //make it a child of the object this script is attached to.  in this case, a piece is a child of the board.
            //print(c.getColor() + " " + c.name + " (" + c.CurrentX + ", " + c.CurrentY + ")");
            boardcopy.activeChessman.Add(go);
        }

        //ASSIGN THE ENPASSANT VALUES
        List<GSData> enpassData = data["Enpass"];
        Enpass e0 = Enpass.CreateFromJSON(enpassData[0].JSON);
        boardcopy.EnPassantMoveBlue[0] = e0.number;
        Enpass e1 = Enpass.CreateFromJSON(enpassData[1].JSON);
        boardcopy.EnPassantMoveBlue[1] = e1.number;
        Enpass e2 = Enpass.CreateFromJSON(enpassData[2].JSON);
        boardcopy.EnPassantMoveYellow[0] = e2.number;
        Enpass e3 = Enpass.CreateFromJSON(enpassData[3].JSON);
        boardcopy.EnPassantMoveYellow[1] = e3.number;
        Enpass e4 = Enpass.CreateFromJSON(enpassData[4].JSON);
        boardcopy.EnPassantMoveRed[0] = e4.number;
        Enpass e5 = Enpass.CreateFromJSON(enpassData[5].JSON);
        boardcopy.EnPassantMoveRed[1] = e5.number;
        Enpass e6 = Enpass.CreateFromJSON(enpassData[6].JSON);
        boardcopy.EnPassantMoveGreen[0] = e6.number;
        Enpass e7 = Enpass.CreateFromJSON(enpassData[7].JSON);
        boardcopy.EnPassantMoveGreen[1] = e7.number;

        // get last pieces
        List<GSData> LastPieces = data["LastPieces"];
        if (LastPieces != null)
        {
            for (int i = 0; i < LastPieces.Count; i++)
            {
                PieceData piece = PieceData.CreateFromJSON(LastPieces[i].JSON);
                if (piece.isBlue) { boardcopy.resetManBlue = boardcopy.Chessmans[piece.SaveX, piece.SaveY]; }
                if (piece.isYellow) { boardcopy.resetManYellow = boardcopy.Chessmans[piece.SaveX, piece.SaveY]; }
                if (piece.isRed) { boardcopy.resetManRed = boardcopy.Chessmans[piece.SaveX, piece.SaveY]; }
                if (piece.isGreen) { boardcopy.resetManGreen = boardcopy.Chessmans[piece.SaveX, piece.SaveY]; }
            }
        }

        // get the leaders
        List<GSData> Ks = data["ks"];
        for (int i = 0; i < Ks.Count; i++)
        {
            PieceData piece = PieceData.CreateFromJSON(Ks[i].JSON);
            if (piece.isBlue) { boardcopy.k1 = boardcopy.Chessmans[piece.SaveX, piece.SaveY]; }
            if (piece.isYellow) { boardcopy.k2 = boardcopy.Chessmans[piece.SaveX, piece.SaveY]; }
            if (piece.isRed) { boardcopy.k3 = boardcopy.Chessmans[piece.SaveX, piece.SaveY]; }
            if (piece.isGreen) { boardcopy.k4 = boardcopy.Chessmans[piece.SaveX, piece.SaveY]; }
        }

        // get last tiles
        GSData lastBlueTiles = data["LastTiles"][0];
        if (lastBlueTiles != null)
        {
            LastTiles b = LastTiles.CreateFromJSON(lastBlueTiles.JSON);
            boardcopy.lastBlueX = b.LastX;
            boardcopy.lastBlueY = b.LastY;
        }

        GSData lastYellowTiles = data["LastTiles"][1];
        if (lastYellowTiles != null)
        {
            LastTiles y = LastTiles.CreateFromJSON(lastYellowTiles.JSON);
            boardcopy.lastYellowX = y.LastX;
            boardcopy.lastYellowY = y.LastY;
        }

        GSData lastRedTiles = data["LastTiles"][2];
        if (lastRedTiles != null)
        {
            LastTiles r = LastTiles.CreateFromJSON(lastRedTiles.JSON);
            boardcopy.lastRedX = r.LastX;
            boardcopy.lastRedY = r.LastY;
        }

        GSData lastGreenTiles = data["LastTiles"][3];
        if (lastGreenTiles != null)
        {
            LastTiles g = LastTiles.CreateFromJSON(lastGreenTiles.JSON);
            boardcopy.lastGreenX = g.LastX;
            boardcopy.lastGreenY = g.LastY;
        }
        Destroy(BoardManagerGameObject);
        return boardcopy;
    }
    private EvaluationBoard GetBoardForEvaluation()
    {
        // Serialize the game data.
        Dictionary<string, List<GSData>> dict = SerializeForEvaluation();

        // Deserialize Game Data into a new boardmanager objects - one to fuck with and the other to retain/revert.
        GameObject BoardManagerGameObject = new GameObject();
        BoardManagerGameObject.AddComponent<EvaluationBoard>();
        boardcopy = BoardManagerGameObject.GetComponent<EvaluationBoard>();
        BoardManagerGameObject.name = "EvaluationBoard";
        boardcopy = CloneBoardmanager(dict);
        Destroy(BoardManagerGameObject);
        return boardcopy;
    }
    private void New_Checkmate()
    {
        if (!blueout && k1.isChecked)
        {
            int TotalPossibleMoves = 0;
            int MovesThatResultInCheck = 0;
            List<string> ListMovesThatResultInCheck = new List<string>();

            foreach (GameObject go in activeChessman)
            {
                Chessman me = go.GetComponent<Chessman>();
                if (me.isBlue)
                {
                    bool[,] theseallowedMoves;
                    bool[,] thoseallowedMoves;
                    theseallowedMoves = go.GetComponent<Chessman>().PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen);

                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (theseallowedMoves[i, j])
                            {
                                //Debug.Log(me.name + " " + me.CurrentX + ", " + me.CurrentY);
                                //Debug.Log(i + ", " + j);

                                // if this is a tile within my piece's movement.
                                // and this is not a retreat tile, nor am i a retreat piece.
                                bool isRetreatTile = false;
                                bool isRetreatPiece = false;

                                if (lastBlueX == i && lastBlueY == j)
                                {
                                    isRetreatTile = true;
                                }
                                else
                                {
                                    isRetreatTile = false;
                                }
                                if (resetManBlue)
                                {
                                    if (Chessmans[me.CurrentX, me.CurrentY] == Chessmans[resetManBlue.CurrentX, resetManBlue.CurrentY])
                                    {
                                        isRetreatPiece = true;
                                    }
                                }
                                else
                                {
                                    isRetreatPiece = false;
                                }

                                bool ShouldEvaluate = true;

                                if (isRetreatPiece && isRetreatTile)
                                {
                                    ShouldEvaluate = false;
                                }

                                if (ShouldEvaluate)
                                {
                                    boardcopy = GetBoardForEvaluation();

                                    if (Chessmans[i, j] != null && Chessmans[i, j].isBlue) //if there's  a piece in the way then i need to capture it, and then respawn it later...
                                    {
                                        // 'move' there. it shouldnt do it physically, only logically in this hypothetical board manager.
                                        // capture a piece if it is there  - No need to store
                                        print(me.getColor() + " " + me.name + " from (" + me.CurrentX + ", " + me.CurrentY + ") KILLZ " +
                                        boardcopy.Chessmans[i, j].getColor() + " " + boardcopy.Chessmans[i, j].name + " at (" + i + " ," + j + ")");
                                        boardcopy.activeChessman.Remove(boardcopy.Chessmans[i, j].gameObject);
                                        Destroy(boardcopy.Chessmans[i, j].gameObject);

                                        if (boardcopy.Chessmans[i, j] == null)
                                        {
                                            print("this motherfucker was indeed removed from the virtual board");
                                        }
                                    }

                                    boardcopy.mateoldX = me.CurrentX;
                                    boardcopy.mateoldY = me.CurrentY;

                                    // Move there
                                    boardcopy.Chessmans[me.CurrentX, me.CurrentY].SetPosition(i, j);
                                    boardcopy.Chessmans[i, j] = boardcopy.Chessmans[me.CurrentX, me.CurrentY];
                                    boardcopy.Chessmans[boardcopy.mateoldX, boardcopy.mateoldY] = null;
                                    TotalPossibleMoves++;
                                    // consider all of the pieces that are not MY TEAM's color.
                                    foreach (GameObject a in boardcopy.activeChessman)
                                    {
                                        if (!a.GetComponent<Chessman>().isBlue)
                                        {
                                            Chessman enemy = a.GetComponent<Chessman>();

                                            // get each piece's possible movements.
                                            thoseallowedMoves = enemy.PossibleMove(boardcopy.Chessmans,
                                                                                   boardcopy.playerTurn,
                                                                                   boardcopy.EnPassantMoveBlue,
                                                                                   boardcopy.EnPassantMoveYellow,
                                                                                   boardcopy.EnPassantMoveRed,
                                                                                   boardcopy.EnPassantMoveGreen);
                                            int FoundCheck = 0;
                                            for (int g = 0; g < 12; g++)
                                            {
                                                for (int k = 0; k < 12; k++)
                                                {
                                                    // see if any of those movements can capture my leader
                                                    if (thoseallowedMoves[g, k])
                                                    {
                                                        if (g == boardcopy.k1.CurrentX && k == boardcopy.k1.CurrentY)
                                                        {
                                                            FoundCheck++;

                                                            // silly shit drunk me came up with that i probably should figure out what it does...
                                                            string codeword = me.name + " from " + boardcopy.mateoldX + ", " + boardcopy.mateoldY + " to " + i + ", " + j;

                                                            if (ListMovesThatResultInCheck.Count == 0)
                                                            {
                                                                MovesThatResultInCheck++;
                                                                ListMovesThatResultInCheck.Add(codeword);
                                                            }
                                                            else
                                                            {
                                                                bool MatchFound = false;
                                                                foreach (string s in ListMovesThatResultInCheck)
                                                                {
                                                                    if (codeword == s)
                                                                    {
                                                                        MatchFound = true;
                                                                    }
                                                                }
                                                                if (!MatchFound)
                                                                {
                                                                    MovesThatResultInCheck++;
                                                                    ListMovesThatResultInCheck.Add(codeword);
                                                                }
                                                            }
                                                            // Debug.Log(codeword);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    Destroy(boardcopy);
                                }
                            }
                        }
                    }
                }
            }
            if (TotalPossibleMoves == MovesThatResultInCheck)
            {
                k1.isMated = true;
                if (playerTurn == 1)
                {
                    GameText.text = "The Blue team is in Checkmate. Make a move that results in Check.";
                }
                Debug.Log("Blue is in Checkmate");
            }
            else if (TotalPossibleMoves > MovesThatResultInCheck)
            {
                k1.isMated = false;
            }
        }
        if (!yelout && k2.isChecked)
        {
            int TotalPossibleMoves = 0;
            int MovesThatResultInCheck = 0;
            List<string> ListMovesThatResultInCheck = new List<string>();

            foreach (GameObject go in activeChessman)
            {
                Chessman me = go.GetComponent<Chessman>();
                if (me.isYellow)
                {
                    bool[,] theseallowedMoves;
                    bool[,] thoseallowedMoves;
                    theseallowedMoves = me.PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen);

                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (theseallowedMoves[i, j])
                            {
                                //Debug.Log(me.name + " " + me.CurrentX + ", " + me.CurrentY);
                                //Debug.Log(i + ", " + j);

                                // if this is a tile within my piece's movement.
                                // and this is not a retreat tile, nor am i a retreat piece.
                                bool isRetreatTile = false;
                                bool isRetreatPiece = false;

                                if (lastYellowX == i && lastYellowY == j)
                                {
                                    isRetreatTile = true;
                                }
                                else
                                {
                                    isRetreatTile = false;
                                }
                                if (resetManYellow)
                                {
                                    if (Chessmans[me.CurrentX, me.CurrentY] == Chessmans[resetManYellow.CurrentX, resetManYellow.CurrentY])
                                    {
                                        isRetreatPiece = true;
                                    }
                                }
                                else
                                {
                                    isRetreatPiece = false;
                                }

                                bool ShouldEvaluate = true;

                                if (isRetreatPiece && isRetreatTile)
                                {
                                    ShouldEvaluate = false;
                                }

                                if (ShouldEvaluate)
                                {
                                    boardcopy = GetBoardForEvaluation();

                                    if (boardcopy.Chessmans[i, j] != null && !boardcopy.Chessmans[i, j].isYellow) //if there's  a piece in the way then i need to capture it, and then respawn it later...
                                    {
                                        // 'move' there. it shouldnt do it physically, only logically in this hypothetical board manager.
                                        // capture a piece if it is there  - No need to store
                                        print(me.getColor() + " " + me.name + " from (" + me.CurrentX + ", " + me.CurrentY + ") KILLZ " +
                                        boardcopy.Chessmans[i, j].getColor() + " " + boardcopy.Chessmans[i, j].name + " at (" + i + " ," + j + ")");
                                        boardcopy.activeChessman.Remove(boardcopy.Chessmans[i, j].gameObject);
                                        Destroy(boardcopy.Chessmans[i, j].gameObject);

                                        if (boardcopy.Chessmans[i, j] == null)
                                        {
                                            print("this motherfucker was indeed removed from the virtual board");
                                        }
                                    }

                                    boardcopy.mateoldX = me.CurrentX;
                                    boardcopy.mateoldY = me.CurrentY;

                                    // Move there
                                    boardcopy.Chessmans[me.CurrentX, me.CurrentY].SetPosition(i, j);
                                    boardcopy.Chessmans[i, j] = boardcopy.Chessmans[me.CurrentX, me.CurrentY];
                                    boardcopy.Chessmans[boardcopy.mateoldX, boardcopy.mateoldY] = null;
                                    TotalPossibleMoves++;
                                    // consider all of the pieces that are not MY TEAM's color.
                                    foreach (GameObject a in boardcopy.activeChessman)
                                    {
                                        if (!a.GetComponent<Chessman>().isYellow)
                                        {
                                            Chessman enemy = a.GetComponent<Chessman>();

                                            // get each piece's possible movements.
                                            thoseallowedMoves = enemy.PossibleMove(boardcopy.Chessmans,
                                                                                   boardcopy.playerTurn,
                                                                                   boardcopy.EnPassantMoveBlue,
                                                                                   boardcopy.EnPassantMoveYellow,
                                                                                   boardcopy.EnPassantMoveRed,
                                                                                   boardcopy.EnPassantMoveGreen);
                                            int FoundCheck = 0;
                                            for (int g = 0; g < 12; g++)
                                            {
                                                for (int k = 0; k < 12; k++)
                                                {
                                                    // see if any of those movements can capture my leader
                                                    if (thoseallowedMoves[g, k])
                                                    {
                                                        if (g == boardcopy.k2.CurrentX && k == boardcopy.k2.CurrentY)
                                                        {
                                                            FoundCheck++;

                                                            // silly shit drunk me came up with that i probably should figure out what it does...
                                                            string codeword = me.name + " from " + boardcopy.mateoldX + ", " + boardcopy.mateoldY + " to " + i + ", " + j;

                                                            if (ListMovesThatResultInCheck.Count == 0)
                                                            {
                                                                MovesThatResultInCheck++;
                                                                ListMovesThatResultInCheck.Add(codeword);
                                                            }
                                                            else
                                                            {
                                                                bool MatchFound = false;
                                                                foreach (string s in ListMovesThatResultInCheck)
                                                                {
                                                                    if (codeword == s)
                                                                    {
                                                                        MatchFound = true;
                                                                    }
                                                                }
                                                                if (!MatchFound)
                                                                {
                                                                    MovesThatResultInCheck++;
                                                                    ListMovesThatResultInCheck.Add(codeword);
                                                                }
                                                            }
                                                            // Debug.Log(codeword);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    Destroy(boardcopy);
                                }
                            }
                        }
                    }
                }
            }
            if (TotalPossibleMoves == MovesThatResultInCheck)
            {
                k2.isMated = true;
                if (playerTurn == 2)
                {
                    GameText.text = "The Yellow team is in Checkmate. Make a move that results in Check.";
                }
                Debug.Log("Yellow is in Checkmate");
            }
            else if (TotalPossibleMoves > MovesThatResultInCheck)
            {
                k2.isMated = false;
            }
        }
        if (!redout && k3.isChecked)
        {
            int TotalPossibleMoves = 0;
            int MovesThatResultInCheck = 0;
            List<string> ListMovesThatResultInCheck = new List<string>();

            foreach (GameObject go in activeChessman)
            {
                Chessman me = go.GetComponent<Chessman>();
                if (me.isRed)
                {
                    bool[,] theseallowedMoves;
                    bool[,] thoseallowedMoves;
                    theseallowedMoves = me.PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen);

                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (theseallowedMoves[i, j])
                            {
                                //Debug.Log(me.name + " " + me.CurrentX + ", " + me.CurrentY);
                                //Debug.Log(i + ", " + j);

                                // if this is a tile within my piece's movement.
                                // and this is not a retreat tile, nor am i a retreat piece.
                                bool isRetreatTile = false;
                                bool isRetreatPiece = false;

                                if (lastRedX == i && lastRedY == j)
                                {
                                    isRetreatTile = true;
                                }
                                else
                                {
                                    isRetreatTile = false;
                                }
                                if (resetManRed)
                                {
                                    if (Chessmans[me.CurrentX, me.CurrentY] == Chessmans[resetManRed.CurrentX, resetManRed.CurrentY])
                                    {
                                        isRetreatPiece = true;
                                    }
                                }
                                else
                                {
                                    isRetreatPiece = false;
                                }

                                bool ShouldEvaluate = true;

                                if (isRetreatPiece && isRetreatTile)
                                {
                                    ShouldEvaluate = false;
                                }

                                if (ShouldEvaluate)
                                {
                                    boardcopy = GetBoardForEvaluation();

                                    if (boardcopy.Chessmans[i, j] != null && !boardcopy.Chessmans[i, j].isRed) //if there's  a piece in the way then i need to capture it, and then respawn it later...
                                    {
                                        // 'move' there. it shouldnt do it physically, only logically in this hypothetical board manager.
                                        // capture a piece if it is there  - No need to store
                                        print(me.getColor() + " " + me.name + " from (" + me.CurrentX + ", " + me.CurrentY + ") KILLZ " +
                                        boardcopy.Chessmans[i, j].getColor() + " " + boardcopy.Chessmans[i, j].name + " at (" + i + " ," + j + ")");
                                        boardcopy.activeChessman.Remove(boardcopy.Chessmans[i, j].gameObject);
                                        Destroy(boardcopy.Chessmans[i, j].gameObject);

                                        if (boardcopy.Chessmans[i, j] == null)
                                        {
                                            print("this motherfucker was indeed removed from the virtual board");
                                        }
                                    }


                                    boardcopy.mateoldX = me.CurrentX;
                                    boardcopy.mateoldY = me.CurrentY;

                                    // Move there
                                    boardcopy.Chessmans[me.CurrentX, me.CurrentY].SetPosition(i, j);
                                    boardcopy.Chessmans[i, j] = boardcopy.Chessmans[me.CurrentX, me.CurrentY];
                                    boardcopy.Chessmans[boardcopy.mateoldX, boardcopy.mateoldY] = null;
                                    TotalPossibleMoves++;
                                    // consider all of the pieces that are not MY TEAM's color.
                                    foreach (GameObject a in boardcopy.activeChessman)
                                    {
                                        if (!a.GetComponent<Chessman>().isRed)
                                        {
                                            Chessman enemy = a.GetComponent<Chessman>();

                                            // get each piece's possible movements.
                                            thoseallowedMoves = enemy.PossibleMove(boardcopy.Chessmans,
                                                                                   boardcopy.playerTurn,
                                                                                   boardcopy.EnPassantMoveBlue,
                                                                                   boardcopy.EnPassantMoveYellow,
                                                                                   boardcopy.EnPassantMoveRed,
                                                                                   boardcopy.EnPassantMoveGreen);
                                            int FoundCheck = 0;
                                            for (int g = 0; g < 12; g++)
                                            {
                                                for (int k = 0; k < 12; k++)
                                                {
                                                    // see if any of those movements can capture my leader
                                                    if (thoseallowedMoves[g, k])
                                                    {
                                                        if (g == boardcopy.k3.CurrentX && k == boardcopy.k3.CurrentY)
                                                        {
                                                            FoundCheck++;

                                                            // silly shit drunk me came up with that i probably should figure out what it does...
                                                            string codeword = me.name + " from " + boardcopy.mateoldX + ", " + boardcopy.mateoldY + " to " + i + ", " + j;

                                                            if (ListMovesThatResultInCheck.Count == 0)
                                                            {
                                                                MovesThatResultInCheck++;
                                                                ListMovesThatResultInCheck.Add(codeword);
                                                            }
                                                            else
                                                            {
                                                                bool MatchFound = false;
                                                                foreach (string s in ListMovesThatResultInCheck)
                                                                {
                                                                    if (codeword == s)
                                                                    {
                                                                        MatchFound = true;
                                                                    }
                                                                }
                                                                if (!MatchFound)
                                                                {
                                                                    MovesThatResultInCheck++;
                                                                    ListMovesThatResultInCheck.Add(codeword);
                                                                }
                                                            }
                                                            // Debug.Log(codeword);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    Destroy(boardcopy);
                                }
                            }
                        }
                    }
                }
            }
            if (TotalPossibleMoves == MovesThatResultInCheck)
            {
                k3.isMated = true;
                if (playerTurn == 3)
                {
                    GameText.text = "The Red team is in Checkmate. Make a move that results in Check.";
                }
                Debug.Log("Red is in Checkmate");
            }
            else if (TotalPossibleMoves > MovesThatResultInCheck)
            {
                k3.isMated = false;
            }
        }
        if (!greenout && k4.isChecked)
        {
            int TotalPossibleMoves = 0;
            int MovesThatResultInCheck = 0;
            List<string> ListMovesThatResultInCheck = new List<string>();

            foreach (GameObject go in activeChessman)
            {
                Chessman me = go.GetComponent<Chessman>();
                if (me.isGreen)
                {
                    bool[,] theseallowedMoves;
                    bool[,] thoseallowedMoves;
                    theseallowedMoves = me.PossibleMove(Chessmans, playerTurn, EnPassantMoveBlue, EnPassantMoveYellow, EnPassantMoveRed, EnPassantMoveGreen);

                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (theseallowedMoves[i, j])
                            {
                                //Debug.Log(me.name + " " + me.CurrentX + ", " + me.CurrentY);
                                //Debug.Log(i + ", " + j);

                                // if this is a tile within my piece's movement.
                                // and this is not a retreat tile, nor am i a retreat piece.
                                bool isRetreatTile = false;
                                bool isRetreatPiece = false;

                                if (lastGreenX == i && lastGreenY == j)
                                {
                                    isRetreatTile = true;
                                }
                                else
                                {
                                    isRetreatTile = false;
                                }
                                if (resetManGreen)
                                {
                                    if (Chessmans[me.CurrentX, me.CurrentY] == Chessmans[resetManGreen.CurrentX, resetManGreen.CurrentY])
                                    {
                                        isRetreatPiece = true;
                                    }
                                }
                                else
                                {
                                    isRetreatPiece = false;
                                }

                                bool ShouldEvaluate = true;

                                if (isRetreatPiece && isRetreatTile)
                                {
                                    ShouldEvaluate = false;
                                }

                                if (ShouldEvaluate)
                                {
                                    boardcopy = GetBoardForEvaluation();

                                    if (boardcopy.Chessmans[i, j] != null && !boardcopy.Chessmans[i, j].isGreen) //if there's  a piece in the way then i need to capture it, and then respawn it later...
                                    {
                                        // 'move' there. it shouldnt do it physically, only logically in this hypothetical board manager.
                                        // capture a piece if it is there  - No need to store
                                        print(me.getColor() + " " + me.name + " from (" + me.CurrentX + ", " + me.CurrentY + ") KILLZ " +
                                        boardcopy.Chessmans[i, j].getColor() + " " + boardcopy.Chessmans[i, j].name + " at (" + i + " ," + j + ")");
                                        boardcopy.activeChessman.Remove(boardcopy.Chessmans[i, j].gameObject);
                                        Destroy(boardcopy.Chessmans[i, j].gameObject);

                                        if (boardcopy.Chessmans[i, j] == null)
                                        {
                                            print("this motherfucker was indeed removed from the virtual board");
                                        }
                                    }


                                    boardcopy.mateoldX = me.CurrentX;
                                    boardcopy.mateoldY = me.CurrentY;

                                    // Move there
                                    boardcopy.Chessmans[me.CurrentX, me.CurrentY].SetPosition(i, j);
                                    boardcopy.Chessmans[i, j] = boardcopy.Chessmans[me.CurrentX, me.CurrentY];
                                    boardcopy.Chessmans[boardcopy.mateoldX, boardcopy.mateoldY] = null;
                                    TotalPossibleMoves++;
                                    // consider all of the pieces that are not MY TEAM's color.
                                    foreach (GameObject a in boardcopy.activeChessman)
                                    {
                                        if (!a.GetComponent<Chessman>().isGreen)
                                        {
                                            Chessman enemy = a.GetComponent<Chessman>();

                                            // get each piece's possible movements.
                                            thoseallowedMoves = enemy.PossibleMove(boardcopy.Chessmans,
                                                                                   boardcopy.playerTurn,
                                                                                   boardcopy.EnPassantMoveBlue,
                                                                                   boardcopy.EnPassantMoveYellow,
                                                                                   boardcopy.EnPassantMoveRed,
                                                                                   boardcopy.EnPassantMoveGreen);
                                            int FoundCheck = 0;
                                            for (int g = 0; g < 12; g++)
                                            {
                                                for (int k = 0; k < 12; k++)
                                                {
                                                    // The code makes it here....

                                                    // see if any of those movements can capture my leader
                                                    if (thoseallowedMoves[g, k])
                                                    {
                                                        if (g == boardcopy.k4.CurrentX && k == boardcopy.k4.CurrentY)
                                                        {
                                                            FoundCheck++;

                                                            // silly shit drunk me came up with that i probably should figure out what it does...
                                                            string codeword = me.name + " from " + boardcopy.mateoldX + ", " + boardcopy.mateoldY + " to " + i + ", " + j;

                                                            if (ListMovesThatResultInCheck.Count == 0)
                                                            {
                                                                MovesThatResultInCheck++;
                                                                ListMovesThatResultInCheck.Add(codeword);
                                                            }
                                                            else
                                                            {
                                                                bool MatchFound = false;
                                                                foreach (string s in ListMovesThatResultInCheck)
                                                                {
                                                                    if (codeword == s)
                                                                    {
                                                                        MatchFound = true;
                                                                    }
                                                                }
                                                                if (!MatchFound)
                                                                {
                                                                    MovesThatResultInCheck++;
                                                                    ListMovesThatResultInCheck.Add(codeword);
                                                                }
                                                            }
                                                            //Debug.Log(codeword);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    // reset the board to the save state and preform the loop again.
                                    Destroy(boardcopy);
                                }
                            }
                        }
                    }
                }
            }
            if (TotalPossibleMoves == MovesThatResultInCheck)
            {
                k4.isMated = true;
                if (playerTurn == 4)
                {
                    GameText.text = "The Green team is in Checkmate. Make a move that results in Check.";
                }
                Debug.Log("Green is in Checkmate");
            }
        }
    }


    public void ShowActiveChessmen()
    {

        foreach (GameObject go in activeChessman)
        {
            Debug.Log(go.GetComponent<Chessman>().getColor() + " " + go.name + "(" + go.GetComponent<Chessman>().CurrentX + ", " + go.GetComponent<Chessman>().CurrentY + ")");
        }

        Debug.Log(activeChessman.Count + " Active Chessman");
    }
    //called at the end of the movement function....might need to check this to be more cohesive with check enforcment.
    private void PrisonExchange(int x, int y)
    {
        man = Chessmans[x, y];

        if (man.name == "pawn(Clone)")
        {
            if (k1 != null)
            {
                if (!k1.isChecked)
                {
                    if (man.isBlue)
                    {
                        id = 1;

                        if (man.pawnDirection == 1)
                        {
                            if (yelout == false)
                            {
                                if (man.CurrentX <= 5 && man.CurrentY == 11)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(yelPrisonersBlue);
                                    listid = 4;
                                }
                            }
                            if (redout == false)
                            {
                                if (man.CurrentX >= 6 && man.CurrentY == 11)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(redPrisonersBlue);
                                    listid = 7;
                                }
                            }
                        }

                        if (man.pawnDirection == 2)
                        {
                            if (redout == false)
                            {
                                if (man.CurrentX == 11 && man.CurrentY >= 6)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(redPrisonersBlue);
                                    listid = 7;
                                }
                            }
                            if (greenout == false)
                            {
                                if (man.CurrentX == 11 && man.CurrentY <= 5)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(greenPrisonersBlue);
                                    listid = 10;
                                }
                            }
                        }
                    }
                }
            }
            //
            if (k2 != null)
            {
                if (!k2.isChecked)
                {
                    if (man.isYellow)
                    {
                        id = 2;

                        if (man.pawnDirection == 1)
                        {
                            if (redout == false)
                            {
                                if (man.CurrentX == 11 && man.CurrentY >= 6)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(redPrisonersYellow);
                                    listid = 8;
                                }
                            }
                            if (greenout == false)
                            {
                                if (man.CurrentX == 11 && man.CurrentY <= 5)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(greenPrisonersYellow);
                                    listid = 11;
                                }
                            }
                        }

                        if (man.pawnDirection == 2)
                        {
                            if (blueout == false)
                            {
                                if (man.CurrentX <= 5 && man.CurrentY == 0)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(bluePrisonersYellow);
                                    listid = 1;
                                }
                            }

                            if (greenout == false)
                            {
                                if (man.CurrentX >= 6 && man.CurrentY == 0)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(greenPrisonersYellow);
                                    listid = 11;
                                }
                            }
                        }
                    }
                }
            }

            //
            if (k3 != null)
            {
                if (!k3.isChecked)
                {
                    if (man.isRed)
                    {
                        id = 3;

                        if (man.pawnDirection == 1)
                        {
                            if (greenout == false)
                            {
                                if (man.CurrentX >= 6 && man.CurrentY == 0)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(greenPrisonersRed);
                                    listid = 12;
                                }
                            }
                            if (blueout == false)
                            {
                                if (man.CurrentX <= 5 && man.CurrentY == 0)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(bluePrisonersRed);
                                    listid = 2;
                                }
                            }
                        }
                        if (man.pawnDirection == 2)
                        {
                            if (yelout == false)
                            {
                                if (man.CurrentX == 0 && man.CurrentY >= 6)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(yelPrisonersRed);
                                    listid = 5;
                                }
                            }

                            if (blueout == false)
                            {
                                if (man.CurrentX == 0 && man.CurrentY <= 5)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(bluePrisonersRed);
                                    listid = 2;
                                }
                            }
                        }
                    }
                }
            }
            //
            if (k4 != null)
            {
                if (!k4.isChecked)
                {
                    if (man.isGreen)
                    {
                        id = 4;

                        if (man.pawnDirection == 1)
                        {
                            if (blueout == false)
                            {
                                if (man.CurrentX == 0 && man.CurrentY <= 5)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(bluePrisonersGreen);
                                    listid = 3;
                                }
                            }
                            if (yelout == false)
                            {
                                if (man.CurrentX == 0 && man.CurrentY >= 6)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(yelPrisonersGreen);
                                    listid = 6;
                                }
                            }
                        }
                        if (man.pawnDirection == 2)
                        {
                            if (redout == false)
                            {
                                if (man.CurrentX >= 6 && man.CurrentY == 11)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(redPrisonersGreen);
                                    listid = 9;
                                }
                            }
                            if (yelout == false)
                            {
                                if (man.CurrentX <= 5 && man.CurrentY == 11)
                                {
                                    isDisplayed = true;
                                    prisonerList.AddRange(yelPrisonersGreen);
                                    listid = 6;
                                }
                            }
                        }
                    }
                }
            }
            //

            if (isDisplayed)
            {
                if (prisonerList.Count > 0)
                {
                    myCanvas.GetComponent<Canvas>().enabled = true;
                    int GreatestRank = 0;
                    List<GameObject> ListOfPrisonButtons = new List<GameObject>();
                    string HighestPiece = "piece";

                    //Populate the list AND figure out which is the highest ranking piece.
                    foreach (string s in prisonerList)
                    {
                        int ThisRank = 0;

                        GameObject go = Instantiate(PrisonerListItem, PrisListingSpawn.transform);
                        go.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = s;

                        //the rest of this if for if the AI is doing pris exchange.
                        ListOfPrisonButtons.Add(go);

                        if (s == "Rook")
                        {
                            ThisRank = 1;
                        }
                        if (s == "Knight")
                        {
                            ThisRank = 2;
                        }
                        if (s == "Bishop")
                        {
                            ThisRank = 3;
                        }
                        if (s == "Queen")
                        {
                            ThisRank = 4;
                        }

                        if (ThisRank > GreatestRank)
                        {
                            GreatestRank = ThisRank;
                            HighestPiece = s;
                        }
                    }

                    //if there is AI involved - we must pick for them using what we know about the highest ranking piece as defined above.
                    if (man.isBlue && isBlueAI ||
                        man.isYellow && isYellowAI ||
                        man.isRed && isRedAI ||
                        man.isGreen && isGreenAI)
                    {
                        foreach (GameObject go in ListOfPrisonButtons)
                        {
                            if (go.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text == HighestPiece)
                            {
                                go.GetComponentInChildren<Button>().onClick.Invoke();
                                Debug.Log(go.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text + " was clicked by the AI");
                                GameText.text = go.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text + " was clicked by the AI";
                                okCanvasRemove();
                                break;
                            }
                        }
                    }


                }
                if (prisonerList.Count == 0)
                {
                    //what is this? why is this here? past vinny was off his rocker....commenting out for now.
                    //okCanvas.GetComponent<Canvas>().enabled = true;


                    //make sure the screen is not displayed for an AI player turn. turnLoop() has already advanced the playerTurn by this time, but turnControl() has not yet been called (which skips a playerTurn number when on a player that is out).
                    if (man.isBlue && isBlueAI ||
                        man.isYellow && isYellowAI ||
                        man.isRed && isRedAI ||
                        man.isGreen && isGreenAI)
                    {
                        okCanvasRemove();
                    }
                }
            }

        }
    }

    public void okCanvasRemove()
    {
        isDisplayed = false;
        okCanvas.GetComponent<Canvas>().enabled = false;
    }

    public bool ThisMoveResultsInCheck(int i, int j, int ColorCode, int x, int y)
    {
        boardcopy = GetBoardForEvaluation();

        // i and j are where we want to move. x and y are where we are.


        Chessman me = boardcopy.Chessmans[x, y];
        bool[,] thoseallowedMoves;
        int FoundCheck = 0;
        if (boardcopy.Chessmans[i, j] != null && boardcopy.Chessmans[i, j].getColor() != ColorCode) //if there's  a piece in the way then i need to capture it, and then respawn it later...
        {
            // 'move' there. it shouldnt do it physically, only logically in this hypothetical board manager.
            // capture a piece if it is there  - No need to store

            // boardcopy.activeChessman.Remove(boardcopy.Chessmans[i, j].gameObject);
            // Destroy(boardcopy.Chessmans[i, j].gameObject);
            // enemyHere = true;
        }

        // Move there
        boardcopy.Chessmans[me.CurrentX, me.CurrentY] = null; //set the current position to null because we're going to move it.
        me.SetPosition(i, j);
        boardcopy.Chessmans[i, j] = me; //make the intended peice BE the selected piece
        //boardcopy.Chessmans[me.CurrentX, me.CurrentY].SetPosition(i, j);
        //boardcopy.Chessmans[i, j] = boardcopy.Chessmans[me.CurrentX, me.CurrentY];
        //boardcopy.Chessmans[x, y] = null;

        // consider all of the pieces that are not MY TEAM's color.
        foreach (GameObject a in activeChessman)
        {
            if (a.GetComponent<Chessman>().getColor() != ColorCode)
            {
                Chessman enemy = a.GetComponent<Chessman>();

                if (enemy.CurrentX != i && enemy.CurrentY != j) //if this piece is not in the location we moved (which would have meant it got captured)
                {
                    // get each piece's possible movements.
                    thoseallowedMoves = enemy.PossibleMove(boardcopy.Chessmans,
                                                           boardcopy.playerTurn,
                                                           boardcopy.EnPassantMoveBlue,
                                                           boardcopy.EnPassantMoveYellow,
                                                           boardcopy.EnPassantMoveRed,
                                                           boardcopy.EnPassantMoveGreen);

                    for (int g = 0; g < 12; g++)
                    {
                        for (int k = 0; k < 12; k++)
                        {
                            // see if any of those movements can capture my leader
                            if (thoseallowedMoves[g, k])
                            {
                                // need to check and make sure its not their retreat tile, or check tile.....
                                // if you're moving a piece that is NOT your leader then it doesn't pick up the fact that you're moving into check.
                                if (ColorCode == 1)
                                {
                                    if (g == boardcopy.k1.CurrentX && k == boardcopy.k1.CurrentY)
                                    {
                                        FoundCheck++;
                                        Destroy(boardcopy);
                                        return true;
                                    }
                                }
                                if (ColorCode == 2)
                                {
                                    if (g == boardcopy.k2.CurrentX && k == boardcopy.k2.CurrentY)
                                    {
                                        FoundCheck++;
                                        Destroy(boardcopy);
                                        return true;
                                    }
                                }
                                if (ColorCode == 3)
                                {
                                    if (g == boardcopy.k3.CurrentX && k == boardcopy.k3.CurrentY)
                                    {
                                        FoundCheck++;
                                        Destroy(boardcopy);
                                        return true;
                                    }
                                }
                                if (ColorCode == 4)
                                {
                                    if (g == boardcopy.k4.CurrentX && k == boardcopy.k4.CurrentY)
                                    {
                                        FoundCheck++;
                                        Destroy(boardcopy);
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        Destroy(boardcopy);
        return false;
    }

    public bool ThisIsARetreatMove(int i, int j, int ColorCode, int x, int y)
    {
        bool isRetreatTile = false;
        bool isRetreatPiece = false;

        if(ColorCode == 1)
        {
            if (lastBlueX == i && lastBlueY == j)
            {
                isRetreatTile = true;
            }
            else
            {
                isRetreatTile = false;
            }
            if (resetManBlue)
            {
                if (Chessmans[x, y] == Chessmans[resetManBlue.CurrentX, resetManBlue.CurrentY])
                {
                    isRetreatPiece = true;
                }
            }
            else
            {
                isRetreatPiece = false;
            }
        }
        if (ColorCode == 2)
        {
            if (lastYellowX == i && lastYellowY == j)
            {
                isRetreatTile = true;
            }
            else
            {
                isRetreatTile = false;
            }
            if (resetManYellow)
            {
                if (Chessmans[x, y] == Chessmans[resetManYellow.CurrentX, resetManYellow.CurrentY])
                {
                    isRetreatPiece = true;
                }
            }
            else
            {
                isRetreatPiece = false;
            }
        }
        if (ColorCode == 3)
        {
            if (lastRedX == i && lastRedY == j)
            {
                isRetreatTile = true;
            }
            else
            {
                isRetreatTile = false;
            }
            if (resetManRed)
            {
                if (Chessmans[x, y] == Chessmans[resetManRed.CurrentX, resetManRed.CurrentY])
                {
                    isRetreatPiece = true;
                }
            }
            else
            {
                isRetreatPiece = false;
            }
        }
        if (ColorCode == 4)
        {
            if (lastGreenX == i && lastGreenY == j)
            {
                isRetreatTile = true;
            }
            else
            {
                isRetreatTile = false;
            }
            if (resetManGreen)
            {
                if (Chessmans[x, y] == Chessmans[resetManGreen.CurrentX, resetManGreen.CurrentY])
                {
                    isRetreatPiece = true;
                }
            }
            else
            {
                isRetreatPiece = false;
            }
        }

        if(isRetreatPiece && isRetreatTile)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}



public class EvaluationBoard : MonoBehaviour
{
    public List<GameObject> activeChessman;
    public bool[,] allowedMoves { set; get; }
    public Chessman[,] Chessmans { set; get; }
    public int[] EnPassantMoveBlue = new int[2] { -1, -1 };
    public int[] EnPassantMoveYellow = new int[2] { -1, -1 };
    public int[] EnPassantMoveRed = new int[2] { -1, -1 };
    public int[] EnPassantMoveGreen = new int[2] { -1, -1 };
    public int playerTurn = 1; //track whose turn it is. blue is 1, yellow is 2, red is 3, green is 4.
    public int prevTurn = 0;
    public int lastBlueX;
    public int lastBlueY;
    public int lastYellowX;
    public int lastYellowY;
    public int lastRedX;
    public int lastRedY;
    public int lastGreenX;
    public int lastGreenY;
    //The last piece each team moved.
    public Chessman resetManBlue;
    public Chessman resetManYellow;
    public Chessman resetManRed;
    public Chessman resetManGreen;
    public Chessman k1;
    public Chessman k2;
    public Chessman k3;
    public Chessman k4;
    public bool blueout = false;
    public bool yelout = false;
    public bool redout = false;
    public bool greenout = false;
    public int mateoldX = -1;
    public int mateoldY = -1;
}


