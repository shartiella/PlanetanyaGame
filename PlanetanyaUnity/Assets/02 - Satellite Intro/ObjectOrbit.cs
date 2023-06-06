using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrbit : MonoBehaviour
{
    private Transform obj;
    private Vector3 initialPosition;
    [SerializeField] private float spinForce;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Transform>();
        initialPosition = obj.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(spinForce*0.001f, 0, 0);

        obj.position = new Vector3();

        obj.Rotate(new Vector3(1, 0, 0), direction.y * 180);
        obj.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
        obj.Translate(initialPosition);

    }
}
