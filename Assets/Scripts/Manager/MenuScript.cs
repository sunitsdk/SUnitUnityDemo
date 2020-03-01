/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public static MenuScript instance;

    public const int TRUE_RESULT = 1;
    public const int FALSE_RESULT = 0;

    public const string MENU_NAME = "MainMenu";
    public const string MAP_NAME = "Map";
    public const string STORE_NAME = "Store";
    public const string LEVEL_KEY = "Level";
    public const string LOCK_KEY = "lock";
    public const string STAR_KEY = "star";
    public const string BOOM_KEY = "boom";
    public const string MONEY_KEY = "money";
    public const string SOUND_KEY = "sound";
    public const string SFX_KEY = "sfx";

    public const string FIRST_GAME_CHECK = "firstGame";

    public Text TXTenergy;
    public Text TXTboom;
    public Slider sliderSound;
    public Slider sliderSFX;


    void Awake()
    {
        instance = this;
		Application.targetFrameRate = 60;
        
    }
    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevelName == MenuScript.MENU_NAME)
        {
            sliderSFX.value = PlayerPrefs.GetFloat(MenuScript.SFX_KEY);
            sliderSound.value = PlayerPrefs.GetFloat(MenuScript.SOUND_KEY);
        }
        updateData();
    }

    public void onDialog(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void closeDialog(GameObject obj)
    {
        obj.SetActive(false);
    }

    public void sound()
    {
        PlayerPrefs.SetFloat(MenuScript.SOUND_KEY, sliderSound.value);
        SoundManager.instance.updateVol();
    }

    public void sfx()
    {
        PlayerPrefs.SetFloat(MenuScript.SFX_KEY, sliderSFX.value);
        SoundManager.instance.updateVol();
    }

    public void updateData()
    {
        if (TXTboom != null)
        {
            TXTboom.text = PlayerPrefs.GetInt(BOOM_KEY) + "";
        }
        if (TXTenergy != null)
        {
            TXTenergy.text = PlayerPrefs.GetInt(MONEY_KEY) + "";
            
        }
    }

    public void loadScene()
    {
        string name_scene = Application.loadedLevelName;
        if (name_scene.Equals(MAP_NAME))
        {
            Application.LoadLevel(MENU_NAME);
        }
        if (name_scene.Equals(STORE_NAME))
        {
            Application.LoadLevel(MAP_NAME);
        }
        if (name_scene.Equals(MENU_NAME))
        {
            Application.LoadLevel(MAP_NAME);
        }
    } 

	public void MoreGame()
	{
//		AdsControl.Instance.showAds ();
	}
}
