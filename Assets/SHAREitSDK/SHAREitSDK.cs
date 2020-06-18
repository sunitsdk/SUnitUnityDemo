using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace SHAREitSDK {
    public class SHAREitSDK : MonoBehaviour
    {
        public const string ENV_TEST = "Test";
        public const string ENV_PROD = "Prod";

        public const string KEY_CHANNEL = "channel";
        public const string KEY_ENV = "env";
        public const string KEY_HOST_CLASS = "hostClass";
        public const string KEY_IS_MAIN_CLASS = "isMainProcess";
        public const string KEY_IS_EU_AGREED = "isEUAgreed";

        public const string BOOL_TRUE = "1";
        public const string BOOL_FALSE = "0";

        public static int AD_LOADED = 0;
        public static int AD_LOAD_ERROR = 1;

        public const int AD_SHOW_FAILED = 1;
        public const int AD_SHOW_IMPRESSION = 2;
        public const int AD_SHOW_CLICKED = 3;
        public const int AD_SHOW_REWARDED = 4;
        public const int AD_SHOW_CLOSED = 5;

        public const string ACTION_TYPE_LOGIN_SUCCESS = "loginSuccess";
        public const string ACTION_TYPE_SHOW_RATE_FAIL = "showRateFail";
        public const string ACTION_TYPE_PAY_RESULT = "payResult";
        public const string ACTION_TYPE_INTERSTITIAL_AD_LOAD = "interstitialAdLoad";
        public const string ACTION_TYPE_INTERSTITIAL_AD_SHOW = "InterstitialAdShow";
        public const string ACTION_TYPE_REWARDED_AD_LOAD = "rewardAdLoad";
        public const string ACTION_TYPE_REWARDED_AD_SHOW = "rewardAdShow";

        private NativeInterface instance;

        private PayResultListener payResultListener = null;
        private LoginListener loginListener = null;
        private RateListener rateListener = null;
        private AdLoadListener interstitialAdLoadListener = null;
        private AdShowListener interstitialAdShowListener = null;
        private AdLoadListener rewardedAdLoadListener = null;
        private AdShowListener rewardedAdShowListener = null;

        public static void init(InitBean initBean)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            #if UNITY_ANDROID
                AndroidJavaObject paras = new AndroidJavaObject("java.util.HashMap");
                paras.Call<string>("put", KEY_ENV, initBean.Env == SHAREitEnv.Test ? ENV_TEST : ENV_PROD);
                paras.Call<string>("put", KEY_CHANNEL, initBean.Channel);
                paras.Call<string>("put", KEY_HOST_CLASS, initBean.MainHostClass);
                paras.Call<string>("put", KEY_IS_EU_AGREED, initBean.IsEUAgreed ? BOOL_TRUE : BOOL_FALSE);
                paras.Call<string>("put", KEY_IS_MAIN_CLASS, initBean.IsMainProcess ? BOOL_TRUE : BOOL_FALSE);

                AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.ushareit.aggregationsdk.SHAREitGameWrapper");
                androidJavaObject.CallStatic("init", CommonUtil.getContext(), paras);
            #endif
        }


        public static bool IsMainThread()
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return false;

#if UNITY_ANDROID
            AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.ushareit.aggregationsdk.SHAREitGameWrapper");
            return androidJavaObject.CallStatic<bool>("isMainThread");
#else
            return false;
#endif
        }

        public static void setEnv(string env)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

#if UNITY_ANDROID

            AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.ushareit.aggregationsdk.SHAREitGameWrapper");
            androidJavaObject.CallStatic("setEnv", env, CommonUtil.getContext());
#endif
        }

        public static void requestStoragePermissions()
        {
#if UNITY_ANDROID
            AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.ushareit.aggregationsdk.SHAREitGameWrapper");
            androidJavaObject.CallStatic("requestStoragePermissions", CommonUtil.getContext());
#endif
        }

        void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        instance = new AndroidNativeInterface(gameObject.name);
#else
            instance = new BaseNativeInterface(gameObject.name);
