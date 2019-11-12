using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        MsgCenter.Register<int>(MsgId.MsgA, OnMsgA);
        MsgCenter.Register<float, string>(MsgId.MsgB, OnMsgB);
    }

    void OnDestroy()
    {
        MsgCenter.UnRegister<int>(MsgId.MsgA, OnMsgA); // TODO 可以由 UIManager.CloseUI 自动执行
        MsgCenter.UnRegister<float, string>(MsgId.MsgB, OnMsgB);
    }

    void OnEnable()
    {
        MsgCenter.SendMsg(MsgId.MsgA, 5);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMsgA(int val)
    {
        Debug.LogFormat("OnMsgA:{0}", val);
    }

    void OnMsgB(float f, string str)
    {
        Debug.LogFormat("OnMsgB:{0}, {1}", f, str);
    }
}
