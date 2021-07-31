using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestingButtons : MonoBehaviour
{
    public static TestingButtons Instance;

    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject stats;
    public TextMeshProUGUI statsText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if(Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.F1))
                buttons.SetActive(!buttons.activeSelf);

            if (Input.GetKeyDown(KeyCode.F2))
            {
                StatsController.Instance.UpdateDebugDisplay();
                stats.SetActive(!stats.activeSelf);
            }
        }


    }
}
