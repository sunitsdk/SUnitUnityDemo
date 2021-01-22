using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MerchantParamBean
{

    public const string PAY_RESULT_TYPE_SDK = "1";
    public const string PAY_RESULT_TYPE_MERCHANT = "0";

    protected const string KEY_MERCHANT_ID = "merchantId"; //商户Id
    protected const string KEY_ORDER_ID = "orderId"; //商户订单Id
    protected const string KEY_PRICE = "price"; //总金额
    protected const string KEY_CURRENCY = "currency"; //货币代码

    protected const string KEY_CALLBACK_URL = "callbackUrl"; //商户回调地址
    protected const string KEY_USER_ID = "custId"; //商户用户id
    protected const string KEY_SUBJECT = "subject"; //订单标题
    protected const string KEY_TOKEN = "token";
    protected const string KEY_COUNTRY_CODE = "countryCode"; //2位大写国家码，参考：ISO 3166-1 alpha-2
    protected const string KEY_PRODUCT_CODE = "productCode"; //IAP商品Id（not merchant product id)

    //可选字段
    protected const string KEY_LANGUAGE = "language"; //当前语言
    protected const string KEY_DESCRIPTION = "description"; //商户自定义信息
    protected const string KEY_EXTRA = "extra"; //商户自定义数据

    //使用支付结果页类型, "0"：使用SDK结果页；"1"：使用商户结果页
    protected const string KEY_RESULT_TYPE = "usePayResultType";
    //支付有效时长，单位: 秒
    protected const string KEY_EXPIRE_TIME = "payValidDuration";
    protected const string KEY_BILLING_DETAIL = "paymentDetail";
    protected const string KEY_USER_DETAIL = "userDetail";
    protected const string KEY_BIZ_TYPE = "bizType";
    protected const string KEY_ORIGINAL_CURRENCY = "originalCurrency";
    protected const string KEY_ORIGINAL_PRICE = "originalPrice";


    private Dictionary<string, string> paraDic = new Dictionary<string, string>();

    public MerchantParamBean(Dictionary<string, string> dic)
    {
        foreach (KeyValuePair<string, string> pair in dic)
        {
            paraDic.Add(pair.Key, pair.Value);
        }
    }

    public Dictionary<string, string> getParams()
    {
        return paraDic;
    }

    public class Builder : BuilderInner<MerchantParamBean.Builder>
    {
        public Builder setProductDetail(ProductDetailBean detail)
        {
            if (detail != null)
            {
                paraMap[KEY_PRICE] = detail.price;
                paraMap[KEY_CURRENCY] = detail.currency;
                paraMap[KEY_SUBJECT] = detail.productName;
                paraMap[KEY_DESCRIPTION] = detail.productDesc;
                paraMap[KEY_PRODUCT_CODE] = detail.productCode;
                paraMap[KEY_COUNTRY_CODE] = detail.country;
                if (string.IsNullOrEmpty(detail.originalPrice))
                    paraMap[KEY_ORIGINAL_PRICE] = detail.price;
                else
                    paraMap[KEY_ORIGINAL_PRICE] = detail.originalPrice;
                if (string.IsNullOrEmpty(detail.originalCurrency))
                    paraMap[KEY_ORIGINAL_CURRENCY] = detail.currency;
                else
                    paraMap[KEY_ORIGINAL_CURRENCY] = detail.originalCurrency;
            }
            return this;
        }
    }

    public class BuilderInner<T> where T : BuilderInner<T>
    {
        protected Dictionary<string, string> paraMap = new Dictionary<string, string>();

        public T addParams(Dictionary<string, string> paramDic)
        {
            if (paramDic != null)
            {
                foreach (KeyValuePair<string, string> kv in paramDic)
                {
                    paraMap.Add(kv.Key, kv.Value);
                }
            }
            return (T)this;
        }

        public T setMerchantId(string merchantId)
        {
            paraMap.Add(KEY_MERCHANT_ID, merchantId);
            return (T)this;
        }

        public T setToken(string token)
        {
            paraMap.Add(KEY_TOKEN, token);
            return (T)this;
        }

        public T setCountryCode(string countryCode)
        {
            paraMap.Add(KEY_COUNTRY_CODE, countryCode);
            return (T)this;
        }

        public T setOrderId(string orderId)
        {
            paraMap.Add(KEY_ORDER_ID, orderId);
            return (T)this;
        }

        public T setCallbackUrl(string callbackUrl)
        {
            paraMap.Add(KEY_CALLBACK_URL, callbackUrl);
            return (T)this;
        }

        public T setUserId(string userId)
        {
            paraMap.Add(KEY_USER_ID, userId);
            return (T)this;
        }

        public T setReference(string reference)
        {
            paraMap.Add(KEY_EXTRA, reference);
            return (T)this;
        }

        public T setLanguage(string language)
        {
            paraMap.Add(KEY_LANGUAGE, language);
            return (T)this;
        }

        public T setShowResult(string resultType)
        {
            paraMap.Add(KEY_RESULT_TYPE, resultType);
            return (T)this;
        }

        // unit: s
        public T setExpireTime(long duration)
        {
            paraMap.Add(KEY_EXPIRE_TIME, duration + "");
            return (T)this;
        }

        public T setBillingDetail(string billingDetail)
        {
            paraMap.Add(KEY_BILLING_DETAIL, billingDetail);
            return (T)this;
        }

        public T setUserDetail(string userDetail)
        {
            paraMap.Add(KEY_USER_DETAIL, userDetail);
            return (T)this;
        }

        public T setBizType(string bizType)
        {
            paraMap.Add(KEY_BIZ_TYPE, bizType);
            return (T)this;
        }

        public MerchantParamBean build()
        {
            return new MerchantParamBean(paraMap);
        }
    }
}

public class WoSkuMerchantParamBean : MerchantParamBean
{
    public WoSkuMerchantParamBean(Dictionary<string, string> dic) : base(dic)
    {
    }

    new public class Builder : BuilderInner<WoSkuMerchantParamBean.Builder>
    {
        public Builder setProductName(string productName)
        {
            paraMap.Add(KEY_SUBJECT, productName);
            return this;
        }

        public Builder setCurrency(string currency)
        {
            paraMap.Add(KEY_CURRENCY, currency);
            return this;
        }

        public Builder setPrice(string price)
        {
            paraMap.Add(KEY_PRICE, price);
            return this;
        }

        new public WoSkuMerchantParamBean build()
        {
            return new WoSkuMerchantParamBean(paraMap);
        }

    }
}