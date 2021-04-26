using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logging
{
    public static void LogComment(string in_name, string in_comment)
    {
        Debug.Log(in_name + ": " + in_comment);
    }
}
