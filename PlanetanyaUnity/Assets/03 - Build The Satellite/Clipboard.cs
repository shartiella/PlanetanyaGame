using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Clipboard : MonoBehaviour
{
    [SerializeField] private AllObjects _allObjects;

    public Animator animator;
    private bool IsOpened;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI partList;
    [SerializeField] private List<GameObject> partChecks;

    // Start is called before the first frame update
    void Start()
    {
        IsOpened = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateList();
    }

    private void OnMouseDown()
    {
        BlinkColor.glowOn = false;
        if (BuildIU.counter == 3)
        {
            BuildIU.counter = 7;
        }
        if (BuildIU.counter == 5)
        {
            BuildIU.counter = 6;
        }

        if (IsOpened)
        {
            IsOpened=false;
            animator.SetTrigger("closeClip");
        }
        else
        {
            IsOpened = true;
            animator.SetTrigger("openClip");
        }
    }

    private void updateList()
    {
        title.text = "לוויין " + Globals.ChosenSatellite.Kind + ": רכיבים"; 

        string textForList = "";
        int index = 0;

        BuildIU.numberOfCorrectObjectsConnected = 0;
        BuildIU.numberOfWrongObjectsConnected = 0;
        BuildIU.overallNumberOfCorrectParts = 0;

        foreach (SatPart sp in _allObjects.satParts)
        {
            if (sp.isCorrect)
            {
                textForList += sp.WhatItDoes + "\n";
                BuildIU.overallNumberOfCorrectParts++;

                if (sp.isConnected)
                {
                    partChecks[index].gameObject.SetActive(true);
                    BuildIU.numberOfCorrectObjectsConnected++;
                }
                else
                {
                    partChecks[index].gameObject.SetActive(false);
                }
                index++;

            }
            else if (sp.isConnected)
            {
                BuildIU.numberOfWrongObjectsConnected++;
            }
        }
        partList.text = textForList;
    }
}
