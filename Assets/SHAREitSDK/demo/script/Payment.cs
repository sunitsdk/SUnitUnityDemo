using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.SceneManagement;

public class Payment : MonoBehaviour
{
    public const string KEY_BUILD_TYPE = "buildType";
    public const string KEY_MERCHANT_ID = "merchantId";
    public const string KEY_ORDER_ID = "orderId";
    public const string KEY_CALLBACK_URL = "callbackUrl";
    public const string KEY_USER_ID = "userId";
    public const string KEY_MERCHANT_KEY = "merchantKey";
    public const string KEY_TOKEN = "token";
    public const string KEY_SECRET_KEY = "secretKey";
    public const string KEY_COUNTRY_CODE = "countryCode"; //国家码：2位字母缩写，必须大写
    public const string KEY_TEST_URL = "testUrl";

    public const string KEY_LANGUAGE = "language";
    public const string KEY_USER_DETAIL = "userDetail";
    public const string KEY_REFERENCE = "reference";
    public const string KEY_PAY_VALID_DURATION = "payValidDuration"; // 单位：秒
    public const string KEY_PAY_RESULT_TYPE = "payResultType"; //是否使用商户支付结果页

    public const string KEY_BIZ_TYPE = "bizType";
    public const string KEY_PAYMENT_DETAIL = "paymentDetail";
    public const string KEY_APP_ID = "appId";

    public const string KEY_PRODUCT_CODE= "productCode";
    public const string KEY_PRODUCT_NAME = "productName";
    public const string KEY_AMOUNT = "amount";
    public const string KEY_CURRENCY = "currency";
    public const string KEY_PRODUCT_TYPE = "productType";
    public const string KEY_ENABLE_CUSTOM_PRODUCT = "enableCustomProduct";

    public const string MERCHANT_ID = "M36977092608";
    public const string SECRET_KEY = "payment-bootstra";
    public const string DEFAULT_APP_ID = "Code_Habe0727001";
    private const string DEFAULT_URL = "";
    private const string DEFAULT_USER_ID = "";

    public Button backBtn;
    public Text envText;
    public Dropdown bizTypeDropdown;
    public InputField merchantIdInput;
    public InputField orderInput;
    public Button orderGenerateBtn;
    public InputField custIdInput;
    public InputField tokenInput;
    public Button tokenGenerateBtn;
    public InputField callbackUrlInput;
    public InputField secretKeyInput;
    public InputField countryCodeInput;

    //Not use IAP Product List
    public Toggle enableNoIapProductToggle;
    public GameObject noIapProductPanel;
    public InputField productCodeInput;
    public InputField titleInput;
    public InputField amountInput;
    public InputField currencyInput;

    // optional 
    public Dropdown languageDropdown;
    public InputField paymentDetailInput;
    public InputField userDetailInput;
    public InputField extraInput;
    public InputField durationInput;
    public Toggle userMerchantResultUIToggle;

    public Button payBtn;
    public GameObject dialog;

    public SHAREitSDK.SHAREitSDK shareitSDK;

    //商品列表
    public GameObject productItemPrefab;
    public GameObject paymentUI;
    public GameObject productListUI;
    public GameObject productListRoot;
    public GameObject loadingUI;
    public Text resultText;
    

    private const string TAG = "Payment";
    private MerchantParamBean merchantParamBean;

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(onBack);
        orderGenerateBtn.onClick.AddListener(onGenerateOrderId);
        tokenGenerateBtn.onClick.AddListener(onGenerateToken);
        payBtn.onClick.AddListener(startPay);
        envText.text = PlayerPrefs.GetString(Main.ENV_STORE_KEY);

