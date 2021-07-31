using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
	public static SettingsController Instance;

	public Image img;
	public AnimationCurve curve;
	public GameObject settingsCanvas;
	public GameObject aboutCanvas;
	public GameObject intro;
	public bool isPaused;
	[SerializeField] private Animator[] animators;
	private bool introPlayed = false;
	private void Awake()
    {
		if (Instance == null)
			Instance = this;
    }

    private void Start()
    {
		PlayerInput.BlockPlayerInput(true);

        foreach (Animator animator in animators)
        {
			animator.keepAnimatorControllerStateOnDisable = true;
		}

	}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
				isPaused = true;
				PlayerInput.BlockPlayerInput(true);
				settingsCanvas.SetActive(isPaused);
			}
        }

		if(!introPlayed && Input.GetKeyDown(KeyCode.Space))
        {
			introPlayed = true;
			PlayerInput.BlockPlayerInput(false);
			intro.SetActive(false);
        }

    }

	public void PauseGame(bool toggle)
    {
		isPaused = toggle;
		PlayerInput.BlockPlayerInput(toggle);
		settingsCanvas.SetActive(toggle);

	}

	public void ToggleAboutCanvas(bool toggle)
    {
		settingsCanvas.SetActive(!toggle);
		aboutCanvas.SetActive(toggle);
	}

    public void FadeOutAndIn()
    {
		StartCoroutine(FadeOut());
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

		settingsCanvas.SetActive(isPaused);
		//StartCoroutine(FadeIn());

	}

}
