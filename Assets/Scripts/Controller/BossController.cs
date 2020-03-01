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

public class BossController : MonoBehaviour {

    public Collider2D[] listBoss;
    private EnemyController enemyController;
    private float hp;
    private float savePos;

	// Use this for initialization
	void Start () {
        enemyController = GetComponent<EnemyController>();
        hp = enemyController._Hp;
        savePos = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector2(savePos + Mathf.Sin(Time.time), transform.position.y);

        if (enemyController._Hp >= hp * 2 / 3)
        {
            activeBoss(0);
        }

        if (enemyController._Hp < hp * 2 / 3 && enemyController._Hp >= hp/3)
        {
            activeBoss(1);
        }

        if (enemyController._Hp < hp / 3)
        {
            activeBoss(2);
        }

    }

    void activeBoss(int index)
    {
        for (int i = 0; i < listBoss.Length; i++)
        {
            if (index == i)
            {
                UbhShotCtrl ubh = listBoss[i].GetComponent<UbhShotCtrl>();
                if (ubh.enabled != true)
                {
                    listBoss[i].enabled = true;
                    ubh.enabled = true;
                }
            }
            else
            {
                UbhShotCtrl ubh = listBoss[i].GetComponent<UbhShotCtrl>();
                if (ubh.enabled != false)
                {
                    listBoss[i].GetComponent<PlaneDie>().Explosion();
                    listBoss[i].enabled = false;
                    ubh.StopShotRoutine();
                    ubh.enabled = false;
                }
            }
        }
    }
}
