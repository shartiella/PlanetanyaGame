using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField] private Material normalColor;
    [SerializeField] private Material goodColor;
    public List<Vector3> positions= new List<Vector3>();
    public static int positionCounter = 1;
    public static int GoalsAccomplished = 0;
    Vector3 initialSize;

    private void Start()
    {
        GoalsAccomplished = 0;
        positionCounter = 1;
    }

    // Start is called before the first frame update
    void Awake()
    {
        initialSize = transform.localScale;
    }

    private void OnEnable()
    {
        if (GoalsAccomplished==positionCounter)
        {
            positionCounter++;
        }

        //transform.localScale = initialSize;
        transform.position = positions[GoalsAccomplished];

        transform.localScale = new Vector3(0, 0, 0);
        transform.LeanScale(initialSize, 1).setDelay(0.5f).setEaseOutElastic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //GetComponent<MeshRenderer>().material = goodColor;
        transform.LeanScale(Vector3.zero, 0.5f).setEaseInOutBack().setOnComplete(disableSelf);

        GoalsAccomplished = positionCounter;
        //Debug.Log("Goal " + GoalsAccomplished + " accomplished");
    }

    private void OnTriggerExit(Collider other)
    {

    }

    void disableSelf()
    {
        gameObject.SetActive(false);
    }
}
