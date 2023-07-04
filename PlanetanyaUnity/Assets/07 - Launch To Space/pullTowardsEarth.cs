using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullTowardsEarth : MonoBehaviour
{
    [SerializeField] private GameObject earth;
    [SerializeField] private GameObject rocket;
    [SerializeField] private Rigidbody rocketRB;
    public float massProduct;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rocketRB.velocity += Globals.GravityForce(earth, rocket, massProduct);
    }
}
