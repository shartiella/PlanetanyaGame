using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavascriptHook : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform logo;

    //מקבל את תוכן תיבת הטקסט מהדפדפן (לפני עליית המשחק) ומזריק אותו למשתנה גלובלי של שם הקבוצה
    public void getGroupName(string text)
    {
        Globals.GroupName = text;
        cam.transform.LookAt(logo);
        //Debug.Log(Globals.GroupName);
    }
}
