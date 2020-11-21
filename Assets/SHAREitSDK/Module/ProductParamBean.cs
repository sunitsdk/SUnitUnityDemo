using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductParamBean
{

    private const string KEY_MERCHANT_ID = "merchantId"; //商户Id
    private const string KEY_TOKEN = "token";
    private const string KEY_COUNTRY_CODE = "countryCode"; //2位大写国家码，参考：ISO 3166-1 alpha-2

    private Dictionary<string, string> paraDic = new Dictionary<string, string>();

    public ProductParamBean(Dictionary<string, string> dic)
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
        public Builder setMerchantId(string merchantId)
        {
            paraMap.Add(KEY_MERCHANT_ID, merchantId);
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

        public ProductParamBean build()
        {
            return new ProductParamBean(paraMap);
        }
    }
}
