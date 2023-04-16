using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject[] TrajectoryDots;
    [SerializeField] private int dotNumber;
    [SerializeField] private Rigidbody earth;

    // Start is called before the first frame update
    void Awake()
    {
        TrajectoryDots = new GameObject[dotNumber]; //יצירת מערך נקודות הסימון


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
