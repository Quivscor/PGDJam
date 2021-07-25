using System.Collections;
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

	public void FadeOutAndIn()
	{
		StartCoroutine(FadeOut());
	}
	public void FadeOutAndInWithTeleport(Transform teleportDestination)
	{
		StartCoroutine(FadeOutTeleport(teleportDestination));
	}
	IEnumerator FadeIn()
	{
		float t = 2f;

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

		StartCoroutine(FadeIn());

	}

	IEnumerator FadeOutTeleport(Transform teleportDestination)
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}
		FindObjectOfType<PlayerMovement>().gameObject.transform.position = teleportDestination.position;
		StartCoroutine(FadeIn());

	}
}
