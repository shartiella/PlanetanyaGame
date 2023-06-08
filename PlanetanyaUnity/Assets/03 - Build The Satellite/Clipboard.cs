using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    public Animator animator;
    private bool IsOpened;

    // Start is called before the first frame update
    void Start()
    {
        IsOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Debug.Log("clicked " + IsOpened);
        if (IsOpened)
        {
            IsOpened=false;
            animator.SetTrigger("closeClip");
        }
        else
        {
            IsOpened = true;
            animator.SetTrigger("openClip");
        }


    }
}
