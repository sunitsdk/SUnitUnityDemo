using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SHAREitSDK
{
    //payment end
    public class PaymentListener
    {
        public delegate void OnProductResponseCallback(int code, string message, List<ProductDetailBean> productDetailList);
        public delegate void OnPurchaseResponseCallback(int code, string merchantOrderNo, string message, string reference);
        public delegate void OnQueryPurchaseResponseCallback(int code, string message, List<QueryDetailBean> productDetailList);
        public delegate void OnConsumeResponseCallback(int code, string message);
    }
    //payment end

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
