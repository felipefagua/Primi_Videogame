using System;
using UnityEngine;

public class XBox360Input : GamePadInput {

    private const string XBOX360_PLATFORM_NAME = "XBox360";
    public const string BUTTON_A   = "A";
    public const string BUTTON_B = "B";
    public const string BUTTON_X = "X";
    public const string BUTTON_Y = "Y";
    public const string BUTTON_LB = "LB";
    public const string BUTTON_RB = "RB";
    public const string BUTTON_BACK = "Back";
    public const string BUTTON_START = "Start";
    public const string BUTTON_LEFT_ANALOGUE = "LeftAnalogue";
    public const string BUTTON_RIGHT_ANALOGUE = "RightAnalogue";

    public XBox360Input(int playerNumber) : base(playerNumber) {
        platformNamePrefix = XBOX360_PLATFORM_NAME;
    }

    public override bool GetButton(string buttonName) {
        XBox360KeyCode keyCode = ParseButtonNameToKeyCode(buttonName);
        return Input.GetKey(GetKeyCode(keyCode));
    }

    public override bool GetButtonDown(string buttonName) {
        XBox360KeyCode keyCode = ParseButtonNameToKeyCode(buttonName);
        return Input.GetKeyDown(GetKeyCode(keyCode));
    }

    public override bool GetButtonUp(string buttonName) {
        XBox360KeyCode keyCode = ParseButtonNameToKeyCode(buttonName);
        return Input.GetKeyUp(GetKeyCode(keyCode));
    }

    private KeyCode GetKeyCode(XBox360KeyCode keyCode) {
        string keyCodeName = GetButtonName(keyCode);
        return (KeyCode)Enum.Parse(typeof(KeyCode), keyCodeName);
    }

    private string GetButtonName(XBox360KeyCode keyCode) {
        int buttonNumber = (int)keyCode;
        string buttonName = GamePadInput.JOYSTICK + GetPlayerNumber(playerNumber) + GamePadInput.BUTTON + buttonNumber;
        return buttonName;
    }

    private string GetPlayerNumber(int i)
    {
        if (i == 0)
            return "";

        return "" + i;
    }


    private XBox360KeyCode ParseButtonNameToKeyCode(string buttonName) {
        XBox360KeyCode keyCode = (XBox360KeyCode)Enum.Parse(typeof(XBox360KeyCode), buttonName);
        return keyCode;
    }

}