#endif
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void onHandleShareitSdk(string paras)
        {
            JsonData jsonData = JsonMapper.ToObject(paras);
            Debug.Log("onHandleShareitSdk paras=" + paras);
            string actionType = (string)jsonData["actionType"];
            switch (actionType)
            {
                case ACTION_TYPE_LOGIN_SUCCESS:
                    if(loginListener != null)
                        loginListener.onLoginSuccess((string)jsonData["userId"], (string)jsonData["useName"], (string)jsonData["avatarUrl"]);
                    break;
                case ACTION_TYPE_SHOW_RATE_FAIL:
                    if (rateListener != null)
                        rateListener.onRateShowFail((int)jsonData["resultCode"], (string)jsonData["msg"]);
                    break;
                case ACTION_TYPE_PAY_RESULT:
                    if (payResultListener != null)
                        payResultListener.onResult((int)jsonData["code"], (string)jsonData["orderId"], (string)jsonData["message"], (string)jsonData["extra"]);
                    break;
                case ACTION_TYPE_INTERSTITIAL_AD_LOAD:
                    handleAdLoad(false, jsonData);
                    break;
                case ACTION_TYPE_INTERSTITIAL_AD_SHOW:
                    handleAdShow(false, jsonData);
                    break;
                case ACTION_TYPE_REWARDED_AD_LOAD:
                    handleAdLoad(true, jsonData);
                    break;
                case ACTION_TYPE_REWARDED_AD_SHOW:
                    handleAdShow(true, jsonData);
                    break;

            }
        }

        public void startPayActivity(MerchantParamBean merchantParamBean, PayResultListener payResultListener)
        {
            if (instance == null || merchantParamBean == null)
                return;
            Debug.Log("startPayActivity");
            this.payResultListener = payResultListener;
            instance.startPayActivity(merchantParamBean.getParams());
        }

        //public void gameStart()
        //{
        //    if (instance == null)
        //        return;
        //    instance.gameStart();
        //}

        //public void gameEnd()
        //{
        //    if (instance == null)
        //        return;
        //    instance.gameEnd();
        //}

        public void gameLevelStart(string level)
        {
            if (instance == null)
                return;
            instance.gameLevelStart(level);
        }

        //public void gameLevelEnd(string level)
        //{
        //    if (instance == null)
        //        return;
        //    instance.gameLevelEnd(level);
        //}
        public void gameLevelEnd(string level,bool isPass)
        {
            if (instance == null)
                return;
            instance.gameLevelEnd(level,isPass);
        }

        public void onEvent(string eventId, Dictionary<string, string> dictionary)
        {
            if (instance == null)
                return;
            instance.onEvent(eventId,dictionary);
        }

        public bool isLogin()
        {
            return instance.isLogin();
        }

        public void userLogin(string gameSecret, LoginListener loginListener)
        {
            this.loginListener = loginListener;
            instance.userLogin(gameSecret);
        }

        public void showRateDialog(RateListener rateListener)
        {
            this.rateListener = rateListener;
            instance.showRateDialog();
        }

        public string getUserId()
        {
            return instance.getUserId();
        }

        public bool logout()
        {
            return instance.logout();
        }


        public void loadInterstitialAd(string unitId)
        {
            instance.loadInterstitialAd(unitId);
        }


        public void loadInterstitialAdForShow(string unitId, AdLoadListener adLoadListener)
        {
            interstitialAdLoadListener = adLoadListener;
            instance.loadInterstitialAdForShow(unitId);
        }

        public void showInterstitialAd(string unitId, AdShowListener adShowListener)
        {
            interstitialAdShowListener = adShowListener;
            instance.showInterstitialAd(unitId);
        }

        public bool isInterstitialAdReady(string unitId,string scenePortal)
        {
            return instance.isInterstitialAdReady(unitId,scenePortal);
        }

        public void loadRewardedAd(string unitId)
        {
            instance.loadRewardedAd(unitId);
        }

        public void loadRewardedAdForShow(string unitId, AdLoadListener adLoadListener)
        {
            rewardedAdLoadListener = adLoadListener;
            instance.loadRewardedAdForShow(unitId);
        }

        public void showRewardedAd(string unitId, AdShowListener adShowListener)
        {
            rewardedAdShowListener = adShowListener;
            instance.showRewardedAd(unitId);
        }

        public bool isRewardedAdReady(string unitId)
        {
            return instance.isRewardedAdReady(unitId);
        }

        public bool isRewardedAdReady(string unitId,string scenePortal)
        {
            return instance.isRewardedAdReady(unitId, scenePortal);
        }

        public bool isRewardedAdReady(string unitId, string scenePortal, string subPortal)
        {
            return instance.isRewardedAdReady(unitId, scenePortal, subPortal);
        }

        public void showInterstitialAdByWrapper(string wrapperId, AdShowListener adShowListener)
        {
            interstitialAdShowListener = adShowListener;
            instance.showInterstitialAdByWrapper(wrapperId);
        }

        public void showRewardedAdByWrapper(string wrapperId, AdShowListener adShowListener)
        {
            rewardedAdShowListener = adShowListener;
            instance.showRewardedAdByWrapper(wrapperId);
        }

        public void showRewardedBadgeView(string scenePortal)
        {
            instance.showRewardedBadgeView(scenePortal);
        }

        public void showRewardedBadgeView(string scenePortal, string subPortal)
        {
            instance.showRewardedBadgeView(scenePortal, subPortal);
        }

        private void handleAdLoad(bool isRewardedAd, JsonData jsonData)
        {
            AdLoadListener adLoadListener = isRewardedAd ? rewardedAdLoadListener : interstitialAdLoadListener;

            if (adLoadListener == null)
                return;
            int loadStatus = (int)jsonData["loadStatus"];
            if (loadStatus == AD_LOADED)
                adLoadListener.onAdLoaded((string)jsonData["unitId"], (string)jsonData["adWrapperId"]);
            else
                adLoadListener.onAdError((string)jsonData["unitId"], (int)jsonData["errCode"], (string)jsonData["errMessage"]);
        }

        private void handleAdShow(bool isRewaredAd, JsonData jsonData)
        {
            AdShowListener adShowListener = isRewaredAd ? rewardedAdShowListener : interstitialAdShowListener;
            if (adShowListener == null)
                return;

            int showStatus = (int)jsonData["showStatus"];
            switch(showStatus)
            {
                case AD_SHOW_FAILED:
                    adShowListener.onAdShowFailed((string)jsonData["unitId"], (int)jsonData["errCode"], (string)jsonData["errMessage"]);
                    break;
                case AD_SHOW_IMPRESSION:
                    adShowListener.onAdImpression((string)jsonData["unitId"],  (string)jsonData["adSourceName"]);
                    break;
                case AD_SHOW_CLICKED:
                    adShowListener.onAdClicked((string)jsonData["unitId"], (string)jsonData["adSourceName"]);
                    break;
                case AD_SHOW_REWARDED:
                    adShowListener.onAdRewarded((string)jsonData["unitId"], (string)jsonData["adSourceName"]);
                    break;
                case AD_SHOW_CLOSED:
                    adShowListener.onAdClosed((string)jsonData["unitId"], (string)jsonData["adSourceName"], (bool)jsonData["hasRewarded"]);
                    break;
            }
        }

        public void preloadBannerAd(string unitId)
        {
            instance.preloadBannerAd(unitId);
        }

        public void showBanner(string unitId, int gravity)
        {
            instance.showBanner(unitId, gravity);
        }
        //public void showBanner(string unitId, Activity activity, int gravity)
        //{
        //    instance.showBanner(unitId,activity,gravity);
        //}

        //public void showBanner(string unitId, ViewGroup container)
        //{
        //    instance.showBanner(unitId, container);
        //}

        public void hiddenBannerAd()
        {
            instance.hiddenBannerAd();
        }
    }

}
