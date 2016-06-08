using UnityEngine;
using System.Collections;

public class ScalableRatioUIComponent : MonoBehaviour {

    [SerializeField]
    private Rect _ratioSettings;

    private Rect _screenSettings;

    [SerializeField]
    private bool _updateOnUpdate;

    private RectTransform _myTransform;

	// Use this for initialization
	void Start () {
        _myTransform = GetComponent<RectTransform>();
        UpdateUIComponent();
	}
	
    // Update is called once per frame
	void Update () {
	    if (_updateOnUpdate)
            UpdateUIComponent();
    }

    private void UpdateUIComponent() {
        UpdateScreenSettings();
        UpdateComponentSize();
        UpdateComponentPosition();
    }

    private void UpdateScreenSettings() {
        RectTransform canvasTransform = (RectTransform)FindObjectOfType<Canvas>().transform;
        _screenSettings.width = canvasTransform.rect.width;
        _screenSettings.height = canvasTransform.rect.height;
    }

    private void UpdateComponentSize() {
        float width = (_screenSettings.width * _ratioSettings.width);
        float height = (_screenSettings.height * _ratioSettings.height);
        Vector2 size = new Vector2(width, height);
        _myTransform.sizeDelta = size;
    }

    private void UpdateComponentPosition() {
        float x = (_screenSettings.width * _ratioSettings.x);
        float y = (_screenSettings.height * _ratioSettings.y);
        Vector2 position = new Vector2(x, y);
        _myTransform.anchoredPosition = position;
    }
}
