/*
 * 回调执行器，用于执行缓存的消息(用户调用 PostMessage 产生)
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMsgInvoker
{
    void Invoke();
}

public class MsgInvoker : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo)
    {
        _cbInfo = cbInfo;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for(int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action)();
            }
            catch(Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1>)(_data1);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2>)(_data1, _data2);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2, T3> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;
    private T3 _data3;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2, T3 data3)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
        _data3 = data3;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2, T3>)(_data1, _data2, _data3);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2, T3, T4> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;
    private T3 _data3;
    private T4 _data4;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2, T3 data3, T4 data4)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
        _data3 = data3;
        _data4 = data4;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2, T3, T4>)(_data1, _data2, _data3, _data4);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2, T3, T4, T5> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;
    private T3 _data3;
    private T4 _data4;
    private T5 _data5;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
        _data3 = data3;
        _data4 = data4;
        _data5 = data5;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2, T3, T4, T5>)(_data1, _data2, _data3, _data4, _data5);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2, T3, T4, T5, T6> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;
    private T3 _data3;
    private T4 _data4;
    private T5 _data5;
    private T6 _data6;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
        _data3 = data3;
        _data4 = data4;
        _data5 = data5;
        _data6 = data6;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2, T3, T4, T5, T6>)(_data1, _data2, _data3, _data4, _data5, _data6);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2, T3, T4, T5, T6, T7> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;
    private T3 _data3;
    private T4 _data4;
    private T5 _data5;
    private T6 _data6;
    private T7 _data7;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
        _data3 = data3;
        _data4 = data4;
        _data5 = data5;
        _data6 = data6;
        _data7 = data7;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2, T3, T4, T5, T6, T7>)(_data1, _data2, _data3, _data4, _data5, _data6, _data7);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2, T3, T4, T5, T6, T7, T8> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;
    private T3 _data3;
    private T4 _data4;
    private T5 _data5;
    private T6 _data6;
    private T7 _data7;
    private T8 _data8;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
        _data3 = data3;
        _data4 = data4;
        _data5 = data5;
        _data6 = data6;
        _data7 = data7;
        _data8 = data8;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2, T3, T4, T5, T6, T7, T8>)(_data1, _data2, _data3, _data4, _data5, _data6, _data7, _data8);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;
    private T3 _data3;
    private T4 _data4;
    private T5 _data5;
    private T6 _data6;
    private T7 _data7;
    private T8 _data8;
    private T9 _data9;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, T9 data9)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
        _data3 = data3;
        _data4 = data4;
        _data5 = data5;
        _data6 = data6;
        _data7 = data7;
        _data8 = data8;
        _data9 = data9;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>)(_data1, _data2, _data3, _data4, _data5, _data6, _data7, _data8, _data9);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}

public class MsgInvoker<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IMsgInvoker
{
    private MessageCenter.CallbackInfo _cbInfo;
    private T1 _data1;
    private T2 _data2;
    private T3 _data3;
    private T4 _data4;
    private T5 _data5;
    private T6 _data6;
    private T7 _data7;
    private T8 _data8;
    private T9 _data9;
    private T10 _data10;

    public MsgInvoker(MessageCenter.CallbackInfo cbInfo, T1 data1, T2 data2, T3 data3, T4 data4, T5 data5, T6 data6, T7 data7, T8 data8, T9 data9, T10 data10)
    {
        _cbInfo = cbInfo;
        _data1 = data1;
        _data2 = data2;
        _data3 = data3;
        _data4 = data4;
        _data5 = data5;
        _data6 = data6;
        _data7 = data7;
        _data8 = data8;
        _data9 = data9;
        _data10 = data10;
    }

    public void Invoke()
    {
        List<Delegate> cbLst = _cbInfo.cbLst;
        for (int i = 0, imax = cbLst.Count; i < imax; i++)
        {
            try
            {
                (cbLst[i] as Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)(_data1, _data2, _data3, _data4, _data5, _data6, _data7, _data8, _data9, _data10);
            }
            catch (Exception ex)
            {
                MessageCenter.LogException(ex);
            }
        }
    }
}