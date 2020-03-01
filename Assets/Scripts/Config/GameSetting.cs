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

public class GameSetting : MonoBehaviour {

    public static GameSetting instance;

    public static Vector2 sizeCam;
    public static Vector2 positionCam;

    private int use_plane;
    public static int level;

    public static int name_plane;
    public static int level_plane;

    private float ratio_speed_plane;
    private float ratio_armor_plane;
    private float ratio_attack_plane;

    public float speed_base_plane;
    public float armor_base_plane;
    public float attack_base_plane;
    public float time_active_support;
    public int for_money_win = 5;
    public int for_boom_win = 1;

    public static float speed_plane;
    public static float armor_plane;
    public static float attack_plane;

    public int life;

    void Awake()
    {
        instance = this;
        sizeCam = new Vector2(2f * Camera.main.aspect * Camera.main.orthographicSize, 2f * Camera.main.orthographicSize);
        positionCam = Camera.main.transform.position;
        setting_plane();
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void setting_plane()
    {
        use_plane = PlayerPrefs.GetInt(PlaneInformation.USE_PlANE);
        level = PlayerPrefs.GetInt(MenuScript.LEVEL_KEY);

        name_plane = PlayerPrefs.GetInt(PlaneInformation.USE_PlANE);
        level_plane = PlayerPrefs.GetInt(PlaneInformation.LEVEL_PLANE+name_plane);
        ratio_speed_plane = PlayerPrefs.GetFloat(PlaneInformation.SPEED_KEY);
        ratio_armor_plane = PlayerPrefs.GetFloat(PlaneInformation.ARMOR_KEY);
        ratio_attack_plane = PlayerPrefs.GetFloat(PlaneInformation.ATTACK_KEY);

        speed_plane = ratio_speed_plane * speed_base_plane;
        armor_plane = ratio_armor_plane * armor_base_plane;
        attack_plane = ratio_attack_plane * attack_base_plane;
    }

    public bool getLife()
    {
        life--;
        if (life < 0)
            return false;
        else
            return true;
    }
}
