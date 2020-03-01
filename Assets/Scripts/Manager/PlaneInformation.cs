/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlaneInformation : MonoBehaviour {

    public const string STAR_NAME = "star";
    public const string LEVEL_PLANE = "level_plane";
    public const string SPEED_KEY = "speed";
    public const string ARMOR_KEY = "armor";
    public const string ATTACK_KEY = "attack";
    public const string USE_PlANE = "plane";

    public int price_buy_plane;

    public float speed_base;
    public float armor_base;
    public float attack_base;

    public float str = 0.3f;

    public Image icon_plane;
    [HideInInspector]
    public int id_plane;

    public List<level_plane> level_Plane = new List<level_plane>();
    public GameObject[] level_star;
    public GameObject btShowUpgrade;
    public Button play;
    public Button upgrade;
    public Button close;
    public GameObject lockPlane;
    public GameObject unlockPlane;
    public GameObject tableUpgrade;

    public GameObject table_warning;
    public GameObject table_buy_plane;
    public Button buy_plane;
    public Button close_table_plane;
    public Button close_table_warning;

    public Image preSpeed;
    public Image preArmor;
    public Image preAttack;

    public Image preSpeed_buy_plane;
    public Image preArmor_buy_plane;
    public Image preAttack_buy_plane;

    public Image preSpeed_old;
    public Image preArmor_old;
    public Image preAttack_old;

    public Image preSpeed_new;
    public Image preArmor_new;
    public Image preAttack_new;

    public Image plane_old;
    public Image plane_new;
    public Image plane_buy;
    public Text txtPrice_upgrade;
    public Text txtPrice_plane;

    private float speed;
    private float armor;
    private float attack;

    void Start()
    {
        getLevel(PlayerPrefs.GetInt(LEVEL_PLANE + id_plane),true);
    }
    // Use this for initialization
    void OnEnable () {
        getLevel(PlayerPrefs.GetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane),false);
        close_table_warning.onClick.AddListener(this.closeWarning);
    }
	
	// Update is called once per frame
	public void showUpgrade () {
        int level = PlayerPrefs.GetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane);
        plane_old.sprite = level_Plane[level-1].icon_level_Plane;
        plane_new.sprite = level_Plane[level].icon_level_Plane;

        txtPrice_upgrade.text = level_Plane[level].price + "";

        setInf(preSpeed_old, preArmor_old, preAttack_old, level-1);
        setInf(preSpeed_new, preArmor_new, preAttack_new, level);

        tableUpgrade.SetActive(true);

        upgrade.onClick.RemoveListener(btUpgrade);
        close.onClick.RemoveListener(this.closeUpgrade);
        upgrade.onClick.AddListener(this.btUpgrade);
        close.onClick.AddListener(this.closeUpgrade);
    }

    public void showBuy()
    {
        plane_buy.sprite = level_Plane[0].icon_level_Plane;
        setInf(preSpeed_buy_plane, preArmor_buy_plane, preAttack_buy_plane, 0);
        table_buy_plane.SetActive(true);
        txtPrice_plane.text = price_buy_plane + "";

        buy_plane.onClick.RemoveAllListeners();
        close_table_plane.onClick.RemoveAllListeners();
        buy_plane.onClick.AddListener(this.btBuy);
        close_table_plane.onClick.AddListener(this.closeBuy);

    }

    public void btUpgrade()
    {
        Debug.Log("upgarade");
        if (takeMoney(level_Plane[PlayerPrefs.GetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane)].price))
        {
            int level = PlayerPrefs.GetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane);
            PlayerPrefs.SetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane, level + 1);
            closeUpgrade();
            getLevel(PlayerPrefs.GetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane),false);
        }
    }

    public void btBuy()
    {
        Debug.Log("buy");
        if (takeMoney(price_buy_plane))
        {
            int level = PlayerPrefs.GetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane);
            PlayerPrefs.SetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane, level + 1);
            closeBuy();
            getLevel(PlayerPrefs.GetInt(LEVEL_PLANE + ScrollRectSnap_CS.select_plane),false);
        }
    }

    public void closeUpgrade()
    {
        tableUpgrade.SetActive(false);
    }

    public void closeWarning()
    {
        table_warning.SetActive(false);
		Debug.Log ("Close");
    }

    public void closeBuy()
    {
        table_buy_plane.SetActive(false);
    }

    public void getLevel(int level,bool firstLoad)
    {
        for (int i = 0; i <= level_Plane.Count; i++) {
            if (i == level)
                setLevel(i,firstLoad);
        }
    }

    void setLevel(int level, bool firstLoad)
    {
        if (level == 0)
        {
            icon_plane.sprite = level_Plane[0].icon_level_Plane;
            lockPlane.SetActive(true);
            unlockPlane.SetActive(false);
            if (!firstLoad)
            {
                play.enabled = false;
                play.GetComponent<Image>().color = Color.red;
            }

            setInf(preSpeed, preArmor, preAttack, level);
        }
        else {
            lockPlane.SetActive(false);
            unlockPlane.SetActive(true);
            if (!firstLoad)
            {
                play.enabled = true;
                play.GetComponent<Image>().color = Color.white;
            }
          
            icon_plane.sprite = level_Plane[level-1].icon_level_Plane;
            for (int i = 0; i < level_star.Length; i++)
            {
                if (i < level)
                {

                    level_star[i].SetActive(true);
                }
                else {
                    level_star[i].SetActive(false);
                }
            }

            setInf(preSpeed, preArmor, preAttack, level-1);

            if (level == level_Plane.Count)
                btShowUpgrade.SetActive(false);

            play.onClick.RemoveListener(btPlay);
            play.onClick.AddListener(this.btPlay);
        }
    }

    void OnDisable()
    {
        close_table_warning.onClick.RemoveAllListeners();
        play.onClick.RemoveListener(btPlay);
        upgrade.onClick.RemoveAllListeners();
        close.onClick.RemoveAllListeners();
    }

    public void btPlay()
    {
        Application.LoadLevel(PlayerPrefs.GetInt(MenuScript.LEVEL_KEY)+"");

        PlayerPrefs.SetInt(USE_PlANE,ScrollRectSnap_CS.select_plane);
        PlayerPrefs.SetFloat(SPEED_KEY, speed);
        PlayerPrefs.SetFloat(ARMOR_KEY, armor);
        PlayerPrefs.SetFloat(ATTACK_KEY, attack);
    }

    void setInf(Image preSpeed,Image preArmor,Image preAttack,int level)
    {
        speed = speed_base + str * level;
        armor = armor_base + str * level;
        attack = attack_base + str * level;

        preSpeed.fillAmount = speed;
        preArmor.fillAmount = armor;
        preAttack.fillAmount = attack;
    }

    bool takeMoney(int price)
    {
        Debug.Log("take");
        int money = PlayerPrefs.GetInt(MenuScript.MONEY_KEY);
        if (money >= price)
        {
            money -= price;
            PlayerPrefs.SetInt(MenuScript.MONEY_KEY, money);
            MenuScript.instance.updateData();
            return true;
        }
        else
        {
            table_warning.SetActive(true);
            table_buy_plane.SetActive(false);
            tableUpgrade.SetActive(false);
			close_table_warning.onClick.AddListener(this.closeWarning);
            return false;
        }
    }
}

[System.Serializable]
public class level_plane
{
    public Sprite icon_level_Plane;
    public int price;
}
