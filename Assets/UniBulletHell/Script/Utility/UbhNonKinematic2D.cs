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

/// <summary>
/// Unity4.3 "2d kinematic and trigger bug" counterplan.
/// http://answers.unity3d.com/questions/575438/how-to-make-ontriggerenter2d-work.html
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class UbhNonKinematic2D : UbhMonoBehaviour
{
#if UNITY_4_3
    Vector3 _FixedPosition = Vector3.zero;
    float _OrgGravityScale;
#endif

    void Awake ()
    {
#if !UNITY_4_3
        rigidbody2D.isKinematic = true;
        enabled = false;
        return;
#else
        _FixedPosition = transform.localPosition;
#endif
    }

    void Update ()
    {
#if !UNITY_4_3
        enabled = false;
        return;
#else
        if (rigidbody2D == null) {
            return;
        }
        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.angularVelocity = 0;
        transform.localPosition = _FixedPosition;
#endif
    }

    void OnEnable ()
    {
#if !UNITY_4_3
        enabled = false;
        return;
#else
        if (rigidbody2D == null) {
            return;
        }
        _OrgGravityScale = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0f;
#endif
    }

    void OnDisable ()
    {
#if !UNITY_4_3
        return;
#else
        if (rigidbody2D == null) {
            return;
        }
        rigidbody2D.gravityScale = _OrgGravityScale;
#endif
    }
}
