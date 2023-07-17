using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countdown : MonoBehaviour
{
    private Vector3 finalPosition;
    private Vector3 startPosition = Vector3.zero;

    private Vector3 finalSize;
    private Vector3 startSize = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        finalPosition= transform.localPosition;
        startPosition.y = finalPosition.y - 300;
        finalSize= transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = startSize;
        transform.localPosition = startPosition;

        transform.LeanScale(finalSize, 0.5f).setEaseOutQuart();
        transform.LeanMoveLocalY(finalPosition.y, 0.9f).setEaseOutQuart();
    }

    //void afterAnimation()
    //{
    //    gameObject.SetActive(false);
    //}
}
