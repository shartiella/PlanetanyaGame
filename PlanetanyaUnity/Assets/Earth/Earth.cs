using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    //public Rigidbody rocketRigidbody;
    //public Rigidbody EarthRigidbody;

    //[SerializeField] private GameObject rocket;
    //[SerializeField] private GameObject earth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void FixedUpdate()
    //{

    //    if (Globals.Gravity)
    //    {
    //        //AddGravityForce(EarthRigidbody, rocketRigidbody);
    //        rocket.GetComponent<Rigidbody>().AddForce(Globals.GravityForce(earth, rocket, 0.5f));
    //    }
    //}


    ////כוח המשיכה של כדור הארץ
    //public static void AddGravityForce(Rigidbody attractor, Rigidbody target)
    //{
    //    float G = 25;
    //    float massProduct = attractor.mass * target.mass; //מסה משותפת

    //    Vector3 difference = attractor.position - target.position;
    //    float distance = difference.magnitude;

    //    float unScaledForceMagnitude = massProduct / Mathf.Pow(distance, 2);
    //    float forceMagnitude = G * unScaledForceMagnitude;

    //    Vector3 forceDirection = difference.normalized;

    //    Vector3 forceVector = forceDirection * forceMagnitude;
    //    target.AddForce(forceVector);
    //}
}
