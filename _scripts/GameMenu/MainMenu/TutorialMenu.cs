using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour {

	public static TutorialMenu Instance { set; get; }
	public GameObject ActiveMenu;
	public List<GameObject> Screens;

	// Use this for initialization
	void Start () {
		
		Instance = this;

		foreach (GameObject menu in Screens)
		{         
			menu.gameObject.SetActive(false);
		}

		ActiveMenu = Screens[0];
		ActiveMenu.gameObject.SetActive(true);        
	}
	public void GoToHistory()
    {
		ActiveMenu.gameObject.SetActive(false); 
        ActiveMenu = Screens[0];
		ActiveMenu.gameObject.SetActive(true); 
    }
	public void GoToObjective()
    {
		ActiveMenu.gameObject.SetActive(false); 
		ActiveMenu = Screens[1];
		ActiveMenu.gameObject.SetActive(true); 
    }
	public void GoToSetUp()
    {
		ActiveMenu.gameObject.SetActive(false); 
        ActiveMenu = Screens[2];
		ActiveMenu.gameObject.SetActive(true); 
    }
    public void GoToKing()
	{
		ActiveMenu.gameObject.SetActive(false); 
		ActiveMenu = Screens[3];
		ActiveMenu.gameObject.SetActive(true); 
	}
	public void GoToQueen()
    {
		ActiveMenu.gameObject.SetActive(false); 
        ActiveMenu = Screens[4];
		ActiveMenu.gameObject.SetActive(true); 
    }
	public void GoToBishop()
    {
		ActiveMenu.gameObject.SetActive(false); 
        ActiveMenu = Screens[5];
		ActiveMenu.gameObject.SetActive(true); 
    }
	public void GoToKnight()
    {
		ActiveMenu.gameObject.SetActive(false); 
        ActiveMenu = Screens[6];
		ActiveMenu.gameObject.SetActive(true); 
    }
	public void GoToRook()
    {
		ActiveMenu.gameObject.SetActive(false); 
        ActiveMenu = Screens[7];
		ActiveMenu.gameObject.SetActive(true); 
    }
	public void GoToPawn()
    {
		ActiveMenu.gameObject.SetActive(false); 
        ActiveMenu = Screens[8];
		ActiveMenu.gameObject.SetActive(true); 
    }
	public void GoToSpecialMoves()
    {
		ActiveMenu.gameObject.SetActive(false); 
        ActiveMenu = Screens[9];
		ActiveMenu.gameObject.SetActive(true); 
    }
}
