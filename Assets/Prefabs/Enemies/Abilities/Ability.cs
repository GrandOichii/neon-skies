using UnityEngine;
using UnityEngine.Events;

public class Ability : MonoBehaviour {
    #region Events

    public UnityEvent AbilityStarted;
    public UnityEvent AbilityEnded;

    #endregion


    private bool _active = false;
    public void Do(Object o) {
        if (_active) return;

        _active = true;
        StartAbility(o);
    }
    public virtual void StartAbility(UnityEngine.Object o) {
        AbilityStarted?.Invoke();
    }
    public virtual void EndAbility() {
        _active = false;
        AbilityEnded?.Invoke();
    }
}