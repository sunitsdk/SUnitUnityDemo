using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Advertisement : MonoBehaviour
{
    public Text descInput;

    public Button interstitalShowBtn;
    public Button interstitalLoadAndShowBtn;

    public Button showRewardedBadgeViewBtn;
    public Button rewardedShowBtn;
    public Button rewardedLoadAndShowBtn;

    public Button showBannerBtn;
    public Button hiddenBannerAdBtn;

    public SHAREitSDK.SHAREitSDK shareitSDK;

    private const string LAYER_ITL_AD = "adcolonyitl";
    private const string LAYER_RWD_AD = "adcolonyrwd";
    private const string SCREEN_PORTAL = "AdUnityDemo";
    private const string SUB_PORTAL = "subPortal";

    private const string INTERSTITIAL_UNIT_ID = "1014yOdwNy";
    private const string REWARD_UNIT_ID = "1014yOdeiW";
    private const string BANNER_UNIT_ID = "1014yOdnGC";

    // Start is called before the first frame update
    void Start()
    {
        shareitSDK.loadInterstitialAd(INTERSTITIAL_UNIT_ID);
        interstitalShowBtn.onClick.AddListener(onInterstitalShow);

        shareitSDK.loadRewardedAd(REWARD_UNIT_ID);
        rewardedShowBtn.onClick.AddListener(onRewardedShow);
        interstitalLoadAndShowBtn.onClick.AddListener(onInterstitalLoadAndShow);
        rewardedLoadAndShowBtn.onClick.AddListener(onRewardedLoadAndShow);
        showRewardedBadgeViewBtn.onClick.AddListener(showRewardedBadgeViewClick);
     
        shareitSDK.preloadBannerAd(BANNER_UNIT_ID);
        showBannerBtn.onClick.AddListener(showBannerClick);
        hiddenBannerAdBtn.onClick.AddListener(hiddenBannerAdClick);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainScene");
    }


    public void onInterstitalLoadAndShow()
    {
        shareitSDK.loadInterstitialAdForShow(INTERSTITIAL_UNIT_ID, new SHAREitSDK.AdLoadListener((unitId, adWrapperId) =>
            {
                descInput.text = "Interstitial onAdLoaded and start show unitId=" + unitId + " adWrapperId = " + adWrapperId;
                showInterstitialAdByWrapper(adWrapperId);
            },
            (unitId, errCode, errMessage) =>
            {
                descInput.text = "Interstitial onAdError unitId=" + unitId + " errCode=" + errCode + " errMessage=" + errMessage;
            }));
    }


    public void onInterstitalShow()
    {
        //The param "scene" is decided by your game scene,it's just a sample value here
        string scene = SHAREitSDK.BaseNativeInterface.GAME_LEVEL_START;
        //string scene = SHAREitSDK.BaseNativeInterface.GAME_LEVEL_END;
        //string scene = SHAREitSDK.BaseNativeInterface.GAME_REGAIN_FOCUS;
        //string scene = SHAREitSDK.BaseNativeInterface.LOOP;
        if (shareitSDK.isInterstitialAdReady(INTERSTITIAL_UNIT_ID, scene))
        {
            shareitSDK.showInterstitialAd(INTERSTITIAL_UNIT_ID, new SHAREitSDK.AdShowListener(
            (string unitId, int errCode, string errMessage) =>
            {
                descInput.text = "Interstitial onAdShowFailed unitId=" + unitId + " errCode=" + errCode + " errMessage=" + errMessage;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Interstitial onAdImpression unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Interstitial onAdClicked unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Interstitial onAdRewarded unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName, bool hasRewarded) =>
            {
                descInput.text = "Interstitial onAdClosed unitId=" + unitId + " adSourceName=" + adSourceName + " hasRewarded=" + hasRewarded;
            }));
        }
        else
        {
            descInput.text = "showInterstitial !isAdReady";
        }
    }


    public void onRewardedLoadAndShow()
    {
        shareitSDK.loadRewardedAdForShow(REWARD_UNIT_ID, new SHAREitSDK.AdLoadListener((unitId, adWrapperId) =>
        {
            descInput.text = "Rewarded onAdLoaded and start show unitId=" + unitId + " adWrapperId = " + adWrapperId;
            showRewardedAdByWrapper(adWrapperId);
        },
            (unitId, errCode, errMessage) =>
            {
                descInput.text = "Rewarded onAdError unitId=" + unitId + " errCode=" + errCode + " errMessage=" + errMessage;
            }));
    }

    public void onRewardedShow()
    {
        //The param "scene" is decided by your game scene,it's just a sample value here
        string scene = SHAREitSDK.BaseNativeInterface.LOOP;
        if (shareitSDK.isRewardedAdReady(REWARD_UNIT_ID, scene))
        {
            shareitSDK.showRewardedAd(REWARD_UNIT_ID, new SHAREitSDK.AdShowListener(
            (string unitId, int errCode, string errMessage) =>
            {
                descInput.text = "Rewarded onAdShowFailed unitId=" + unitId + " errCode=" + errCode + " errMessage=" + errMessage;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Rewarded onAdImpression unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Rewarded onAdClicked unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Rewarded onAdRewarded unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName, bool hasRewarded) =>
            {
                descInput.text = "Rewarded onAdClosed unitId=" + unitId + " adSourceName=" + adSourceName + " hasRewarded=" + hasRewarded;
            }));
        }
        else
        {
            descInput.text = "showReward !isAdReady";
        }

    }

    private void showInterstitialAdByWrapper(string adWrapperId)
    {
        //The param "scene" is decided by your game scene,it's just a sample value here
        string scene = SHAREitSDK.BaseNativeInterface.GAME_LEVEL_START;
        //string scene = SHAREitSDK.BaseNativeInterface.GAME_LEVEL_END;
        //string scene = SHAREitSDK.BaseNativeInterface.GAME_REGAIN_FOCUS;
        //string scene = SHAREitSDK.BaseNativeInterface.LOOP;
        if (shareitSDK.isInterstitialAdReady(INTERSTITIAL_UNIT_ID, scene))
        {
            shareitSDK.showInterstitialAdByWrapper(adWrapperId, new SHAREitSDK.AdShowListener(
            (string unitId, int errCode, string errMessage) =>
            {
                descInput.text = "Interstitial AdWrapper onAdShowFailed unitId=" + unitId + " errCode=" + errCode + " errMessage=" + errMessage;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Interstitial AdWrapper onAdImpression unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Interstitial AdWrapper onAdClicked unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Interstitial AdWrapper onAdRewarded unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName, bool hasRewarded) =>
            {
                descInput.text = "Interstitial AdWrapper onAdClosed unitId=" + unitId + " adSourceName=" + adSourceName + " hasRewarded=" + hasRewarded;
            }));
        }
        else
        {
            descInput.text = "showInterstitial AdWrapper !isAdReady";
        }
    }

    private void showRewardedAdByWrapper(string adWrapperId)
    {
        if (shareitSDK.isRewardedAdReady(REWARD_UNIT_ID, SCREEN_PORTAL, SUB_PORTAL))
        {
            shareitSDK.showRewardedAdByWrapper(adWrapperId, new SHAREitSDK.AdShowListener(
            (string unitId, int errCode, string errMessage) =>
            {
                descInput.text = "Rewarded AdWrapper onAdShowFailed unitId=" + unitId + " errCode=" + errCode + " errMessage=" + errMessage;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Rewarded AdWrapper onAdImpression unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Rewarded AdWrapper onAdClicked unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName) =>
            {
                descInput.text = "Rewarded AdWrapper onAdRewarded unitId=" + unitId + " adSourceName=" + adSourceName;
            }, (string unitId, string adSourceName, bool hasRewarded) =>
            {
                descInput.text = "Rewarded AdWrapper onAdClosed unitId=" + unitId + " adSourceName=" + adSourceName + " hasRewarded=" + hasRewarded;
            }));
        }
        else
        {
            descInput.text = "showReward AdWrapper !isAdReady";
        }
    }

    public void showRewardedBadgeViewClick()
    {
        descInput.text = "Reward badge view click";
        shareitSDK.showRewardedBadgeView(SCREEN_PORTAL, SUB_PORTAL);
    }

    public void showBannerClick()
    {
        descInput.text = "Banner ad start show and unitId=" + BANNER_UNIT_ID;
        //shareitSDK.showBanner(BANNER_UNIT_ID, SHAREitSDK.BaseNativeInterface.GRAVITY_TOP);
        shareitSDK.showBanner(BANNER_UNIT_ID, SHAREitSDK.BaseNativeInterface.GRAVITY_BOTTOM);
    }

    public void hiddenBannerAdClick()
    {
        descInput.text = "Banner ad hide and unitId=" + BANNER_UNIT_ID;
        shareitSDK.hiddenBannerAd();
    }
}
