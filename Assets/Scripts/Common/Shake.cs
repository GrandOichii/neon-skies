using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

// TODO I don't like this shake
public class Shake : MonoBehaviour
{
    #region Serialized

    public float playerHitDuration;
    public float playerHitIntensivity;
    public float playerFiredDuration;
    public float playerFiredIntensivity;

    #endregion

    private CinemachineVirtualCamera _vCamera;
    private CinemachineBasicMultiChannelPerlin _cbmcp;
    private float _timer;

    void Awake() {
        _vCamera = GetComponent<CinemachineVirtualCamera>();
        _stop();
    }

    public void PlayerGotHit() {
        _cbmcp = _vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = playerHitIntensivity;
        _timer = playerHitDuration;

    }

    public void PlayerFired() {
        _cbmcp = _vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = playerFiredIntensivity;
        _timer = playerFiredDuration;
    }

    private void _stop() {
        _cbmcp = _vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0;
        _timer = 0;
    }

    void Update() {
        if (_timer > 0) {
            _timer -= Time.deltaTime;
            if (_timer <= 0) {
                _stop();
            }
        }
    }
}
