using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseBean
{
    private string actionType;

    public string ActionType
    {
        get { return actionType; }
        set { actionType = value; }
    }
}
