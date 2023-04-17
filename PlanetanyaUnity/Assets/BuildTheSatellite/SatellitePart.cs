using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SatellitePart : MonoBehaviour
{
    private Vector3 initialobjectPosition; //������ ������� �� ������
    [SerializeField] private bool isCorrect;


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
    }

    private void OnMouseUp()
    {
        //GetComponent<Renderer>().material = defaultColor;

        //����� ��� ���� ��� ��� ����
        if (isCorrect)
        {

        }
        else
        {
            transform.position = initialobjectPosition;
        }
    }

}
