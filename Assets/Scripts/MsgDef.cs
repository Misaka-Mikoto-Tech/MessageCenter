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
    public static void BindTypes(Dictionary<MsgId, Type> dicType)
    {
        dicType.Add(MsgId.MsgA, typeof(Action<int>));
        dicType.Add(MsgId.MsgB, typeof(Action<float, string>));
    }
}




        


