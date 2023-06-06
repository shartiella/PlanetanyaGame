using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsAnimtion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
      transform.localPosition=new Vector3(0,-Screen.height,0);
        transform.LeanMoveLocalY(0, 1);
    }



}
