/*
 * Author: Misaka Mikoto
 * Url:    https://github.com/easy66/MsgCenter
 * Desc:   消息派发中心
 * 
 * 使用方式：
 *  1. 在 MsgDef 中定义消息枚举和回调函数类型
 *  2. 使用 Register 注册回调
 *  3. SendMsg 派发消息
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MsgCenter
{
    static Dictionary<MsgId, List<Delegate>> s_dicActions = new Dictionary<MsgId, List<Delegate>>();
    static Dictionary<MsgId, Type> s_dicType = new Dictionary<MsgId, Type>();
    /// <summary>
    /// 记录对象注册了哪些消息，以便快速移除指定对象所有回调
    /// </summary>
    static Dictionary<object, List<MsgId>> s_dicObjs = new Dictionary<object, List<MsgId>>();

    static MsgCenter()
    {
        MsgTypeBinder.BindTypes(s_dicType);
    }

    public static void Register<T>(MsgId msgId, Action<T> callback)
    {
        DoRegister(msgId, callback, typeof(Action<T>));
    }

    public static void Register<T1, T2>(MsgId msgId, Action<T1, T2> callback)
    {
        DoRegister(msgId, callback, typeof(Action<T1, T2>));
    }

    public static void UnRegister<T>(MsgId msgId, Action<T> callback)
    {
        // TODO
    }

    public static void UnRegister<T1, T2>(MsgId msgId, Action<T1, T2> callback)
    {
        // TODO
    }

    public static void UnRegisterOfObj(object obj)
    {
        // TODO
    }

    /// <summary>
    /// 将函数体移动到 DoRegister 以避免 Register 泛型代码膨胀
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="callback"></param>
    /// <param name="cbType"></param>
    private static void DoRegister(MsgId msgId, Delegate callback, Type cbType)
    {
        Type type;
        if (s_dicType.TryGetValue(msgId, out type))
        {
            if (type != cbType)
            {
                Debug.LogErrorFormat("错误的消息回调函数参数类型:Msg:{0}, 期望类型:{1}", msgId, cbType.Name);
                return;
            }

            List<Delegate> lst;
            if (!s_dicActions.TryGetValue(msgId, out lst))
            {
                lst = new List<Delegate>();
                s_dicActions.Add(msgId, lst);
            }

            if (!lst.Contains(callback))
            {
                lst.Add(callback);

                object obj = callback.Target;
                if (obj != null)
                {
                    List<MsgId> idLst;
                    if (!s_dicObjs.TryGetValue(obj, out idLst))
                    {
                        idLst = new List<MsgId>();
                        s_dicObjs.Add(obj, idLst);
                    }

                    idLst.Add(msgId);
                }
            }
        }
    }

    public static void SendMsg<T>(MsgId msgId, T data)
    {
        List<Delegate> lst = FindCBList(msgId, typeof(Action<T>));
        if (lst != null && lst.Count > 0)
        {
            foreach (var callback in lst)
            {
                (callback as Action<T>)(data);
            }
        }
    }

    public static void SendMsg<T1, T2>(MsgId msgId, T1 data1, T2 data2)
    {
        List<Delegate> lst = FindCBList(msgId, typeof(Action<T1, T2>));
        if (lst != null && lst.Count > 0)
        {
            foreach (var callback in lst)
            {
                (callback as Action<T1, T2>)(data1, data2);
            }
        }
    }

    /// <summary>
    /// 将查找代码移动到 FindCBList 以避免 SendMsg 泛型代码膨胀
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="cbType"></param>
    /// <returns></returns>
    private static List<Delegate> FindCBList(MsgId msgId, Type cbType)
    {
        List<Delegate> lst = null;
        Type type;
        if (s_dicType.TryGetValue(msgId, out type))
        {
            if (type == cbType)
            {
                s_dicActions.TryGetValue(msgId, out lst);
            }
        }
        return lst;
    }

}