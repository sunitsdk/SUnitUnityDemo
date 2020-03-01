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
using UnityEngine.UI;

public class ScrollRectSnap_CS : MonoBehaviour 
{
	// Public Variables
	public RectTransform panel;	// To hold the ScrollPanel
	public GameObject[] bttn;
	public RectTransform center;	// Center to compare the distance for each button

    public float speed;

	// Private Variables
	public float[] distance;	// All buttons' distance to the center
	public float[] distReposition;
	private bool dragging = false;	// Will be true, while we drag the panel
	private int bttnDistance;	// Will hold the distance between the buttons
	private int minButtonNum;	// To hold the number of the button, with smallest distance to center
	private int bttnLength;

    public static int select_plane;

	void Start()
	{
		bttnLength = bttn.Length;
		distance = new float[bttnLength];
		distReposition = new float[bttnLength];

        setID_Plane();
		// Get distance between buttons
		bttnDistance  = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
	}

    void setID_Plane()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            bttn[i].GetComponent<PlaneInformation>().id_plane = i;
        }
    }

	void Update()
	{
		for (int i = 0; i < bttn.Length; i++)
		{
			distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
			distance[i] = Mathf.Abs(distReposition[i]);

		}
	
		float minDistance = Mathf.Min(distance);	// Get the min distance

		for (int a = 0; a < bttn.Length; a++)
		{
			if (minDistance == distance[a])
			{
                select_plane = a;
                minButtonNum = a;
                bttn[a].GetComponent<PlaneInformation>().enabled = true;
            }
            else
            {
                bttn[a].GetComponent<PlaneInformation>().enabled = false;
            }

		}

		if (!dragging)
		{
            //	LerpToBttn(minButtonNum * -bttnDistance);
            LerpToBttn (-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
            

        }
	}

	void LerpToBttn(float position)
	{
        float newX = Mathf.Lerp (panel.anchoredPosition.x, position, Time.deltaTime * speed);
        Vector2 newPosition = new Vector2 (newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
        //panel.anchoredPosition = new Vector2(position, panel.anchoredPosition.y);

    }

	public void StartDrag()
	{
		dragging = true;
	}

	public void EndDrag()
	{
		dragging = false;
	}

}













