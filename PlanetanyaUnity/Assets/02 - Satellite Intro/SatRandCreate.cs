using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatRandCreate : MonoBehaviour
{
    [SerializeField] private GameObject satPrefab;
    [SerializeField] private GameObject[] satsArray;
    public int satNumber;
    public float maxRadius;

    // Start is called before the first frame update
    void Start()
    {
        //initialLauncherPosition = transform.position; //����� ������ ������� �� ������
        //fullScale = transform.localScale;
        //initialrocketPosition = new Vector3(0, 1.7f, 0); //����� ������ ������� �� ����

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        satsArray = new GameObject[satNumber]; //����� ���� ������ ������
        SatOrbit.maxRadius = maxRadius;

        for (int i = 1; i < satNumber; i++)
        {
            satsArray[i] = Instantiate(satPrefab, transform);
            //float relativeScale = (float)(dotNumber - i) / (float)dotNumber;
            //satsArray[i].transform.localScale = new Vector3(relativeScale, 0.01f, relativeScale);
            //Debug.Log(TrajectoryDots[i].transform.localScale);
        }
    }
}
