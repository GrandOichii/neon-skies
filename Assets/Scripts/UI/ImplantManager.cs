using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplantManager : MonoBehaviour
{
    #region Serialized



    #endregion

    #region Events



    #endregion

    private bool _visible = true;
    private bool _inProcess = false;

    void Awake() {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
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
