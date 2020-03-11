using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text contextText;
    public Button confirmBtn;

    // Start is called before the first frame update
    void Start()
    {
           
        confirmBtn.onClick.AddListener(() => 
        {
            gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setContent(string content)
    {
        contextText.text = content;
    }
}
