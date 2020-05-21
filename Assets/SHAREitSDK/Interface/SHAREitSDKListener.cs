using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHAREitSDK
{

    public class PayResultListener
    {
        public delegate void OnResult(int payCode, string payOrderId, string payMessage, string payExtra);
        public OnResult onResult;

        public PayResultListener(OnResult onResult )
        {
            this.onResult = onResult;
        }
    }

    public class RateListener
    {
        public delegate void OnRateShowFail(int showRateResultCode, string msg);
        public OnRateShowFail onRateShowFail;
        public RateListener(OnRateShowFail onRateShowFail)
        {
            this.onRateShowFail = onRateShowFail;
        }
    }

    public class LoginListener
    {
        public delegate void OnLoginSuccess(string userId, string username, string avatarUrl);
        public OnLoginSuccess onLoginSuccess;
        public LoginListener(OnLoginSuccess onLoginSuccess)
        {
            this.onLoginSuccess = onLoginSuccess;
        }
    }

    public class AdLoadListener
    {
        public delegate void OnAdLoaded(string unitId, string adWrapperId);
        public delegate void OnAdError(string unitId, int errCode, string errMessage);

        public OnAdLoaded onAdLoaded;
        public OnAdError onAdError;

        public AdLoadListener(OnAdLoaded onAdLoaded, OnAdError onAdError)
        {
            this.onAdLoaded = onAdLoaded;
            this.onAdError = onAdError;
        }
    }

    public class AdShowListener
    {
        public delegate void OnAdShowFailed(string unitId, int errCode, string errMessage);
        public delegate void OnAdImpression(string unitId, string adSourceName);
        public delegate void OnAdClicked(string unitId, string adSourceName);
        public delegate void OnAdRewarded(string unitId, string adSourceName);
        public delegate void OnAdClosed(string unitId, string adSourceName, bool hasRewarded);

        public OnAdShowFailed onAdShowFailed;
        public OnAdImpression onAdImpression;
        public OnAdClicked onAdClicked;
        public OnAdRewarded onAdRewarded;
        public OnAdClosed onAdClosed;

        public AdShowListener(OnAdShowFailed onAdShowFailed, OnAdImpression onAdImpression, OnAdClicked onAdClicked, OnAdRewarded onAdRewarded, OnAdClosed onAdClosed)
        {
            this.onAdShowFailed = onAdShowFailed;
            this.onAdImpression = onAdImpression;
            this.onAdClicked = onAdClicked;
            this.onAdRewarded = onAdRewarded;
            this.onAdClosed = onAdClosed;
        }


    }
}
