using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TestXBoxInputHandler : MonoBehaviour {
    
    public int playerNumber = -1;
    public float thumbStickTolerance = 0.1f;
    
    private XBox360Input XBox360Input;
    private bool debug;
    
    public int PlayerNumber {
        get { return playerNumber; }
        set {
            playerNumber = value;
            InitInput();
        }
    }

    private void Start() {
        InitInput();
    }

    private void InitInput() {
        if (playerNumber > 0) 
            XBox360Input = new XBox360Input(playerNumber);
    }

    private void Update() {
        CheckButtons();
        CheckButtonsDown();
        CheckButtonsUp();
        CheckAxis();
    }

    private void CheckButtons() {
        if (XBox360Input.GetButton(XBox360Input.BUTTON_A)) {
            DebugPress(XBox360Input.BUTTON_A);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_B)) {
            DebugPress(XBox360Input.BUTTON_B);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_X)) {
            DebugPress(XBox360Input.BUTTON_X);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_Y)) {
            DebugPress(XBox360Input.BUTTON_Y);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_LB)) {
            DebugPress(XBox360Input.BUTTON_LB);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_RB)) {
            DebugPress(XBox360Input.BUTTON_RB);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_BACK)) {
            DebugPress(XBox360Input.BUTTON_BACK);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_START)) {
            DebugPress(XBox360Input.BUTTON_START);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_LEFT_ANALOGUE)) {
            DebugPress(XBox360Input.BUTTON_LEFT_ANALOGUE);
        }
        if (XBox360Input.GetButton(XBox360Input.BUTTON_RIGHT_ANALOGUE)) {
            DebugPress(XBox360Input.BUTTON_RIGHT_ANALOGUE);
        }
    }

    private void CheckButtonsDown() {
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_A))
        {
            DebugDown(XBox360Input.BUTTON_A);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_B))
        {
            DebugDown(XBox360Input.BUTTON_B);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_X))
        {
            DebugDown(XBox360Input.BUTTON_X);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_Y))
        {
            DebugDown(XBox360Input.BUTTON_Y);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_LB))
        {
            DebugDown(XBox360Input.BUTTON_LB);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_RB))
        {
            DebugDown(XBox360Input.BUTTON_RB);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_BACK))
        {
            DebugDown(XBox360Input.BUTTON_BACK);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_START))
        {
            DebugDown(XBox360Input.BUTTON_START);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_LEFT_ANALOGUE))
        {
            DebugDown(XBox360Input.BUTTON_LEFT_ANALOGUE);
        }
        if (XBox360Input.GetButtonDown(XBox360Input.BUTTON_RIGHT_ANALOGUE))
        {
            DebugDown(XBox360Input.BUTTON_RIGHT_ANALOGUE);
        }
    }

    private void CheckButtonsUp() {
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_A)) {
            DebugUp(XBox360Input.BUTTON_A);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_B)) {
            DebugUp(XBox360Input.BUTTON_B);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_X)) {
            DebugUp(XBox360Input.BUTTON_X);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_Y)) {
            DebugUp(XBox360Input.BUTTON_Y);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_LB)) {
            DebugUp(XBox360Input.BUTTON_LB);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_RB)) {
            DebugUp(XBox360Input.BUTTON_RB);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_BACK)) {
            DebugUp(XBox360Input.BUTTON_BACK);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_START)) {
            DebugUp(XBox360Input.BUTTON_START);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_LEFT_ANALOGUE)) {
            DebugUp(XBox360Input.BUTTON_LEFT_ANALOGUE);
        }
        if (XBox360Input.GetButtonUp(XBox360Input.BUTTON_RIGHT_ANALOGUE)) {
            DebugUp(XBox360Input.BUTTON_RIGHT_ANALOGUE);
        }
    }

    private void CheckAxis() {
        Vector2 leftThumbStickValues = XBox360Input.GetLeftThumbStick();
        if (AxisIsActive(leftThumbStickValues)) {
            DebugAxis(GamePadInput.LEFT_THUMB_STICK_SUFIX, leftThumbStickValues);
        }

        Vector2 directionalPadValues = XBox360Input.GetDirectionalPad();
        if (AxisIsActive(directionalPadValues)) {
            DebugAxis(GamePadInput.DIRECTIONAL_PAD_SUFIX, directionalPadValues);
        }

        Vector2 rightThumbStickValues = XBox360Input.GetRightThumbStick();
        if (AxisIsActive(rightThumbStickValues)) {
            DebugAxis(GamePadInput.RIGHT_THUMB_STICK_SUFIX, rightThumbStickValues);
        }

        float leftTriggerValue = XBox360Input.GetLeftTrigger();
        if (AxisIsActive(leftTriggerValue)) {
            DebugAxis(GamePadInput.LEFT_TRIGGER_SUFIX, leftTriggerValue);
        }

        float rightTriggerValue = XBox360Input.GetRightTrigger();
        if (AxisIsActive(rightTriggerValue)) {
            DebugAxis(GamePadInput.RIGHT_TRIGGER_SUFIX, rightTriggerValue);
        }
    }

    private bool AxisIsActive(float value) {
        bool isActive = (Math.Abs(value) > thumbStickTolerance);
        return isActive;
    }

    private bool AxisIsActive(Vector2 axisValues) {
        bool isActive = (Math.Abs(axisValues.x) > thumbStickTolerance || Math.Abs(axisValues.y) > thumbStickTolerance);
        return isActive;
    }

    private void DebugAxis(string axisName, float axisValue) {
        Debug.Log("XboxController " + playerNumber + "axis: " + axisName + " value: " + axisValue);
    }

    private void DebugAxis(string axisName, Vector2 axisValues) {
        Debug.Log("XboxController " + playerNumber + "axis: " + axisName + " values: " + axisValues);
    }

    private void DebugPress(string buttonName) {
        Debug.Log(GetButtonMessage() + buttonName + " is pressed");
    }

    private void DebugDown(string buttonName) {
        Debug.Log(GetButtonMessage() + buttonName + " is down");
    }

    private void DebugUp(string buttonName) {
        Debug.Log(GetButtonMessage() + buttonName + " is up");
    }

    private string GetButtonMessage() {
        return "XBoxController " + playerNumber + " button: ";
    }
}

