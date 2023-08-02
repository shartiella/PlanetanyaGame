using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MoveMap : MonoBehaviour
{
    //[DllImport("__Internal")]
    //private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void LocateTheDevice();
    //[DllImport("__Internal")]
    //private static extern void success(string[] array);
    //[DllImport("__Internal")]
    //private static extern void error(string[] array);

    public float speed;
    private float X;
    private float Y;
    private float Z;

    [SerializeField] private GameObject window1;
    [SerializeField] private GameObject window2;
    [SerializeField] private GameObject arrivalBTN;

    [SerializeField] private TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        getLocation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 moveby = new Vector3(Input.GetAxis("Mouse X") * speed, Input.GetAxis("Mouse Y") * speed, 0);
            X = transform.position.x;
            Y = transform.position.y;
            Z = transform.position.z;
            transform.transform.position = new Vector3(X + moveby.x, Y, Z + moveby.y);
            //Debug.Log(transform.position);
        }

        if ((transform.position.x > -1.7 && transform.position.x < -0.5) && (transform.position.z > -3.6 && transform.position.z < -2.1))
        {
            //Debug.Log("correct");
            //Debug.Log(transform.position);
            arrivalBTN.SetActive(true);
            //Globals.rocketStatus = "LookAtRocket";
        }
    }

    public void arrival()
    {
        arrivalBTN.SetActive(false);
        //Debug.Log("asdasd");
        if ((transform.position.x > -1.7 && transform.position.x < -0.5) && (transform.position.z > -3.6 && transform.position.z < -2.1))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            window2.SetActive(true);
            window1.SetActive(false);
        }
    }

    public void tryagain()
    {
        window2.SetActive(false);
        window1.SetActive(true);
    }

    public void getLocation()
    {
        LocateTheDevice();
    }

    public void coor(string message)
    {
        txt.text = message;
    }
}
