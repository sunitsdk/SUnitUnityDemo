using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHAREitSDK
{
    public class CommonUtil
    {
        #if UNITY_ANDROID
        public static AndroidJavaObject dicToMap(Dictionary<string, string> dictionary) {
            if(dictionary == null) {
                return null;
            }
            AndroidJavaObject map = new AndroidJavaObject("java.util.HashMap");
            foreach(KeyValuePair<string, string> pair in dictionary) {
                map.Call<string>("put", pair.Key, pair.Value);
            }
            return map;
        }

        public static AndroidJavaObject getContext()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return jc.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public static void RunOnUIThread(AndroidJavaRunnable runnable)
        {
            getContext().Call("runOnUiThread", runnable);
        }
        #endif

        public static bool IsInvalidRuntime(RuntimePlatform platform)
        {
            if (Application.platform != platform)
            {
                return true;
            }
            return false;
        }

        //获取当前时间
        public static long GetTimeStamp(bool bflag = true)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            long ret;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds);
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds);
            return ret;
        }
    }


}