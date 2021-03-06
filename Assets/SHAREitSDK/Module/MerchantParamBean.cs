﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MerchantParamBean
{

    public const string RESULT_TYPE_SDK = "1";
    public const string RESULT_TYPE_MERCHANT = "0";

    public const string BIZ_TYPE = "STANDARD";

    private const string KEY_MERCHANT_ID = "merchantId"; //商户Id
    private const string KEY_ORDER_ID = "orderId"; //商户订单Id
    private const string KEY_TOTAL_AMOUNT = "totalAmount"; //总金额
    private const string KEY_CURRENCY = "currency"; //货币代码

    private const string KEY_CALLBACK_URL = "callbackUrl"; //商户回调地址
    private const string KEY_USER_ID = "custId"; //商户用户id
    private const string KEY_SUBJECT = "subject"; //订单标题
    private const string KEY_TOKEN = "token";
    private const string KEY_COUNTRY_CODE = "countryCode"; //2位大写国家码，参考：ISO 3166-1 alpha-2

    //可选字段
    private const string KEY_LANGUAGE = "language"; //当前语言

    private const string KEY_DESCRIPTION = "description"; //商户自定义信息
    private const string KEY_EXTRA = "extra"; //商户自定义数据
    //使用支付结果页类型, "0"：使用SDK结果页；"1"：使用商户结果页
    private const string KEY_RESULT_TYPE = "usePayResultType";
    //支付有效时长，单位: 秒
    private const string KEY_EXPIRE_TIME = "payValidDuration";
    protected const string KEY_BILLING_DETAIL = "paymentDetail";
    protected const string KEY_USER_DETAIL = "userDetail";
    protected const string KEY_BIZ_TYPE = "bizType";


    private Dictionary<string, string> paraDic = new Dictionary<string, string>();

    public MerchantParamBean(Dictionary<string, string> dic)
    {
        foreach(KeyValuePair<string, string> pair in dic)
        {
            paraDic.Add(pair.Key, pair.Value);
        }
    }

    public Dictionary<string, string> getParams()
    {
        return paraDic;
    }

    public class Builder
    {
        private Dictionary<string, string> paraMap = new Dictionary<string, string>();

        public Builder setBizType(string bizType)
        {
            paraMap.Add(KEY_BIZ_TYPE, bizType);
            return this;
        }

        public Builder setMerchantId(string merchantId)
        {
            paraMap.Add(KEY_MERCHANT_ID, merchantId);
            return this;
        }

        public Builder setOrderId(string orderId)
        {
            paraMap.Add(KEY_ORDER_ID, orderId);
            return this;
        }

        public Builder setAmount(string totalAmount)
        {
            paraMap.Add(KEY_TOTAL_AMOUNT, totalAmount);
            return this;
        }

        public Builder setCurrency(string currentcy)
        {
            paraMap.Add(KEY_CURRENCY, currentcy);
            return this;
        }

        public Builder setCallbackUrl(string callbackUrl)
        {
            paraMap.Add(KEY_CALLBACK_URL, callbackUrl);
            return this;
        }

        public Builder setSubject(string subject)
        {
            paraMap.Add(KEY_SUBJECT, subject);
            return this;
        }

        public Builder setCustId(string custId)
        {
            paraMap.Add(KEY_USER_ID, custId);
            return this;
        }

        public Builder setDescription(string description)
        {
            paraMap.Add(KEY_DESCRIPTION, description);
            return this;
        }

        public Builder setReference(string extra)
        {
            paraMap.Add(KEY_EXTRA, extra);
            return this;
        }

        public Builder setToken(string token)
        {
            paraMap.Add(KEY_TOKEN, token);
            return this;
        }

        public Builder setCountryCode(string countryCode)
        {
            paraMap.Add(KEY_COUNTRY_CODE, countryCode);
            return this;
        }

        public Builder setLanguage(string language)
        {
            paraMap.Add(KEY_LANGUAGE, language);
            return this;
        }

        public Builder setResultPageShowType(string payResultType)
        {
            paraMap.Add(KEY_RESULT_TYPE, payResultType);
            return this;
        }

        // unit: s
        public Builder setExpireTime(long duration)
        {
            paraMap.Add(KEY_EXPIRE_TIME, duration + "");
            return this;
        }

        public Builder setBillingDetail(string billingDetail)
        {
            paraMap.Add(KEY_BILLING_DETAIL, billingDetail);
            return this;
        }

        public Builder setUserDetail(string userDetail)
        {
            paraMap.Add(KEY_USER_DETAIL, userDetail);
            return this;
        }

        public MerchantParamBean build()
        {
            return new MerchantParamBean(paraMap);
        }
    }
}
