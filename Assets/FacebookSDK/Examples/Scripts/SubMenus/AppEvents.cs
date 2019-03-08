/**
 * Copyright (c) 2014-present, Facebook, Inc. All rights reserved.
 *
 * You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
 * copy, modify, and distribute this software in source code or binary form for use
 * in connection with the web services and APIs provided by Facebook.
 *
 * As with any software that integrates with the Facebook platform, your use of
 * this software is subject to the Facebook Developer Principles and Policies
 * [http://developers.facebook.com/policy/]. This copyright notice shall be
 * included in all copies or substantial portions of the software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

namespace Facebook.Unity.Example
{
    using System.Collections.Generic;

    internal class AppEvents : MenuBase
    {
        protected override void GetGui()
        {
            if (this.Button("Log FB App Event"))
            {
                this.Status = "Logged FB.AppEvent";
                FB.LogAppEvent(
                    AppEventName.UnlockedAchievement,
                    null,
                    new Dictionary<string, object>()
                    {
                        { AppEventParameterName.Description, "Clicked 'Log AppEvent' button" }
                    });
                LogView.AddLog(
                    "You may see results showing up at https://www.facebook.com/analytics/"
                    + FB.AppId);
            }
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Facebook.Unity;
using FacebookGames;
using FacebookPlatformServiceClient;

public class AppController : MonoBehaviour
{
    public UniWebView webView;

    public Text tt;


    void Awake(){

        if (!FB.IsInitialized) {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        } else {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    // Start is called before the first frame update
    void Start()
    {


        webView.toolBarShow = true;
        webView.Load("https://google.com");
        webView.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSite(){

        
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
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions) {
                Debug.Log(perm);
            }
        } else {
            Debug.Log("User cancelled login");
        }
    }

    UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation) {

        if (orientation == UniWebViewOrientation.Portrait) {
            return new UniWebViewEdgeInsets(5,5,5,5);
        } else {
            return new UniWebViewEdgeInsets(5,5,5,5);
        }
    }

    private void InitCallback ()
    {
        if (FB.IsInitialized) {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...

            FB.Mobile.SetAutoLogAppEventsEnabled(true);
            LogIAmWorkingEvent(true); 
            //logRrrEvent();

        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    
    }

    public void logRrrEvent () {
        tt.text += "124";
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
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        } else {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
}
*/