﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doneBTN;
    public static GameObject currentTouchingPart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Touching Satellite Body");
        Debug.Log("I'm touching " + other.gameObject.name);
        currentTouchingPart= other.gameObject;

        //BuildIU.isTouchingSatBody = true;


    }

    void OnTriggerExit(Collider other)
    {
        currentTouchingPart = null;
        Debug.Log("Untouching Satellite Body");

        //BuildIU.isTouchingSatBody = false;
    }
}
