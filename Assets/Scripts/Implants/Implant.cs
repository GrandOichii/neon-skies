using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO should be plural?
public enum ImplantSlot {
    HEAD,
    TORSO,
    ARM,
    HAND,
    LEG,
    FOOT
}

[CreateAssetMenu(fileName = "Implant")]
public class Implant : ScriptableObject
{
    public new string name;
    [TextArea]
    public string description;
    public ImplantSlot slot;
    public List<string> enables;

}
