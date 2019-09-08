using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class SplashMenu : MonoBehaviour
{

	private static SplashMenu instance = null;
	public float KillSplashImage = 0;
	public GameObject SplashImage;
	public GameObject TableTopSetUp;
	public GameObject TutorialMenu;
	public Text BlueSetUpText;
	public Text YellowSetUpText;
	public Text RedSetUpText;
	public Text GreenSetUpText;
	public GameObject Main;
	public GameObject QuitSpawn;
	public GameObject QuitPopUp;
	public bool BlueIsAI = false;
	public bool YellowIsAI = false;
	public bool RedIsAI = false;
	public bool GreenIsAI = false;
    public Button BackFromTutorialButton, BackFromLocalSetupButton;


	private void Awake()
	{
		TableTopSetUp.gameObject.SetActive(false);
		TutorialMenu.gameObject.SetActive(false);
        BackFromTutorialButton.gameObject.SetActive(false);
        BackFromLocalSetupButton.gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Update()
	{
		KillSplashImage += Time.deltaTime;

		if (KillSplashImage >= 1)
		{
			Destroy(SplashImage.gameObject);
		}
	}

	public void LoadSetUpMenu()
	{
		TableTopSetUp.gameObject.SetActive(true);
        BackFromLocalSetupButton.gameObject.SetActive(true);
		Main.gameObject.SetActive(false);
	}
	public void BackFromSetUp()
	{
		TableTopSetUp.gameObject.SetActive(false);
        BackFromLocalSetupButton.gameObject.SetActive(false);
		Main.gameObject.SetActive(true);
	}

	public void LoadTutorialMenu()
	{
		TutorialMenu.gameObject.SetActive(true);
        BackFromTutorialButton.gameObject.SetActive(true);
		Main.gameObject.SetActive(false);
	}
	public void BackFromTutorialMenu()
	{
		Main.gameObject.SetActive(true);
        BackFromTutorialButton.gameObject.SetActive(false);
		TutorialMenu.gameObject.SetActive(false);
	}

	public void BlueSetting()
	{
		if (BlueSetUpText.text == "AI")
		{
			BlueSetUpText.text = "HUMAN";
			BlueIsAI = false;
		}
		else
		{
			BlueSetUpText.text = "AI";
			BlueIsAI = true;
		}
	}
	public void YellowSetting()
	{
		if (YellowSetUpText.text == "AI")
		{
			YellowSetUpText.text = "HUMAN";
			YellowIsAI = false;
		}
		else
		{
			YellowSetUpText.text = "AI";
			YellowIsAI = true;
		}
	}
	public void RedSetting()
	{
		if (RedSetUpText.text == "AI")
		{
			RedSetUpText.text = "HUMAN";
			RedIsAI = false;
		}
		else
		{
			RedSetUpText.text = "AI";
			RedIsAI = true;
		}
	}
	public void GreenSetting()
	{
		if (GreenSetUpText.text == "AI")
		{
			GreenSetUpText.text = "HUMAN";
			GreenIsAI = false;
		}
		else
		{
			GreenSetUpText.text = "AI";
			GreenIsAI = true;
		}
	}

	public void LoadSinglePlayer()
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


		SceneManager.LoadScene("KingsAndQueens");
	}
	public void LoadMultiPlayerAuthentication()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void Quit()
	{
		//      Application.Quit();
		if (GameObject.Find("PopUpPanel_AreYouSure(Clone)") == null)
		{
			GameObject go = Instantiate(QuitPopUp, QuitSpawn.transform);
		}
	}

	//http://www.thegamecontriver.com/2015/04/unity-share-text-message-game-link-android.html
	public void AppInviteMethod()
	{
		string subject = "KingsAndQueens";
		string body = "Come play Kings and Queens: War Games with me!  Get the Android Version:  https://play.google.com/apps/internaltest/4701588489567481601 or Get the iOS Version: https://testflight.apple.com/join/XXgDxfEJ";
		//execute the below lines if being run on a Android device
#if UNITY_ANDROID
		//Refernece of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
		//Refernece of AndroidJavaObject class for intent
		AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
		//call setAction method of the Intent object created
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		//set the type of sharing that is happening
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		//add data to be passed to the other activity i.e., the data to be sent
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body);
		//get the current activity
		AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//start the activity by sending the intent data
		currentActivity.Call("startActivity", intentObject);
#endif

	}
}
