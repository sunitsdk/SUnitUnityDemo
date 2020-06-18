using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHAREitSDK
{
    public class BaseNativeInterface : NativeInterface
    {
        public const int GRAVITY_TOP = 1;
        public const int GRAVITY_BOTTOM = 2;

        public const string LOOP = "loop";
        public const string GAME_LEVEL_START = "itl_game_level_start";
        public const string GAME_LEVEL_END = "itl_game_level_end";
        public const string GAME_REGAIN_FOCUS = "itl_game_regain_focus";
        public const string HOME = "home";
        public const string SHOP = "shop";
        public const string POPUP = "pupup";

        protected string gameObjectName;

        public BaseNativeInterface(string gameObjectName)
        {
            this.gameObjectName = gameObjectName;
        }

        public static void init(Dictionary<string, string> dictionary) { }

        public virtual void startPayActivity(Dictionary<string, string> dictionary) { }

        //public virtual void gameStart() { }

        //public virtual void gameEnd() { }

        public virtual void gameLevelStart(string level){ }

        //public virtual void gameLevelEnd(string level) { }
        public virtual void gameLevelEnd(string level,bool isPass) { }

        public virtual void onEvent(string eventId, Dictionary<string, string> dictionary) { }

        public virtual bool isLogin() { return false; }

        public virtual void userLogin(string gameSecret) { }

        public virtual void showRateDialog() { }

        public virtual string getUserId() { return ""; }

        public virtual bool logout() { return true; }

        public virtual void loadInterstitialAd(string unitId) { }

        public virtual void loadInterstitialAdForShow(string unitId) { }

        public virtual void showInterstitialAd(string unitId) { }

        public virtual bool isInterstitialAdReady(string unitId,string scenePortal) { return false; }

        public virtual void loadRewardedAd(string unitId) { }

        public virtual void loadRewardedAdForShow(string unitId) { }

        public virtual void showRewardedAd(string unitId) { }

        public virtual bool isRewardedAdReady(string unitId) { return false; }

        public virtual bool isRewardedAdReady(string unitId,string scenePortal) { return false; }

        public virtual bool isRewardedAdReady(string unitId, string scenePortal,string subPortal) { return false; }

        public virtual void showInterstitialAdByWrapper(string wrapperId)
        {
        }

        public virtual void showRewardedAdByWrapper(string wrapperId)
        {
        }

        public virtual void showRewardedBadgeView(string scenePortal)
        {
        }

        public virtual void showRewardedBadgeView(string scenePortal, string subPortal)
        {
        }

        public virtual void preloadBannerAd(string unitId)
        {
        }


        public virtual void showBanner(string unitId, int gravity) { }

        //public virtual void showBanner(string unitId, Activity activity, int gravity)
        //{
        //}

        //public virtual void showBanner(string unitId, ViewGroup container)
        //{
        //}

        public virtual void hiddenBannerAd()
        {
        }
    }
}
