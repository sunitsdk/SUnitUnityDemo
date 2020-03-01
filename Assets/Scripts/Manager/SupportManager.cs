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

public enum TypeSupport { hp, homing, add_dame, plane, up_bullet, shield, add_bullet_up, add_bullet_left_right };

public class SupportManager : MonoBehaviour {

    public float timeSupport;
    public int add_HP;
    public GameObject homing;
    public static bool add_Dame;
    public static int dame;
    public GameObject add_Plane;
    public int lv_up_bullet;
    public GameObject[] up_bullet;
    public GameObject shield;
    public GameObject add_bullet_up;
    public GameObject add_bullet_left_right;

    // Use this for initialization
    void Start () {
        timeSupport = GameSetting.instance.time_active_support;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setTypeSupport(TypeSupport type)
    {
        
        switch (type) {
            case TypeSupport.add_bullet_left_right:
                StopCoroutine(activeSupport(add_bullet_left_right));
                StartCoroutine(activeSupport(add_bullet_left_right));
                break;
            case TypeSupport.add_bullet_up:
                StopCoroutine(activeSupport(add_bullet_up));
                StartCoroutine(activeSupport(add_bullet_up));
                break;
            case TypeSupport.homing:
                StopCoroutine(activeSupport(homing));
                StartCoroutine(activeSupport(homing));
                break;
            case TypeSupport.hp:
                GetComponent<PlaneController>().add_HP(add_HP);
                break;
            case TypeSupport.plane:
                StopCoroutine(activeSupport(add_Plane));
                StartCoroutine(activeSupport(add_Plane));
                break;
            case TypeSupport.shield:
                active_shield();
                StopCoroutine(activeSupport(shield));
                StartCoroutine(activeSupport(shield));
                break;
            case TypeSupport.add_dame:
                active_add_dame();
                break;
            case TypeSupport.up_bullet:
                active_up_bullet();
                break;
        }
    }

    void active_up_bullet()
    {
        lv_up_bullet++;
        if (lv_up_bullet > 2)
            lv_up_bullet = 2;
        else
            up_bullet[lv_up_bullet].SetActive(true);
    }

    void active_add_dame()
    {
        StopCoroutine("reset_add_dame");
        add_Dame = true;
        StartCoroutine("reset_add_dame");
    }

    IEnumerator reset_add_dame()
    {
        yield return new WaitForSeconds(timeSupport);
        add_Dame = false;
    }

    void active_shield()
    {
        StopCoroutine("resetShield");
        GetComponent<PlaneController>().activeShield(true);
        StartCoroutine("resetShield");
    }

    IEnumerator resetShield()
    {
        yield return new WaitForSeconds(timeSupport);
        GetComponent<PlaneController>().activeShield(false);
    }

    IEnumerator activeSupport(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(timeSupport);
        obj.SetActive(false);
    }

    public void resetSupport()
    {
        StopAllCoroutines();
        // reset shield
        shield.SetActive(false);
        GetComponent<PlaneController>().activeShield(false);
        // reset bullet,plane...
        add_bullet_left_right.SetActive(false);
        add_bullet_up.SetActive(false);
        homing.SetActive(false);
        add_Plane.SetActive(false);

    }
}
