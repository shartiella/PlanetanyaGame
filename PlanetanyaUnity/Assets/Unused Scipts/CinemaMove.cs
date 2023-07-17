using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinemaMove : MonoBehaviour
{
    //סקריפט לא פעיל

    //[SerializeField] private GameObject cam;
    //[SerializeField] private GameObject Phone;
    //[SerializeField] private GameObject TV;
    //[SerializeField] private GameObject Computer;

    private Animator camAnimator;


    // Start is called before the first frame update
    void Awake()
    {
        camAnimator = GetComponent<Animator>();
        //transform.position = cam.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (MoveCamera.deviceClicked!="")
        {
            camAnimator.SetTrigger(MoveCamera.deviceClicked);
            //MoveCamera.deviceClicked = "";
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoom(Input.GetAxis("Mouse ScrollWheel") * 10);
        }
    }


    public void zoom(float increment)
    {
        //Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - increment, 0, 60);

        //Debug.Log("I feel the scroll " + increment);

        //if (increment < 0 && MoveCamera.finalPart)
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
    }
}
