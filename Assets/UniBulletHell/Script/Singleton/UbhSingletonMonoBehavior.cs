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
/// Ubh singleton mono behavior.
/// </summary>
public class UbhSingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _Instance;

    /// <summary>
    /// Get singleton instance.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_Instance == null) {
                _Instance = FindObjectOfType<T>();

                if (_Instance == null) {
                    _Instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
            }
            return _Instance;
        }
    }

    GameObject _MyGameObject;
    Transform _MyTransform;

    /// <summary>
    /// Cache gameObject.
    /// </summary>
    protected GameObject _GameObject
    {
        get
        {
            if (_MyGameObject == null) {
                _MyGameObject = this.gameObject;
            }
            return _MyGameObject;
        }
    }

    /// <summary>
    /// Cache transform.
    /// </summary>
    protected Transform _Transform
    {
        get
        {
            if (_MyTransform == null) {
                _MyTransform = this.transform;
            }
            return _MyTransform;
        }
    }

    /// <summary>
    /// Call from override Awake method in inheriting classes.
    /// Example : protected override void Awake () { base.Awake (); }
    /// </summary>
    protected virtual void Awake ()
    {
        if (this != Instance) {
            GameObject obj = this.gameObject;
            Destroy(this);
            Destroy(obj);
            return;
        }
    }
}
