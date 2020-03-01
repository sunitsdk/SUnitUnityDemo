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
/// Ubh timer.
/// </summary>
public class UbhTimer : UbhSingletonMonoBehavior<UbhTimer>
{
    float _LastTime;
    float _DeltaTime;
    float _FrameCount;
    bool _Pausing;

    /// <summary>
    /// Get delta time of UniBulletHell.
    /// </summary>
    public float DeltaTime
    {
        get
        {
            return _Pausing ? 0f : _DeltaTime;
        }
    }

    /// <summary>
    /// Get frame count of UniBulletHell.
    /// </summary>
    public float FrameCount
    {
        get
        {
            return _FrameCount;
        }
    }

    protected override void Awake ()
    {
        _LastTime = Time.time;

        base.Awake();
    }

    void Update ()
    {
        float nowTime = Time.time;
        _DeltaTime = nowTime - _LastTime;
        _LastTime = nowTime;

        if (_Pausing == false) {
            _FrameCount++;
        }
    }

    /// <summary>
    /// Pause time of UniBulletHell.
    /// </summary>
    public void Pause ()
    {
        _Pausing = true;
    }

    /// <summary>
    /// Resume time of UniBulletHell.
    /// </summary>
    public void Resume ()
    {
        _Pausing = false;
        _LastTime = Time.time;
    }
}
