using BitBenderGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using GameSparks.Core;

public class GameMenu : MonoBehaviour {

    public Canvas InGameCanvas;
    //    public Text OptionsButtonText;
    public Canvas OptionsCanvas;
    public Button ChatButton, BlueChatButton, YellowChatButton, RedChatButton, GreenChatButton;
    public GameObject ChatMenu, ChatToBlueMenu, ChatToYellowMenu, ChatToRedMenu, ChatToGreenMenu;
    public GameObject TurnReviewButtons;
    public GameObject PrisonMenus;
    public GameObject ListOne, ListTwo, ListThree; //prisonList scrollview spawnpoints
    public Button PrisonButton, BluePrison, YellowPrison, RedPrison, GreenPrison; //buttons to show the menu, and then each team's prison
    public Text ListOneTitle, ListTwoTitle, ListThreeTitle;
    public Button bb, ba, yb, ya, rb, ra, gb, ga, Summon; //before and after boardstates.
                                                          //   public Text SummonButtonText;

    public Text P1Status, P2Status, P3Status, P4Status;
    public Button OptionsButton;
    public boardmanager bm;
    public List<GameObject> ListOneContents = new List<GameObject>();
    public List<GameObject> ListTwoContents = new List<GameObject>();
    public List<GameObject> ListThreeContents = new List<GameObject>();
    public GameObject PrisonerListing;
    public Text OnlinePlayerOne, OnlinePlayerTwo, OnlinePlayerThree, OnlinePlayerFour;
    public GameObject PlayerOneWindow, PlayerTwoWindow, PlayerThreeWindow, PlayerFourWindow;
    public Camera MyCamera;
    public GameObject QuitPopUpSpawn;
    public GameObject QuitPopUp;
    public Button PlayerSurrenderButton;
    public GameObject SurrenderPopUp;

	public GameObject CastYourVoteMenu;
	public GameObject VoteProgressMenu;
	public Text YayName;
	public Text NayName;
	public int YayCount = 0;
	public int NayCount = 0;
	public GameObject VoteMenu;
	public Button OpenVoteMenuButton;
	public bool isVoteActive = false;
	public List<string> YesVoteList;
	public List<string> NoVoteList;
	public List<string> WaitingVoteList;
	public Button MainMenuButton;
	public Button ResetCameraButton;
	public Button QuitButton;
	public Button SurrenderButton;
	public Button VoteMenuButton;

	public GameObject PublicStar, BlueStar, YellowStar, RedStar, GreenStar;
	public Text PrivateBlueButtonText, PrivateYellowButtonText, PrivateRedButtonText, PrivateGreenButtonText;
	public ScrollRect PublicChatScroll, BlueScroll, YellowScroll, RedScroll, GreenScroll;
	public Button GUIblueChat, GUIyellowChat, GUIredChat, GUIgreenChat;
    public Text HideButtonText;
    public string HUDstatus = "on";
    public Button ShowHideHUD_Button;
    public Button MusicSwitch;
    
