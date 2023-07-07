using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI groupname;
    [SerializeField] private Button startBTN;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //groupname.text = Globals.GroupName; //שם הקבוצה

        //if (Globals.GroupName == "" || Globals.GroupName == "שם הקבוצה") //אפרור הכפתור אם אין שם קבוצה
        //{
        //    startBTN.interactable= false;
        //}
        //else
        //{
        //    startBTN.interactable= true;
        //}
    }

    public void clickStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }
}
