using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {


	void Awake ()
    {
        MessageCenter.Register(MsgTypeVar.MsgA, OnMsgA);
        MessageCenter.Register(MsgTypeVar.MsgB, OnMsgB);
    }

    void OnDestroy()
    {
        MessageCenter.UnRegister(MsgTypeVar.MsgA, OnMsgA); // TODO 可以由 UIManager.CloseUI 自动执行
        MessageCenter.UnRegister(MsgTypeVar.MsgB, OnMsgB);
    }

    void OnEnable()
    {
        StartCoroutine(coTestMessage());
        StartCoroutine(coUpdateMessageCenter());
    }
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    void OnMsgA(int val)
    {
        Debug.LogFormat("OnMsgA:{0}, time:{1}", val, Time.time);
    }

    void OnMsgB(float f, string str)
    {
        Debug.LogFormat("OnMsgB:{0}, {1}, time:{2}", f, str, Time.time);
    }

    IEnumerator coTestMessage()
    {
        MessageCenter.SendMessage(MsgTypeVar.MsgA, 5);

        yield return new WaitForSeconds(1f);
        MessageCenter.PostMessage(MsgTypeVar.MsgA, 100);
        //MessageCenter.PostMessage(MsgTypeVar.MsgB, true, 1);      // 编译错误
        MessageCenter.PostMessage(MsgTypeVar.MsgB, 1.2f, "abc");

        yield return new WaitForSeconds(3.01f);
        Debug.Log("反注册 OnMsgA");
        MessageCenter.UnRegister(MsgTypeVar.MsgA, OnMsgA);
        MessageCenter.PostMessage(MsgTypeVar.MsgA, 999);            // 不会再执行
        MessageCenter.SendMessage(MsgTypeVar.MsgB, 246f, "大好き");
        yield return new WaitForSeconds(1.2f);

        Debug.LogFormat("移除对象 {0} 的所有回调", this.name);
        MessageCenter.UnRegisterOfObj(this);
        // 以下都不会执行
        MessageCenter.SendMessage(MsgTypeVar.MsgA, 666);
        MessageCenter.SendMessage(MsgTypeVar.MsgB, 2f, "Misaka");

        MessageCenter.SendMessage(MsgTypeVar.MsgA, 123);
        MessageCenter.SendMessage(MsgTypeVar.MsgB, 5f, "Mikoto");
    }

    /// <summary>
    /// 故意以非常慢的速度执行异步消息回调
    /// </summary>
    /// <returns></returns>
    IEnumerator coUpdateMessageCenter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            MessageCenter.Update();
        }
    }
}
