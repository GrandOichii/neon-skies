using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedLookForPlayer : EnemyBehaviour
{
    #region Serialized

    public float range;
    public string onFound;
    public float soundReactWait;
    public float soundReactRotationSpeed;

    #endregion

    private float _soundReactLeft = 0;

    private Vector3? _target;

    public override void EBStart()
    {
        base.EBStart();
        StartCoroutine(_watch());
    }


    public override void EBUpdate()
    {
        base.EBUpdate();

        if (_target != null) {
            var prev = _controller.transform.up;
            var target = _target - _controller.transform.position;
            _controller.transform.up = Vector3.RotateTowards(_controller.transform.up, (Vector3)target, soundReactRotationSpeed, 0f);
            if (prev.Equals(_controller.transform.up)) {
                _target = null;
                _soundReactLeft = soundReactWait;
            }
        }

        _point();

        if (_soundReactLeft <= 0) return;

        _soundReactLeft -= Time.deltaTime;
        if (_soundReactLeft <= 0) {
            _soundReactEnd();
        }
    }

    int _step = 1;
    IEnumerator _watch() {
        while (_active) {
            yield return new WaitUntil(() => _soundReactLeft <= 0 && _target == null);
            // _controller.transform.Rotate(0f, 0f, _step);
            _controller.transform.Rotate(Vector3.forward, _step);
            yield return new WaitForSeconds(.02f);
        }
    }

    void _point() {
        // var hit = _controller.FireRay(_controller.transform.up * range);
        var hit = _controller.FireRay(range);
        
        if (hit.collider != null && hit.collider.CompareTag("player")) {
            _controller.Set("enemy", hit.collider);
            _controller.Current = onFound;
        }
        
    }

    public void OnHeardSound(Collider2D collider) {
        _target = collider.transform.position;
        // _controller.LookAt(collider.transform.position);
    }

    void _soundReactEnd() {
        _soundReactLeft = 0;
    }


}
