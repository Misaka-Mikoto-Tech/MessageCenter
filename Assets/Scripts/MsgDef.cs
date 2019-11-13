using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MsgId
{
    MsgA,
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




        


