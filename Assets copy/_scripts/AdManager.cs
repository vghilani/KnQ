using System;
using UnityEngine;

// If we dont want ads running in our project, then it wont do this, and therefore
// will not cause errors.
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class AdManager : MonoBehaviour
{
    // https://www.youtube.com/watch?v=XHTTjyRzxmw

    #region Instance
    private static AdManager instance;
    public static AdManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<AdManager>();
                if(instance == null)
                {
                    instance = new GameObject("Spawned AdManager", typeof(AdManager)).GetComponent<AdManager>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    /*
        Apple App Store
        3105821
               
        Google Play Store
        3105820   
     */

    [Header("Config")]
    [SerializeField] private string gameID = "";
    [SerializeField] private bool testMode = true;
    [SerializeField] private string rewardedVideoPlacementId;
    [SerializeField] private string regularPlacementId;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
            gameID = "3105820";
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            gameID = "3105821";

        DontDestroyOnLoad(this.gameObject);
        Advertisement.Initialize(gameID, testMode);
    }
    public void ShowRegularAd(Action<ShowResult> callback)
    {
#if UNITY_ADS
        if(Advertisement.IsReady(regularPlacementId))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(regularPlacementId, so);
        }
        else
        {
            Debug.Log("Ad is not ready. Make sure you're online.");
        }
#else
        Debug.Log("Ads not supported");
#endif
    }
    public void ShowRewardedAd(Action<ShowResult> callback)
    {
#if UNITY_ADS
        if (Advertisement.IsReady(rewardedVideoPlacementId))
        {
            ShowOptions so = new ShowOptions();
            so.resultCallback = callback;
            Advertisement.Show(rewardedVideoPlacementId, so);
        }
        else
        {
            Debug.Log("Ad is not ready. Make sure you're online.");
        }
#else
        Debug.Log("Ads not supported");
#endif
    }
}
