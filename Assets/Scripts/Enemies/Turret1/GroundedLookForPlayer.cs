using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedLookForPlayer : EnemyBehaviour
{
    #region Serialized

    public float range;
    public string onFound;

    #endregion

    public override void EBStart()
    {
        base.EBStart();
        StartCoroutine(_watch());
    }

    public override void EBUpdate()
    {
        base.EBUpdate();
    }

    int _step = 1;
    IEnumerator _watch() {
        while (_active) {
            _controller.transform.Rotate(0f, 0f, _step);
            _point();
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
    
}
