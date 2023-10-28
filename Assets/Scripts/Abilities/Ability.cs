using UnityEngine;
using UnityEngine.Events;

public class Ability : MonoBehaviour {
    #region Events

    public UnityEvent AbilityStarted;
    public UnityEvent AbilityEnded;

    #endregion


    private bool _active = false;
    public void Do() {
        if (_active) return;

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