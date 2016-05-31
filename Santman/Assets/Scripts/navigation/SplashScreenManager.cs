using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField]
    private float _maxTime;

    [SerializeField]
    private float _currentTime;

    [SerializeField] 
    private SpriteRenderer _image;

    [SerializeField]
    private string _mainMenuSceneName;

	// Use this for initialization
	void Start () {
	    _currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    _currentTime += Time.deltaTime;
	    float alpha = _currentTime/_maxTime;
	    if (alpha >= 1)
	        alpha = 1;

	    float imageAlpha = 1-alpha;
	    Color newColor = _image.color;
	    newColor.a = imageAlpha;
	    _image.color = newColor;

        if (alpha == 1)
            SceneManager.LoadScene(_mainMenuSceneName);
	}
}
