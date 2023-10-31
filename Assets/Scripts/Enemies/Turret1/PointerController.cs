using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    #region Serialized
    
    public LineRenderer pointer;
    public GameObject pointerOrigin;

    #endregion

    public void OnLookedAt(Vector2 point) {
        pointer.SetPosition(0, pointerOrigin.transform.position);
        pointer.SetPosition(1, point);
        // pointer.SetPosition(1, new Vector2(point.x - pointerOrigin.transform.localPosition.x, point.y - pointerOrigin.transform.localPosition.y));
    }
}
