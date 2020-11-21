using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QueryPurchaseParamBean
{

    private const string KEY_MERCHANT_ID = "merchantId"; //商户Id
    private const string KEY_TOKEN = "token";
    private const string KEY_USERR_ID = "userId";

    private Dictionary<string, string> paraDic = new Dictionary<string, string>();

    public QueryPurchaseParamBean(Dictionary<string, string> dic)
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

        public Builder setUserId(string userId)
        {
            paraMap.Add(KEY_USERR_ID, userId);
            return this;
        }

        public QueryPurchaseParamBean build()
        {
            return new QueryPurchaseParamBean(paraMap);
        }
    }
}
