using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavascriptHook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //מקבל את תוכן תיבת הטקסט מהדפדפן (לפני עליית המשחק) ומזריק אותו למשתנה גלובלי של שם הקבוצה
    public void getGroupName(string text)
    {
        Globals.GroupName = text;
    }
}
