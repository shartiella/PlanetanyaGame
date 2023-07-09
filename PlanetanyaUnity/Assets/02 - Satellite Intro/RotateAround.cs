using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateAround : MonoBehaviour
{
    public float speed;
    private float X;
    private float Y;
    private float Z;

    [SerializeField] private GameObject info1;
    [SerializeField] private GameObject info2;
    [SerializeField] private GameObject info3;
    
    [SerializeField] private GameObject Moon;
    [SerializeField] private GameObject Satellite;

    [SerializeField] private GameObject FadeGO;

    private Camera cam;
    private Vector3 previousPosition;
    private Vector3 initialPosition;
    private bool RotateByDragging = false;

    // Start is called before the first frame update
    void Start()
    {
        //info1.SetActive(true);
        //Moon.SetActive(true);

    }

    private void Awake()
    {
        cam = GetComponent<Camera>();
        initialPosition = cam.transform.position;

        Vector3 firstStaticCameraPosition = GetComponent<Transform>().localPosition;
        Vector3 otherPosition = new Vector3(0,0,-21);
        transform.localPosition = otherPosition;
        transform.LeanMoveLocal(firstStaticCameraPosition, 3).setDelay(1).setEaseInOutSine().setOnComplete(EnableRotationByDrag);
    }

    // Update is called once per frame
    void Update()
    {
        if (RotateByDragging)
        {
            cameraRotation();
        }
        //if (Input.GetMouseButton(0))
        //{
        //    Vector3 moveby = new Vector3(Input.GetAxis("Mouse X") * speed, -Input.GetAxis("Mouse Y") * speed, 0);
        //    X = transform.position.x;
        //    Y = transform.position.y;
        //    Z = transform.position.z;
        //    transform.transform.position = new Vector3(X, Y + moveby.y*2, Z + moveby.x);
        //    Debug.Log(transform.position);
        //}
    }

    public void openSecondInfo()
    {
        info1.SetActive(false);
        info2.SetActive(true);
        Moon.SetActive(false);
        Satellite.SetActive(true);
    }

    public void openThirdInfo()
    {
        info2.SetActive(false);
        info3.SetActive(true);
    }

    public void nextScene()
    {
        FadeGO.SetActive(true);
    }

    private void EnableRotationByDrag()
    {
        RotateByDragging = true;
    }

    private void cameraRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = new Vector3();

            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
            cam.transform.Translate(initialPosition);

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
