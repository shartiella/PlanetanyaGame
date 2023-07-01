using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarthHitTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //פגיעת הטיל בכדור הארץ
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Globals.rocketStatus == "launched")
            {
                Globals.rocketStatus = "crashed";
                OrbitManager.crashFromEarthCollision = true;
                Debug.Log(Globals.rocketStatus + " because of collision");

                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {

        }
    }
}
