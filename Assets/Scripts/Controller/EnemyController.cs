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

[RequireComponent(typeof(PlaneDie))]
public class EnemyController : MonoBehaviour
{

    public const string NAME_PLAYER = "Player";
    public const string NAME_PLAYER_BULLET = "PlayerBullet";
    public const string NAME_PLAYER_HOMING = "HomingPlayer";

    const string ANIM_DAMAGE_TRIGGER = "Damage";
    const string DESTROY_AREA = "DestroyArea";

    public int _Point = 100;
    public bool _UseStop = false;
    public float _StopPoint = 2f;
    public float speedMove;
    public float speedTurn;
    public bool canMove;
    public bool fly_up;

    public Color color;

    private PlaneDie planeDie;
    private Rigidbody2D rigidbody2D;
    public Anchor_Plane anchor;
    public TypePath typePath;

    private Vector2 saveScale;
    private float offset;
    public float _Hp = 1;
    private GameManager _gameManager;
    void Start()
    {
        offset = Time.deltaTime * speedMove;
        planeDie = GetComponent<PlaneDie>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager = FindObjectOfType<GameManager>();

        saveScale = transform.localScale;

        if (typePath == TypePath.none && !_UseStop)
        {
            Move_stop(transform.up.normalized * -offset * 10);
        }

        if (_UseStop)
        {
            typePath = TypePath.none;
            Move_stop(transform.up.normalized * -offset*10);
        }
    }

    void FixedUpdate()
    {
        offset = Time.deltaTime * speedMove;

        if (_UseStop)
        {
            if (transform.position.y < _StopPoint)
            {
                rigidbody2D.velocity = Vector2.zero;
                _UseStop = false;
            }
        }

        if (canMove && !_UseStop && typePath != TypePath.none)
            Move();

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.name.Contains(NAME_PLAYER_BULLET))
        {
            StartCoroutine(getHit());
            BulletController bullet = c.transform.parent.GetComponent<BulletController>();

            UbhObjectPool.Instance.ReleaseGameObject(c.transform.parent.gameObject);

            _Hp = _Hp - bullet._Power;
            enemyDie();
        }

        if (c.tag.Equals(NAME_PLAYER_HOMING))
        {
            StartCoroutine(getHit());

            BulletController bullet = c.GetComponent<BulletController>();
            Destroy(c.gameObject);

            _Hp = _Hp - bullet._Power;
            enemyDie();
        }

        if (c.name.Contains(DESTROY_AREA))
        {
            StartCoroutine(startShot());
        }
    }

    IEnumerator startShot()
    {
        yield return new WaitForSeconds(1);
        UbhShotCtrl shotCtrl = GetComponent<UbhShotCtrl>();
        if (shotCtrl != null)
            shotCtrl.StartShotRoutine();
    }

    void OnDisable()
    {
        _gameManager.countEnemy++;
    }

    void enemyDie()
    {

        if (_Hp <= 0)
        {
            planeDie.Explosion();
            _gameManager.addPoint(_Point);
            _gameManager.countEnemyDie++;
            Destroy(gameObject);
        }
        else {
        }
    }

    IEnumerator getHit()
    {
        StopCoroutine("getHit");
        SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            color = Color.red;
            sr.color = color;
            yield return new WaitForSeconds(0.1f);
            color = Color.white;
            sr.color = color;
        }
    }

    public void Move_stop(Vector2 direction)
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0,-90));
        rigidbody2D.velocity = direction * speedMove;
    }

    public void Move()
    {
        int direc;
        if (fly_up)
            direc = -1;
        else
            direc = 1;

        if (typePath == TypePath.curve_up)
        {
            transform.localScale = new Vector3(direc * saveScale.x, saveScale.y, 0);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z - speedTurn));
            transform.position += direc * transform.right * offset;
        }

        if (typePath == TypePath.curve_down)
        {
            transform.localScale = new Vector3(-direc * saveScale.x, saveScale.y, 0);
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z + speedTurn));
            transform.position -= direc * transform.right * offset;
        }

        if (typePath == TypePath.line)
        {
            if (anchor == Anchor_Plane.top)
            {
                transform.localScale = new Vector3(-direc * saveScale.x, saveScale.y, 0);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                transform.position -= transform.right * offset;
            }

            if (anchor == Anchor_Plane.right)
            {
                transform.localScale = new Vector3(saveScale.x, saveScale.y, 0);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 180));
                transform.position += transform.right * offset;
            }

            if (anchor == Anchor_Plane.left)
            {
                transform.localScale = new Vector3(saveScale.x, saveScale.y, 0);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                transform.position += transform.right * offset;
            }

            if (anchor == Anchor_Plane.botton_left)
            {
                transform.localScale = new Vector3(direc * saveScale.x, saveScale.y, 0);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 60));
                transform.position += transform.right * offset;
            }

            if (anchor == Anchor_Plane.botton_right)
            {
                transform.localScale = new Vector3(direc * saveScale.x, saveScale.y, 0);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 120));
                transform.position += transform.right * offset;
            }

            if (anchor == Anchor_Plane.top_left)
            {
                transform.localScale = new Vector3(direc * saveScale.x, saveScale.y, 0);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -60));
                transform.position += transform.right * offset;
            }

            if (anchor == Anchor_Plane.top_right)
            {
                transform.localScale = new Vector3(direc * saveScale.x, saveScale.y, 0);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -120));
                transform.position += transform.right * offset;
            }
        }

    }

}
