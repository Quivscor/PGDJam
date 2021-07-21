using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    public static bool lockControls = false;
    public static RegisteredInputs inputs;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        inputs = new RegisteredInputs();
    }

    // Update is called once per frame
    void Update()
    {
        if (lockControls)
            return;

        inputs.Reset();

        inputs.axis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputs.jump = Input.GetKeyDown(KeyCode.Space);
        inputs.releasedJump = Input.GetKeyUp(KeyCode.Space);
    }

    public static RegisteredInputs GetPlayerInput()
    {
        return inputs;
    }

    public static void BlockPlayerInput(bool value)
    {
        lockControls = value;
    }
}

public class RegisteredInputs
{
    public bool jump;
    public bool releasedJump;
    public Vector2 axis;

    public void Reset()
    {
        jump = releasedJump = false;
        axis = Vector2.zero;
    }

    public void DisplayInput()
    {
        Debug.Log("jump " + jump);
        Debug.Log("jumpRel " + releasedJump);
        Debug.Log(axis);
    }

}
