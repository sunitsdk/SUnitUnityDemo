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

public enum Anchor_Plane { top, top_left, top_right, right, left, botton_left, botton_right };
public enum TypePath {none,line,curve_up,curve_down};
public class AnchorPlane : MonoBehaviour {

    private Vector2 top = new Vector2(0, -3);
    private Vector2 top_left = new Vector2(-5, -3);
    private Vector2 top_right = new Vector2(5, -3);
    private Vector2 left = new Vector2(-5, -10);
    private Vector2 right = new Vector2(5, -10);
    private Vector2 botton_left = new Vector2(-5, -17);
    private Vector2 botton_right = new Vector2(5, -17);

    private Vector2 size;
    private Vector2 positionCam;

    public Anchor_Plane anchor_start;
    public TypePath type_Path;
    public float timeDelay = 0.5f;

    public float _Hp = 1;
    public int _Point = 100;
    public bool _UseStop = false;
    public float _StopPoint = 2f;
    public float speedMove;
    public float speedTurn;
    public bool fly_up;
    public bool isBoss;
    public float hpBoss_add_level;

    // Use this for initialization
    void Awake () {
        size = new Vector2(Mathf.Abs(GameSetting.sizeCam.x / 2)+2, Mathf.Abs(GameSetting.sizeCam.y / 2)+2);
        positionCam = GameSetting.positionCam;

        if (type_Path == TypePath.none)
            timeDelay = 0;

        top = new Vector2(0, positionCam.y + size.y);
        top_left = new Vector2(positionCam.x - size.x, positionCam.y + size.y);
        top_right = new Vector2(positionCam.x + size.x, positionCam.y + size.y);
        left = new Vector2(positionCam.x - size.x, positionCam.y);
        right = new Vector2(positionCam.x + size.x, positionCam.y);
        botton_left = new Vector2(-5, positionCam.y - size.y);
        botton_right = new Vector2(5, positionCam.y - size.y);

        transform.position = resultPosition();
    }

    Vector2 resultPosition()
    {
        switch (anchor_start)
        {
            case Anchor_Plane.top:
                StartCoroutine("settingPlane");
                return top;
            case Anchor_Plane.top_left:
                StartCoroutine("settingPlane");
                return top_left;
            case Anchor_Plane.top_right:
                StartCoroutine("settingPlane");
                return top_right;
            case Anchor_Plane.left:
                StartCoroutine("settingPlane");
                return left;
            case Anchor_Plane.right:
                StartCoroutine("settingPlane");
                return right;
            case Anchor_Plane.botton_left:
                StartCoroutine("settingPlane");
                return botton_left;
            case Anchor_Plane.botton_right:
                StartCoroutine("settingPlane");
                return botton_right;
        }

        return Vector2.zero;
    }

    IEnumerator settingPlane()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            EnemyController enemy = transform.GetChild(i).GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.anchor = anchor_start;
                enemy.typePath = type_Path;
                if (!isBoss)
                    enemy._Hp = _Hp;
                else
                {
                    enemy._Hp = _Hp + GameSetting.level * hpBoss_add_level;
                }
                enemy._Point = _Point;
                enemy._StopPoint = _StopPoint;
                enemy._UseStop = _UseStop;
                enemy.speedMove = speedMove;
                enemy.speedTurn = speedTurn;
                enemy.fly_up = fly_up;
            }
        }

        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            EnemyController enemy = transform.GetChild(i).GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.canMove = true;
                yield return new WaitForSeconds(timeDelay);
            }
        }
    }
}
