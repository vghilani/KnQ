using GameSparks.Core;
using GameSparks.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using UnityEngine.SceneManagement;

public class GameSparksManager : MonoBehaviour {

    /// <summary>The GameSparks Manager singleton</summary>
    private static GameSparksManager instance = null;
    public boardmanager bm;
    public mainMenu mm;
	  public Chat ChatVariable;
    public Chat BlueChatVariable, YellowChatVariable, RedChatVariable, GreenChatVariable;
    private int TurnTakencounter = 0;

    
    void Awake()
    {
        if (instance == null) // check to see if the instance has a reference
        {
            instance = this; // if not, give it a reference to this class...
            DontDestroyOnLoad(this.gameObject); // and make this object persistent as we load new scenes
        }
        else // if we already have a reference then remove the extra manager from the scene
        {
            Destroy(this.gameObject);
        }

        if(GameSparks.Api.Messages.ChallengeTurnTakenMessage.Listener == null)
        {
            GameSparks.Api.Messages.ChallengeTurnTakenMessage.Listener += TurnTakenHandler;
        }
        if(GameSparks.Api.Messages.ChallengeWonMessage.Listener == null)
        {
            GameSparks.Api.Messages.ChallengeWonMessage.Listener += GameWinHandler;
        }
        if(GameSparks.Api.Messages.ChallengeLostMessage.Listener == null)
        {
            GameSparks.Api.Messages.ChallengeLostMessage.Listener += GameLoseHandler;
        }
        if(GameSparks.Api.Messages.ScriptMessage_pubgame.Listener == null)
        {
            GameSparks.Api.Messages.ScriptMessage_pubgame.Listener += PublicGameInvite;
        }
        if (GameSparks.Api.Messages.ScriptMessage.Listener == null)
        {
            GameSparks.Api.Messages.ScriptMessage.Listener += ScriptMessageHandler;
        }
        if(GameSparks.Api.Messages.ChallengeIssuedMessage.Listener == null)
        {
            GameSparks.Api.Messages.ChallengeIssuedMessage.Listener += ChallangeInvite;
        }
        if(GameSparks.Api.Messages.ChallengeStartedMessage.Listener == null)
        {
            GameSparks.Api.Messages.ChallengeStartedMessage.Listener += ChallengeStartedHandler;
        }
        if(GameSparks.Api.Messages.ChallengeChatMessage.Listener == null)
        {
            GameSparks.Api.Messages.ChallengeChatMessage.Listener += ChallengeChatHandler;
        }
        if(GameSparks.Api.Messages.ScriptMessage_PlayerQuit.Listener == null)
        {
            GameSparks.Api.Messages.ScriptMessage_PlayerQuit.Listener += PlayerQuitHandler;
        }
        if(GameSparks.Api.Messages.ScriptMessage_Skip.Listener == null)
        {
            GameSparks.Api.Messages.ScriptMessage_Skip.Listener += SkipHandler;
        }
    		if(GameSparks.Api.Messages.ScriptMessage_DrawVote.Listener == null)
    		{
    			GameSparks.Api.Messages.ScriptMessage_DrawVote.Listener += DrawVoteHandler;
    		}
    		if(GameSparks.Api.Messages.ChallengeDrawnMessage.Listener == null)
    		{
    			GameSparks.Api.Messages.ChallengeDrawnMessage.Listener += ChallengeDrawHandler;
    		}
    		if(GameSparks.Api.Messages.ScriptMessage_MoveExpiredPlayer.Listener == null)
    		{
    			GameSparks.Api.Messages.ScriptMessage_MoveExpiredPlayer.Listener += MoveExpiredPlayerHandler;	
    		}
    		if(GameSparks.Api.Messages.ScriptMessage_PrivateChat.Listener == null)
    		{
    			GameSparks.Api.Messages.ScriptMessage_PrivateChat.Listener += PrivateChatHandler;
    		}
    }
      
	private void Update()
    {
        

        if(bm == null)
        {
            if(GameObject.Find("Chessboard"))
            {
                bm = GameObject.Find("Chessboard").GetComponent<boardmanager>();
            }
        }

        if(mm == null)
        {
            if(GameObject.Find("Menu Manager"))
            {
                mm = GameObject.Find("Menu Manager").GetComponent<mainMenu>();
            }
        }

        if(ChatVariable == null)
        {
            if(GameObject.FindGameObjectWithTag("chatMenu"))
            {
                ChatVariable = GameObject.FindGameObjectWithTag("chatMenu").GetComponent<Chat>();
            }
        }
    }


