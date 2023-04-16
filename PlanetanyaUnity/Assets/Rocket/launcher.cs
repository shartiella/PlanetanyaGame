using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class launcher : MonoBehaviour
{
    [SerializeField] private Material holdingFeedback;
    [SerializeField] private Material defaultColor;

    private Vector3 initialLauncherPosition; //������ ������� �� ������
    private Vector3 initialrocketPosition; //������ ������� �� ����

    private Vector3 forceAtPlayer; //��� ������
    [SerializeField] private float forceFactor;

    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject[] TrajectoryDots;
    [SerializeField] private int dotNumber;
    [SerializeField] private GameObject Earth;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject trajectoryDotsParent;

    [SerializeField] private Vector3 currentPosition;
    [SerializeField] private Vector3 currentSpeed;
    [SerializeField] private Vector3 nextPosition;
    [SerializeField] private Vector3 nextSpeed;

    [SerializeField] private float timeFactor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Globals.rocketStatus == "inOrbit" || Globals.demo == true)
        {

            GetComponent<MeshRenderer>().enabled = false;
        }

        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Globals.currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));

    }

    private void Awake()
    {
        gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = true;

        initialLauncherPosition = transform.position; //����� ������ ������� �� ������
            initialrocketPosition = new Vector3(0, 1.7f, 0); //����� ������ ������� �� ����


            TrajectoryDots = new GameObject[dotNumber]; //����� ���� ������ ������

    }

    private void OnMouseDown()
    {
        GetComponent<Renderer>().material= holdingFeedback; //����� ������ �����

        //����� ������� ��� ��������
        for (int i = 1; i < dotNumber; i++)
        {
            TrajectoryDots[i] = Instantiate(dotPrefab, trajectoryDotsParent.transform);
            //TrajectoryDots[i].transform.localScale = new Vector3(1/i, 0.01f, 1/i);
            Debug.Log(TrajectoryDots[i].transform.position);
        }

    }

    private void OnMouseDrag()
    {
        if (Globals.rocketStatus == "toLaunch")
        {
            transform.position = Globals.currentMousePosition; //����� - ������ ���� ���� �����

            //����� ������
            //distanceBetweenInitialToRelease = Vector3.Distance(initialPosition, Globals.currentMousePosition);
            //directionFromInitialPosition = (initialPosition - currentMousePosition).normalized;

            //direction = initialPosition - Globals.currentMousePosition;
            //Globals.launchForce = direction * forceFactor;

            //����� �������
            forceAtPlayer = initialLauncherPosition - Globals.currentMousePosition;

            Globals.launchForce = forceAtPlayer * forceFactor;

            currentSpeed = Globals.launchForce;
            currentPosition = initialrocketPosition;
            //currentPosition = new Vector3(0, 0, 0);

            for (int i = 1; i < dotNumber; i++)
            {
                TrajectoryDots[i].transform.position = currentPosition;
                calculateNextPosition(i* timeFactor, TrajectoryDots[i]);
                currentPosition = nextPosition;
                currentSpeed = nextSpeed;
            }
            rocket.transform.LookAt(TrajectoryDots[3].transform);
            rocket.transform.Rotate(-90, 180, 0);
        }
    }

    private void OnMouseUp()
    {
        GetComponent<Renderer>().material = defaultColor;
        if (Globals.demo==false)
        {
            Globals.numberOfLaunches++;
        }

        if (Globals.rocketStatus == "toLaunch")
        {
            //Globals.launchForce = forceAtPlayer * forceFactor;

            Globals.lastFingerRelease = Globals.currentMousePosition; //����� ������ ������ �� �����
            Globals.rocketStatus = "LAUNCH"; //����� ������
            transform.position = initialLauncherPosition; //���� ������ �������
            GetComponent<MeshRenderer>().enabled = false; //�����

            //����� �������
            for (int i = 1; i < dotNumber; i++)
            {
                Destroy(TrajectoryDots[i]);
            }
        }

    }


    public void calculateNextPosition(float timeInterval, GameObject dot)
    {
        //return new Vector3(initialPosition.x, initialPosition.y + 0.1f, 0) +
        //    new Vector3(direction.x * forceFactor, direction.y * forceFactor, 0) * elapsedTime +
        //    0.5f * Globals.GravityForce(Earth, dot, 0.5f) * elapsedTime * elapsedTime;

        //����� ������ �� ������� ���:
        //������ ���� �� ������
        //���� �� ������
        //��� ������ �� ���� ����

        //    return new Vector3(initialPosition.x, initialPosition.y + 0.2f, 0) +
        //forceAtPlayer * forceFactor * elapsedTime +
        //0.5f * Globals.GravityForce(Earth, dot, 0.5f) * elapsedTime * elapsedTime;

        nextPosition = currentPosition + currentSpeed * timeInterval + 0.5f * Globals.GravityForce(Earth, dot, 0.5f) / Time.fixedDeltaTime * timeInterval * timeInterval ;
        nextSpeed = currentSpeed + Globals.GravityForce(Earth, dot, 0.5f) / Time.fixedDeltaTime * timeInterval;
        
    }

}
