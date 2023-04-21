﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doneBTN;

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

        BuildIU.isTouchingSatBody = true;

        doneBTN.SetActive(true);

    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Untouching Satellite Body");

        BuildIU.isTouchingSatBody = false;
    }
}
