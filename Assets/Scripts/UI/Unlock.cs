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

public class Unlock : MonoBehaviour {

	public int numberlevel;
	private int checkLock;

    public Button play;
	public GameObject unLock;
	public GameObject Star;

	// Use this for initialization
	void Start () {
	
		checkLock = PlayerPrefs.GetInt (MenuScript.LOCK_KEY+numberlevel);

		if (checkLock == MenuScript.TRUE_RESULT) {
			unLock.SetActive (true);
			Star.SetActive(true);
		} else {
			unLock.SetActive (false);
			Star.SetActive(false);
		}

        play.onClick.AddListener(this.playLevel);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void playLevel(){
		if (checkLock == MenuScript.TRUE_RESULT) {
            PlayerPrefs.SetInt(MenuScript.LEVEL_KEY,numberlevel);
			Application.LoadLevel(MenuScript.STORE_NAME);
		}
	}
}
