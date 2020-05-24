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
    void Awake()
    {


        string storeEnv = PlayerPrefs.GetString(ENV_STORE_KEY);
        if (string.IsNullOrEmpty(storeEnv))
        {
            storeEnv = SHAREitSDK.SHAREitSDK.ENV_TEST;
            PlayerPrefs.SetString(ENV_STORE_KEY, SHAREitSDK.SHAREitSDK.ENV_TEST);  
        }
        SHAREitSDK.SHAREitSDK.setEnv(storeEnv);

        envDropdown.captionText.text = storeEnv;

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


        //InitBean bean = new InitBean();
        //bean.Env = SHAREitSDK.SHAREitSDK.ENV_TEST.Equals(storeEnv) ? SHAREitEnv.Test : SHAREitEnv.Prod;
        //bean.MainHostClass = "com.unity3d.player.UnityPlayerActivity";
        //bean.IsMainProcess = true;
        //bean.Channel = "goo";
        //bean.IsEUAgreed = false;

        //SHAREitSDK.SHAREitSDK.init(bean);

        //request storage permission
        SHAREitSDK.SHAREitSDK.requestStoragePermissions();
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

}
