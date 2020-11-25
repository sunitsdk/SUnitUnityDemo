using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button paymentBtn;
    public Button adBtn;
    public Button gameBtn;
    public Dropdown envDropdown;
    private const string TAG = "Main";

    public const string ENV_STORE_KEY = "shareitEnv";
    public SHAREitSDK.SHAREitSDK shareitSDK;

    private string[] envArray = new string[] { SHAREitSDK.SHAREitSDK.ENV_TEST, SHAREitSDK.SHAREitSDK.ENV_PROD };
    void Awake()
    {


        string storeEnv = PlayerPrefs.GetString(ENV_STORE_KEY);
        if (string.IsNullOrEmpty(storeEnv))
        {
            storeEnv = SHAREitSDK.SHAREitSDK.ENV_TEST;
            PlayerPrefs.SetString(ENV_STORE_KEY, SHAREitSDK.SHAREitSDK.ENV_TEST);  
        }
        SHAREitSDK.SHAREitSDK.setEnv(storeEnv);

        envDropdown.value = Array.IndexOf(envArray, storeEnv);

        Debug.Log(TAG + " storeEnv " + storeEnv);

        envDropdown.onValueChanged.AddListener((int arg0) => {
            Debug.Log(TAG + " select env: index=" + arg0);
            string env;
            if (arg0 == 0)
                env = SHAREitSDK.SHAREitSDK.ENV_TEST;
            else
                env = SHAREitSDK.SHAREitSDK.ENV_PROD;
            PlayerPrefs.SetString(ENV_STORE_KEY, env);
            SHAREitSDK.SHAREitSDK.setEnv(env);
        }); 
        Debug.Log(TAG + " unity SHAREitSDK init");

        StartCoroutine(checkNonConsumableProducts());
        //checkNonConsumableProducts();
        //InitBean bean = new InitBean();
        //bean.Env = SHAREitSDK.SHAREitSDK.ENV_TEST.Equals(storeEnv) ? SHAREitEnv.Test : SHAREitEnv.Prod;
        //bean.MainHostClass = "com.unity3d.player.UnityPlayerActivity";
        //bean.IsMainProcess = true;
        //bean.Channel = "goo";
        //bean.IsEUAgreed = false;

        //SHAREitSDK.SHAREitSDK.init(bean);

        //request storage permission
        SHAREitSDK.SHAREitSDK.requestStoragePermissions();
        // init cloud config
        initCloudConfig();
    }

    // Start is called before the first frame update
    void Start()
    {
        paymentBtn.onClick.AddListener(onPaymentClick);
        adBtn.onClick.AddListener(onAdClick);
        gameBtn.onClick.AddListener(onGameClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void initCloudConfig()
    {
        //test key, need change to your right key
        string testKey = "key_interstitial_show_enable";
        if (SHAREitSDK.SHAREitSDK.hasConfig(testKey, true))
        {
            bool config = SHAREitSDK.SHAREitSDK.getBooleanConfig(testKey, true, true);
        }
    }

    public void onPaymentClick()
    {
        Debug.Log(TAG + " onPaymentClick");
        SceneManager.LoadScene("Payment");
    }

    public void onAdClick()
    {
        Debug.Log(TAG + " onAdClick");
        SceneManager.LoadScene("Advertisement");
    }

    public void onGameClick()
    {
        Debug.Log(TAG + " onGameClick");
        SceneManager.LoadScene("Game");
    }

    IEnumerator checkNonConsumableProducts()
    {
        yield return new WaitForSeconds(10);

        string merchantId = PlayerPrefs.GetString(Payment.KEY_MERCHANT_ID);
        string secretKey = PlayerPrefs.GetString(Payment.KEY_SECRET_KEY);
        string env = PlayerPrefs.GetString(ENV_STORE_KEY);

        Debug.Log(TAG + "checkNonConsumableProducts sendToken sendToken env=" + env + " merchantId=" + merchantId);
        StartCoroutine(TokenHelper.Instance.sendToken(env, merchantId, secretKey, (int tokenCode, string data) =>
            {
                Debug.Log(TAG + " checkNonConsumableProducts sendToken code=" + tokenCode + " data=" + data);
                if (tokenCode == 1)
                {
                    string userId = PlayerPrefs.GetString(Payment.KEY_USER_ID);
                    string token = data;

                    QueryPurchaseParamBean paramBean = new QueryPurchaseParamBean.Builder()
                        .setMerchantId(merchantId)
                        .setToken(token)
                        .setUserId(userId)
                        .build();

                    Debug.Log(TAG + " checkNonConsumableProducts queryPurchases userId=" + userId + " merchantId=" + merchantId);
                    shareitSDK.queryPurchases(paramBean, new SHAREitSDK.PaymentListener.OnQueryPurchaseResponseCallback((int code, string message, List < QueryDetailBean > dataList) =>
                    {
                        Debug.Log(TAG + " checkNonConsumableProducts queryPurchases code=" + code + " message=" + message + " dataList =" + dataList?.Count);
                        if (code == 10000)
                        {
                            if (dataList != null && dataList.Count > 0)
                            {
                                for (int i = 0; i < dataList.Count; i++)
                                {
                                    ConsumeParamBean consumeParams = new ConsumeParamBean.Builder()
                                        .setMerchantId(merchantId)
                                        .setToken(token)
                                        .setMerchantOrderNo(dataList[i].MerchantOrderNo)
                                        .build();
                                    Debug.Log(TAG + " checkNonConsumableProducts consumeParams");
                                    shareitSDK.consume(consumeParams, null);
                                }
                            }
                        }
                    }));
               }
            })
        );
    }

}
