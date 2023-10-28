using UnityEngine;
using UnityEngine.Events;

public class Ability : MonoBehaviour {
    #region Events

    public UnityEvent AbilityStarted;
    public UnityEvent AbilityEnded;

    #endregion

    private int _activeCounter;
    public int ActiveCounter {
        get => _activeCounter;
        set {
            _activeCounter = value;
            // shouldn't happen
            if (_activeCounter < 0) _activeCounter = 0;
        }
    }
    public bool CanActivate => ActiveCounter > 0;


    private bool _active = false;
    public void Do() {
        // TODO don't know if this should be here or in the implant controller
        if (!CanActivate || _active) return;

        _active = true;
        StartAbility();
    }
    public virtual void StartAbility() {
        AbilityStarted?.Invoke();
    }
    public virtual void EndAbility() {
        _active = false;
        AbilityEnded?.Invoke();
    }
}