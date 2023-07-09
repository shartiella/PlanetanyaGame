using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ObjectOrbit : MonoBehaviour
{
    private Transform obj;
    private Vector3 initialPosition;
    [SerializeField] private float spinForce;
    [SerializeField] private bool RandomRotation;
    private float randomNum;
    private Vector3 direction;
    [SerializeField] private Vector3 rotateX;
    [SerializeField] private Vector3 rotateY;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Transform>();
        initialPosition = obj.position;

        if (RandomRotation)
        {
            randomNum=Random.value;
            Debug.Log(randomNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(spinForce * 0.001f, 0, 0);
        obj.position = new Vector3();

        if (RandomRotation)
        {
            //obj.Rotate(rotateY, direction.y * 180);
            obj.Rotate(rotateX, -direction.x * 180, Space.World);
        }
        else
        {

            obj.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            obj.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
        }
        obj.Translate(initialPosition);
    }
}
