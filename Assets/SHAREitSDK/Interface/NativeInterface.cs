using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHAREitSDK
{
    public interface NativeInterface
    {
        //payment start
        void purchase(Dictionary<string, string> dictionary);

        void queryProducts(Dictionary<string, string> dictionary, string[] productIds);

        void launchBillingFlow(Dictionary<string, string> dictionary);

        void queryPurchases(Dictionary<string, string> dictionary);

        void consume(Dictionary<string, string> dictionary);
        //payment end

        //void gameStart();

        //void gameEnd();

        void gameLevelStart(string level);

        //void gameLevelEnd(string level);

        void gameLevelEnd(string level,bool isPass);

        void onEvent(string eventId, Dictionary<string, string> dictionary);

        bool isLogin();

        void userLogin(string gameSecret);

        void showRateDialog();

        string getUserId();

        bool logout();

        void loadInterstitialAd(string unitId);

        void loadInterstitialAdForShow(string unitId);

        void showInterstitialAd(string unitId);

        void showInterstitialAdByWrapper(string wrapperId);

        bool isInterstitialAdReady(string unitId, string scenePortal);

        void loadRewardedAd(string unitId);

        void loadRewardedAdForShow(string unitId);

        void showRewardedAd(string unitId);

        void showRewardedAdByWrapper(string wrapperId);

        bool isRewardedAdReady(string unitId, string scenePortal);

        bool isRewardedAdReady(string unitId, string scenePortal, string subPortal);

        bool isRewardedAdReady(string unitId);

        void showRewardedBadgeView(string scenePortal);

        void showRewardedBadgeView(string scenePortal, string subPortal);

        void preloadBannerAd(string unitId);

        void showBanner(string unitId, int gravity);

        //void showBanner(string unitId, Activity activity, int gravity);

        //void showBanner(string unitId, ViewGroup container);

        void hiddenBannerAd();

        void showVideoDialog(int x, int y, string scene, bool isMute);

        void hideVideoDialog();
    }
}
