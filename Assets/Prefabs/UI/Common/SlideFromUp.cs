using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideFromUp : MonoBehaviour
{
    
    #region Serialized

    public KeyCode toggleVisibilityKey;

    #endregion

    private bool _visible = false;
    private bool _inProcess = false;

    void Awake() {
        transform.position = new(transform.position.x, Screen.height, transform.position.z);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleVisibilityKey)) {
            _toggleVisible();
        }
    }
    
    private void _toggleVisible() {
        
        if (_inProcess) return;
        
        _inProcess = true;
        var moveY = Screen.height;
        if (!_visible) moveY = 0;

        LeanTween.moveY(gameObject, moveY, .2f).setOnComplete(() => _inProcess = false);
        _visible = !_visible;
    }
}
