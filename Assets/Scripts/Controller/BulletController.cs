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

public enum BulletPath {line,sin};

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour {

    public BulletPath bullet_path;
    public float amblitude_sin_path = 1;
    public float speed_sin_path;
    public bool reverse;

    public float _Power;
    public float _Speed = 10;
    private Rigidbody2D rigi;
    private float a;
    void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rigi.velocity = transform.up.normalized * _Speed;
    }

    void Update()
    {
        if (bullet_path == BulletPath.sin)
        {
           
            if (reverse)
                a = amblitude_sin_path * Mathf.Sin(Time.time * speed_sin_path);
            else
                a = amblitude_sin_path * Mathf.Sin(Time.time * speed_sin_path + 180);
            rigi.velocity = new Vector2(a, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

}
