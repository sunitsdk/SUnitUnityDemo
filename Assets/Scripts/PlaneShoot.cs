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

public class PlaneShoot : MonoBehaviour {

    public float shootDelay;
    public int countWave;
    [SerializeField]
    public GameObject bulletPrefab;
    public bool soundShoot;


    // Use this for initialization
    void OnEnable () {
        StartCoroutine(startShoot());
    }

    IEnumerator startShoot()
    {
        while (true)
        {
            StartCoroutine("shoot");
            yield return new WaitForSeconds(shootDelay);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    IEnumerator shoot()
    {
        for (int i = 0; i < countWave; i++)
        {
            if (bulletPrefab != null)
                UbhObjectPool.Instance.GetGameObject(bulletPrefab, transform.position, transform.rotation);
            if (soundShoot && SoundManager.instance != null)
                SoundManager.instance.playSoundShoot_1();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
