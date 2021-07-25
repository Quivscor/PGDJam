﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeController : MonoBehaviour
{
    public static ScreenFadeController Instance;

	public Image img;
	public AnimationCurve curve;

	private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

	public void Fade()
	{
		StartCoroutine(FadeOut());
	}
	IEnumerator FadeIn()
	{
		float t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}
	}

	IEnumerator FadeOut()
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}

		//settingsCanvas.SetActive(isPaused);
		//StartCoroutine(FadeIn());

	}
}
