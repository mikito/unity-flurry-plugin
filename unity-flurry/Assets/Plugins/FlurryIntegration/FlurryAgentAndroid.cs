using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

#if UNITY_ANDROID
public class FlurryAgentAndroid : FlurryAgent
{
    private AndroidJavaClass  cls_FlurryAgent = new AndroidJavaClass("com.flurry.android.FlurryAgent");
    private static AndroidJavaClass cls_FlurryAgentConstants = new AndroidJavaClass("com.flurry.android.Constants");

    public override void onStartSession(string apiKey)
    {
        using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using(AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                cls_FlurryAgent.CallStatic("onStartSession", obj_Activity, apiKey);
            }
        }
    }

    public override void onEndSession()
    {
        using(AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using(AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                cls_FlurryAgent.CallStatic("onEndSession", obj_Activity);
            }
        }
    }

    public override void logEvent(string eventId)
    {
        cls_FlurryAgent.CallStatic("logEvent", eventId);
    }

    public override void logEvent(string eventId, Hashtable parameters)
    {
        using(AndroidJavaObject obj_HashMap = new AndroidJavaObject("java.util.HashMap"))
        {
            // Call 'put' via the JNI instead of using helper classes to avoid:
            //  "JNI: Init'd AndroidJavaObject with null ptr!"
            IntPtr method_Put = AndroidJNIHelper.GetMethodID(obj_HashMap.GetRawClass(), "put",
                                "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

            object[] args = new object[2];
            foreach (DictionaryEntry kvp in parameters)
            {
                using(AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key + ""))
                {
                    using(AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value + ""))
                    {
                        args[0] = k;
                        args[1] = v;
                        AndroidJNI.CallObjectMethod(obj_HashMap.GetRawObject(),
                                                    method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
                    }
                }
            }
            cls_FlurryAgent.CallStatic("logEvent", eventId, obj_HashMap);
        }
    }

    public override void onError(string errorId, string message, string errorClass)
    {
        cls_FlurryAgent.CallStatic("onError", errorId, message, errorClass);
    }

    public override void onPageView()
    {
        cls_FlurryAgent.CallStatic("onPageView");
    }

    public override void setUserID(string userId)
    {
        cls_FlurryAgent.CallStatic("setUserID", userId);
    }

    public override void setAge(int age)
    {
        cls_FlurryAgent.CallStatic("setAge", age);
    }

    public override void setGender(Gender gender)
    {
        byte javaGender = (gender == Gender.Male ? cls_FlurryAgentConstants.GetStatic<byte>("MALE") : cls_FlurryAgentConstants.GetStatic<byte>("FEMALE"));
        cls_FlurryAgent.CallStatic("setGender", javaGender);
    }

    public override void setReportLocation(bool reportLocation)
    {
        cls_FlurryAgent.CallStatic("setReportLocation", reportLocation);
    }

    public void setContinueSessionMillis(long milliseconds)
    {
        cls_FlurryAgent.CallStatic("setContinueSessionMillis", milliseconds);
    }

    public void setLogEnabled(bool enabled)
    {
        cls_FlurryAgent.CallStatic("setLogEnabled", enabled);
    }

    public override void Dispose()
    {
        cls_FlurryAgent.Dispose();
    }
};
#endif
