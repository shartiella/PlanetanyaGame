using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllObjects : MonoBehaviour
{
    public static string BuildingState = "";

    public List<SatPart> satParts; //התוכן מוגדר באינספקטור
    public static SatPart newestPartTouching = null;
    public static bool aPartIsBeingDragged = false;

    // Start is called before the first frame update
    void Start()
    {
        BuildingState = "";
        aPartIsBeingDragged = false;
        newestPartTouching = null;
    }

    public static void connectedPart(SatPart newestPart)
    {
        newestPartTouching = newestPart;
        newestPartTouching.isConnected = true;
    }
    public static void disConnectedPart(SatPart newestPart)
    {
        newestPartTouching = newestPart;
        newestPartTouching.isConnected = false;
    }
}

[System.Serializable]
public class SatPart
{
    public string Name;
    public string Description;
    public string WhatItDoes;
    public GameObject gObject;
    public List<string> relatedSatellites;
    public bool isCorrect;
    public bool isDragged;
    public bool isConnected;
}


