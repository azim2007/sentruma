﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Debugger
{
    private static bool debugMode = true;
    public static void Log(string message)
    {
        if (debugMode)
        {
            Debug.Log(message);
        }
    }
}