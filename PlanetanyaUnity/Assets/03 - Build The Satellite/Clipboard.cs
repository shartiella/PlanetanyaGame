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
    void Update()
    {
        updateList();
    }

    private void OnMouseDown()
    {
        BlinkColor.glowOn = false;
        if (BuildIUCopy.counter == 3)
        {
            BuildIUCopy.counter = 7;
        }
        if (BuildIUCopy.counter == 5)
        {
            BuildIUCopy.counter = 6;
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

        BuildIUCopy.numberOfCorrectObjectsConnected = 0;
        BuildIUCopy.numberOfWrongObjectsConnected = 0;
        BuildIUCopy.overallNumberOfCorrectParts = 0;

        foreach (SatPart sp in _allObjects.satParts)
        {
            if (sp.isCorrect)
            {
                textForList += sp.WhatItDoes + "\n";
                BuildIUCopy.overallNumberOfCorrectParts++;

                if (sp.isConnected)
                {
                    partChecks[index].gameObject.SetActive(true);
                    BuildIUCopy.numberOfCorrectObjectsConnected++;
                }
                else
                {
                    partChecks[index].gameObject.SetActive(false);
                }
                index++;

            }
            else if (sp.isConnected)
            {
                BuildIUCopy.numberOfWrongObjectsConnected++;
            }
        }
        partList.text = textForList;
    }
}
