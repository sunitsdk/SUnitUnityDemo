using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

public class TokenHelper : MonoBehaviour
{
    private static TokenHelper instance;
    public static TokenHelper Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject NetworkManager = new GameObject("TokenHelper");
                instance = NetworkManager.AddComponent<TokenHelper>();
            }
            return instance;
        }
    }

    private String getTokenUrl(string env)
    {
        String testUrl = "https://pay-gate-uat.shareitpay.in/aggregate-pay-gate/api/gateway";
        String prodUrl = "https://pay-gate.shareitpay.in/aggregate-pay-gate/api/gateway";

        String url = "";
        switch (env)
        {
            case SHAREitSDK.SHAREitSDK.ENV_TEST:
                url = testUrl;
                break;
            case SHAREitSDK.SHAREitSDK.ENV_PROD:
                url = prodUrl;
                break;
        }
        return url;
    }

    public IEnumerator sendToken(string env, string merchantId, string secretKey, Action<int, string> actionResult)
    {
        Dictionary<string, string> paras = new Dictionary<string, string>();
        paras.Add("bizType", "token");
        paras.Add("merchantId", merchantId);
        paras.Add("timestamp", SHAREitSDK.CommonUtil.GetTimeStamp().ToString());
        paras.Add("version", "1.0");
        paras.Add("secretKey", secretKey);
        string url = getTokenUrl(env);

        Debug.Log("sendToken: merchantId" + merchantId + " url=" + url + " env=" + env);

        UnityWebRequest request = UnityWebRequest.Post(url, paras);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        yield return request.Send();
        if (request.isDone)
        {
            string responseData = request.downloadHandler.text;
            JsonData jsonData = JsonMapper.ToObject(responseData);
            Debug.Log("response: " + responseData);
            if ("0000".Equals((string)jsonData["bizCode"]))
            {
                if(actionResult != null)
                    actionResult.Invoke(1, (string)jsonData["data"]);
            }
            else
                actionResult.Invoke(0, "bizCode invalid.  " + jsonData["bizCode"]);

        }
        else
            actionResult.Invoke(0, "network error");
    }
}
