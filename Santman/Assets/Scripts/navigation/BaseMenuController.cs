using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMenuController : MonoBehaviour {
    [SerializeField]
    protected Button[] _buttons;

    [SerializeField]
    protected Image _selectedOptionBar;

    [SerializeField]
    protected float _barOffset;

    [SerializeField]
    protected int _currentOption = 0;

    [SerializeField]
    protected KeyCode _nextOptionKeyCode;

    [SerializeField]
    protected KeyCode _prevOptionKeyCode;

    [SerializeField]
    protected KeyCode _selectOptionKeyCode;

    protected virtual void Update() {
        CheckInput();
        CheckPS4Input();
    }

    private void CheckPS4Input() {
		if (Input.GetAxis("Vertical") < -0.5)
			GotoOption(1);
		else if (Input.GetAxis("Vertical") > 0.5f)
			GotoOption(-1);
		else if (Input.GetButtonDown("Jump"))
			SelectOption(_currentOption);
    }

    private void CheckInput() {
        if (Input.GetKeyDown(_nextOptionKeyCode))
            GotoOption(1);
        else if (Input.GetKeyDown(_prevOptionKeyCode))
            GotoOption(-1);
        else if (Input.GetKeyDown(_selectOptionKeyCode))
            SelectOption(_currentOption);
    }

    private void GotoOption(int direction) {
        _currentOption += direction;
        if (_currentOption < 0)
            _currentOption = 0;
        if (_currentOption >= _buttons.Length)
            _currentOption = _buttons.Length - 1;
        MoveSelectedOptionBar(_currentOption);
    }

    protected abstract void SelectOption(int option);

    protected void MoveSelectedOptionBar(int optionIndex) {
        Button buttonToMove = _buttons[optionIndex];
        Vector3 newPosition = buttonToMove.transform.position;
        newPosition.y += _barOffset;
        _selectedOptionBar.rectTransform.position = newPosition;
    }
}