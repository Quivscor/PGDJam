using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingButtons : MonoBehaviour
{
    [SerializeField] private GameObject buttons;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            buttons.SetActive(!buttons.activeSelf);

    }
}
