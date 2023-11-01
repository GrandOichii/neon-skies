using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FireGun))]
public class LookAt : EnemyBehaviour
{
    #region Serialized

    public float followRange;
    public string revertToOnLost;

    #endregion

    GameObject _lookAt;
    public override void EBStart()
    {
        base.EBStart();

        _lookAt = _controller.Get<Collider2D>("enemy").gameObject;
    }
    public override void EBUpdate()
    {
        base.EBUpdate();

        var pos = _lookAt.transform.position;
        
        _controller.LookAt(pos);

        var hit = _controller.FireRay(followRange);
        if (hit.collider == null || !hit.collider.CompareTag("player")) {
            // TODO lost track of player
            _controller.Current = revertToOnLost;
        }

        GetComponent<FireGun>().Fire();
    }
}
