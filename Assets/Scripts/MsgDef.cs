using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MsgId
{
    MsgA,
    MsgB,
    MsgC,
}

/// <summary>
/// 消息类型变量类, 所有定义的消息枚举都需要在此定义一个指定类型的变量
/// </summary>
public static class MsgTypeVar
{
    public static readonly MsgType<int>                 MsgA = new MsgType<int>(MsgId.MsgA);
    public static readonly MsgType<float, string>       MsgB = new MsgType<float, string>(MsgId.MsgB);
    public static readonly MsgType<string>              MsgC = new MsgType<string>(MsgId.MsgC);
}






#region MsgType
interface IMsgType { }
public struct MsgType : IMsgType
{
    public static readonly Type type = typeof(Action);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1> : IMsgType
{
    public static readonly Type type = typeof(Action<T1>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2, T3> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2, T3>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2, T3, T4> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2, T3, T4>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2, T3, T4, T5> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2, T3, T4, T5>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2, T3, T4, T5, T6> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2, T3, T4, T5, T6>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2, T3, T4, T5, T6, T7> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2, T3, T4, T5, T6, T7>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2, T3, T4, T5, T6, T7, T8> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
public struct MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IMsgType
{
    public static readonly Type type = typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>);
    public MsgId msgId;

    public MsgType(MsgId msgId)
    {
        this.msgId = msgId;
        MessageCenter.BindType(msgId, type);
    }
}
#endregion
