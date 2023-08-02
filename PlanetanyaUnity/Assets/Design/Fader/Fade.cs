using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Animator animator;
    public bool doFadeIn;
    public bool doFadeOut;

    private void Start()
    {
        if (doFadeIn)
        {
            animator.SetTrigger("FadeIn");
        }
        else if (!doFadeOut)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void OnEnable()
    {
        if (doFadeOut)
        {
            animator.SetTrigger("FadeOut");
        }
        else
        {
            //OnFadeOutComplete();
        }
    }

    public void OnFadeOutComplete()
    {
        //Debug.Log("ACTIVE SCENE"+SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnFadeInComplete()
    {
        transform.gameObject.SetActive(false);
    }
}
