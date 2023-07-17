using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavascriptHook : MonoBehaviour
{
    //מקבל את תוכן תיבת הטקסט מהדפדפן (לפני עליית המשחק) ומזריק אותו למשתנה גלובלי של שם הקבוצה
    public void getGroupName(string text)
    {
        Globals.GroupName = text;
        //Debug.Log(Globals.GroupName);
    }
}
