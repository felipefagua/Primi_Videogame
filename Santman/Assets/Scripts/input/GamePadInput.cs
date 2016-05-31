using UnityEngine;

public abstract class GamePadInput {

    public const string JOYSTICK                = "Joystick";
    public const string BUTTON                  = "Button";
    public const string LEFT_THUMB_STICK_SUFIX  = "_LeftThumbstick";
    public const string DIRECTIONAL_PAD_SUFIX   = "_DPad";
    public const string RIGHT_THUMB_STICK_SUFIX = "_RightThumbstick";
    public const string LEFT_TRIGGER_SUFIX      = "_LeftTrigger";
    public const string RIGHT_TRIGGER_SUFIX     = "_RightTrigger";
    public const string HORIZONTAL_SUFIX        = "_H";
    public const string VERTICAL_SUFIX          = "_V";

    protected string platformNamePrefix;
    protected int playerNumber;

    public GamePadInput(int playerNumber) {
        this.playerNumber = playerNumber;
    }

    public Vector2 GetLeftThumbStick() {
        return GetInputVectorByName(LEFT_THUMB_STICK_SUFIX);
    }

    public Vector2 GetDirectionalPad() {
        return GetInputVectorByName(DIRECTIONAL_PAD_SUFIX);
    }

    public Vector2 GetRightThumbStick() {
        return GetInputVectorByName(RIGHT_THUMB_STICK_SUFIX);
    }

    public float GetLeftTrigger() {
        return Input.GetAxis(platformNamePrefix + GetStringPlayerNumber() + LEFT_TRIGGER_SUFIX);
    }

    public float GetRightTrigger() {
        return Input.GetAxis(platformNamePrefix + GetStringPlayerNumber() + RIGHT_TRIGGER_SUFIX);
    }

    private Vector2 GetInputVectorByName(string axisName) {
        float x = Input.GetAxis(platformNamePrefix + GetStringPlayerNumber() + axisName + HORIZONTAL_SUFIX);
        float y = Input.GetAxis(platformNamePrefix + GetStringPlayerNumber() + axisName + VERTICAL_SUFIX);
        return new Vector2(x, y);
    }

    public abstract bool GetButton(string buttonName);

    public abstract bool GetButtonDown(string buttonName);

    public abstract bool GetButtonUp(string buttonName);

    private string GetStringPlayerNumber() {
        string stringPlayerNumber = "" + playerNumber;
        if (playerNumber < 10)
            stringPlayerNumber = "0" + playerNumber;
        stringPlayerNumber = "_" + stringPlayerNumber;
        return stringPlayerNumber;
    }
}