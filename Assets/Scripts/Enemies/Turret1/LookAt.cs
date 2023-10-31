using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : EnemyBehaviour
{
    #region Serialized

    public float followRange;

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
        print(pos);
        
        _controller.LookAt(pos);
        // TODO check that still looking at player
        _controller.FireRay(followRange);
    }
}
