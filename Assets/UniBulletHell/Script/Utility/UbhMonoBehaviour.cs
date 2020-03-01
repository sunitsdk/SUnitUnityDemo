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
/// Ubh mono behaviour.
/// </summary>
public abstract class UbhMonoBehaviour : MonoBehaviour
{
    Transform _Transform;
    Renderer _Renderer;
    Rigidbody _Rigidbody;
    Rigidbody2D _Rigidbody2D;

    public new Transform transform
    {
        get
        {
            if (_Transform == null) {
                _Transform = GetComponent<Transform>();
            }
            return _Transform;
        }
    }

    public new Renderer renderer
    {
        get
        {
            if (_Renderer == null) {
                _Renderer = GetComponent<Renderer>();
            }
            return _Renderer;
        }
    }

    public new Rigidbody rigidbody
    {
        get
        {
            if (_Rigidbody == null) {
                _Rigidbody = GetComponent<Rigidbody>();
            }
            return _Rigidbody;
        }
    }

    public new Rigidbody2D rigidbody2D
    {
        get
        {
            if (_Rigidbody2D == null) {
                _Rigidbody2D = GetComponent<Rigidbody2D>();
            }
            return _Rigidbody2D;
        }
    }
}
