using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchBTN : MonoBehaviour
{
    [SerializeField] private GameObject cineMachine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("button press");
        cineMachine.SetActive(true); //לבטל כשנצליח להטמיע מציאות רבודה

        Globals.rocketStatus = "launching";
    }

    //private void OnMouseUp()
    //{
    //    Debug.Log("button press");
    //    cineMachine.SetActive(true); //לבטל כשנצליח להטמיע מציאות רבודה

    //    Globals.rocketStatus = "launching";
    //}
}