        initData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainScene");
    }

    public void onGenerateToken()
    {
        StartCoroutine("sendToken");
    }

    private void initData()
    {
        initDefaultData();

        initEditText(merchantIdInput, KEY_MERCHANT_ID);
        initEditText(orderInput, KEY_ORDER_ID);
        initEditText(callbackUrlInput, KEY_CALLBACK_URL);
        initEditText(custIdInput, KEY_USER_ID);
        initEditText(tokenInput, KEY_TOKEN);
        initEditText(secretKeyInput, KEY_SECRET_KEY);
        initEditText(countryCodeInput, KEY_COUNTRY_CODE);

        initEditText(paymentDetailInput, KEY_PAYMENT_DETAIL);
        initEditText(productCodeInput, KEY_PRODUCT_CODE);
        initEditText(titleInput, KEY_PRODUCT_NAME);
        initEditText(amountInput, KEY_AMOUNT);
        initEditText(currencyInput, KEY_CURRENCY);

        string language = PlayerPrefs.GetString(KEY_LANGUAGE, "en");
        languageDropdown.captionText.text = language;

        string bizType = PlayerPrefs.GetString(KEY_BIZ_TYPE, "STDANDARD");
        bizTypeDropdown.captionText.text = bizType;

        initEditText(userDetailInput, KEY_USER_DETAIL);
        initEditText(extraInput, KEY_REFERENCE);
        initEditText(durationInput, KEY_PAY_VALID_DURATION);

        string useMerchantResult = PlayerPrefs.GetString(KEY_PAY_RESULT_TYPE, "0");
        userMerchantResultUIToggle.isOn = useMerchantResult == "1" ? true : false;

        string nonIapProduct = PlayerPrefs.GetString(KEY_ENABLE_CUSTOM_PRODUCT, "0");
        enableNoIapProductToggle.isOn = nonIapProduct == "1" ? true : false;

    }

    private void initDefaultData()
    {
        string merchantId = PlayerPrefs.GetString(KEY_MERCHANT_ID, "-1");
        if (string.IsNullOrEmpty(merchantId) || merchantId == "-1")
        {
            PlayerPrefs.SetString(KEY_MERCHANT_ID, MERCHANT_ID);
            PlayerPrefs.SetString(KEY_ORDER_ID, "2015032001010100091");
            PlayerPrefs.SetString(KEY_CALLBACK_URL, "");
            //            if(TextUtils.isEmpty(DEFAULT_USER_ID))
            //                DEFAULT_USER_ID = "u_" + new Date().getTime();
            PlayerPrefs.SetString(KEY_USER_ID, DEFAULT_USER_ID);
            PlayerPrefs.SetString(KEY_TOKEN, "test");
            PlayerPrefs.SetString(KEY_SECRET_KEY, SECRET_KEY);
            PlayerPrefs.SetString(KEY_COUNTRY_CODE, "IN");
            PlayerPrefs.SetString(KEY_TEST_URL, DEFAULT_URL);
            PlayerPrefs.SetString(KEY_APP_ID, DEFAULT_APP_ID);
        }
    }

    private void initEditText(InputField editText, String key)
    {
        string val = PlayerPrefs.GetString(key, "");
        if (!string.IsNullOrEmpty(val))
        {
            editText.text = val;
        }
    }


    private void saveAll()
    {
        saveEditText(merchantIdInput, KEY_MERCHANT_ID);
        saveEditText(orderInput, KEY_ORDER_ID);
        saveEditText(callbackUrlInput, KEY_CALLBACK_URL);
        saveEditText(custIdInput, KEY_USER_ID);
        saveEditText(tokenInput, KEY_TOKEN);
        saveEditText(secretKeyInput, KEY_SECRET_KEY);
        saveEditText(countryCodeInput, KEY_COUNTRY_CODE);

        saveEditText(userDetailInput, KEY_USER_DETAIL);
        saveEditText(extraInput, KEY_REFERENCE);
        saveEditText(durationInput, KEY_PAY_VALID_DURATION);

        save(KEY_LANGUAGE, languageDropdown.captionText.text);
        save(KEY_PAY_RESULT_TYPE, userMerchantResultUIToggle.isOn ? "1" : "0");

        save(KEY_BIZ_TYPE, bizTypeDropdown.captionText.text);
        saveEditText(paymentDetailInput, KEY_PAYMENT_DETAIL);
        //        saveRadioGroup(productTypeRadioGroup, KEY_PRODUCT_TYPE);
        saveEditText(productCodeInput, KEY_PRODUCT_CODE);
        saveEditText(titleInput, KEY_PRODUCT_NAME);
        saveEditText(amountInput, KEY_AMOUNT);
        saveEditText(currencyInput, KEY_CURRENCY);
    }

    private void save(String key, String val)
    {
        PlayerPrefs.SetString(key, val);
    }


    private void saveEditText(InputField editText, String key)
    {
        string val = editText.text;
        string storeVal = PlayerPrefs.GetString(key, "!---!");
        if (storeVal == "!---!")
            PlayerPrefs.SetString(key, val);
        else if (val != storeVal)
            PlayerPrefs.SetString(key, val);
    }

    public void onNoIapProductToggleValueChanged(bool value)
    {
        noIapProductPanel.SetActive(enableNoIapProductToggle.isOn);
        Text text = payBtn.transform.Find("Text").GetComponent<Text>();
        text.text = enableNoIapProductToggle.isOn ? "Start pay" : "Open Product List";
    }

    private IEnumerator sendToken() {
        Dictionary<string, string> paras = new Dictionary<string, string>();
        paras.Add("bizType", "token");
        paras.Add("merchantId", merchantIdInput.text);
        paras.Add("timestamp", SHAREitSDK.CommonUtil.GetTimeStamp().ToString());
        paras.Add("version", "1.0");
        paras.Add("secretKey", secretKeyInput.text);
        string url = getTokenUrl();

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
                tokenInput.text = (string)jsonData["data"];
            }
            else
                tokenInput.text = "bizCode invalid.  " + jsonData["bizCode"] ;

        }
        else
            tokenInput.text = "network error";
    }


    public void onGenerateOrderId()
    {
        DateTime time = System.DateTime.Now;

        orderInput.text = "" + time.Year + time.Month + time.Day + time.Hour + time.Minute + time.Second + time.Millisecond;
    }

    public void startPay()
    {
        saveAll();
        if (enableNoIapProductToggle.isOn)
        {
            //不使用IAP产品列表
            onPurchase();
        }
        else
        {
            //打开产品列表
            openProductList();
        }
    }

    private void openProductList()
    {
        paymentUI.SetActive(false);
        productListUI.SetActive(true);
        showLoading();


        int childCount = productListRoot.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(productListRoot.transform.GetChild(0).gameObject);
        }

        string env = PlayerPrefs.GetString(Main.ENV_STORE_KEY);
        string merchantId = merchantIdInput.text;
        string merchantOrderId = orderInput.text;
        string amount = amountInput.text;
        string currency = currencyInput.text;
        string callUrl = callbackUrlInput.text;
        string title = titleInput.text;
        string custId = custIdInput.text;
        string token = tokenInput.text;
        string secretKey = secretKeyInput.text;
        string countryCode = countryCodeInput.text;
        string paymentDetail = paymentDetailInput.text;
        string userDetail = userDetailInput.text;

        string language = languageDropdown.captionText.text;
        string extra = extraInput.text;
        string durationStr = durationInput.text;
        bool useMerchantPayResult = userMerchantResultUIToggle.isOn;
        string bizType = bizTypeDropdown.captionText.text;

        Debug.Log("env=" + env + ", merchantId=" + merchantId + ", merchantOrderId=" + merchantOrderId + ", amount=" + amount + ", currency=" + currency
          + " callUrl=" + callUrl + " title=" + title + " custId=" + custId + " token=" + token
            + " secretKey=" + secretKey + " countryCode=" + countryCode + " language=" + language
            + " extra=" + extra + " durationStr=" + durationStr + " usePayResult=" + userMerchantResultUIToggle);

        ProductDetailBean detailBean = new ProductDetailBean();
        detailBean.productCode = productCodeInput.text;
        detailBean.productName = title;
        detailBean.price = amount;
        detailBean.currency = currency;
        detailBean.country = countryCode;

        MerchantParamBean.Builder builder = new MerchantParamBean.Builder()
            .setMerchantId(merchantId)
            .setCallbackUrl(callUrl)
            .setUserId(custId)
            .setToken(token)
            .setCountryCode(countryCode)
            .setUserDetail(userDetail)
            .setBizType(bizType)
            .setReference(extra)
            .setLanguage(language)
            .setShowResult(useMerchantPayResult ? MerchantParamBean.PAY_RESULT_TYPE_MERCHANT : MerchantParamBean.PAY_RESULT_TYPE_SDK);
        if (!string.IsNullOrEmpty(durationStr))
        {
            try
            {
                long duration = int.Parse(durationStr);
                builder.setTimeoutInSeconds(duration);
            }
            catch (Exception e)
            {
            }
        }
        if(!string.IsNullOrEmpty(paymentDetail))
        {
            builder.setPaymentDetail(paymentDetail);
        }
        merchantParamBean = builder.build();
        getProductList();
    }

    private void onPurchase()
    {
        paymentUI.SetActive(true);
        productListUI.SetActive(false);

        string env = PlayerPrefs.GetString(Main.ENV_STORE_KEY);
        string merchantId = merchantIdInput.text;
        string merchantOrderId = orderInput.text;
        string amount = amountInput.text;
        string currency = currencyInput.text;
        string callUrl = callbackUrlInput.text;
        string title = titleInput.text;
        string custId = custIdInput.text;
        string token = tokenInput.text;
        string secretKey = secretKeyInput.text;
        string countryCode = countryCodeInput.text;
        string paymentDetail = paymentDetailInput.text;
        string userDetail = userDetailInput.text;

        string language = languageDropdown.captionText.text;
        string extra = extraInput.text;
        string durationStr = durationInput.text;
        bool useMerchantPayResult = userMerchantResultUIToggle.isOn;
        string bizType = bizTypeDropdown.captionText.text;

        Debug.Log("env=" + env + ", merchantId=" + merchantId + ", merchantOrderId=" + merchantOrderId + ", amount=" + amount + ", currency=" + currency
          + " callUrl=" + callUrl + " title=" + title + " custId=" + custId + " token=" + token
            + " secretKey=" + secretKey + " countryCode=" + countryCode + " language=" + language
            + " extra=" + extra + " durationStr=" + durationStr + " usePayResult=" + userMerchantResultUIToggle);

        ProductDetailBean detailBean = new ProductDetailBean();
        detailBean.productCode = productCodeInput.text;
        detailBean.productName = title;
        detailBean.price = amount;
        detailBean.currency = currency;
        detailBean.country = countryCode;

        MerchantParamBean.Builder builder = new MerchantParamBean.Builder()
            .setMerchantId(merchantId)
            .setOrderId(merchantOrderId)
            .setUserId(custId)
            .setToken(token)
            .setProductDetail(detailBean)
            .setUserDetail(userDetail)
            .setBizType(bizType)
            .setReference(extra)
            .setCallbackUrl(callUrl)
            .setLanguage(language)
            .setShowResult(useMerchantPayResult ? MerchantParamBean.PAY_RESULT_TYPE_MERCHANT : MerchantParamBean.PAY_RESULT_TYPE_SDK);
        if (!string.IsNullOrEmpty(durationStr))
        {
            try
            {
                long duration = int.Parse(durationStr);
                builder.setTimeoutInSeconds(duration);
            }
            catch (Exception e)
            {
            }
        }
        if (!string.IsNullOrEmpty(paymentDetail))
        {
            builder.setPaymentDetail(paymentDetail);
        }
        merchantParamBean = builder.build();


        shareitSDK.launchBillingFlow(merchantParamBean, new SHAREitSDK.PaymentListener.OnPurchaseResponseCallback((int code, string orderId, string payMessage, string payExtra) =>
        {
            string result = "code=" + code + " orderId=" + orderId + " message=" + payMessage + " extra=" + payExtra;
            Debug.Log(TAG + " onPurchase result " + result);
            if (code == 10000)
                Debug.Log(TAG + " Payment success");
            else if (code == 10001)
                Debug.Log(TAG + " Payment pending");
            else
                Debug.Log(TAG + " Payment failed");

            showDialog(result);
        }));
    }

    public void onBack() {
        if (productListUI.activeSelf)
        {
            paymentUI.SetActive(true);
            productListUI.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene("MainScene");
        }
     }

    private void showDialog(string context)
    {
        dialog.SetActive(true);
        Dialog dialogScript = dialog.GetComponent<Dialog>();
        dialogScript.setContent(context);
    }


    private String getTokenUrl()
    {
        String testUrl = "https://pay-gate-uat.shareitpay.in/aggregate-pay-gate/api/gateway";
        String prodUrl = "https://pay-gate.shareitpay.in/aggregate-pay-gate/api/gateway";

        String selVal = PlayerPrefs.GetString(Main.ENV_STORE_KEY);
        String url = "";
        switch (selVal)
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

    private void getProductList()
    {
        string merchantId = merchantIdInput.text;
        string token = tokenInput.text;
        string countryCode = countryCodeInput.text;

        ProductParamBean bean = new ProductParamBean.Builder()
            .setMerchantId(merchantId)
            .setToken(token)
            .setCountryCode(countryCode)
            .build();

        string[] productIds = new string[] { };
        Debug.Log(TAG + " getProductList merchantId=" + merchantId + " token=" + token + " countryCode=" + countryCode);
        shareitSDK.queryProducts(bean, productIds, new SHAREitSDK.PaymentListener.OnProductResponseCallback((int code, string message, List<ProductDetailBean> productDetailList) =>
        {
            if (code == 10000)
            {
                if (productDetailList != null)
                {
                    hideLoading();
                    for (int i = 0; i < productDetailList.Count; i++)
                    {
                        GameObject item = GameObject.Instantiate(productItemPrefab);
                        item.transform.parent = productListRoot.transform;
                        item.transform.localScale = new Vector3(1, 1, 1);

                        ProductItem productItem = item.GetComponent<ProductItem>();
                        productItem.setProductDetail(productDetailList[i]);
                        productItem.onClick = onProductItemClick;
                    }
                }
                else
                {
                    showProductListError("Data is empty.");
                }
            }
            else
                showProductListError("code=" + code + ", message=" + message);
        }));

        //test only
        //hideLoading();
        //string jsonStr = "{\"actionType\":\"1\",\"code\":10000,\"message\":\"\",\"dataList\":\"[{\\\"appCode\\\":\\\"GTW0001\\\",\\\"country\\\":\\\"ID\\\",\\\"productId\\\":\\\"GoosCode-Rant_01\\\",\\\"productDesc\\\":\\\"第1个商品\\\",\\\"productCode\\\":\\\"GoosId-Rant_01\\\",\\\"productName\\\":\\\"R_GOODS_NAME_id\\\",\\\"type\\\":0,\\\"originalCurrency\\\":\\\"IDR\\\",\\\"originalPrice\\\":\\\"5000.00\\\",\\\"exchangeRate\\\":\\\"\\\",\\\"currency\\\":\\\"IDR\\\",\\\"price\\\":\\\"5000.00\\\"},{\\\"appCode\\\":\\\"GTW0001\\\",\\\"country\\\":\\\"DEFAULT\\\",\\\"productId\\\":\\\"GoosCode-Rant_02\\\",\\\"productDesc\\\":\\\"第2个商品\\\",\\\"productCode\\\":\\\"GoosId-Rant_02\\\",\\\"productName\\\":\\\"R_GOODS_NAME_2DEFAULT\\\",\\\"type\\\":0,\\\"originalCurrency\\\":\\\"USD\\\",\\\"originalPrice\\\":\\\"100.00\\\",\\\"exchangeRate\\\":\\\"15147.6261642555\\\",\\\"currency\\\":\\\"IDR\\\",\\\"price\\\":\\\"1514763.00\\\"}]\"}";
        //JsonData jsonData = JsonMapper.ToObject(jsonStr);
        //string dataStr = "{\"Items\":" + jsonData["dataList"] + "}";
        //Debug.Log(TAG + " dataListt= " + dataStr);
        //ProductDetailBean[] productDetails = JsonHelper.FromJson<ProductDetailBean>(dataStr);

        //for (int i = 0; i < productDetails.Length; i++)
        //{
        //    GameObject item = GameObject.Instantiate(productItemPrefab);
        //    item.transform.parent = productListRoot.transform;
        //    item.transform.localScale = new Vector3(1, 1, 1);
        //    ProductItem productItem = item.GetComponent<ProductItem>();
        //    productItem.setProductDetail(productDetails[i]);
        //    productItem.onClick = onProductItemClick;
        //}


    }

    private void onProductItemClick(ProductDetailBean bean)
    {

        MerchantParamBean merchantParam = new MerchantParamBean.Builder()
            .addParams(merchantParamBean.getParams())
            .setProductDetail(bean)
            .setOrderId(createOrderId())
            .build();
        shareitSDK.launchBillingFlow(merchantParam, new SHAREitSDK.PaymentListener.OnPurchaseResponseCallback((int code, string orderId, string message, string reference) =>
        {
            string result = "code=" + code + " orderId=" + orderId + " message=" + message + " extra=" + reference;
            Debug.Log(TAG + " launchBillingFlow result " + result);
            if (code == 10000)
                Debug.Log(TAG + " Payment success");
            else if (code == 10001)
                Debug.Log(TAG + " Payment pending");
            else
                Debug.Log(TAG + " Payment failed");

            showDialog(result);
        }));
    }

    private string createOrderId()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "";
    }

    private void showLoading()
    {
        resultText.text = "Loading...";
        loadingUI.SetActive(true);
    }

    private void hideLoading()
    {
        loadingUI.SetActive(false);
    }

    private void showProductListError(string message)
    {
        resultText.text = message;
        loadingUI.SetActive(true);
    }
}
