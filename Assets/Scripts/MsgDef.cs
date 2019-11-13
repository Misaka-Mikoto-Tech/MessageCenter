using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息类型定义
/// </summary>
public enum MsgId
{
    /// <summary>
    /// 测试消息A, 参数 (int)
    /// </summary>
    MsgA,
    /// <summary>
    /// 测试消息B, 参数 (float, string)
    /// </summary>
    MsgB,
}

public static class MsgTypeBinder
{
    public static void BindTypes()
    {
        MessageCenter.BindType(MsgId.MsgA, typeof(Action<int>));
        MessageCenter.BindType(MsgId.MsgB, typeof(Action<float, string>));
    }
}




        


