using UnityEngine;
using System.Collections;

public class NextLevelArrow : MonoBehaviour {

    [SerializeField]
    private float _minScale;

    [SerializeField]
    private float _maxScale;

    [SerializeField]
    private float _scaleTime;

    [SerializeField]
    private float _elapsedTime;

    [SerializeField]
    private bool _reducing;

    private RangeAttribute _scaleRange;

    private RectTransform _myTransform;

	// Use this for initialization
	void Start () {
	    _myTransform = GetComponent<RectTransform>();
        _scaleRange =  new RangeAttribute(_minScale, _maxScale);
	    InitRectTransform();
	}

    private void InitRectTransform() {
        _reducing = false;
        _elapsedTime = 0;
        SetScale(_scaleRange.min);
    }
    
    // Update is called once per frame
	void Update () {
	    Scale();
	}

    private void Scale() {
        _elapsedTime += Time.deltaTime;
        
        if (_reducing)
            Reduce();
        else
            Enlarge();

        if (_elapsedTime > _scaleTime)
            ToggleScalingMode();
    }

    private void ToggleScalingMode() {
        _elapsedTime = 0;
        _reducing = !_reducing;
    }

    private void Enlarge() {
        float alpha = GetAlpha();
        Scale(alpha);
    }

    private void Reduce() {
        float alpha = 1 - GetAlpha();
        Scale(alpha);
    }

    private void Scale(float alpha) {
        float scale = GetScale(alpha);
        SetScale(scale);
    }

    private float GetScale(float alpha) {
        return _scaleRange.min + ((_scaleRange.max - _scaleRange.min) * alpha);
    }

    private float GetAlpha() {
        if (_elapsedTime > _scaleTime) {
            return 1;
        }

        return _elapsedTime/_scaleTime;
    }
    
    private void SetScale(float scale) {
        _myTransform.localScale =  new Vector3(scale, scale, scale);
    }
}