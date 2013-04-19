using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class FlurryManager : MonoBehaviour
{
    private static FlurryManager mInstance = null;
    private static FlurryAgent flurryAgent = null;
    public string apiKey_iPhone;
    public string apiKey_iPad;
    public string apiKey_Android;

    void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static FlurryManager instance
    {
        get
        {
            return mInstance;
        }
    }

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern bool flurryIsIpad();
#endif

    void Start ()
    {
#if UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string apiKey = apiKey_iPhone;
            if (flurryIsIpad() && apiKey_iPad != null)
            {
                apiKey = apiKey_iPad;
            }

            if (apiKey != null)
            {
                flurryAgent = new FlurryAgentIOS();
                flurryAgent.onStartSession(apiKey);
            }
        }
#endif

#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android && apiKey_Android != null)
        {
            flurryAgent = new FlurryAgentAndroid();
            flurryAgent.onStartSession(apiKey_Android);
        }
#endif
    }

    void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android && apiKey_Android != null && flurryAgent != null)
        {
            if (pause)
            {
                flurryAgent.onEndSession();
            }
            else
            {
                flurryAgent.onStartSession(apiKey_Android);
            }
        }
    }

    void OnApplicationQuit()
    {
        if (Application.platform == RuntimePlatform.Android && apiKey_Android != null)
        {
            if (flurryAgent != null)
            {
                flurryAgent.onEndSession();
            }
        }
    }

    static public void logEvent(string eventId)
    {
        if (flurryAgent != null)
        {
            flurryAgent.logEvent(eventId);
        }
    }

    static public void logEvent(string eventId, Hashtable parameters)
    {
        if (flurryAgent != null)
        {
            flurryAgent.logEvent(eventId, parameters);
        }
    }

    static public void onError(string errorId, string message, string errorClass)
    {
        if (flurryAgent != null)
        {
            flurryAgent.onError(errorId, message, errorClass);
        }
    }

    static public void onPageView()
    {
        if (flurryAgent != null)
        {
            flurryAgent.onPageView();
        }
    }

    static public void setUserID(string userId)
    {
        if (flurryAgent != null)
        {
            flurryAgent.setUserID(userId);
        }
    }

    static public void setAge(int age)
    {
        if (flurryAgent != null)
        {
            flurryAgent.setAge(age);
        }
    }

    static public void setGender(FlurryAgent.Gender gender)
    {
        if (flurryAgent != null)
        {
            flurryAgent.setGender(gender);
        }
    }

    static public void setReportLocation(bool reportLocation)
    {
        if (flurryAgent != null)
        {
            flurryAgent.setReportLocation(reportLocation);
        }
    }
}