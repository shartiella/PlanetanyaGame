using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SatellitePartScript : MonoBehaviour
{
    private Vector3 initialobjectPosition; //������ ������� �� ������
    //[SerializeField] private bool isCorrect;
    private float dragTimer;

    // Start is called before the first frame update
    void Awake()
    {
        initialobjectPosition = transform.position; //����� ������ ������� �� ��������
    }

    // Update is called once per frame
    void Update()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Globals.currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));


    }

    private void OnMouseDown()
    {
        //GetComponent<Renderer>().material = holdingFeedback; //����� ������ �����

        dragTimer = 0;

        //����� ���� ��� ���� ���� ����� ���
        foreach (SatPart part in Globals.ChosenSatellite.PartsList)
        {
            if (part.Name == transform.name)
            {
                Globals.ChosenSatPart = part;
            }
        }
        foreach (SatPart part in Globals.ChosenSatellite.DistractorsList)
        {
            if (part.Name == transform.name)
            {
                Globals.ChosenSatPart = part;
            }
        }

        //����� ������ ������ �����
        if (AllObjects.showInfoPanel)
        {
            AllObjects.showInfoPanel = false;
        }
        else
        {
            AllObjects.showInfoPanel = true;
            
        }
    }

    private void OnMouseDrag()
    {
        transform.position = Globals.currentMousePosition; //����� - ������ ���� ���� �����

        dragTimer += Time.deltaTime;

        if (dragTimer >= 0.2f)
        {
            AllObjects.showInfoPanel = false;
        }
    }

    private void OnMouseUp()
    {
        //GetComponent<Renderer>().material = defaultColor;

        //����� ��� ���� ��� ��� ����
        Debug.Log("x " + transform.position.x);
        Debug.Log("y " + transform.position.y);
        Debug.Log("z " + transform.position.z);
        //        if (transform.position.x > -0.4 && transform.position.x < 0.4 && transform.position.y > 0.3 && transform.position.y < 0.7 && transform.position.z > 0.14 && transform.position.z < 0.22)

        if (AllObjects.isTouchingSatBody)
        {
            Debug.Log("I'm In");

            //����� �� ��� �� ��� ������ ��������� ������� ������
            bool isCorrectPart = false;
            foreach (SatPart part in Globals.ChosenSatellite.PartsList)
            {
                if (transform.name == part.Name)
                {
                    isCorrectPart = true;
                }
            }

            if (isCorrectPart)
            {
                Debug.Log("I'm correct");
            }
            else
            {
                Debug.Log("I'm wrong");
                transform.position = initialobjectPosition;
            }
        }
        else
        {
            Debug.Log("I'm out");
            transform.position = initialobjectPosition;
        }
    }

}
