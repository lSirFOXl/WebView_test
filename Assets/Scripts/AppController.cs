using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class AppController : MonoBehaviour
{
    public UniWebView webView;

    public Text tt;


    void Awake(){
        
        /*if (FB.IsInitialized) {
            tt.text += "qwertbn"+2+"\n\r";
            FB.ActivateApp();
            FB.Mobile.SetAutoLogAppEventsEnabled(true); 
            LogIAmWorkingEvent(true);
        } 
        else {
            //Handle FB.Init
            tt.text += "qwertbn"+3+"\n\r";
            FB.Init( () => {
                FB.ActivateApp();

                var perms = new List<string>(){"public_profile", "email"};
                FB.LogInWithReadPermissions(perms, AuthCallback);

                FB.Mobile.SetAutoLogAppEventsEnabled(true); 
                LogIAmWorkingEvent(true);
                tt.text += "qwertbn"+4+"\n\r";
            });
        }*/

        if (!FB.IsInitialized) {
            FB.Init(InitCallback, OnHideUnity);
        } else {
            FB.ActivateApp();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        AppsFlyer.setAppsFlyerKey ("yHiUWGDALquAxRVr3LCn8V");
        #if UNITY_IOS
            AppsFlyer.setAppID ("yHiUWGDALquAxRVr3LCn8V");
            AppsFlyer.trackAppLaunch ();
        #elif UNITY_ANDROID
            AppsFlyer.setAppID ("com.qwe.qwe-Standalone");
            AppsFlyer.init ("yHiUWGDALquAxRVr3LCn8V","AppsFlyerTrackerCallbacks");
        #endif

        StartCoroutine(appFlyerCor());
        

        OneSignal.StartInit("f512932f-26c4-4060-82bc-ee1a54a2d5d7")
        .HandleNotificationOpened(HandleNotificationOpened)
        .EndInit();

        OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.DEBUG, OneSignal.LOG_LEVEL.DEBUG);
    
        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;


        var webViewGameObject = new GameObject("UniWebView");
        webView = webViewGameObject.AddComponent<UniWebView>();

        webView.toolBarShow = true;
        webView.Load("https://google.com");
        webView.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator appFlyerCor() {
        yield return new WaitForSeconds(1f);
        AppsFlyer.trackRichEvent ("I_am_working", null);
            
    }

    public void showSite(){
        /* WebViewer.OnLoadComplete += OnLoadComplete;

        WebViewer.InsetsForScreenOreitation += InsetsForScreenOreitation;
        WebViewer.toolBarShow = true;

        // Now, we could set the url and load the page.
        WebViewer.url = "https://google.com";
        WebViewer.Load();*/
        
    }

    public void aaa(){
        tt.text += " bbb ";

        AppsFlyer.trackRichEvent ("af_login", null);
        

        Dictionary<string, string> LevelAchievedEvent = new Dictionary<string, string>();
        LevelAchievedEvent.Add("af_level", "10");
        LevelAchievedEvent.Add("af_score", "500");
        AppsFlyer.trackRichEvent ("af_level_achieved", LevelAchievedEvent);
    }

    private static void HandleNotificationOpened(OSNotificationOpenedResult result) {

    }

    

    void OnLoadComplete(UniWebView webView, bool success, string errorMessage) {
        if (success) {
            webView.Show();
        } else {
            Debug.Log("Something wrong in webview loading: " + errorMessage);
        }
    }

    private void AuthCallback (ILoginResult result) {
        if (FB.IsLoggedIn) {

            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            Debug.Log(aToken.UserId);
            foreach (string perm in aToken.Permissions) {
                Debug.Log(perm);
            }
        } else {
            Debug.Log("User cancelled login");
        }
    }

    private void InitCallback ()
    {
        if (FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
            //tt.text += "q";

            /*var perms = new List<string>(){"public_profile", "email"};
            FB.LogInWithReadPermissions(perms, AuthCallback);*/

            FB.Mobile.SetAutoLogAppEventsEnabled(true); 
            LogIAmWorkingEvent(true);

        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    public void LogIAmWorkingEvent (bool val) {
        var parameters = new Dictionary<string, object>();
        parameters["is"] = val;   
        FB.LogAppEvent(
            "I am working",
            parameters: parameters
        );
        //tt.text += "2";
    }

    private void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        } else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
}
