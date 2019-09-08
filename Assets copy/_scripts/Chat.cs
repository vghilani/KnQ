using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class Chat : MonoBehaviour {

    public InputField Statement;
    public Button SendButton;
    public GameObject ChatMessageSpawn;
    public GameObject ChatListing;
    boardmanager bm;

    // Use this for initialization
    void Start () {
        SendButton.interactable = false;
        bm = GameObject.Find("Chessboard").GetComponent<boardmanager>();
    }
	
	// Update is called once per frame
	void Update () {
		if(Statement.text != null && Statement.text != "Enter text..." && Statement.text != " " && Statement.text != "")
        {
            SendButton.interactable = true;
        }
        else
        {
            SendButton.interactable = false;
        }
	}

    //public chat
	public void SendChatMessage()
	{
		string myName = "name";
		foreach (var p in bm.playerList)
		{
			if(p.id == bm.myID)
			{
				myName = p.playerName;
			}
		}

		new ChatOnChallengeRequest()
        	.SetChallengeInstanceId(GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
			.SetMessage(myName + ": " + Statement.text)
        	.Send((response) =>
			{
				if (response.HasErrors)
				{
					Debug.Log("Chat Send Error");
				}

			});
		Statement.text = "";
	}
    
    //private chat button triggers
    public void ChatWithBlue()
	{
		string myName = "name";
        string myColor = "color";
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }

		if(myColor == "Yellow")
		{
			SendPrivateChatMessage_BlueAndYellow();
		}
		if(myColor == "Red")
		{
			SendPrivateChatMessage_BlueAndRed();
		}
		if (myColor == "Green")
        {
            SendPrivateChatMessage_BlueAndGreen();
        }
        Statement.text = "";
	}
    public void ChatWithYellow()
	{
		string myName = "name";
        string myColor = "color";
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }

        if (myColor == "Blue")
        {
            SendPrivateChatMessage_BlueAndYellow();
        }
        if (myColor == "Red")
        {
            SendPrivateChatMessage_YellowAndRed();
        }
        if (myColor == "Green")
        {
            SendPrivateChatMessage_YellowAndGreen();
        }
        Statement.text = "";
	}
    public void ChatWithRed()
	{
		string myName = "name";
        string myColor = "color";
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }

        if (myColor == "Yellow")
        {
			SendPrivateChatMessage_YellowAndRed();
        }
        if (myColor == "Blue")
        {
            SendPrivateChatMessage_BlueAndRed();
        }
        if (myColor == "Green")
        {
            SendPrivateChatMessage_RedAndGreen();
        }
        Statement.text = "";
	}
    public void ChatWithGreen()
	{
		string myName = "name";
        string myColor = "color";
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }

        if (myColor == "Yellow")
        {
			SendPrivateChatMessage_YellowAndGreen();
        }
        if (myColor == "Red")
        {
            SendPrivateChatMessage_RedAndGreen();
        }
        if (myColor == "Blue")
        {
            SendPrivateChatMessage_BlueAndGreen();
        }
        Statement.text = "";
	}
    //private chat logic and sending
    public void SendPrivateChatMessage_BlueAndYellow()
	{
		string myName = "name";
		string myColor = "color";
		Player them = new Player();

        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
				myColor = p.color;
            }
        }

		if(myColor == "Blue")
		{
			them = bm.playerList[1]; //if im blue they're yellow
		}else{
			them = bm.playerList[0]; //if im not blue then they are.
		}

		new LogEventRequest()
			.SetEventKey("PrivateChat")
			.SetEventAttribute("cid", GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
			.SetEventAttribute("msg", myName + ": " + Statement.text)
			.SetEventAttribute("senderID", bm.myID)
			.SetEventAttribute("recipientID", them.id)
			.SetEventAttribute("senderColor", myColor)
			.SetEventAttribute("recipientColor", them.color)
			.Send((response) =>
			{
				if (response.HasErrors)
				{
					Debug.Log("Private Chat Send Error");
				}
				else
				{
					new ChatOnChallengeRequest()
        			.SetChallengeInstanceId(GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
					.SetMessage("System: " + myName + " whispered to " + them.playerName + "...")
        			.Send((response2) =>
        			{
        				if (response2.HasErrors)
        				{
        					Debug.Log("Chat Send Error");
        				}               
        			});
				}
			});


	}
	public void SendPrivateChatMessage_BlueAndRed()
    {
        string myName = "name";
        string myColor = "color";
		Player them;
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }
		if (myColor == "Blue")
        {
            them = bm.playerList[2]; //if im blue they're red
        }
        else
        {
            them = bm.playerList[0]; //if im not blue then they are.
        }

		new LogEventRequest()
        	.SetEventKey("PrivateChat")
        	.SetEventAttribute("cid", GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
        	.SetEventAttribute("msg", myName + ": " + Statement.text)
        	.SetEventAttribute("senderID", bm.myID)
        	.SetEventAttribute("recipientID", them.id)
        	.SetEventAttribute("senderColor", myColor)
        	.SetEventAttribute("recipientColor", them.color)
        	.Send((response) =>
        	{
        		if (response.HasErrors)
        		{
        			Debug.Log("Private Chat Send Error");
        		}
        		else
        		{
        			new ChatOnChallengeRequest()
        			.SetChallengeInstanceId(GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
					.SetMessage("System: " + myName + " whispered to " + them.playerName + "...")
        			.Send((response2) =>
        			{
        				if (response2.HasErrors)
        				{
        					Debug.Log("Chat Send Error");
        				}
        			});
        		}
        	});
    }
	public void SendPrivateChatMessage_BlueAndGreen()
    {
        string myName = "name";
        string myColor = "color";
		Player them;
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }
		if (myColor == "Blue")
        {
            them = bm.playerList[3]; //if im blue they're green
        }
        else
        {
            them = bm.playerList[0]; //if im not blue then they are.
        }

		new LogEventRequest()
            .SetEventKey("PrivateChat")
            .SetEventAttribute("cid", GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            .SetEventAttribute("msg", myName + ": " + Statement.text)
            .SetEventAttribute("senderID", bm.myID)
            .SetEventAttribute("recipientID", them.id)
            .SetEventAttribute("senderColor", myColor)
            .SetEventAttribute("recipientColor", them.color)
            .Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log("Private Chat Send Error");
                }
                else
                {
                    new ChatOnChallengeRequest()
                    .SetChallengeInstanceId(GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
                    .SetMessage("System: " + myName + " whispered to " + them.playerName + "...")
                    .Send((response2) =>
                    {
                        if (response2.HasErrors)
                        {
                            Debug.Log("Chat Send Error");
                        }
                    });
                }
            });
    }
	public void SendPrivateChatMessage_YellowAndRed()
    {
        string myName = "name";
        string myColor = "color";
		Player them;
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }
		if (myColor == "Yellow")
        {
            them = bm.playerList[2]; //if im yellow theyre red
        }
        else
        {
            them = bm.playerList[1]; //if im not yellow then they are.
        }

		new LogEventRequest()
            .SetEventKey("PrivateChat")
            .SetEventAttribute("cid", GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            .SetEventAttribute("msg", myName + ": " + Statement.text)
            .SetEventAttribute("senderID", bm.myID)
            .SetEventAttribute("recipientID", them.id)
            .SetEventAttribute("senderColor", myColor)
            .SetEventAttribute("recipientColor", them.color)
            .Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log("Private Chat Send Error");
                }
                else
                {
                    new ChatOnChallengeRequest()
                    .SetChallengeInstanceId(GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
					.SetMessage("System: " + myName + " whispered to " + them.playerName + "...")
                    .Send((response2) =>
                    {
                        if (response2.HasErrors)
                        {
                            Debug.Log("Chat Send Error");
                        }
                    });
                }
            });
    }
	public void SendPrivateChatMessage_YellowAndGreen()
    {
        string myName = "name";
        string myColor = "color";
		Player them;
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }
		if (myColor == "Yellow")
        {
            them = bm.playerList[3]; //if im yellow theyre green
        }
        else
        {
            them = bm.playerList[1]; //if im not yellow then they are.
        }

		new LogEventRequest()
            .SetEventKey("PrivateChat")
            .SetEventAttribute("cid", GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            .SetEventAttribute("msg", myName + ": " + Statement.text)
            .SetEventAttribute("senderID", bm.myID)
            .SetEventAttribute("recipientID", them.id)
            .SetEventAttribute("senderColor", myColor)
            .SetEventAttribute("recipientColor", them.color)
            .Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log("Private Chat Send Error");
                }
                else
                {
                    new ChatOnChallengeRequest()
                    .SetChallengeInstanceId(GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
					.SetMessage("System: " + myName + " whispered to " + them.playerName + "...")
                    .Send((response2) =>
                    {
                        if (response2.HasErrors)
                        {
                            Debug.Log("Chat Send Error");
                        }
                    });
                }
            });
    }
	public void SendPrivateChatMessage_RedAndGreen()
    {
        string myName = "name";
        string myColor = "color";
		Player them;
        foreach (var p in bm.playerList)
        {
            if (p.id == bm.myID)
            {
                myName = p.playerName;
                myColor = p.color;
            }
        }
		if (myColor == "Red")
        {
            them = bm.playerList[3]; //if im red theyre green
        }
        else
        {
            them = bm.playerList[2]; //if im not red then they are.
        }

		new LogEventRequest()
            .SetEventKey("PrivateChat")
            .SetEventAttribute("cid", GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
            .SetEventAttribute("msg", myName + ": " + Statement.text)
            .SetEventAttribute("senderID", bm.myID)
            .SetEventAttribute("recipientID", them.id)
            .SetEventAttribute("senderColor", myColor)
            .SetEventAttribute("recipientColor", them.color)
            .Send((response) =>
            {
                if (response.HasErrors)
                {
                    Debug.Log("Private Chat Send Error");
                }
                else
                {
                    new ChatOnChallengeRequest()
                    .SetChallengeInstanceId(GameObject.Find("Chessboard").GetComponent<boardmanager>().OnlineGame.GetComponent<GameInviteMessage>().challengeId)
					.SetMessage("System: " + myName + " whispered to " + them.playerName + "...")
                    .Send((response2) =>
                    {
                        if (response2.HasErrors)
                        {
                            Debug.Log("Chat Send Error");
                        }
                    });
                }
            });
    }
}
