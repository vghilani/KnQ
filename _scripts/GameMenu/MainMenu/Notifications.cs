using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using GameSparks.Api.Requests;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{

    public string MyToken;
    public string MyRegistrationId;
    public Text mytoken;
    public Text reg;
    public Image AccountImage;
    public Sprite AccountIcon_Auth;
    public Sprite AccountIcon_noAuth;
    public bool FirebaseDependancies = false;

    public void Start()
    {
        // Make Sure Firebase doesn't need updated.
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.Result == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Firebase Depenencies are good");
                FirebaseDependancies = true;

            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

    public void Update()
    {
        //if (mytoken.text == "" && reg.text == "")
        //{
        //    AccountImage.sprite = AccountIcon_noAuth;
        //}
    }

    //Not in use
    public void InitializeFirebase()
    {
        if(FirebaseDependancies)
        {
            Debug.Log("Firebase Initializing, I think");
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
            Firebase.Messaging.FirebaseMessaging.TokenRegistrationOnInitEnabled = true;
        }
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
        MyToken = token.Token;

        new PushRegistrationRequest()
           .SetDurable(true)
           .SetDeviceOS("FCM")
           .SetPushId(MyToken)
           .Send((response) =>
           {
               MyRegistrationId = response.RegistrationId;
               AccountImage.sprite = AccountIcon_Auth;
           });
    }
    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from: " + e.Message.From);

    }

    public void KillListeners()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived -= OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived -= OnMessageReceived;
        mytoken.text = "";
        reg.text = "";
    }
    public void OnDestroy()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived -= OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived -= OnMessageReceived;
    }
    public void OnDisable()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived -= OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived -= OnMessageReceived;
    }
}
