/*
 * Author: Misaka Mikoto
 * Url:    https://github.com/easy66/MessageCenter
 * Desc:   消息派发中心
 * 
 * 使用方式：
 *  1. 在 MsgDef 中定义消息枚举和回调函数类型
 *  2. 使用 Register 注册回调
 *  3. SendMessage/PostMessage 派发消息
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 消息中心
/// </summary>
public class MessageCenter
{
    /// <summary>
    /// 每调用一次 Update 执行的异步消息数量
    /// </summary>
    public static int Invoke_Count_Per_Update = 1;

    public struct CallbackInfo
    {
        public MsgId            msgId;
        public List<Delegate>   cbLst;
        //public List<object>     static_objLst; // 静态回调关联的 object, 不打算启用，原因是静态回调不属于任何对象，强行关联会造成意义混乱
        public Type             cbType;
    }

    /// <summary>
    /// 记录每个消息Id的回调信息，每个消息的回调列表中不会有重复的回调
    /// </summary>
    private static Dictionary<MsgId, CallbackInfo> s_dicCallbacks = new Dictionary<MsgId, CallbackInfo>(MsgIdComparer.instance);

    /// <summary>
    /// 记录对象注册了哪些消息，以便快速移除指定对象所有回调(一个对象不允许多次订阅同一个消息)
    /// </summary>
    /// <remarks>List 无法设置Comparer, 所以枚举类型会有装箱问题, 但HashSet又太重,因此使用int类型</remarks>
    private static Dictionary<object, List<int>> s_dicObjs = new Dictionary<object, List<int>>(RefEqualComparer.instance);
    /// <summary>
    /// PostMessage 执行后缓存的执行器，用于延迟执行
    /// </summary>
    private static Queue<IMsgInvoker> s_queueInvokers = new Queue<IMsgInvoker>();


    public static void BindType(MsgId msgId, Type cbType)
    {
        s_dicCallbacks.Add(msgId, new CallbackInfo() { msgId = msgId, cbType = cbType });
    }

    #region Register
    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register(MsgType msgType, Action callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1>(MsgType<T1> msgType, Action<T1> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2>(MsgType<T1, T2> msgType, Action<T1, T2> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2, T3>(MsgType<T1, T2, T3> msgType, Action<T1, T2, T3> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2, T3, T4>(MsgType<T1, T2, T3, T4> msgType, Action<T1, T2, T3, T4> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2, T3, T4, T5>(MsgType<T1, T2, T3, T4, T5> msgType, Action<T1, T2, T3, T4, T5> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2, T3, T4, T5, T6>(MsgType<T1, T2, T3, T4, T5, T6> msgType, Action<T1, T2, T3, T4, T5, T6> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2, T3, T4, T5, T6, T7>(MsgType<T1, T2, T3, T4, T5, T6, T7> msgType, Action<T1, T2, T3, T4, T5, T6, T7> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <typeparam name="T8"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2, T3, T4, T5, T6, T7, T8>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8> msgType, Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <typeparam name="T8"></typeparam>
    /// <typeparam name="T9"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9> msgType, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册消息回调，msgType 定义在 MsgTypeVar 内
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <typeparam name="T8"></typeparam>
    /// <typeparam name="T9"></typeparam>
    /// <typeparam name="T10"></typeparam>
    /// <param name="msgType"></param>
    /// <param name="callback"></param>
    public static void Register<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> msgType, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
    {
        RegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 注册回调非泛型重载
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="callback"></param>
    /// <param name="cbType"></param>
    public static void RegisterWithId(MsgId msgId, Delegate callback)
    {
        // 记录此对象注册的协议Id(并且不允许一个对象重复订阅同一个消息)
        object obj = callback.Target;
        if (!ReferenceEquals(obj, null))
        {
            List<int> idLst;
            if (s_dicObjs.TryGetValue(obj, out idLst))
            {
                if (idLst.Contains((int)msgId))
                {
                    Debug.LogErrorFormat("对象 {0} 不允许重复订阅同一个消息Id:{1}", obj.ToString(), msgId);
                    return;
                }
            }
            else
            {
                idLst = new List<int>();
                s_dicObjs.Add(obj, idLst);
            }

            idLst.Add((int)msgId);
        }


        CallbackInfo cbInfo;
        if (s_dicCallbacks.TryGetValue(msgId, out cbInfo))
        {
            Type cbType__ = callback.GetType();
            if (cbInfo.cbType != cbType__)
            {
                Debug.LogErrorFormat("错误的消息回调函数参数类型:Msg:{0}, 期望类型:{1}", msgId, cbType__.Name);
                return;
            }

            List<Delegate> cbLst = cbInfo.cbLst;
            if (cbLst == null)
            {
                cbInfo.cbLst = cbLst = new List<Delegate>();
                s_dicCallbacks[msgId] = cbInfo;
            }

            if (!cbLst.Contains(callback))
                cbLst.Add(callback);
        }
        else
        {
            Debug.LogErrorFormat("未注册消息 {0}", msgId);
        }
    }
    #endregion


    #region UnRegister
    /// <summary>
    /// 移除指定对象订阅的所有回调
    /// </summary>
    /// <param name="obj"></param>
    public static void UnRegisterOfAllObj(object obj)
    {
        if (ReferenceEquals(obj, null))
            return;

        List<int> lst;
        if(s_dicObjs.TryGetValue(obj, out lst))
        {
            CallbackInfo cbInfo;
            for (int i = 0, imax = lst.Count; i < imax; i++)
            {
                MsgId id = (MsgId)lst[i];
                if(s_dicCallbacks.TryGetValue(id, out cbInfo))
                {
                    List<Delegate> cbLst = cbInfo.cbLst;
                    if(cbLst != null)
                    {
                        for(int j = cbLst.Count - 1; j >= 0; j--)
                        {
                            Delegate @delegate = cbLst[j];
                            if(ReferenceEquals(@delegate.Target, obj))
                            {
                                cbLst.RemoveAt(j);
                            }
                        }
                    }
                }
            }

            s_dicObjs.Remove(obj);
        }
    }

    public static void UnRegister(MsgType msgType, Action callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1>(MsgType<T1> msgType, Action<T1> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2>(MsgType<T1, T2> msgType, Action<T1, T2> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2, T3>(MsgType<T1, T2, T3> msgType, Action<T1, T2, T3> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2, T3, T4>(MsgType<T1, T2, T3, T4> msgType, Action<T1, T2, T3, T4> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2, T3, T4, T5>(MsgType<T1, T2, T3, T4, T5> msgType, Action<T1, T2, T3, T4, T5> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2, T3, T4, T5, T6>(MsgType<T1, T2, T3, T4, T5, T6> msgType, Action<T1, T2, T3, T4, T5, T6> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2, T3, T4, T5, T6, T7>(MsgType<T1, T2, T3, T4, T5, T6, T7> msgType, Action<T1, T2, T3, T4, T5, T6, T7> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2, T3, T4, T5, T6, T7, T8>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8> msgType, Action<T1, T2, T3, T4, T5, T6, T7, T8> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2, T3, T4, T5, T6, T7, T8, T9>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9> msgType, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    public static void UnRegister<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> msgType, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> callback)
    {
        UnRegisterWithId(msgType.msgId, callback);
    }

    /// <summary>
    /// 移除回调非泛型重载
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="callback"></param>
    public static void UnRegisterWithId(MsgId msgId, Delegate callback)
    {
        if (callback == null)
            return;

        // 将回调从消息列表的回调列表中移除
        CallbackInfo cbInfo;
        if (s_dicCallbacks.TryGetValue(msgId, out cbInfo))
        {
            if(callback.GetType() != cbInfo.cbType)
            {
                Debug.LogErrorFormat("取消注册时回调类型 {0} 与期望类型 {1} 不符", callback.GetType(), cbInfo.cbType);
                return;
            }

            List<Delegate> cbLst = cbInfo.cbLst;
            if (cbLst != null)
                cbLst.Remove(callback);
        }

        // 将回调从对象消息列表中移除
        object obj = callback.Target;
        if(!ReferenceEquals(obj, null))
        {
            List<int> msgLst;
            if (s_dicObjs.TryGetValue(obj, out msgLst))
            {
                msgLst.Remove((int)msgId); // 列表是不重复的
                if(msgLst.Count == 0)
                    s_dicObjs.Remove(obj);
            }
        }
    }
    #endregion


    #region SendMessage
    public static void SendMessage(MsgType msgType)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action)();
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1>(MsgType<T1> msgType, T1 data)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1>)(data);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2>(MsgType<T1, T2> msgType, T1 data1, T2 data2)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2>)(data1, data2);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2, T3>(MsgType<T1, T2, T3> msgType, T1 data1, T2 data2, T3 data3)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2, T3>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2, T3>)(data1, data2, data3);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2, T3, T4>(MsgType<T1, T2, T3, T4> msgType, T1 data1, T2 data2, T3 data3, T4 data4)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2, T3, T4>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2, T3, T4>)(data1, data2, data3, data4);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2, T3, T4, T5>(MsgType<T1, T2, T3, T4, T5> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2, T3, T4, T5>)(data1, data2, data3, data4, data5);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2, T3, T4, T5, T6>(MsgType<T1, T2, T3, T4, T5, T6> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2, T3, T4, T5, T6>)(data1, data2, data3, data4, data5, data6);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2, T3, T4, T5, T6, T7>(MsgType<T1, T2, T3, T4, T5, T6, T7> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6, T7>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2, T3, T4, T5, T6, T7>)(data1, data2, data3, data4, data5, data6, data7);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2, T3, T4, T5, T6, T7, T8>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2, T3, T4, T5, T6, T7, T8>)(data1, data2, data3, data4, data5, data6, data7, data8);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2, T3, T4, T5, T6, T7, T8, T9>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, T9 data9)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>)(data1, data2, data3, data4, data5, data6, data7, data8, data9);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    public static void SendMessage<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, T9 data9, T10 data10)
    {
        List<Delegate> lst = FindCbList(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>));
        if (lst == null)
            return;

        for (int i = 0, imax = lst.Count; i < imax; i++)
        {
            try
            {
                (lst[i] as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)(data1, data2, data3, data4, data5, data6, data7, data8, data9, data10);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
    }

    /// <summary>
    /// SendMessage 的非泛型不定参数重载，注意：此方法性能比较差
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="args"></param>
    public static void SendMessage(MsgId msgId, params object[] args)
    {
        CallbackInfo cbInfo;
        if (s_dicCallbacks.TryGetValue(msgId, out cbInfo))
        {
            List<Delegate> lst = cbInfo.cbLst;
            if(lst != null)
            {
                for(int i = 0, imax = lst.Count; i < imax; i++)
                {
                    try
                    {
                        lst[i].DynamicInvoke(args);
                    }
                    catch (Exception ex)
                    {
                        LogException(ex);
                    }
                }
            }
        }
    }
    #endregion

    #region PostMessage
    /// <summary>
    /// 发送异步执行的消息
    /// </summary>
    /// <param name="msgId"></param>
    public static void PostMessage(MsgType msgType)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker(cbInfo)); // TODO 如果调用PostMessage非常频繁考虑对MsgInvoker进行缓存
    }

    public static void PostMessage<T1>(MsgType<T1> msgType, T1 data1)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1>(cbInfo, data1));
    }

    public static void PostMessage<T1, T2>(MsgType<T1, T2> msgType, T1 data1, T2 data2)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2>(cbInfo, data1, data2));
    }

    public static void PostMessage<T1, T2, T3>(MsgType<T1, T2, T3> msgType, T1 data1, T2 data2, T3 data3)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2, T3>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2, T3>(cbInfo, data1, data2, data3));
    }

    public static void PostMessage<T1, T2, T3, T4>(MsgType<T1, T2, T3, T4> msgType, T1 data1, T2 data2, T3 data3, T4 data4)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2, T3, T4>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2, T3, T4>(cbInfo, data1, data2, data3, data4));
    }

    public static void PostMessage<T1, T2, T3, T4, T5>(MsgType<T1, T2, T3, T4, T5> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2, T3, T4, T5>(cbInfo, data1, data2, data3, data4, data5));
    }

    public static void PostMessage<T1, T2, T3, T4, T5, T6>(MsgType<T1, T2, T3, T4, T5, T6> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2, T3, T4, T5, T6>(cbInfo, data1, data2, data3, data4, data5, data6));
    }

    public static void PostMessage<T1, T2, T3, T4, T5, T6, T7>(MsgType<T1, T2, T3, T4, T5, T6, T7> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6, T7>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2, T3, T4, T5, T6, T7>(cbInfo, data1, data2, data3, data4, data5, data6, data7));
    }

    public static void PostMessage<T1, T2, T3, T4, T5, T6, T7, T8>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2, T3, T4, T5, T6, T7, T8>(cbInfo, data1, data2, data3, data4, data5, data6, data7, data8));
    }

    public static void PostMessage<T1, T2, T3, T4, T5, T6, T7, T8, T9>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, T9 data9)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2, T3, T4, T5, T6, T7, T8, T9>(cbInfo, data1, data2, data3, data4, data5, data6, data7, data8, data9));
    }

    public static void PostMessage<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(MsgType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> msgType, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, T9 data9, T10 data10)
    {
        CallbackInfo cbInfo = FindCallbackInfo(msgType.msgId, typeof(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>));
        if (cbInfo.cbType == null)
            return;

        s_queueInvokers.Enqueue(new MsgInvoker<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(cbInfo, data1, data2, data3, data4, data5, data6, data7, data8, data9, data10));
    }

    /// <summary>
    /// PostMessage 的非泛型不定参数重载，注意：此方法性能比较差
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="args"></param>
    public static void PostMessage(MsgId msgId, params object[] args)
    {
        CallbackInfo cbInfo;
        if (s_dicCallbacks.TryGetValue(msgId, out cbInfo))
        {
            s_queueInvokers.Enqueue(new MsgInvokerDynamic(cbInfo, args));
        }
    }
    #endregion

    #region Find CallbackInfo/CallbackList
    /// <summary>
    /// 将查找代码移动到 FindCbList 以避免 SendMsg 泛型代码膨胀
    /// </summary>
    /// <param name="msgId"></param>
    /// <param name="cbType"></param>
    /// <returns></returns>
    private static List<Delegate> FindCbList(MsgId msgId, Type cbType)
    {
        List<Delegate> lst = null;
        CallbackInfo cbInfo;
        if (s_dicCallbacks.TryGetValue(msgId, out cbInfo))
        {
            if (cbInfo.cbType != cbType)
            {
                Debug.LogErrorFormat("错误的消息回调函数参数类型:Msg:{0}, 期望类型:{1}", msgId, cbType.FullName);
                return null;
            }

            lst = cbInfo.cbLst;
            if (lst != null && lst.Count == 0) // 让泛型方法减少一个判断
                lst = null;
        }
        return lst;
    }

    private static CallbackInfo FindCallbackInfo(MsgId msgId, Type cbType)
    {
        CallbackInfo cbInfo;
        if (s_dicCallbacks.TryGetValue(msgId, out cbInfo))
        {
            if (cbInfo.cbType != cbType)
            {
                Debug.LogErrorFormat("错误的消息回调函数参数类型:Msg:{0}, 期望类型:{1}", msgId, cbType.FullName);
                return new CallbackInfo();
            }
        }
        return cbInfo;
    }
    #endregion

    /// <summary>
    /// 执行异步消息
    /// </summary>
    public static void Update()
    {
        int invokeCount = 0;
        while(s_queueInvokers.Count > 0 && invokeCount < Invoke_Count_Per_Update)
        {
            IMsgInvoker invoker = s_queueInvokers.Dequeue();
            invoker.Invoke();
            invokeCount++;
        }
    }

    #region Comparer
    class MsgIdComparer : IEqualityComparer<MsgId>
    {
        public static MsgIdComparer instance = new MsgIdComparer();

        public bool Equals(MsgId x, MsgId y) { return (int)x == (int)y; }
        public int GetHashCode(MsgId obj) { return (int)obj; }
    }

    /// <summary>
    /// 由于继承自 UnityEngine.Object 的对象被重写了 operator equal, 因此可能出现多个对象被误判断为相等的问题
    /// </summary>
    class RefEqualComparer : IEqualityComparer<object>
    {
        public static RefEqualComparer instance = new RefEqualComparer();

        public new bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
    #endregion

    #region CommonFunc
    public static void LogException(Exception ex)
    {
        Debug.LogErrorFormat("执行消息出错:{0}\r\nstack:\r\n:{1}", ex.Message, ex.StackTrace);
    }
    #endregion
}