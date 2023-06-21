using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlinkColor : MonoBehaviour
{
    public Color startColor;
    public Color endColor;
    [Range(0, 10)]
    public float speed = 1;
    public float glow = 20;
    public static bool glowOn;

    List<MeshRenderer> renlist;

    void Awake()
    {
        Debug.Log(MoveCamera.deviceClicked+ "deviceClicked");
        //ren = GetComponent<Renderer>();
        renlist = GetComponents<MeshRenderer>().ToList();
    }

    void Update()
    {
        if (MoveCamera.deviceClicked == "" && glowOn)
        {
            Color color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
            float intensity = Mathf.PingPong(Time.time * speed * glow, glow) - 1;
            foreach (MeshRenderer ren in renlist)
            {
                ren.material.SetColor("_EmissionColor", color * intensity);
            }
        }
        else
        {
            foreach (MeshRenderer ren in renlist)
            {
                ren.material.SetColor("_EmissionColor", startColor);
            }
        }
    }
}