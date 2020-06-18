using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
namespace SHAREitSDK
{
    public class AndroidNativeInterface : BaseNativeInterface
    {
        private const string TAG = "AndroidNativeInterface";

        private AndroidJavaObject sdkWrapper = null;

        public AndroidNativeInterface(string gameObjectName):base(gameObjectName)
        {
            sdkWrapper = new AndroidJavaObject("com.ushareit.aggregationsdk.SHAREitGameWrapper", gameObjectName);
        }

        public override void startPayActivity(Dictionary<string, string> dictionary) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            Debug.Log(TAG + " startPayActivity");
            AndroidJavaObject paras = CommonUtil.dicToMap(dictionary);
            sdkWrapper.Call("startPayActivity", getContext(), paras);
        }

        //public override void gameStart() {
        //    if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
        //        return;

        //    sdkWrapper.Call("gameStart");
        //}

        //public override void gameEnd() {
        //    if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
        //        return;

        //    sdkWrapper.Call("gameEnd");
        //}

        public override void gameLevelStart(string level) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("gameLevelStart",level);
        }

        //public override void gameLevelEnd(string level) {
        //    if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
        //        return;

        //    sdkWrapper.Call("gameLevelEnd",level);
        //}

        public override void gameLevelEnd(string level,bool isPass)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("gameLevelEnd", level, isPass);
        }

        public override void onEvent(string eventId, Dictionary<string, string> dictionary) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            AndroidJavaObject paras = CommonUtil.dicToMap(dictionary);
            sdkWrapper.Call("onEvent", getContext(), eventId, paras);
        }

        public override bool isLogin()
        {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return false;

            return sdkWrapper.Call<bool>("isLogin");
        }

        public override void userLogin(string gameSecret) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("userLogin", getContext(), gameSecret);
        }

        public override void showRateDialog()
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("showRateDialog", getContext());
        }

        public override string getUserId() {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return null;
            return sdkWrapper.Call<string>("getUserId");
        }

        public override bool logout()
        {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return false;

            return sdkWrapper.Call<bool>("logout");
        }

        public override void loadInterstitialAd(string unitId) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("loadInterstitialAd", unitId);
        }

        public override void loadInterstitialAdForShow(string unitId)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("loadInterstitialAdForShow", unitId);
        }


        public override void showInterstitialAd(string unitId) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("showInterstitialAd", unitId);
        }

        public override bool isInterstitialAdReady(string unitId, string scenePortal)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return false;

            return sdkWrapper.Call<bool>("isInterstitialAdReady", unitId, scenePortal);
        }

        public override void loadRewardedAd(string unitId) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("loadRewardedAd", unitId);
        }

        public override void loadRewardedAdForShow(string unitId)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("loadRewardedAdForShow", unitId);
        }

        public override void showRewardedAd(string unitId) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("showRewardedAd", unitId);
        }

        public override bool isRewardedAdReady(string unitId)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return false;

            return sdkWrapper.Call<bool>("isRewardedAdReady", unitId);
        }

        public override bool isRewardedAdReady(string unitId, string scenePortal) {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return false;

            return sdkWrapper.Call<bool>("isRewardedAdReady", unitId, scenePortal);
        }

        public override bool isRewardedAdReady(string unitId, string scenePortal, string subPortal)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return false;

            return sdkWrapper.Call<bool>("isRewardedAdReady", unitId, scenePortal, subPortal);
        }

        private static AndroidJavaObject getContext()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return jc.GetStatic<AndroidJavaObject>("currentActivity");
        }

        public override void showInterstitialAdByWrapper(string wrapperId)
        {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("showInterstitialAdByWrapper", wrapperId);
        }

        public override void showRewardedAdByWrapper(string wrapperId)
        {
            if ( CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;

            sdkWrapper.Call("showRewardedAdByWrapper", wrapperId);
        }

        public override void showRewardedBadgeView(string scenePortal)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;
            sdkWrapper.Call("showRewardedBadgeView", scenePortal);
            base.showRewardedBadgeView(scenePortal);
        }

        public override void showRewardedBadgeView(string scenePortal, string subPortal)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;
            sdkWrapper.Call("showRewardedBadgeView", scenePortal, subPortal);
            base.showRewardedBadgeView(scenePortal);
        }

        public override void preloadBannerAd(string unitId)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;
            sdkWrapper.Call("preloadBannerAd", unitId);
        }

        public override void showBanner(string unitId, int gravity)
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;
            sdkWrapper.Call("showBanner", unitId, getContext(), gravity);
        }

        //public override void showBanner(string unitId, Activity activity, int gravity)
        //{
        //    if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
        //        return;

        //    sdkWrapper.Call("showBanner", unitId,activity,gravity);
        //}

        //public override void showBanner(string unitId, ViewGroup container)
        //{
        //    if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
        //        return;

        //    sdkWrapper.Call("showBanner", unitId,container);
        //}

        public override void hiddenBannerAd()
        {
            if (CommonUtil.IsInvalidRuntime(RuntimePlatform.Android))
                return;
            sdkWrapper.Call("hiddenBannerAd");
        }
    }
}
#endif
