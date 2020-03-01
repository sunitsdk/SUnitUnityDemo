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

public class FirstSave : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        //PlayerPrefs.SetInt(MenuScript.FIRST_GAME_CHECK,MenuScript.FALSE_RESULT);
        if (PlayerPrefs.GetInt(MenuScript.FIRST_GAME_CHECK) != MenuScript.TRUE_RESULT)
        {
            Debug.Log("save first");
            setUp_firstGame();
            PlayerPrefs.SetInt(MenuScript.FIRST_GAME_CHECK, MenuScript.TRUE_RESULT);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setUp_firstGame()
    {
        PlayerPrefs.SetInt(PlaneInformation.LEVEL_PLANE + 0, 1);
        PlayerPrefs.SetInt(PlaneInformation.LEVEL_PLANE + 1, 0);
        PlayerPrefs.SetInt(PlaneInformation.LEVEL_PLANE + 2, 0);
        PlayerPrefs.SetInt(MenuScript.BOOM_KEY, 2);
        PlayerPrefs.SetInt(MenuScript.MONEY_KEY, 0);
        PlayerPrefs.SetInt(MenuScript.LOCK_KEY + 1, MenuScript.TRUE_RESULT);
        PlayerPrefs.SetFloat(MenuScript.SOUND_KEY, 1);
        PlayerPrefs.SetFloat(MenuScript.SFX_KEY, 0.5f);
    }
}
