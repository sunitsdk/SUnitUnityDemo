using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public Button uploadDataBtn;
    public Button gameLevelStartBtn;
    public Button gameLevelEndBtn;

    public Button loginInBtn;
    public Button loginOutBtn;
    public Button rateBtn;
    public Text descText;

    private const string TAG = "Game";
    public SHAREitSDK.SHAREitSDK shareitSDK;

    private const string GAME_SECRET = "cb41338ff0e6779eaeb44740b28e0cdc";
    private const string REWARD_UNIT_ID = "1014yOdeiW";
    // Start is called before the first frame update
    void Start()
    {
        uploadDataBtn.onClick.AddListener(onUploadData);
        gameLevelStartBtn.onClick.AddListener(onGameLevelStart);
        gameLevelEndBtn.onClick.AddListener(onGameLevelEnd);

        loginInBtn.onClick.AddListener(onLoginIn);
        loginOutBtn.onClick.AddListener(onLoginOut);
        rateBtn.onClick.AddListener(onRateClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainScene");
    }

    public void onUploadData()
    {
        Dictionary<string, string> paras = new Dictionary<string, string>();
        paras.Add("name", "test");
        paras.Add("age", "180");

        shareitSDK.onEvent("TEST_EVENT_NAME", paras);
    }

    public void onGameLevelStart()
    {
        descText.text = "gameLeveleStart upload,level = 1";
        shareitSDK.gameLevelStart("1");
    }

    public void onGameLevelEnd()
    {
        //根据玩家实际通关情况来设置此值，通关为 true ，失败或者游戏过程中点击重玩等情况为false
        bool isPass = true;
        shareitSDK.gameLevelEnd("1", isPass);
        descText.text = "gameLeveleEnd upload,level = 1,isPass = " + isPass;
    }

    public void onLoginIn()
    {
        if(shareitSDK.isLogin())
        {
            string desc = descText.text;
            if (string.IsNullOrEmpty(desc))
                descText.text = "userId=" + shareitSDK.getUserId();
            return;
        }

        shareitSDK.userLogin(GAME_SECRET, new SHAREitSDK.LoginListener((string userId, string username, string avatarUrl) => {
            descText.text = "userId=" + userId + " userName=" + username + " avatalUrl=" + avatarUrl;
        }));
    }

    public void onLoginOut()
    {
        if (!shareitSDK.isLogin())
        {
            descText.text = "You havn't logged in";
            return;
        }
        bool isLoggout = shareitSDK.logout();

        descText.text = "loggout " + (isLoggout ? "successfully" : "failed");
    }

    public void onRateClick()
    {
        //可选功能，若不需要接入可忽略
        shareitSDK.showRateDialog(new SHAREitSDK.RateListener((int resultCode, string reason) => {
               Debug.Log(TAG + "code is " + resultCode + ",reason :" + reason);
          }));
    }
}
