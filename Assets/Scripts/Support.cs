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

[RequireComponent(typeof(Rigidbody2D))]
public class Support : MonoBehaviour {

    public GameManager _gameManager;

    private float minY;
    private float maxY;
    private float minX;
    private float maxX;
    private Rigidbody2D rig;

    public float speedMove;
    private bool check;

    private TypeSupport typeSupport;

    public Sprite add_HP;
    public Sprite homing;
    public Sprite add_Dame;
    public Sprite add_Plane;
    public Sprite lv_up_bullet;
    public Sprite shield;
    public Sprite add_bullet_up;
    public Sprite add_bullet_left_right;

    // Use this for initialization
    void Start () {
        minX = GameSetting.positionCam.x - Mathf.Abs(GameSetting.sizeCam.x / 2);
        minY = GameSetting.positionCam.y - Mathf.Abs(GameSetting.sizeCam.y / 2);

        maxX = GameSetting.positionCam.x + Mathf.Abs(GameSetting.sizeCam.x / 2);
        maxY = GameSetting.positionCam.y + Mathf.Abs(GameSetting.sizeCam.y / 2);
   
    }

    void OnEnable()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.isKinematic = true;
        rig.velocity = new Vector2(speedMove, speedMove);

        setTypeSupport();
    }

    void OnDisable()
    {
        if (_gameManager.gameState == GameState.Play)
            Invoke("OnSupport", Random.Range(5, 20));
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void OnSupport()
    {
        gameObject.SetActive(true);
    }

    void move()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-rig.velocity.x, rig.velocity.y);
        }

        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-rig.velocity.x, rig.velocity.y);
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(rig.velocity.x, -rig.velocity.y);
        }

        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.2f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(rig.velocity.x, -rig.velocity.y);
        }
    }

    void setTypeSupport()
    {
        int i = Random.Range(0, 8);
        switch (i)
        {
            case 0:
                typeSupport = TypeSupport.hp;
                GetComponent<SpriteRenderer>().sprite = add_HP;
                break;
            case 1:
                typeSupport = TypeSupport.plane;
                GetComponent<SpriteRenderer>().sprite = add_Plane;
                break;
            case 2:
                typeSupport = TypeSupport.shield;
                GetComponent<SpriteRenderer>().sprite = shield;
                break;
            case 3:
                typeSupport = TypeSupport.up_bullet;
                GetComponent<SpriteRenderer>().sprite = lv_up_bullet;
                break;
            case 4:
                typeSupport = TypeSupport.homing;
                GetComponent<SpriteRenderer>().sprite = homing;
                break;
            case 5:
                typeSupport = TypeSupport.add_dame;
                GetComponent<SpriteRenderer>().sprite = add_Dame;
                break;
            case 6:
                typeSupport = TypeSupport.add_bullet_up;
                GetComponent<SpriteRenderer>().sprite = add_bullet_up;
                break;
            case 7:
                typeSupport = TypeSupport.add_bullet_left_right;
                GetComponent<SpriteRenderer>().sprite = add_bullet_left_right;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (_gameManager.gameState == GameState.Play)
            {
                gameObject.SetActive(false);
                col.GetComponent<SupportManager>().setTypeSupport(typeSupport);
            }
        }
    }
}