    void Start() {
        bm = GameObject.Find("Chessboard").GetComponent<boardmanager>();
        //   OptionsButtonText.text = "Options";
        InGameCanvas.GetComponent<Canvas>().enabled = true;
        OptionsCanvas.GetComponent<Canvas>().enabled = false;
        TurnReviewButtons.SetActive(false);
        PrisonMenus.SetActive(false);
        PrisonButton.gameObject.SetActive(false);
        ChatMenu.SetActive(false);
		ChatToBlueMenu.SetActive(false);
		ChatToYellowMenu.SetActive(false);
		ChatToRedMenu.SetActive(false);
		ChatToGreenMenu.SetActive(false);
        //    SummonButtonText.text = "Review Past Turns";
        Summon.gameObject.SetActive(false);
		VoteMenu.SetActive(false);
		CastYourVoteMenu.SetActive(false);
		VoteProgressMenu.SetActive(false);
		PublicStar.gameObject.SetActive(false);
		BlueStar.gameObject.SetActive(false);
		YellowStar.gameObject.SetActive(false);
		RedStar.gameObject.SetActive(false);
		GreenStar.gameObject.SetActive(false);


        if (bm.isOnline)
        {
            ChatButton.gameObject.SetActive(true);
            PlayerSurrenderButton.gameObject.SetActive(true);
			OpenVoteMenuButton.gameObject.SetActive(true);
        }
        else
        {
            ChatButton.gameObject.SetActive(false);
            PlayerSurrenderButton.gameObject.SetActive(false);
			OpenVoteMenuButton.gameObject.SetActive(false);

			//PlayerOneWindow.gameObject.SetActive(false);
			//PlayerTwoWindow.gameObject.SetActive(false);
			//PlayerThreeWindow.gameObject.SetActive(false);
			//PlayerFourWindow.gameObject.SetActive(false);
			GUIblueChat.gameObject.SetActive(false); 
			GUIyellowChat.gameObject.SetActive(false); 
			GUIredChat.gameObject.SetActive(false); 
			GUIgreenChat.gameObject.SetActive(false);
        }



    }
    void Update() {
        if (GameObject.Find("ListItem_Game(Clone)"))
        {
            if (bm.hasBlueTurn || bm.hasYellowTurn || bm.hasRedTurn || bm.hasGreenTurn)
            {
                if(HUDstatus == "on")
                {
                    Summon.gameObject.SetActive(true);
                }
            }
        }

        if(MusicSwitch == null)
        {
            MusicSwitch = GameObject.Find("GameObject_HoldSoundButton").GetComponentInChildren<Button>();
        }

        if (!bm.isOnline)
        {
            if(bm.playerTurn == 1)
            {
                PlayerOneWindow.gameObject.SetActive(true);
                PlayerTwoWindow.gameObject.SetActive(false);
                PlayerThreeWindow.gameObject.SetActive(false);
                PlayerFourWindow.gameObject.SetActive(false);
            }
            if(bm.playerTurn == 2)
            {
                PlayerOneWindow.gameObject.SetActive(false);
                PlayerTwoWindow.gameObject.SetActive(true);
                PlayerThreeWindow.gameObject.SetActive(false);
                PlayerFourWindow.gameObject.SetActive(false);
            }
            if(bm.playerTurn == 3)
            {
                PlayerOneWindow.gameObject.SetActive(false);
                PlayerTwoWindow.gameObject.SetActive(false);
                PlayerThreeWindow.gameObject.SetActive(true);
                PlayerFourWindow.gameObject.SetActive(false);
            }
            if(bm.playerTurn == 4)
            {
                PlayerOneWindow.gameObject.SetActive(false);
                PlayerTwoWindow.gameObject.SetActive(false);
                PlayerThreeWindow.gameObject.SetActive(false);
                PlayerFourWindow.gameObject.SetActive(true);
            }
        }

        if(!bm.blueout)
        {
            if (bm.k1 != null)
            {
                if (bm.k1.isChecked)
                {
                    P1Status.text = "Check";

                    if (bm.k1.isMated)
                    {
                        P1Status.text = "Checkmate";
                    }
                }
            }
        }

        if (!bm.yelout)
        {
            if (bm.k2 != null)
            {
                if (bm.k2.isChecked)
                {
                    P2Status.text = "Check";

                    if (bm.k2.isMated)
                    {
                        P2Status.text = "Checkmate";
                    }
                }
            }
        }
    
        if(!bm.redout)
        {
            if (bm.k3 != null)
            {
                if (bm.k3.isChecked)
                {
                    P3Status.text = "Check";

                    if (bm.k3.isMated)
                    {
                        P3Status.text = "Checkmate";
                    }
                }
            }
        }

        if (!bm.greenout)
        {
            if (bm.k4 != null)
            {
                if (bm.k4.isChecked)
                {
                    P4Status.text = "Check";

                    if (bm.k4.isMated)
                    {
                        P4Status.text = "Checkmate";
                    }
                }
            }
        }


        if (bm.IsStalemate)
        {
            if(bm.playerTurn == 1)
            {
                P1Status.text = "Stalemate";
            }
            if(bm.playerTurn == 2)
            {
                P2Status.text = "Stalemate";
            }
            if(bm.playerTurn == 3)
            {
                P3Status.text = "Stalemate";
            }
            if(bm.playerTurn == 4)
            {
                P4Status.text = "Stalemate";
            }
        }
        else
        {
            if (bm.k1 != null)
            {
                if (bm.blueout || !bm.k1.isChecked)
                {
                    P1Status.text = "";
                }
            }
            if (bm.k2 != null)
            {
                if (bm.yelout || !bm.k2.isChecked)
                {
                    P2Status.text = "";
                }
            }
            if (bm.k3 != null)
            {
                if (bm.redout || !bm.k3.isChecked)
                {
                    P3Status.text = "";
                }
            }
            if (bm.k4 != null)
            {
                if (bm.greenout || !bm.k4.isChecked)
                {
                    P4Status.text = "";
                }
            }
        }



        if (bm.hasBlueTurn)
        {
            bb.gameObject.SetActive(true);
            ba.gameObject.SetActive(true);
        }
        else
        {
            bb.gameObject.SetActive(false);
            ba.gameObject.SetActive(false);
        }
        if (bm.hasYellowTurn)
        {
            yb.gameObject.SetActive(true);
            ya.gameObject.SetActive(true);
        }
        else
        {
            yb.gameObject.SetActive(false);
            ya.gameObject.SetActive(false);
        }
        if (bm.hasRedTurn)
        {
            rb.gameObject.SetActive(true);
            ra.gameObject.SetActive(true);
        }
        else
        {
            rb.gameObject.SetActive(false);
            ra.gameObject.SetActive(false);
        }
        if (bm.hasGreenTurn)
        {
            gb.gameObject.SetActive(true);
            ga.gameObject.SetActive(true);
        }
        else
        {
            gb.gameObject.SetActive(false);
            ga.gameObject.SetActive(false);
        }

        if (bm.bluePrisonersYellow.Count > 0 ||
           bm.bluePrisonersRed.Count > 0 ||
           bm.bluePrisonersGreen.Count > 0 ||

           bm.yelPrisonersBlue.Count > 0 ||
           bm.yelPrisonersRed.Count > 0 ||
           bm.yelPrisonersGreen.Count > 0 ||

           bm.redPrisonersBlue.Count > 0 ||
           bm.redPrisonersYellow.Count > 0 ||
           bm.redPrisonersGreen.Count > 0 ||

           bm.greenPrisonersBlue.Count > 0 ||
           bm.greenPrisonersYellow.Count > 0 ||
           bm.greenPrisonersRed.Count > 0
            )
        {
            if(HUDstatus == "on")
            {
                PrisonButton.gameObject.SetActive(true);
            }
        }

        if (bm.playerList.Count > 0)
        {
            for (int i = 0; i <= bm.playerList.Count - 1; i++)
            {
                if (i == 0)
                {
                    OnlinePlayerOne.text = bm.playerList[i].playerName;
                }
                if (i == 1)
                {
                    OnlinePlayerTwo.text = bm.playerList[i].playerName;
                }
                if (i == 2)
                {
                    OnlinePlayerThree.text = bm.playerList[i].playerName;
                }
                if (i == 3)
                {
                    OnlinePlayerFour.text = bm.playerList[i].playerName;
                }
            }
        }
        else if (!bm.isOnline)
        {
                    OnlinePlayerOne.text = "Player 1";

                    OnlinePlayerTwo.text = "Player 2";

                    OnlinePlayerThree.text = "Player 3";

                    OnlinePlayerFour.text = "Player 4";
        }

		YayCount = YesVoteList.Count;
		NayCount = NoVoteList.Count;

		YayName.text = "Yay: " + YayCount;
		NayName.text = "Nay: " + NayCount;
    }
    
