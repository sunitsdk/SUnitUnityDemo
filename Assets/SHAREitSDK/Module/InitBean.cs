using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InitBean
{
    private SHAREitEnv env;
    private string mainHostClass;
    private bool isMainProcess;
    private bool isEUAgreed;
    private string channel;

    public SHAREitEnv Env
    {
        get { return env; }
        set { env = value; }
    }


    public string MainHostClass
    {
        get { return mainHostClass; }
        set { mainHostClass = value; }
    }


    public bool IsMainProcess
    {
        get { return isMainProcess; }
        set { isMainProcess = value; }
    }


    public bool IsEUAgreed
    {
        get { return isEUAgreed; }
        set { isEUAgreed = value; }
    }


    public string Channel
    {
        get { return channel; }
        set { channel = value; }
    }
}
