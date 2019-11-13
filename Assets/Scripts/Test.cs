using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	void Awake ()
    {
        MessageCenter.Register<int>(MsgId.MsgA, OnMsgA);
        MessageCenter.Register<float, string>(MsgId.MsgB, OnMsgB);
    }

    void OnDestroy()
    {
        MessageCenter.UnRegister<int>(MsgId.MsgA, OnMsgA); // TODO 可以由 UIManager.CloseUI 自动执行
        MessageCenter.UnRegister<float, string>(MsgId.MsgB, OnMsgB);
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
        MessageCenter.SendMessage(MsgId.MsgA, 5);

        yield return new WaitForSeconds(1f);
        MessageCenter.PostMessage(MsgId.MsgA, 100);
        MessageCenter.PostMessage(MsgId.MsgB, true, 1);     // 报回调函数参数不匹配错误
        MessageCenter.PostMessage(MsgId.MsgB, 1.2f, "abc");

        yield return new WaitForSeconds(3.01f);
        Debug.Log("反注册 OnMsgA");
        MessageCenter.UnRegister<int>(MsgId.MsgA, OnMsgA);
        MessageCenter.PostMessage(MsgId.MsgA, 999);         // 不会再执行
        MessageCenter.SendMessage(MsgId.MsgB, 246f, "大好き");
        yield return new WaitForSeconds(1.2f);

        Debug.LogFormat("移除对象 {0} 的所有回调", this.name);
        MessageCenter.UnRegisterOfObj(this);
        // 以下都不会执行
        MessageCenter.SendMessage(MsgId.MsgA, 666);
        MessageCenter.SendMessage(MsgId.MsgB, 2f, "Misaka");

        MessageCenter.SendMessage(MsgId.MsgA, 123);
        MessageCenter.SendMessage(MsgId.MsgB, 5f, "Mikoto");
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
