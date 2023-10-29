using UnityEngine;
using UnityEngine.Events;

// public class StaticAbiltiy : Ability {

// }

// public class ActivatedAbility : Ability {

// }

public class Ability : MonoBehaviour {
    #region Events

    public UnityEvent activated;
    public UnityEvent deactivated;

    #endregion
    
    private int _activeCounter;
    public int ActiveCounter {
        get => _activeCounter;
        set {
            if (value < 0) {
                return;
            }
            if (_activeCounter == 0 && value > 0) {
                Activate();
            }
            _activeCounter = value;
            if (_activeCounter <= 0)  {
                _activeCounter = 0;
                Deactivate();
            }
        }
    }
    public bool CanActivate => ActiveCounter > 0;


    public virtual void Activate() {
        activated.Invoke();
    }

    public virtual void Deactivate() {
        deactivated.Invoke();
    }
}

public class StaticAbility : Ability {

}

public class ActivatedAbility : StaticAbility {
    #region Serialized


    #endregion

    #region Events

    public UnityEvent AbilityStarted;
    public UnityEvent AbilityEnded;

    #endregion

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