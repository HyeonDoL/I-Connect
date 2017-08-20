using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public struct BitsByte
{
    private static bool Null;
    private byte value;

    public bool this[int key]
    {
        get
        {
            return ((uint)this.value & (uint)(1 << key)) > 0U;
        }
        set
        {
            if (value)
                this.value = (byte)((uint)this.value | (uint)(byte)(1 << key));
            else
                this.value = (byte)((uint)this.value & (uint)(byte)~(1 << key));
        }
    }
    
    public int GetValue()
    {
        return value;
    }
    public void SetValue(int value)
    {
        this.value = (byte)value;
    }

}
