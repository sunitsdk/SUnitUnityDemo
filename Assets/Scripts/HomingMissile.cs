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

public class HomingMissile : MonoBehaviour {

    public string targetTag;

	public GameObject target;
    public float distanceAttack;
	public float speedMove;

    private GameObject[] Targets;

    // Use this for initialization
    void Start () {
        //GetComponent<Rigidbody2D> ().gravityScale = 1;
    }
	
	// Update is called once per frame
	void Update () {

        if (target == null)
        {
            findTarget();
        }

        if (target != null)
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;

            Vector2 desiredVelocity = direction * speedMove;
            Vector2 steeringForce = desiredVelocity - GetComponent<Rigidbody2D>().velocity;

            if (GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(steeringForce.x, steeringForce.y));
            }
            else
                GetComponent<Rigidbody2D>().AddForce(steeringForce);
        }
       


        float angleRad = Mathf.Atan2 (GetComponent<Rigidbody2D> ().velocity.y,
		                              GetComponent<Rigidbody2D> ().velocity.x);
		
		float angleDeg = (180 / Mathf.PI) * angleRad;
		transform.rotation = Quaternion.Euler (0, 0, angleDeg - 90);

	}

    void findTarget()
    {
        Targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (Targets.Length != 0)
        {
            foreach (GameObject Target in Targets)
            {
                if (Vector2.Distance(transform.position, Target.transform.position) < distanceAttack)
                {
                    this.target = Target;
                }
                else {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);
                }
            }
        }else
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3);

    }
	
}
