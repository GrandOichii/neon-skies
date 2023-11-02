using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FireGun))]
public class DeviationDisplay : StaticAbility
{
    #region Serialized

    public float lineLength;
    public LineRenderer line;

    #endregion

    private FireGun _fg;
    private bool _activated = false; 

    void Awake() {
        _fg = GetComponent<FireGun>();

    }

    public override void Activate()
    {
        base.Activate();
        _activated = true;

        _toggleLines();
        
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _activated = false;

        _toggleLines();
    }

    void _toggleLines() {
        line.enabled = _activated;
    }

    void Update() {
        if (!_activated) return;

        var pos = gameObject.transform.position;
        line.SetPosition(1, pos);

        float angle = _fg.Deviation;
        float rayRange = lineLength;
        float halfFOV = angle / 2.0f;
        float coneDirection = 180;

        Quaternion upRayRotation = Quaternion.AngleAxis(-halfFOV + coneDirection, Vector3.forward);
        Quaternion downRayRotation = Quaternion.AngleAxis(halfFOV + coneDirection, Vector3.forward);

        Vector3 upRayDirection = upRayRotation * line.transform.up * rayRange;
        Vector3 downRayDirection = downRayRotation * line.transform.up * rayRange;

        line.SetPosition(0, pos - upRayDirection);
        line.SetPosition(2, pos - downRayDirection);
    }
}
