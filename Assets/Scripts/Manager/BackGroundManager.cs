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

public class BackGroundManager : MonoBehaviour {

    public Animator ani;
    public GameManager _gameManager;

    // Use this for initialization
    void Start () {
        transform.position = GameSetting.positionCam;
        float height = GameSetting.sizeCam.y;
        float width = GameSetting.sizeCam.x;
        transform.localScale = new Vector3((width+1) / 10, 1.0f, -(height + 1) / 10);
        ani = GetComponent<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update () {
        if (_gameManager.gameState != GameState.Play)
        {
            if (ani.enabled == true)
                ani.enabled = false;
        }
        else
        {
            if (ani.enabled == false)
                ani.enabled = true;
        }

    }

}
