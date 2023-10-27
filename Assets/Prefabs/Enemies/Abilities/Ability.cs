using UnityEngine;

public class Ability : MonoBehaviour {
    private bool _active = false;
    public void Do(Object o) {
        if (_active) return;

        _active = true;
        StartAbility(o);
    }
    public virtual void StartAbility(UnityEngine.Object o) {}
    public virtual void EndAbility() {
        _active = false;
    }
}