    void TurnTakenHandler(GameSparks.Api.Messages.ChallengeTurnTakenMessage _message)
    {
        if (SceneManager.GetActiveScene().name == "KingsAndQueens")
        {
            if (_message.Challenge.ChallengeId == bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            {
                if (TurnTakencounter == 0)
                {
                    if (_message.Challenge.ScriptData.GetString("LastPlayer") != bm.myID && !bm.DidPlayerTurnExpire)
                    {
                        Debug.Log("turn taken");
                        TurnTakencounter++;
                        //GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineTurnTaken();
                        GameObject.Find("Chessboard").GetComponent<boardmanager>().SmoothMove();
                        GameObject.Find("Chessboard").GetComponent<boardmanager>().Stalemate();

                        //if it becomes my turn while im watching the game, then 
                        //i must watch an unskippable ad.
                        if (_message.Challenge.NextPlayer == bm.myID)
                        {
                            GameObject.Find("AdManager").GetComponent<AdTest>().PlayRewardedAd();
                        }
                    }
                }
                else
                {
                    TurnTakencounter = 0;
                    Debug.Log("skipped one");
                }
            }
        }
    }
    void GameLoseHandler(GameSparks.Api.Messages.ChallengeLostMessage _message)
    {
        if(SceneManager.GetActiveScene().name == "KingsAndQueens")
        {
            if (_message.Challenge.ChallengeId == bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            {
                bm.GameLose(_message.WinnerName);
            }
        }

        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            foreach(GameObject go in mm.ListOfGames)
            {
                if(go.GetComponent<GameInviteMessage>().challengeId == _message.Challenge.ChallengeId)
                {
                    mm.ListOfGames.Remove(go);
                    Destroy(go.gameObject);
                }
            }
        }
    }
    void GameWinHandler(GameSparks.Api.Messages.ChallengeWonMessage _message)
    {
        if(SceneManager.GetActiveScene().name == "KingsAndQueens")
        {
            if (_message.Challenge.ChallengeId == bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            {
                bm.GameWin();
            }
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            foreach (GameObject go in mm.ListOfGames)
            {
                if (go.GetComponent<GameInviteMessage>().challengeId == _message.Challenge.ChallengeId)
                {
                    mm.ListOfGames.Remove(go);
                    Destroy(go.gameObject);
                }
            }
        }
    }
    void FriendMessageHandler(GameSparks.Api.Messages.ScriptMessage_friendRequestMessage _message)
    {
        GameObject go = Instantiate(mm.FriendMessage, mm.spawnpoint.transform);
        mm.MessageCount++;
        mm.ListOfFriendMessages.Add(go);
        MessageInfo fd = MessageInfo.CreateFromJSON(_message.Data.JSON);

        go.GetComponent<FriendMessageID>().friendDisplayName = fd.displayName;
        go.GetComponent<FriendMessageID>().MessageID = fd.requestId;
        go.GetComponent<FriendMessageID>().friendID = fd.senderId;
        go.GetComponent<FriendMessageID>().Message.text = fd.displayName + " would like to be your friend.";
    }
    void FriendAcceptedHandler(GameSparks.Api.Messages.ScriptMessage_friendAcceptedMessage _message)
    {
        mm.FriendCount++;

        GameObject go = Instantiate(mm.DogTag, mm.spawnpoint2.transform);
        MessageInfo fd = MessageInfo.CreateFromJSON(_message.Data.JSON);

        go.GetComponent<Dogtag>().displayName = fd.friendName;
        go.GetComponent<Dogtag>().buttonText.text = fd.friendName;
        go.GetComponent<Dogtag>().playerId = fd.friendId;

        mm.ListOfFriends.Add(go);
    }
    void ScriptMessageHandler(GameSparks.Api.Messages.ScriptMessage _message)
    {
        if (_message.ExtCode == "friendRequestMessage")
        {
            GameObject go = Instantiate(mm.FriendMessage, mm.spawnpoint.transform);
            mm.MessageCount++;
            mm.ListOfFriendMessages.Add(go);
            MessageInfo fd = MessageInfo.CreateFromJSON(_message.Data.JSON);

            go.GetComponent<FriendMessageID>().friendDisplayName = fd.displayName;
            go.GetComponent<FriendMessageID>().MessageID = fd.requestId;
            go.GetComponent<FriendMessageID>().friendID = fd.senderId;
            go.GetComponent<FriendMessageID>().Message.text = fd.displayName + " would like to be your friend.";
        }
        if (_message.ExtCode == "friendAcceptedMessage")
        {
            mm.FriendCount++;

            GameObject go = Instantiate(mm.DogTag, mm.spawnpoint2.transform);
            MessageInfo fd = MessageInfo.CreateFromJSON(_message.Data.JSON);

            go.GetComponent<Dogtag>().displayName = fd.friendName;
            go.GetComponent<Dogtag>().buttonText.text = fd.friendName;
            go.GetComponent<Dogtag>().playerId = fd.friendId;
            
            mm.ListOfFriends.Add(go);

            //populate it on the new 'friend menu' as well
			      GameObject gom = Instantiate(mm.DogTag, mm.spawnpoint6.transform);
            gom.GetComponent<Dogtag>().displayName = fd.friendName;
            gom.GetComponent<Dogtag>().buttonText.text = fd.friendName;
            gom.GetComponent<Dogtag>().playerId = fd.friendId;

        }
        if (_message.ExtCode == "Skip")
        {
            Debug.Log("its actually here");
            bm.OnlinePlayerSkipped(); //this is all it should have to do since the data is collected when an actual player takes an actual turn.
        }
    }
    void PublicGameInvite(GameSparks.Api.Messages.ScriptMessage_pubgame _message)
    {
        MessageInfo mi = MessageInfo.CreateFromJSON(_message.Data.JSON);
        GameObject invite = Instantiate(mm.GameInviteTag, mm.spawnpoint3.transform);

        invite.GetComponent<GameInviteMessage>().challengeId = mi.challenge;
        invite.GetComponent<GameInviteMessage>().Message.text = "You've been summoned to fight in The Battle Of " + mi.chalName;
        invite.GetComponent<GameInviteMessage>().isPublicGame = true;
        mm.ListOfGameInvites.Add(invite);
      
		    invite.GetComponent<GameInviteMessage>().PopulateGameList(mi.challenge);
        #region  ////////////DISPLAY NAMES IN THE TAGS SO YOU KNOW WHO IS IN THE GAME AND WHO'S TURN IT IS
        /*
        new GetChallengeRequest()
              .SetChallengeInstanceId(invite.GetComponent<GameInviteMessage>().challengeId)
              .Send((response2) =>
              {
                  if (!response2.HasErrors)
                  {
                      GameInfo Challenge = GameInfo.CreateFromJSON(response2.Challenge.JSONString);

                      Player challenger = Player.CreateFromJSON(response2.Challenge.Challenger.JSONString);
                      invite.GetComponent<GameInviteMessage>().Player1.text = challenger.name;
                      foreach (var s1 in response2.Challenge.Accepted)
                      {
                          Player p2 = Player.CreateFromJSON(s1.JSONString);
                          if (p2.id == challenger.id)
                          {
                              invite.GetComponent<GameInviteMessage>().Player1.color = Color.yellow;
                          }
                      }

                      foreach (var v in response2.Challenge.Challenged)
                      {
                          Player p = Player.CreateFromJSON(v.JSONString);
                          if (invite.GetComponent<GameInviteMessage>().Player1 == null || invite.GetComponent<GameInviteMessage>().Player1.text == "")
                          {
                              invite.GetComponent<GameInviteMessage>().Player1.text = p.name;
                              foreach (var s1 in response2.Challenge.Accepted)
                              {
                                  Player p2 = Player.CreateFromJSON(s1.JSONString);
                                  if (p2.id == p.id)
                                  {
                                      invite.GetComponent<GameInviteMessage>().Player1.color = Color.yellow;
                                  }
                              }
                          }
                          else if (invite.GetComponent<GameInviteMessage>().Player2 == null || invite.GetComponent<GameInviteMessage>().Player2.text == "")
                          {
                              invite.GetComponent<GameInviteMessage>().Player2.text = p.name;
                              foreach (var s1 in response2.Challenge.Accepted)
                              {
                                  Player p2 = Player.CreateFromJSON(s1.JSONString);
                                  if (p2.id == p.id)
                                  {
                                      invite.GetComponent<GameInviteMessage>().Player2.color = Color.yellow;
                                  }
                              }
                          }
                          else if (invite.GetComponent<GameInviteMessage>().Player3 == null || invite.GetComponent<GameInviteMessage>().Player3.text == "")
                          {
                              invite.GetComponent<GameInviteMessage>().Player3.text = p.name;
                              foreach (var s1 in response2.Challenge.Accepted)
                              {
                                  Player p2 = Player.CreateFromJSON(s1.JSONString);
                                  if (p2.id == p.id)
                                  {
                                      invite.GetComponent<GameInviteMessage>().Player3.color = Color.yellow;
                                  }
                              }
                          }
                          else if (invite.GetComponent<GameInviteMessage>().Player4 == null || invite.GetComponent<GameInviteMessage>().Player4.text == "")
                          {
                              invite.GetComponent<GameInviteMessage>().Player4.text = p.name;
                              foreach (var s1 in response2.Challenge.Accepted)
                              {
                                  Player p2 = Player.CreateFromJSON(s1.JSONString);
                                  if (p2.id == p.id)
                                  {
                                      invite.GetComponent<GameInviteMessage>().Player4.color = Color.yellow;
                                  }
                              }
                          }
                      }

                      if (invite.GetComponent<GameInviteMessage>().Player2.text == null || invite.GetComponent<GameInviteMessage>().Player2.text == "")
                      {
                          invite.GetComponent<GameInviteMessage>().Player2.text = "Open Seat";
                      }
                      if (invite.GetComponent<GameInviteMessage>().Player3.text == null || invite.GetComponent<GameInviteMessage>().Player3.text == "")
                      {
                          invite.GetComponent<GameInviteMessage>().Player3.text = "Open Seat";
                      }
                      if (invite.GetComponent<GameInviteMessage>().Player4.text == null || invite.GetComponent<GameInviteMessage>().Player4.text == "")
                      {
                          invite.GetComponent<GameInviteMessage>().Player4.text = "Open Seat";
                      }
                  }
                  else
                  {
                      invite.GetComponent<GameInviteMessage>().Player1.text = "Public Game";
                  }
              });
              */
          #endregion
    }
    void ChallangeInvite(GameSparks.Api.Messages.ChallengeIssuedMessage _message)
    {
        //show us the invite
        GameObject invite = Instantiate(mm.GameInviteTag, mm.spawnpoint3.transform);
        invite.GetComponent<GameInviteMessage>().challengeId = _message.Challenge.ChallengeId;
        invite.GetComponent<GameInviteMessage>().Message.text = "You're summoned to fight in The Battle Of " + _message.Challenge.ChallengeMessage + " !";
        mm.ListOfGameInvites.Add(invite);
        
        
		    invite.GetComponent<GameInviteMessage>().PopulateGameList(_message.Challenge.ChallengeId);
       #region      ////////////DISPLAY NAMES IN THE TAGS SO YOU KNOW WHO IS IN THE GAME AND WHO'S TURN IT IS
        /*
        new GetChallengeRequest()
              .SetChallengeInstanceId(_message.Challenge.ChallengeId)
              .Send((response) =>
              {
                  if (!response.HasErrors)
                  {
                      GameInfo Challenge = GameInfo.CreateFromJSON(response.Challenge.JSONString);

                      Player challenger = Player.CreateFromJSON(response.Challenge.Challenger.JSONString);
                      invite.GetComponent<GameInviteMessage>().Player1.text = challenger.name;
                      foreach (var s in response.Challenge.Accepted)
                      {
                          Player p2 = Player.CreateFromJSON(s.JSONString);
                          if (p2.id == challenger.id)
                          {
                              invite.GetComponent<GameInviteMessage>().Player1.color = Color.yellow;
                          }
                      }

                      foreach (var v in response.Challenge.Challenged)
                      {
                          Player p = Player.CreateFromJSON(v.JSONString);
                          if (invite.GetComponent<GameInviteMessage>().Player1 == null || invite.GetComponent<GameInviteMessage>().Player1.text == "")
                          {
                              invite.GetComponent<GameInviteMessage>().Player1.text = p.name;
                              foreach (var s in response.Challenge.Accepted)
                              {
                                  Player p2 = Player.CreateFromJSON(s.JSONString);
                                  if (p2.id == p.id)
                                  {
                                      invite.GetComponent<GameInviteMessage>().Player1.color = Color.yellow;
                                  }
                              }
                          }
                          else if (invite.GetComponent<GameInviteMessage>().Player2 == null || invite.GetComponent<GameInviteMessage>().Player2.text == "")
                          {
                              invite.GetComponent<GameInviteMessage>().Player2.text = p.name;
                              foreach (var s in response.Challenge.Accepted)
                              {
                                  Player p2 = Player.CreateFromJSON(s.JSONString);
                                  if (p2.id == p.id)
                                  {
                                      invite.GetComponent<GameInviteMessage>().Player2.color = Color.yellow;
                                  }
                              }
                          }
                          else if (invite.GetComponent<GameInviteMessage>().Player3 == null || invite.GetComponent<GameInviteMessage>().Player3.text == "")
                          {
                              invite.GetComponent<GameInviteMessage>().Player3.text = p.name;
                              foreach (var s in response.Challenge.Accepted)
                              {
                                  Player p2 = Player.CreateFromJSON(s.JSONString);
                                  if (p2.id == p.id)
                                  {
                                      invite.GetComponent<GameInviteMessage>().Player3.color = Color.yellow;
                                  }
                              }
                          }
                          else if (invite.GetComponent<GameInviteMessage>().Player4 == null || invite.GetComponent<GameInviteMessage>().Player4.text == "")
                          {
                              invite.GetComponent<GameInviteMessage>().Player4.text = p.name;
                              foreach (var s in response.Challenge.Accepted)
                              {
                                  Player p2 = Player.CreateFromJSON(s.JSONString);
                                  if (p2.id == p.id)
                                  {
                                      invite.GetComponent<GameInviteMessage>().Player4.color = Color.yellow;
                                  }
                              }
                          }
                      }
                  }
              });
              */
        #endregion
    }
    void ChallengeStartedHandler(GameSparks.Api.Messages.ChallengeStartedMessage _message)
    {
        GameObject[] GameList = GameObject.FindGameObjectsWithTag("game");

        foreach (GameObject go in GameList)
        {
            if (go.GetComponent<GameInviteMessage>().challengeId == _message.Challenge.ChallengeId)
            {
                Destroy(go.gameObject);
            }
        }
        GameObject game2 = Instantiate(mm.GameListTag, mm.spawnpoint4.transform);
        game2.GetComponent<GameInviteMessage>().challengeId = _message.Challenge.ChallengeId;
        game2.GetComponent<GameInviteMessage>().Message.text = "The Battle of " + _message.Challenge.ChallengeMessage + " is underway!";

        if (_message.Challenge.NextPlayer == mainMenu.myID)
        {
            game2.GetComponent<GameInviteMessage>().Message.text = "It's your move in The Battle of " + _message.Challenge.ChallengeMessage + "!";
        }

        game2.GetComponent<GameInviteMessage>().PopulateGameList(_message.Challenge.ChallengeId);

        #region        ////////////DISPLAY NAMES IN THE TAGS SO YOU KNOW WHO IS IN THE GAME AND WHO'S TURN IT IS
        /*

        new GetChallengeRequest()
              .SetChallengeInstanceId(_message.Challenge.ChallengeId)
              .Send((response) =>
              {
                  if (!response.HasErrors)
                  {
                      GameInfo Challenge = GameInfo.CreateFromJSON(response.Challenge.JSONString);

                      foreach (var v in response.Challenge.Accepted)
                      {
                          Player p = Player.CreateFromJSON(v.JSONString);
                          Debug.Log(p.name);
                          if(game2.GetComponent<GameInviteMessage>().Player1 == null || game2.GetComponent<GameInviteMessage>().Player1.text == "")
                          {
                              game2.GetComponent<GameInviteMessage>().Player1.text = p.name;
                              if (p.id == Challenge.nextPlayer)
                              {
                                  game2.GetComponent<GameInviteMessage>().Player1.color = Color.yellow;
                              }
                          }
                          else if(game2.GetComponent<GameInviteMessage>().Player2 == null || game2.GetComponent<GameInviteMessage>().Player2.text == "")
                          {
                              game2.GetComponent<GameInviteMessage>().Player2.text = p.name;
                              if (p.id == Challenge.nextPlayer)
                              {
                                  game2.GetComponent<GameInviteMessage>().Player2.color = Color.yellow;
                              }
                          }
                          else if (game2.GetComponent<GameInviteMessage>().Player3 == null || game2.GetComponent<GameInviteMessage>().Player3.text == "")
                          {
                              game2.GetComponent<GameInviteMessage>().Player3.text = p.name;
                              if (p.id == Challenge.nextPlayer)
                              {
                                  game2.GetComponent<GameInviteMessage>().Player3.color = Color.yellow;
                              }
                          }
                          else if (game2.GetComponent<GameInviteMessage>().Player4 == null || game2.GetComponent<GameInviteMessage>().Player4.text == "")
                          {
                              game2.GetComponent<GameInviteMessage>().Player4.text = p.name;
                              if (p.id == Challenge.nextPlayer)
                              {
                                  game2.GetComponent<GameInviteMessage>().Player4.color = Color.yellow;
                              }
                          }
                      }
                  }
              });
              */
        #endregion  ///

    }
    void PlayerQuitHandler(GameSparks.Api.Messages.ScriptMessage_PlayerQuit _message)
    {
        if (SceneManager.GetActiveScene().name == "KingsAndQueens") //if we're in a game.
        {
            GameInfo MessageData = GameInfo.CreateFromJSON(_message.Data.JSON); //get the message details.

            if (bm.isOnline) //make sure the game we're in is an online game.
            {
                if (bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId == MessageData.challenge) //if we're in the CORRECT online game.
                {
                new GetChallengeRequest()
                  .SetChallengeInstanceId(MessageData.challenge)
                  .Send((response) =>
                  {
                      if (!response.HasErrors)
                      {
                          if (response.Challenge.ScriptData.GetGSData("WinConditions") != null)
                          {
                              GSData wincon = response.Challenge.ScriptData.GetGSData("WinConditions");
                              WinConditions wc = WinConditions.CreateFromJSON(wincon.JSON);
                              //see which team surrendered and report.
                              if (!bm.blueout && wc.Bout)
                              {
                                  Debug.Log("Blue has Surrendered!");
                              }
                              if (!bm.yelout && wc.Yout)
                              {
                                  Debug.Log("Yellow has Surrendered!");
                              }
                              if (!bm.redout && wc.Rout)
                              {
                                  Debug.Log("Red has Surrendered!");
                              }
                              if (!bm.greenout && wc.Gout)
                              {
                                  Debug.Log("Green has Surrendered!");
                              }
                              //Set the Data to my local game, and then call the Lose Function to remove the surrendered team.
                              bm.blueout = wc.Bout;
                              bm.yelout = wc.Yout;
                              bm.redout = wc.Rout;
                              bm.greenout = wc.Gout;
                              bm.Lose();
                              bm.Win();

                              if (bm.onlinePlayerTurn == bm.myID)
                              {
                                  bm.SendGameDataAndAdvanceTurn();
                              }
                          }
                      }
                  });

                }
            }
        }
    }
    void ChallengeChatHandler(GameSparks.Api.Messages.ChallengeChatMessage _message)
    {
        if(_message.Challenge.ChallengeId == bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
        {
            Debug.Log(_message.Message);
            GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
            ChatListing.GetComponentInChildren<Text>().text =  /* _message.Who + ": " +*/ _message.Message;
			      GameObject.Find("MenuController").GetComponent<GameMenu>().PublicChatScroll.verticalNormalizedPosition = 1f; //this isnt working here - but it is working when we open the menu...? 
		      	GameObject.Find("MenuController").GetComponent<GameMenu>().PublicChatScroll.verticalNormalizedPosition = 0f; //experimenting by going all the way up first, and then snapping all the way down....
			      GameObject.Find("MenuController").GetComponent<GameMenu>().PublicStar.SetActive(true);         
        }
    }
    void SkipHandler(GameSparks.Api.Messages.ScriptMessage_Skip _message) 
    {
        Debug.Log("Skip Handler Hit");
        bm.OnlinePlayerSkipped(); //this is all it should have to do since the data is collected when an actual player takes an actual turn.
    }

	void DrawVoteHandler(GameSparks.Api.Messages.ScriptMessage_DrawVote _message)
	{
		Debug.Log("DrawVoteHandler Hit");

		if(SceneManager.GetActiveScene().name == "KingsAndQueens")
		{
			new GetChallengeRequest().SetChallengeInstanceId(bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
						 .Send((response) =>
						 {
							 if (response.Challenge.ScriptData.GetGSData("VotingPlayers") != null)
							 {
								 GSData data = response.Challenge.ScriptData.GetGSData("VotingPlayers");
								 Vote voteData = Vote.CreateFromJSON(data.JSON);

								 if (voteData.WaitingVotes.Length != 0)
								 {
									 GameObject.Find("MenuController").GetComponent<GameMenu>().WaitingVoteList.Clear();
									 foreach (var item in voteData.WaitingVotes)
									 {
										 GameObject.Find("MenuController").GetComponent<GameMenu>().WaitingVoteList.Add(item);
									 }
								 }

								 if (voteData.NoVotes.Length != 0)
								 {
									 GameObject.Find("MenuController").GetComponent<GameMenu>().NoVoteList.Clear();
									 foreach (var item in voteData.NoVotes)
									 {
										 GameObject.Find("MenuController").GetComponent<GameMenu>().NoVoteList.Add(item);
									 }
								 }

								 if (voteData.YesVotes.Length != 0)
								 {
									 GameObject.Find("MenuController").GetComponent<GameMenu>().YesVoteList.Clear();
									 foreach (var item in voteData.YesVotes)
									 {
										 GameObject.Find("MenuController").GetComponent<GameMenu>().YesVoteList.Add(item);
									 }
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
											 if (item == bm.myID)
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
							 else
							 {
								 GameObject.Find("MenuController").GetComponent<GameMenu>().isVoteActive = false;
								 GameObject.Find("MenuController").GetComponent<GameMenu>().WaitingVoteList.Clear();
								 GameObject.Find("MenuController").GetComponent<GameMenu>().NoVoteList.Clear();
								 GameObject.Find("MenuController").GetComponent<GameMenu>().YesVoteList.Clear();

								 if (GameObject.Find("MenuController").GetComponent<GameMenu>().VoteMenu.activeInHierarchy)
								 {
									 GameObject.Find("MenuController").GetComponent<GameMenu>().VoteMenu.SetActive(false);
								 }
								 if (GameObject.Find("MenuController").GetComponent<GameMenu>().VoteProgressMenu.activeInHierarchy)
								 {
									 GameObject.Find("MenuController").GetComponent<GameMenu>().VoteProgressMenu.SetActive(false);
								 }
								 if (GameObject.Find("MenuController").GetComponent<GameMenu>().CastYourVoteMenu.activeInHierarchy)
								 {
									 GameObject.Find("MenuController").GetComponent<GameMenu>().CastYourVoteMenu.SetActive(false);
								 }

								 GameObject.Find("MenuController").GetComponent<GameMenu>().ShowChatButton();
							 }
						 });        			
		}
	}
	void ChallengeDrawHandler(GameSparks.Api.Messages.ChallengeDrawnMessage _message)
	{
		bm.IsDraw = true;
		GameObject.Find("MenuController").GetComponent<GameMenu>().MenuControlForDraw();
        bm.GameWin();
	}
	void MoveExpiredPlayerHandler (GameSparks.Api.Messages.ScriptMessage_MoveExpiredPlayer _message)
	{
		MessageData messageData = MessageData.CreateFromJSON(_message.Data.JSON); 

		if(messageData.ChallengeID == bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
		{
			foreach(string s in messageData.PlayersToSkip)
			{
				bm.ExpiredColors.Add(s);
			}
			Debug.Log("ExpiredColors: " + bm.ExpiredColors);
			bm.OnlineTurnExpired();
		}
	}

	void PrivateChatHandler (GameSparks.Api.Messages.ScriptMessage_PrivateChat _message)
	{

                GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToBlueMenu.SetActive(true);
                BlueChatVariable = GameObject.FindGameObjectWithTag("bluechatmenu").GetComponent<Chat>();
                GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToBlueMenu.SetActive(false);

                GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToYellowMenu.SetActive(true);
                YellowChatVariable = GameObject.FindGameObjectWithTag("yellowchatmenu").GetComponent<Chat>();
                GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToYellowMenu.SetActive(false);

                GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToRedMenu.SetActive(true);
                RedChatVariable = GameObject.FindGameObjectWithTag("redchatmenu").GetComponent<Chat>();
                GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToRedMenu.SetActive(false);

                GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToGreenMenu.SetActive(true);
                GreenChatVariable = GameObject.FindGameObjectWithTag("greenchatmenu").GetComponent<Chat>();
                GameObject.Find("MenuController").GetComponent<GameMenu>().ChatToGreenMenu.SetActive(false);


		MessageData data = MessageData.CreateFromJSON(_message.Data.JSON);
		Debug.Log(data.action);

		string myColor = "color";

		    foreach (Player p in bm.playerList)
        {
			      if (p.id == bm.myID)
            {
                myColor = p.color;
            }
        }

    		if(data.action == "blueandyellow")
    		{
    			  if(myColor == "Blue")
    			  {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, YellowChatVariable.ChatMessageSpawn.transform);
        				ChatListing.GetComponentInChildren<Text>().text = data.message;
        				GameObject.Find("MenuController").GetComponent<GameMenu>().YellowStar.SetActive(true);
    			  }
           	if (myColor == "Yellow")
            {

                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, BlueChatVariable.ChatMessageSpawn.transform);
            		ChatListing.GetComponentInChildren<Text>().text = data.message;
            		GameObject.Find("MenuController").GetComponent<GameMenu>().BlueStar.SetActive(true);
            }
    		}

		    if (data.action == "blueandred")
        {
			     if (myColor == "Blue")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, RedChatVariable.ChatMessageSpawn.transform);
        				ChatListing.GetComponentInChildren<Text>().text = data.message;
        				GameObject.Find("MenuController").GetComponent<GameMenu>().RedStar.SetActive(true);
            }
			      if (myColor == "Red")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, BlueChatVariable.ChatMessageSpawn.transform);
            		ChatListing.GetComponentInChildren<Text>().text = data.message;
            		GameObject.Find("MenuController").GetComponent<GameMenu>().BlueStar.SetActive(true);
            }
        }

		    if (data.action == "blueandgreen")
        {
			      if (myColor == "Blue")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, GreenChatVariable.ChatMessageSpawn.transform);
				        ChatListing.GetComponentInChildren<Text>().text = data.message;
				        GameObject.Find("MenuController").GetComponent<GameMenu>().GreenStar.SetActive(true);
            }
    			  if (myColor == "Green")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, BlueChatVariable.ChatMessageSpawn.transform);
            		ChatListing.GetComponentInChildren<Text>().text = data.message;
            		GameObject.Find("MenuController").GetComponent<GameMenu>().BlueStar.SetActive(true);
            }
        }

		    if (data.action == "yellowandred")
        {
			      if (myColor == "Yellow")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, RedChatVariable.ChatMessageSpawn.transform);
				        ChatListing.GetComponentInChildren<Text>().text = data.message;
				        GameObject.Find("MenuController").GetComponent<GameMenu>().RedStar.SetActive(true);
            }
			      if (myColor == "Red")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, YellowChatVariable.ChatMessageSpawn.transform);
				        ChatListing.GetComponentInChildren<Text>().text = data.message;
				        GameObject.Find("MenuController").GetComponent<GameMenu>().YellowStar.SetActive(true);
            }
        }

		    if (data.action == "yellowandgreen")
        {
			      if (myColor == "Yellow")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, GreenChatVariable.ChatMessageSpawn.transform);
				        ChatListing.GetComponentInChildren<Text>().text = data.message;
				        GameObject.Find("MenuController").GetComponent<GameMenu>().GreenStar.SetActive(true);
            }
			      if (myColor == "Green")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, YellowChatVariable.ChatMessageSpawn.transform);
				        ChatListing.GetComponentInChildren<Text>().text = data.message;
				        GameObject.Find("MenuController").GetComponent<GameMenu>().YellowStar.SetActive(true);
            }
        }

		    if (data.action == "redandgreen")
        {
			      if (myColor == "Red")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, GreenChatVariable.ChatMessageSpawn.transform);
				        ChatListing.GetComponentInChildren<Text>().text = data.message;
				        GameObject.Find("MenuController").GetComponent<GameMenu>().GreenStar.SetActive(true);
            }
			      if (myColor == "Green")
            {
                GameObject ChatListing = Instantiate(ChatVariable.ChatListing, RedChatVariable.ChatMessageSpawn.transform);
				        ChatListing.GetComponentInChildren<Text>().text = data.message;
				        GameObject.Find("MenuController").GetComponent<GameMenu>().RedStar.SetActive(true);
            }
        }
	}

  private void OnDisable()
    {
        GameSparks.Api.Messages.ChallengeTurnTakenMessage.Listener -= TurnTakenHandler;
        GameSparks.Api.Messages.ChallengeWonMessage.Listener -= GameWinHandler;
        GameSparks.Api.Messages.ChallengeLostMessage.Listener -= GameLoseHandler;
        //      GameSparks.Api.Messages.ScriptMessage_friendRequestMessage.Listener -= FriendMessageHandler;
        //      GameSparks.Api.Messages.ScriptMessage_friendAcceptedMessage.Listener -= FriendAcceptedHandler;
        GameSparks.Api.Messages.ScriptMessage_pubgame.Listener -= PublicGameInvite;
        GameSparks.Api.Messages.ScriptMessage.Listener -= ScriptMessageHandler;
        GameSparks.Api.Messages.ChallengeIssuedMessage.Listener -= ChallangeInvite;
        GameSparks.Api.Messages.ChallengeStartedMessage.Listener -= ChallengeStartedHandler;
        GameSparks.Api.Messages.ChallengeChatMessage.Listener -= ChallengeChatHandler;
        GameSparks.Api.Messages.ScriptMessage_PlayerQuit.Listener -= PlayerQuitHandler;
        GameSparks.Api.Messages.ScriptMessage_Skip.Listener -= SkipHandler;
	     	GameSparks.Api.Messages.ScriptMessage_DrawVote.Listener -= DrawVoteHandler;    
    		GameSparks.Api.Messages.ChallengeDrawnMessage.Listener -= ChallengeDrawHandler;
	     	GameSparks.Api.Messages.ScriptMessage_MoveExpiredPlayer.Listener -= MoveExpiredPlayerHandler;
	     	GameSparks.Api.Messages.ScriptMessage_PrivateChat.Listener -= PrivateChatHandler;
        
    }
  private void OnDestroy()
    {
        GameSparks.Api.Messages.ChallengeTurnTakenMessage.Listener -= TurnTakenHandler;
        GameSparks.Api.Messages.ChallengeWonMessage.Listener -= GameWinHandler;
        GameSparks.Api.Messages.ChallengeLostMessage.Listener -= GameLoseHandler;
        //      GameSparks.Api.Messages.ScriptMessage_friendRequestMessage.Listener -= FriendMessageHandler;
        //      GameSparks.Api.Messages.ScriptMessage_friendAcceptedMessage.Listener -= FriendAcceptedHandler;
        GameSparks.Api.Messages.ScriptMessage_pubgame.Listener -= PublicGameInvite;
        GameSparks.Api.Messages.ScriptMessage.Listener -= ScriptMessageHandler;
        GameSparks.Api.Messages.ChallengeIssuedMessage.Listener -= ChallangeInvite;
        GameSparks.Api.Messages.ChallengeStartedMessage.Listener -= ChallengeStartedHandler;
        GameSparks.Api.Messages.ScriptMessage_PlayerQuit.Listener -= PlayerQuitHandler;
        GameSparks.Api.Messages.ScriptMessage_Skip.Listener -= SkipHandler;
		    GameSparks.Api.Messages.ScriptMessage_DrawVote.Listener -= DrawVoteHandler;
		    GameSparks.Api.Messages.ChallengeDrawnMessage.Listener -= ChallengeDrawHandler;
        GameSparks.Api.Messages.ScriptMessage_MoveExpiredPlayer.Listener -= MoveExpiredPlayerHandler;
		    GameSparks.Api.Messages.ScriptMessage_PrivateChat.Listener -= PrivateChatHandler;
        
    }    
  
  public void BuildChatLog(string log)
	{
		GameObject.Find("MenuController").GetComponent<GameMenu>().ChatMenu.SetActive(true);
		ChatVariable = GameObject.FindGameObjectWithTag("chatMenu").GetComponent<Chat>();
		GameObject.Find("MenuController").GetComponent<GameMenu>().ChatMenu.SetActive(false);
		GameObject ChatListing = Instantiate(ChatVariable.ChatListing, ChatVariable.ChatMessageSpawn.transform);
        ChatListing.GetComponentInChildren<Text>().text = log;
	}

}

[System.Serializable]
public class VoteData
{
    public string state;
    public string Yes;
    public string No;
    public bool endvote = false;

    public static VoteData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<VoteData>(jsonString);
    }
}
[System.Serializable]
public class Vote
{  
	public bool isActiveVote;
    public string[] YesVotes;
    public string[] NoVotes;
	public string[] WaitingVotes;

    public static Vote CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Vote>(jsonString);
    }
}
[System.Serializable]
public class MessageData
{
	public string[] PlayersToSkip;
	public string ChallengeID;
	public string message;
	public string action;

	public static MessageData CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<MessageData>(jsonString);
	}
}

