using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatOrbit : MonoBehaviour
{
    //public float randomNumForDistance;
    //public float randomNumForAngle;

    public Vector3 start = Vector3.zero;
    public float startRadius;
    public Vector3 end = Vector3.zero;
    public float endRadius;
    public float speed;
    public static float maxRadius;

    Vector3 axis;

    // Start is called before the first frame update

    private void Start()
    {
        //initialPosition = Vector3.zero;
    }

    private void OnEnable()
    {
        //randomNumForDistance = Random.value;
        //initialPosition= new Vector3(0, 0, -100);
        //transform.position = initialPosition;
        //randomNumForAngle = Random.value;

        //start.x = Random.value * 100;
        //start.y = Random.value * 100;
        //start.z = Random.value * 100;

        while (start.magnitude < 22 || start.magnitude > maxRadius)
        {
            start.x = Random.value * maxRadius;
            start.y = Random.value * maxRadius;
            start.z = Random.value * maxRadius;
        }
        startRadius = start.magnitude;

        while (end.magnitude < 22 || end.magnitude > maxRadius)
        {
            end.x = Random.value * maxRadius;
            end.y = Random.value * maxRadius;
            end.z = Random.value * maxRadius;
        }
        endRadius = end.magnitude;

        transform.position = start;
        axis = Vector3.Cross(start, end);
        Debug.Log(gameObject.name + start.magnitude);
        speed = (maxRadius - start.magnitude) / maxRadius;
    }

    // Update is called once per frame
    void Update()
    {
        ////transform.position = Vector3.zero;

        ////transform.Rotate(new Vector3(1, 0, 0), rotateX);
        ////transform.Rotate(new Vector3(0, 1, 0), rotateY, Space.World);
        ////transform.Rotate(new Vector3(0, 0, 1), rotateZ);

        ////transform.RotateAround(Vector3.zero, new Vector3(1, 0, 0), randomNum);

        //float orbitSpeed = -initialPosition.x/1000;

        ////transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), randomNumForAngle);
        ////transform.RotateAround(Vector3.zero, new Vector3(0, 0, 1), orbitSpeed);

        transform.RotateAround(Vector3.zero, axis, speed);

        ////transform.Translate(initialPosition);
    }
}
