using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimPopUp : MonoBehaviour
{
    private Transform window;
    private Vector3 winScale;
    private Vector3 startScale;

    // Start is called before the first frame update
    void Awake()
    {
        window = GetComponent<Transform>();
        winScale = window.localScale;
        startScale = new Vector3(0, 0, winScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        transform.localScale = startScale;
        transform.LeanScale(winScale, 0.5f).setEaseOutElastic();
    }
}
