using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI groupname;
    //[SerializeField] private Button startBTN;
    public TextMeshProUGUI SatName;
    public TextMeshProUGUI LevelStats1;
    public TextMeshProUGUI LevelStats2;
    public TextMeshProUGUI LevelStats3;
    public TextMeshProUGUI LevelStats4;
    public TextMeshProUGUI LevelStats5;
    public TextMeshProUGUI LevelStats6;

    // Start is called before the first frame update
    void Start()
    {
        groupname.text = Globals.GroupName; //שם הקבוצה
        SatName.text = "סוג לוויין: " + Globals.ChosenSatellite.Kind;
        //LevelStats1.text= Globals.LevelStats1;
        //LevelStats2.text= Globals.LevelStats2;
        //LevelStats3.text= Globals.LevelStats3;
        //LevelStats4.text= Globals.LevelStats4;
        //LevelStats5.text= Globals.LevelStats5;
        //LevelStats6.text= Globals.LevelStats6;
    }
}
