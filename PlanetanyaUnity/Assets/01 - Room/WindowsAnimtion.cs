using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsAnimtion : MonoBehaviour
{
    private Transform window;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        window = GetComponent<Transform>();
        initialPosition = window.localPosition;
        //initialPosition = window.position;
        Debug.Log("to "+initialPosition);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("position "+window.localPosition);
    }

    private void OnEnable()
    {
        Vector3 downPosition = new Vector3(0, -Screen.height, 0);
        Debug.Log("from "+downPosition);
        transform.localPosition = downPosition;
        //transform.position = downPosition;
        transform.LeanMoveLocal(initialPosition, 1);
        //transform.LeanMove(initialPosition, 1);

    }



}
