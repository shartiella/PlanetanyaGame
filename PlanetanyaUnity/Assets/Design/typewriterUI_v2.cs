// Script for having a typewriter effect for UI - Version 2
// Prepared by Nick Hwang (https://www.youtube.com/nickhwang)
// Want to get creative? Try a Unicode leading character(https://unicode-table.com/en/blocks/block-elements/)
// Copy Paste from page into Inspector

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class typewriterUI_v2 : MonoBehaviour
{
	//[SerializeField] Text text;
	[SerializeField] TMP_Text tmpProText;
	string writer;
	[SerializeField] private Coroutine coroutine;

	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;
	[Space(10)] [SerializeField] private bool startOnEnable = false;
	
	[Header("Collision-Based")]
	[SerializeField] private bool clearAtStart = false;
	[SerializeField] private bool startOnCollision = false;
	enum options {clear, complete}
	[SerializeField] options collisionExitOptions;

	// Use this for initialization
	void Awake()
	{		
		if (tmpProText != null)
		{
			writer = tmpProText.text;
            Debug.Log(writer+"AWAKE");
        }
	}

	void Start()
	{
		tmpProText= GetComponent<TMP_Text>();
        writer = tmpProText.text;

        if (!clearAtStart ) return;
		
		//if (tmpProText != null)
		//{
		//	tmpProText.text = "";
		//}
	}

	private void OnEnable()
	{
		print("On Enable!");
		if(startOnEnable) StartTypewriter();
	}

	//private void OnCollisionEnter2D(Collision2D col)
	//{
	//	print("Collision!");
	//	if (startOnCollision)
	//	{
	//		StartTypewriter();
	//	}
	//}

 //   private void OnCollisionExit2D(Collision2D other)
	//{
	//	if (collisionExitOptions == options.complete)
	//	{
	//		if (tmpProText != null)
	//		{
	//			tmpProText.text = writer;
 //               Debug.Log(writer + "OnCollisionExit2D");
 //           }
 //       }
	//	// clear
	//	else
	//	{
	//		if (tmpProText != null)
	//		{
	//			tmpProText.text = "";
	//		}
	//	}
		
	//	StopAllCoroutines();
	//}
    private void OnCollisionEnter(Collision collision)
    {
        print("Collision!");
        if (startOnCollision)
        {
            StartTypewriter();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collisionExitOptions == options.complete)
        {
            if (tmpProText != null)
            {
                tmpProText.text = writer;
                Debug.Log(writer + "OnCollisionExit2D");
            }
        }
        // clear
        else
        {
            if (tmpProText != null)
            {
                tmpProText.text = "";
            }
        }

        StopAllCoroutines();
    }

    private void StartTypewriter()
	{
		StopAllCoroutines();
		
		if (tmpProText != null)
		{
			tmpProText.text = "";

			//StartCoroutine("TypeWriterTMP");
			TypeWriterTMP();
        }
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	IEnumerator TypeWriterTMP()
    {
	    tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

		Debug.Log(writer);

		foreach (char c in writer)
		{
			if (tmpProText.text.Length > 0)
			{
				tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
			}
			tmpProText.text += c;
			tmpProText.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
		}
	}
}