    public void ShowHideHUD()
    {
        if(HUDstatus == "on")
        {
            Summon.gameObject.SetActive(false);
            PrisonButton.gameObject.SetActive(false);
            OptionsButton.gameObject.SetActive(false);
            ChatButton.gameObject.SetActive(false);
            MusicSwitch.gameObject.SetActive(false);
            HUDstatus = "off";
            HideButtonText.text = ">";
        }
        else
        {
            OptionsButton.gameObject.SetActive(true);
            MusicSwitch.gameObject.SetActive(true);

            if (bm.isOnline)
            {
                ChatButton.gameObject.SetActive(true);
            }

            if (bm.bluePrisonersYellow.Count > 0 ||
               bm.bluePrisonersRed.Count > 0 ||
               bm.bluePrisonersGreen.Count > 0 ||

               bm.yelPrisonersBlue.Count > 0 ||
               bm.yelPrisonersRed.Count > 0 ||
               bm.yelPrisonersGreen.Count > 0 ||

               bm.redPrisonersBlue.Count > 0 ||
               bm.redPrisonersYellow.Count > 0 ||
               bm.redPrisonersGreen.Count > 0 ||

               bm.greenPrisonersBlue.Count > 0 ||
               bm.greenPrisonersYellow.Count > 0 ||
               bm.greenPrisonersRed.Count > 0
                )
            {
                PrisonButton.gameObject.SetActive(true);
            }

            if (bm.hasBlueTurn || bm.hasYellowTurn || bm.hasRedTurn || bm.hasGreenTurn)
            {
                Summon.gameObject.SetActive(true);
            }

            HUDstatus = "on";
            HideButtonText.text = "<";
        }
    }
    public void OpenOrCloseVoteMenu()
	{
		if(isVoteActive) //if the vote IS active, show the progress...if not, show the initiate menu.
		{
			if (VoteProgressMenu.activeInHierarchy)
            {
				VoteProgressMenu.SetActive(false);
                MusicSwitch.gameObject.SetActive(true);
            }
            else
            {
				VoteProgressMenu.SetActive(true);
                MusicSwitch.gameObject.SetActive(false);
            }
		}
		else
		{
			if (VoteMenu.activeInHierarchy)
            {
                VoteMenu.SetActive(false);
                MusicSwitch.gameObject.SetActive(true);
            }
            else
            {
                VoteMenu.SetActive(true);
                MusicSwitch.gameObject.SetActive(false);
            }
		}
	}
    public void InitiateDrawVote()
	{
		List<string> VotingPlayers = new List<string>();
		foreach (var player in bm.playerList)
		{
			if(player.color == "Blue" && !bm.blueout && player.id != bm.myID)
			{
				VotingPlayers.Add(player.id);
			}
			if (player.color == "Yellow" && !bm.yelout && player.id != bm.myID)
            {
                VotingPlayers.Add(player.id);
            }
			if (player.color == "Red" && !bm.redout && player.id != bm.myID)
            {
                VotingPlayers.Add(player.id);
            }
			if (player.color == "Green" && !bm.greenout && player.id != bm.myID)
            {
                VotingPlayers.Add(player.id);
            }
		}
        
		List<string> no = new List<string>();
		List<string> yes = new List<string>();
		yes.Add(bm.myID);
		YesVoteList.Add(bm.myID);

		new LogEventRequest().SetEventKey("DrawVote")
		                     .SetEventAttribute("cid", bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
		                     .SetEventAttribute("WaitingVotes", VotingPlayers)
		                     .SetEventAttribute("NoVotes", no)
		                     .SetEventAttribute("YesVotes", yes)
		                     .SetEventAttribute("VoteCast", "To") //this means 'to vote', as in 'waiting to vote'
		                     .SetEventAttribute("GameName", bm.GameName)
		                     .Send((response) =>
                             {
								 if (!response.HasErrors)
								 {
									 Debug.Log("DrawVote Initiated");
									 
									 OpenOrCloseVoteMenu();
			                    	 isVoteActive = true;
								 }
								 else
								 {
									 Debug.Log("Initilizing DrawVote Messed Up");
									 
				                     OpenOrCloseVoteMenu();
								 }
                             });
	}
    public void MenuControlForDraw()
	{
		//Display the correct Menu and lock User input
        GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = false;
        bm.isDisplayed = true;

        OptionsCanvas.GetComponent<Canvas>().enabled = true;
        MainMenuButton.gameObject.SetActive(false);
        ResetCameraButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        SurrenderButton.gameObject.SetActive(false);
        VoteMenuButton.gameObject.SetActive(false);
        MusicSwitch.gameObject.SetActive(false);
    }
    public void MenuControlForEndVote()
	{
		OptionsCanvas.GetComponent<Canvas>().enabled = true;
        MainMenuButton.gameObject.SetActive(true);
        ResetCameraButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
        SurrenderButton.gameObject.SetActive(true);
        VoteMenuButton.gameObject.SetActive(true);
        MusicSwitch.gameObject.SetActive(true);
        InGameCanvas.GetComponent<Canvas>().enabled = true;
        OptionsCanvas.GetComponent<Canvas>().enabled = false;

        GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = true;
        bm.isDisplayed = false;
        CastYourVoteMenu.SetActive(false);    
	}
	public void DemandVoteCast()
	{
		//Display the correct Menu and lock User input
		GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = false;
        GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = false;
        bm.isDisplayed = true;
        
		OptionsCanvas.GetComponent<Canvas>().enabled = true;
		MainMenuButton.gameObject.SetActive(false);
		ResetCameraButton.gameObject.SetActive(false);
		QuitButton.gameObject.SetActive(false);
		SurrenderButton.gameObject.SetActive(false);
		VoteMenuButton.gameObject.SetActive(false);
        MusicSwitch.gameObject.SetActive(false);
        InGameCanvas.GetComponent<Canvas>().enabled = false;

		CastYourVoteMenu.SetActive(true);

        //set the camer right of center so the menu is on the left and the board is on the right.
        Vector3 position = new Vector3(0, 20, 6);
        Quaternion rotation = Quaternion.Euler(90, 0, 0);
        MyCamera.transform.position = position;
        MyCamera.transform.rotation = rotation;
	}

	public void VoteYay()
	{


		WaitingVoteList.Remove(bm.myID);
		YesVoteList.Add(bm.myID);

		new LogEventRequest().SetEventKey("DrawVote")
                     .SetEventAttribute("cid", bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                     .SetEventAttribute("WaitingVotes", WaitingVoteList)
		             .SetEventAttribute("NoVotes", NoVoteList)
		             .SetEventAttribute("YesVotes", YesVoteList)
                     .SetEventAttribute("VoteCast", "Yay") //this means 'to vote', as in 'waiting to vote'
		             .SetEventAttribute("GameName", bm.GameName)
                     .Send((response) =>
                     {
                         if (!response.HasErrors)
                         {
				             OptionsCanvas.GetComponent<Canvas>().enabled = true;
							 MainMenuButton.gameObject.SetActive(true);
							 ResetCameraButton.gameObject.SetActive(true);
							 QuitButton.gameObject.SetActive(true);
							 SurrenderButton.gameObject.SetActive(true);
							 VoteMenuButton.gameObject.SetActive(true);
                             MusicSwitch.gameObject.SetActive(true);
                             InGameCanvas.GetComponent<Canvas>().enabled = true;
				             OptionsCanvas.GetComponent<Canvas>().enabled = false;

				             GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = true;
                             GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = true;
                             bm.isDisplayed = false;
                             CastYourVoteMenu.SetActive(false);          
                             Debug.Log("Voted Yes");

				//			 SceneManager.LoadScene("KingsAndQueens");
                             OpenOrCloseVoteMenu();
							 ResetCamera();
                         }
                         else
                         {
                             Debug.Log("Voting Yes Messed Up");
                         }
                     });

	}
    public void VoteNay()
	{
				WaitingVoteList.Remove(bm.myID);
				NoVoteList.Add(bm.myID);
			
        		new LogEventRequest().SetEventKey("DrawVote")
                     .SetEventAttribute("cid", bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
        		     .SetEventAttribute("WaitingVotes", WaitingVoteList)
        		     .SetEventAttribute("NoVotes", NoVoteList)
        		     .SetEventAttribute("YesVotes", YesVoteList)
                     .SetEventAttribute("VoteCast", "Nay") //this means 'to vote', as in 'waiting to vote'
        		     .SetEventAttribute("GameName", bm.GameName)
                     .Send((response) =>
                     {
                         if (!response.HasErrors)
                         {
        				     OptionsCanvas.GetComponent<Canvas>().enabled = true;
        				     MainMenuButton.gameObject.SetActive(true);
                             ResetCameraButton.gameObject.SetActive(true);
                             QuitButton.gameObject.SetActive(true);
                             SurrenderButton.gameObject.SetActive(true);
                             VoteMenuButton.gameObject.SetActive(true);
                             MusicSwitch.gameObject.SetActive(true);
                             InGameCanvas.GetComponent<Canvas>().enabled = true;
                             OptionsCanvas.GetComponent<Canvas>().enabled = false;

                             GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = true;
                             GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = true;
                             bm.isDisplayed = false;
                             CastYourVoteMenu.SetActive(false);
                             Debug.Log("Voted No");

        				     // SceneManager.LoadScene("KingsAndQueens");
                             OpenOrCloseVoteMenu();
        					 ResetCamera();
        					 EndVoteAsNo();
                         }
                         else
                         {
                             Debug.Log("Voting Yes Messed Up");
                         }
                     });
	}
    public void EndVoteAsNo()
	{
		new GetChallengeRequest().SetChallengeInstanceId(bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
			 .Send((response) =>
			 {
				 if (response.HasErrors)
				 {
					 Debug.Log("error in endvoteasno()");
				 }
				 else
				 {
				     GSData data = response.Challenge.ScriptData.GetGSData("VotingPlayers");
                     Vote voteData = Vote.CreateFromJSON(data.JSON);
					 if (!voteData.isActiveVote)
					 {
						                new ChatOnChallengeRequest()
											  .SetChallengeInstanceId(bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
												  .SetMessage("[A MOVEMENT TO END THE BATTLE IN A DRAW HAS FAILED.]")
												  .Send((response2) =>
												  {
													  if (response2.HasErrors)
													  {
														  Debug.Log("Chat Send Error");
													  }
													  else
													  {
													 //clear the VotingPlayers Data from Script Data
													      List<string> empty = new List<string>();

														  new LogEventRequest().SetEventKey("DrawVote")
															   .SetEventAttribute("cid", bm.OnlineGame.GetComponent<GameInviteMessage>().challengeId)
															   .SetEventAttribute("WaitingVotes", empty)
															   .SetEventAttribute("NoVotes", empty)
															   .SetEventAttribute("YesVotes", empty)
															   .SetEventAttribute("VoteCast", "Done") //this means 'to vote', as in 'waiting to vote'
							                                   .SetEventAttribute("GameName", bm.GameName)
															   .Send((response3) =>
															   {
																   if (response3.HasErrors)
																   {
																	   Debug.Log("Error clearing VotingPlayers in ScriptData");
																   }
																   else
																   {

                                    
																	   WaitingVoteList.Clear();
																	   YesVoteList.Clear();
																	   NoVoteList.Clear();
																	   isVoteActive = false;
																	   VoteProgressMenu.SetActive(false);
																	   Options();
																	   ShowChatButton();
                                    
																   }
															   });
													  }
												  });

					 }

				 }
			 });
	}

    public void ShowTurnReviewButtons()
    {
        if (!TurnReviewButtons.activeInHierarchy)
        {
            //show the options to see previous turns
            TurnReviewButtons.SetActive(true);
            //       SummonButtonText.text = "Return To Game";

            PrisonButton.interactable = false;
            ChatButton.interactable = false;
            OptionsButton.interactable = false;
            ShowHideHUD_Button.interactable = false;
            MusicSwitch.gameObject.SetActive(false);
            bm.isDisplayed = true;
        }
        else
        {
            TurnReviewButtons.SetActive(false);
            //       SummonButtonText.text = "Review Past Turns";
            bm.BackFromPreviousTurn();

            if(HUDstatus == "on")
            {
                PrisonButton.interactable = true;
                OptionsButton.interactable = true;
                ChatButton.interactable = true;
                ShowHideHUD_Button.interactable = true;
                MusicSwitch.gameObject.SetActive(true);
            }
            bm.isDisplayed = false;
        }
    }
    public void ShowChatButton()
    {
        if (bm.menuDisplayed)
        {
            bm.menuDisplayed = false;
            ChatMenu.SetActive(false);
      //      ChatButton.GetComponentInChildren<Text>().text = "Chat";

            if(HUDstatus == "on")
            {
                PrisonButton.interactable = true;
                Summon.interactable = true;
                OptionsButton.interactable = true;
                ShowHideHUD_Button.interactable = true;
                MusicSwitch.gameObject.SetActive(true);
            }

            GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = true;
            bm.isDisplayed = false;
			CloseAllPrivateChatMenus();
        }
        else
        {
            bm.menuDisplayed = true;
            ChatMenu.SetActive(true);
      //      ChatButton.GetComponentInChildren<Text>().text = "Close";
            PrisonButton.interactable = false;
            Summon.interactable = false;
            OptionsButton.interactable = false;
            ShowHideHUD_Button.interactable = false;
            MusicSwitch.gameObject.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = false;
            bm.isDisplayed = true;
			if(OptionsCanvas.GetComponent<Canvas>().enabled)
			{
				Options();
			}
			if(TurnReviewButtons.activeInHierarchy)
			{
				ShowTurnReviewButtons();
			}
			if(PrisonMenus.activeInHierarchy)
			{
				ShowPrisonsButton();
			}

			InactivateMyButton();
			PrivateBlueButtonText.text = "Whisper to " + bm.playerList[0].playerName;
			PrivateYellowButtonText.text = "Whisper to " + bm.playerList[1].playerName;
			PrivateRedButtonText.text = "Whisper to " + bm.playerList[2].playerName;
			PrivateGreenButtonText.text = "Whisper to " + bm.playerList[3].playerName;
            
			PublicChatScroll.verticalNormalizedPosition = 0f;
        }

            PublicStar.gameObject.SetActive(false);
    }
    public void InactivateMyButton()
	{
		string mycolor = "color";
        foreach (Player p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                mycolor = p.color;
            }
        }
        if (mycolor == "Blue")
        {
            BlueChatButton.interactable = false;
        }
        if (mycolor == "Yellow")
        {
            YellowChatButton.interactable = false;
        }
        if (mycolor == "Red")
        {
            RedChatButton.interactable = false;
        }
        if (mycolor == "Green")
        {
            GreenChatButton.interactable = false;
        }
	}
	public void CloseAllPrivateChatMenus(){
		ChatToBlueMenu.SetActive(false);
        ChatToYellowMenu.SetActive(false);
        ChatToRedMenu.SetActive(false);
        ChatToGreenMenu.SetActive(false);
		//PublicStar.gameObject.SetActive(false);
        //BlueStar.gameObject.SetActive(false);
        //YellowStar.gameObject.SetActive(false);
        //RedStar.gameObject.SetActive(false);
        //GreenStar.gameObject.SetActive(false);
	}
	public void OpenChatToBlueMenu()
	{
		CloseAllPrivateChatMenus();
		if(bm.menuDisplayed){
			//the the Chat Menu is already open.
			ChatToBlueMenu.SetActive(true);
			InactivateMyButton();
		} else{
			//the Chat Menu is not open.
			ShowChatButton();
			ChatToBlueMenu.SetActive(true);
		}
		BlueStar.gameObject.SetActive(false);
		BlueScroll.verticalNormalizedPosition = 0f;
	}
	public void OpenChatToYellowMenu()
    {
		CloseAllPrivateChatMenus();
        if (bm.menuDisplayed)
        {
			//the the Chat Menu is already open.
            ChatToYellowMenu.SetActive(true);
			InactivateMyButton();
        }
        else
        {
            //the Chat Menu is not open.
            ShowChatButton();
            ChatToYellowMenu.SetActive(true);
        }
		YellowStar.gameObject.SetActive(false);
		YellowScroll.verticalNormalizedPosition = 0f;
    }
	public void OpenChatToRedMenu()
    {
		CloseAllPrivateChatMenus();
        if (bm.menuDisplayed)
        {
			//the the Chat Menu is already open.
            ChatToRedMenu.SetActive(true);
			InactivateMyButton();
        }
        else
        {
            //the Chat Menu is not open.
            ShowChatButton();
            ChatToRedMenu.SetActive(true);
        }
		RedStar.gameObject.SetActive(false);
		RedScroll.verticalNormalizedPosition = 0f;
    }
	public void OpenChatToGreenMenu()
    {
		CloseAllPrivateChatMenus();
        if (bm.menuDisplayed)
        {
            //the the Chat Menu is already open.
            ChatToGreenMenu.SetActive(true);
			InactivateMyButton();
        }
        else
        {
            //the Chat Menu is not open.
            ShowChatButton();
            ChatToGreenMenu.SetActive(true);
        }
		GreenStar.gameObject.SetActive(false);
		GreenScroll.verticalNormalizedPosition = 0f;
    }
    public void Options()
    {
        if (!OptionsCanvas.GetComponent<Canvas>().enabled)
        {
            //          OptionsButtonText.text = "Close";
            OptionsCanvas.GetComponent<Canvas>().enabled = true;
            bm.isDisplayed = true;
            GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = false;

            PrisonButton.interactable = false;
            Summon.interactable = false;
            ChatButton.interactable = false;
            ShowHideHUD_Button.interactable = false;
        }
        else if (OptionsCanvas.GetComponent<Canvas>().enabled)
        {
            //        OptionsButtonText.text = "Options";
            OptionsCanvas.GetComponent<Canvas>().enabled = false;
            bm.isDisplayed = false;
            GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = true;

            PrisonButton.interactable = true;
            Summon.interactable = true;
            ShowHideHUD_Button.interactable = true;
            if (bm.isOnline)
            {
                ChatButton.interactable = true;
            }
        }
    }
    public void MainMenu()
    {
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
    public void Quit()
    {
       // Application.Quit();
        if (GameObject.Find("PopUpPanel_AreYouSure(Clone)") == null)
        {
            GameObject go = Instantiate(QuitPopUp, QuitPopUpSpawn.transform);
        }
    }

    public void ShowPrevTurnBefore_Blue()
    {
        bm.PreviousTurn("bluebefore");
    }
    public void ShowPrevTurnAfter_Blue()
    {
        bm.PreviousTurn("blueafter");
    }
    public void ShowPrevTurnBefore_Yellow()
    {
        bm.PreviousTurn("yellowbefore");
    }
    public void ShowPrevTurnAfter_Yellow()
    {
        bm.PreviousTurn("yellowafter");
    }
    public void ShowPrevTurnBefore_Red()
    {
        bm.PreviousTurn("redbefore");
    }
    public void ShowPrevTurnAfter_Red()
    {
        bm.PreviousTurn("redafter");
    }
    public void ShowPrevTurnBefore_Green()
    {
        bm.PreviousTurn("greenbefore");
    }
    public void ShowPrevTurnAfter_Green()
    {
        bm.PreviousTurn("greenafter");
    }
    public void ShowPrisonsButton()
    {
        if (!bm.isDisplayed)
        {
            PrisonMenus.SetActive(true);
            ShowBluePrison();
            bm.isDisplayed = true;
            GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = false;

            OptionsButton.interactable = false;
            Summon.interactable = false;
            ChatButton.interactable = false;
            MusicSwitch.interactable = false;
            ShowHideHUD_Button.interactable = false;
        }
        else if (bm.isDisplayed)
        {
            PrisonMenus.SetActive(false);
            bm.isDisplayed = false;
            GameObject.Find("Main Camera").GetComponent<MobileTouchCamera>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<TouchInputController>().enabled = true;

            if(HUDstatus == "on")
            {
                OptionsButton.interactable = true;
                Summon.interactable = true;
                ChatButton.interactable = true;
                MusicSwitch.interactable = true;
                ShowHideHUD_Button.interactable = true;
            }
        }
    }
    public void ShowSurrenderPrompt()
    {
        if (GameObject.Find("PopUpPanel_AreYouSure_surrender(Clone)") == null)
        {
            GameObject go = Instantiate(SurrenderPopUp, QuitPopUpSpawn.transform);
        }
    }
    public void ShowBluePrison()
    {
        BluePrison.interactable = false;
        YellowPrison.interactable = true;
        RedPrison.interactable = true;
        GreenPrison.interactable = true;
        ListOneTitle.text = "Yellow Prisoners:";
        ListTwoTitle.text = "Red Prisoners:";
        ListThreeTitle.text = "Green Prisoners:";

        DisplayLists("blue");
    }
    public void ShowYellowPrison()
    {
        BluePrison.interactable = true;
        YellowPrison.interactable = false;
        RedPrison.interactable = true;
        GreenPrison.interactable = true;
        ListOneTitle.text = "Blue Prisoners:";
        ListTwoTitle.text = "Red Prisoners:";
        ListThreeTitle.text = "Green Prisoners:";

        DisplayLists("yellow");
    }
    public void ShowRedPrison()
    {
        BluePrison.interactable = true;
        YellowPrison.interactable = true;
        RedPrison.interactable = false;
        GreenPrison.interactable = true;
        ListOneTitle.text = "Blue Prisoners:";
        ListTwoTitle.text = "Yellow Prisoners:";
        ListThreeTitle.text = "Green Prisoners:";

        DisplayLists("red");
    }
    public void ShowGreenPrison()
    {
        BluePrison.interactable = true;
        YellowPrison.interactable = true;
        RedPrison.interactable = true;
        GreenPrison.interactable = false;
        ListOneTitle.text = "Blue Prisoners:";
        ListTwoTitle.text = "Yellow Prisoners:";
        ListThreeTitle.text = "Red Prisoners:";

        DisplayLists("green");
    }
    public void DisplayLists(string color)
    {
        if (ListOneContents.Count > 0)
        {
            foreach (GameObject go in ListOneContents)
            {
                Destroy(go.gameObject);
            }
            ListOneContents.Clear();
        }
        if (ListTwoContents.Count > 0)
        {
            foreach (GameObject go in ListTwoContents)
            {
                Destroy(go.gameObject);
            }
            ListTwoContents.Clear();
        }
        if (ListThreeContents.Count > 0)
        {
            foreach (GameObject go in ListThreeContents)
            {
                Destroy(go.gameObject);
            }
            ListThreeContents.Clear();
        }

        if (color == "blue")
        {
            if (bm.bluePrisonersYellow != null)
            {
                foreach (string s in bm.bluePrisonersYellow)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListOne.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListOneContents.Add(go);
                    }
                }
            }
            if (bm.bluePrisonersRed != null)
            {
                foreach (string s in bm.bluePrisonersRed)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListTwo.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListTwoContents.Add(go);
                    }
                }
            }
            if (bm.bluePrisonersGreen != null)
            {
                foreach (string s in bm.bluePrisonersGreen)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListThree.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListThreeContents.Add(go);
                    }
                }
            }
        }
        if (color == "yellow")
        {
            if (bm.yelPrisonersBlue != null)
            {
                foreach (string s in bm.yelPrisonersBlue)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListOne.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListOneContents.Add(go);
                    }
                }
            }

            if (bm.yelPrisonersRed != null)
            {
                foreach (string s in bm.yelPrisonersRed)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListTwo.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListTwoContents.Add(go);
                    }
                }
            }
            if (bm.yelPrisonersGreen != null)
            {
                foreach (string s in bm.yelPrisonersGreen)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListThree.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListThreeContents.Add(go);
                    }
                }
            }
        }
        if (color == "red")
        {
            if (bm.redPrisonersBlue != null)
            {
                foreach (string s in bm.redPrisonersBlue)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListOne.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListOneContents.Add(go);
                    }
                }
            }
            if (bm.redPrisonersYellow != null)
            {
                foreach (string s in bm.redPrisonersYellow)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListTwo.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListTwoContents.Add(go);
                    }
                }
            }
            if (bm.redPrisonersGreen != null)
            {
                foreach (string s in bm.redPrisonersGreen)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListThree.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListThreeContents.Add(go);
                    }
                }
            }
        }
        if (color == "green")
        {
            if (bm.greenPrisonersBlue != null)
            {
                foreach (string s in bm.greenPrisonersBlue)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListOne.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListOneContents.Add(go);
                    }
                }
            }
            if (bm.greenPrisonersYellow != null)
            {
                foreach (string s in bm.greenPrisonersYellow)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListTwo.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListTwoContents.Add(go);
                    }
                }
            }
            if (bm.greenPrisonersRed != null)
            {
                foreach (string s in bm.greenPrisonersRed)
                {
                    if (s != "Select a piece:")
                    {
                        GameObject go = Instantiate(PrisonerListing, ListThree.transform);
                        go.GetComponentInChildren<Text>().text = s;
                        ListThreeContents.Add(go);
                    }
                }
            }
        }
    }

    public void ResetCamera()
    {
        Vector3 position = new Vector3(6, 20, 6);
        Quaternion rotation = Quaternion.Euler(90, 0, 0);

        MyCamera.transform.position = position;
        MyCamera.transform.rotation = rotation;
        Options();
    }
    public void SnapToBlue()
    {
        Vector3 position = new Vector3(-3, 7, -3);
        Quaternion rotation = Quaternion.Euler(40, 46, 2);
        if(MyCamera.transform.position == position && MyCamera.transform.rotation == rotation)
        {
            ResetCamera();
            Options();
        }
        else
        {
            MyCamera.transform.position = position;
            MyCamera.transform.rotation = rotation;
        }
    }
    public void SnapToYellow()
    {
        Vector3 position = new Vector3(-3, 7, 15);
        Quaternion rotation = Quaternion.Euler(40, 136, -2);

        if(MyCamera.transform.position == position && MyCamera.transform.rotation == rotation)
        {
            ResetCamera();
            Options();
        }
        else
        {
            MyCamera.transform.position = position;
            MyCamera.transform.rotation = rotation;
        }
    }
    public void SnapToRed()
    {
        Vector3 position = new Vector3(15, 7, 15);
        Quaternion rotation = Quaternion.Euler(40, 230, 0);

        if(MyCamera.transform.position == position && MyCamera.transform.rotation == rotation)
        {
            ResetCamera();
            Options();
        }
        else
        {
            MyCamera.transform.position = position;
            MyCamera.transform.rotation = rotation;
        }
    }
    public void SnapToGreen()
    {
        Vector3 position = new Vector3(15, 7, -3);
        Quaternion rotation = Quaternion.Euler(40, 315, 0);

        if(MyCamera.transform.position == position && MyCamera.transform.rotation == rotation)
        {
            ResetCamera();
            Options();
        }
        else
        {
            MyCamera.transform.position = position;
            MyCamera.transform.rotation = rotation;
        }
    }

    public void AssignChatVariable()
	{
		ChatMenu.SetActive(true);
		ChatMenu.SetActive(false);
	}

  
}
