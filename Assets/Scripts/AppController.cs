using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using UnityEngine.Networking;

public class AppController : MonoBehaviour
{
    public UniWebView webView;

    public Text tt;


    void Awake(){

        if (!FB.IsInitialized) {
            FB.Init(InitCallback, OnHideUnity);
        } else {
            FB.ActivateApp();
        }
    }

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

    IEnumerator appFlyerCor() {
        yield return new WaitForSeconds(1f);
        AppsFlyer.trackRichEvent ("I_am_working", null);
            
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
            FB.ActivateApp();            

            FB.Mobile.SetAutoLogAppEventsEnabled(true); 
            LogIAmWorkingEvent(true);

            /*var perms = new List<string>(){"public_profile", "email"};
            FB.LogInWithReadPermissions(perms, AuthCallback);*/

            tt.text += "dasf";

            FB.Mobile.FetchDeferredAppLinkData(DeepLinkCallback);


        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }
    void DeepLinkCallback(IAppLinkResult result) {
        Debug.Log(result);
        if(result.TargetUrl == "yandex"){
            webView.Load("https://yandex.com");
            webView.Show();
        }
        else if(result.TargetUrl == "google"){
            webView.Load("https://google.ru/");
            webView.Show();
        }
    }    

    public void LogIAmWorkingEvent (bool val) {
        var parameters = new Dictionary<string, object>();
        parameters["is"] = val;   
        FB.LogAppEvent(
            "I am working",
            parameters: parameters
        );
    }

    private void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
}
