using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dogtag : MonoBehaviour {


    [SerializeField] public string playerId;
    [SerializeField] public string displayName;
    public Text buttonText;

    public Button Invite_Button;

    private void Start()
    {
        buttonText.text = displayName;
    }
    private void Update()
    {
        if(New_CreateMenu.PlayerIDs.Count != 0)
        {
            foreach (string s in New_CreateMenu.PlayerIDs)
            {
                if(s == playerId)
                {
                    Invite_Button.interactable = false;
                }
                else
                {
                    Invite_Button.interactable = true;
                }
            }
        }

    }
    public void InviteButton()
    {
        if(New_CreateMenu.PlayerIDs.Count != 4)
        {
            if(!New_CreateMenu.PlayerIDs.Contains(playerId))
            {
                New_CreateMenu.PlayerIDs.Add(playerId);
				playersData player = new playersData();
				player.id = playerId;
				player.playername = displayName;
				New_CreateMenu.playersData.Add(player);
                New_CreateMenu.ThesePlayerNames.Add(displayName);
                New_CreateMenu.UpdateList = true;
            }
        }
    }
}
