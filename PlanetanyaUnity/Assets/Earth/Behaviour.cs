using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Behaviour : MonoBehaviour
{
    //public float throwForce;
    //public float throwHard;
    //private string rocketRotationDirection = "right";

    //[SerializeField] private GameObject feedbackPrefab;

    //public static string orbit="";

    //public int hitnum=1;
    //public bool rocketHasLaunched=false;

    //טיל
    //public Transform rocketTransform;
    public Rigidbody rocketRigidbody;

    //ארץ
    //public GameObject Earth;
    public Rigidbody EarthRigidbody;
    //public Transform EarthTransform;

    //MEO
    //public Transform meotransform;

    //מד עוצמת שיגור
    //public TextMeshProUGUI forceMeter;

    // Start is called before the first frame update
    void Start()
    {
        //throwForce = 2;
        //throwHard = 1;
    }

    //private void OnMouseDrag()
    //{
    //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    rocketTransform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    //}

    // פונקציה שרצה כל פריים
    void Update()
    {
        //הטייה ימינה ושמאלה לסירוגין
        //if (rocketRotationDirection == "right")
        //{
        //    if (rocketTransform.rotation.z > -0.4)
        //    {
        //        rocketRigidbody.transform.Rotate(0, 0, (float)-0.1);
        //    }
        //    else
        //    {
        //        rocketRotationDirection = "left";
        //    }
        //}
        //else if (rocketRotationDirection == "left")
        //{
        //    if (rocketTransform.rotation.z < 0.4)
        //    {
        //        rocketRigidbody.transform.Rotate(0, 0, (float)0.1);
        //    }
        //    else
        //    {
        //        rocketRotationDirection = "right";
        //    }
        //}
    }

    //פונקציית עדכון לדברים פיזיקליים
    private void FixedUpdate()
    {
        //לחיצות על המסך
        //foreach (Touch touch in Input.touches)
        //{
        //    if (touch.phase == UnityEngine.TouchPhase.Began)
        //    {
        //        Debug.Log("finger DOWN");
        //    }

        //    if (touch.phase == UnityEngine.TouchPhase.Stationary)
        //    {
        //        Debug.Log("finger STAY");
        //    }

        //    if (touch.phase == UnityEngine.TouchPhase.Ended)
        //    {
        //        Debug.Log("finger UP");
        //    }
        //}

        //לחיצות על העכבר
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("mouse DOWN");
        //}

        //if (Input.GetMouseButton(0))
        //{
        //    Debug.Log("mouse STAY");
        //    throwHard += 1;
        //    forceMeter.text = throwHard.ToString();
        //}

        //if (Input.GetMouseButtonUp(0))
        //{

        //}

        if (Globals.Gravity)
        {
            AddGravityForce(EarthRigidbody, rocketRigidbody);
            //CheckDistance();
        }
    }


    private void OnMouseUp()
    {
        //Debug.Log("mouse UP");
        //rocketRotationDirection = "none";

        //throwForce = throwForce * throwHard;

        //Debug.Log("shoot!");
        //Debug.Log("throwForce"+throwForce);
        //Debug.Log("throwHard"+throwHard);
        //rocketRigidbody.useGravity = true;
        //rocketRigidbody.AddForce(throwForce, 100, 0);
        //grav = true;

        //throwForce = 2;
        //throwHard = 1;
    }

    //כוח המשיכה של כדור הארץ
    public static void AddGravityForce(Rigidbody attractor, Rigidbody target)
    {
        float G = 25;
        float massProduct = attractor.mass * target.mass; //מסה משותפת

        Vector3 difference=attractor.position-target.position;
        float distance = difference.magnitude;

        float unScaledForceMagnitude=massProduct/Mathf.Pow(distance,2);
        float forceMagnitude = G * unScaledForceMagnitude;

        Vector3 forceDirection = difference.normalized;

        Vector3 forceVector = forceDirection * forceMagnitude;
        target.AddForce(forceVector);
    }


    //public void OnTriggerEnter(Collider other)
    //{
    //    if (rocketHasLaunched)
    //    {
    //        if (other.gameObject.tag == "Player")
    //        {
    //            hitnum++;
    //            Debug.Log("HIT num " + hitnum);
    //        }
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    if (rocketHasLaunched)
    //    {
    //        if (other.gameObject.tag == "Player")
    //        {
    //            Debug.Log("out");
    //        }
    //    }
    //}

    //public void OnTriggerStay(Collider other)
    //{
    //    if (rocketHasLaunched)
    //    {
    //        if (other.gameObject.tag == "Player")
    //        {
    //            Debug.Log("stay");

    //            if (hitnum > 3)
    //            {
    //                other.GetComponent<MeshRenderer>().material = EarthTransform.gameObject.GetComponent<MeshRenderer>().material;
    //            }
    //        }
    //    }
    //}


}
