using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdTest : MonoBehaviour
{
    public float countdown_timer = 60.0f;
    public float ad_trigger_timer = 0.0f;
    public static bool should_countdown = true;

    public void Update()
    {
        if(SceneManager.GetSceneByName("KingsAndQueens").isLoaded)
        {
            //if we're offline - this is how ads should trigger.
            if (GameObject.Find("Chessboard").GetComponent<boardmanager>().isOnline)
            {
                //if the coundown timer is less than 60 (IE; while it'd counting down)
                //then tick the ad timer up during this time too.
                if (countdown_timer < 60.0f)
                {
                    ad_trigger_timer += Time.deltaTime;
                }
                //when the count down timer reaches zero, reset it to 60, and make
                //sure it doesnt continue counting down.  we only want to add 60
                //seconds maximum to the ad timer during a player's turn.
                if (countdown_timer <= 0.0f)
                {
                    should_countdown = false;
                    countdown_timer = 60.0f;
                }
                //this static bool can be triggered by the boardmanager script after 
                //each turn is taken.
                if (should_countdown)
                {
                    countdown_timer -= Time.deltaTime;
                }
                //when the ad trigger timer reaches 300, play a skipable ad and reset
                //the timer.
                if (ad_trigger_timer >= 300.0f)
                {
                    PlayAd();
                    ad_trigger_timer = 0.0f;
                }
            }
        }
    }

    public void PlayAd()
    {
        AdManager.Instance.ShowRegularAd(OnAdClosed);
    }

    public void PlayRewardedAd()
    {
        AdManager.Instance.ShowRewardedAd(OnRewardedAdClosed);
    }

    private void OnAdClosed(ShowResult result)
    {
        Debug.Log("Ad Closed");
    }
    private void OnRewardedAdClosed(ShowResult result)
    {
        Debug.Log("Rewarded Ad Closed");

        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Rewarded Ad Complete. Reward the Player");
                break;
            case ShowResult.Skipped:
                Debug.Log("Rewarded Ad Skipped. Nothing for the Player");
                break;
            case ShowResult.Failed:
                Debug.Log("Rewarded Ad Failed");
                break;
        }
    }
}
