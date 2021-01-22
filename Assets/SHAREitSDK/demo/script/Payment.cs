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
    public Button backBtn;
    public Text envText;
    public InputField merchantIdInput;
    public InputField orderInput;
    public Button orderGenerateBtn;
    public InputField amountInput;
    public InputField currencyInput;
    public InputField callbackUrlInput;
    public InputField titleInput;
    public InputField custIdInput;
    public InputField tokenInput;
    public Button tokenGenerateBtn;
    public InputField secretKeyInput;
    public InputField countryCodeInput;

    // optional 
    public Dropdown languageDropdown;
    public InputField paymentDetailInput;
    public InputField userDetailInput;
    public InputField extraInput;
    public InputField durationInput;
    public Toggle userPayerMaxResultUIToggle;

    public Button payBtn;
    public GameObject dialog;

    public SHAREitSDK.SHAREitSDK shareitSDK;

    private const string TAG = "Payment";

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(onBack);
        orderGenerateBtn.onClick.AddListener(onGenerateOrderId);
        tokenGenerateBtn.onClick.AddListener(onGenerateToken);
        payBtn.onClick.AddListener(startPay);
        envText.text = PlayerPrefs.GetString(Main.ENV_STORE_KEY);
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
        string billingDetail = paymentDetailInput.text;
        string userDetail = userDetailInput.text;

        //string mail = mailInput.text;
        //string mobileNo = mobileNoInput.text;
        string language = languageDropdown.captionText.text;
        string extra = extraInput.text;
        string durationStr = durationInput.text;
        bool userPayerMaxResult = userPayerMaxResultUIToggle.isOn;

        Debug.Log("env=" + env + ", merchantId=" + merchantId + ", merchantOrderId=" + merchantOrderId + ", amount=" + amount + ", currency=" + currency
          + " callUrl=" + callUrl + " title=" + title + " custId=" + custId + " token=" + token
            + " secretKey=" + secretKey + " countryCode=" + countryCode + " billingDetail=" + billingDetail + " userDetail=" +
          userDetail + " language=" + language + " extra=" + extra + " durationStr=" + durationStr + " userPayerMaxResult=" + userPayerMaxResult);

        MerchantParamBean.Builder builder = new MerchantParamBean.Builder()
            .setBizType(MerchantParamBean.BIZ_TYPE)
            .setMerchantId(merchantId)
            .setOrderId(merchantOrderId)
            .setAmount(amount)
            .setCurrency(currency)
            .setCustId(custId)
            .setSubject(title)
            .setToken(token)
            .setCountryCode(countryCode)
            .setUserDetail(userDetail)
            .setReference(extra)
            .setCallbackUrl(callUrl)
            .setLanguage(language)
            .setResultPageShowType(userPayerMaxResult ? MerchantParamBean.RESULT_TYPE_SDK : MerchantParamBean.RESULT_TYPE_MERCHANT);
        if (!string.IsNullOrEmpty(durationStr))
        {
            try {
                long duration = int.Parse(durationStr);
                builder.setExpireTime(duration);
            }
            catch (Exception e)
            {
            }
        }
        if (!string.IsNullOrEmpty(billingDetail))
        {
            builder.setBillingDetail(billingDetail);
        }

        shareitSDK.recharge(builder.build(), new SHAREitSDK.RechargeResultListener((int code, string orderId, string payMessage, string reference) =>
        {
            string result = "code=" + code + " orderId=" + orderId + " message=" + payMessage + " reference=" + reference;
            Debug.Log(TAG + " recharge result " + result);
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
        SceneManager.LoadScene("MainScene");
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
}
