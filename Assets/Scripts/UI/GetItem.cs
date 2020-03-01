/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetItem : MonoBehaviour
{

	public GameObject bombPanel;
	public GameObject moneyPanel;
	public Text moneyText,bombText;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void GetItemFree ()
	{
		int money = PlayerPrefs.GetInt (MenuScript.MONEY_KEY);
		money += 10;
		PlayerPrefs.SetInt (MenuScript.MONEY_KEY, money);

		moneyText.text = money.ToString ();
	}

	public void ShowRewardVideo ()
	{
//		AdsControl.Instance.ShowRewardVideo ();
	}

	public void BuyBomb ()
	{
		int money = PlayerPrefs.GetInt (MenuScript.MONEY_KEY);
		int bomb = PlayerPrefs.GetInt (MenuScript.BOOM_KEY);
		if (money >= 100) {
			bomb++;
			PlayerPrefs.SetInt (MenuScript.BOOM_KEY, bomb);
			money -= 100;
			PlayerPrefs.SetInt (MenuScript.MONEY_KEY, money);
			bombText.text = bomb.ToString ();
			moneyText.text = money.ToString ();
		}
	}

	public void ShowMoney ()
	{
		moneyPanel.SetActive (true);
	}

	public void ShowBomb ()
	{
		bombPanel.SetActive (true);	
	}

	public void CloseMoney ()
	{
		moneyPanel.SetActive (false);
	}

	public void CloseBomb ()
	{
		bombPanel.SetActive (false);
	}
}
