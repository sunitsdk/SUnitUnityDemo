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
/// Ubh hole circle shot.
/// </summary>
[AddComponentMenu("UniBulletHell/Shot Pattern/Hole Circle Shot")]
public class UbhHoleCircleShot : UbhBaseShot
{
    // "Set a center angle of hole. (0 to 360)"
    [Range(0f, 360f)]
    public float _HoleCenterAngle = 180f;
    // "Set a size of hole. (0 to 360)"
    [Range(0f, 360f)]
    public float _HoleSize = 20f;

    protected override void Awake ()
    {
        base.Awake();
    }

    public override void Shot ()
    {
        if (_BulletNum <= 0 || _BulletSpeed <= 0f) {
            Debug.LogWarning("Cannot shot because BulletNum or BulletSpeed is not set.");
            return;
        }

        _HoleCenterAngle = UbhUtil.Get360Angle(_HoleCenterAngle);
        float startAngle = _HoleCenterAngle - (_HoleSize / 2f);
        float endAngle = _HoleCenterAngle + (_HoleSize / 2f);

        float shiftAngle = 360f / (float) _BulletNum;

        for (int i = 0; i < _BulletNum; i++) {
            float angle = shiftAngle * i;
            if (startAngle <= angle && angle <= endAngle) {
                continue;
            }

            var bullet = GetBullet(transform.position, transform.rotation);
            if (bullet == null) {
                break;
            }

            ShotBullet(bullet, _BulletSpeed, angle);

            AutoReleaseBulletGameObject(bullet.gameObject);
        }

        FinishedShot();
    }
}
