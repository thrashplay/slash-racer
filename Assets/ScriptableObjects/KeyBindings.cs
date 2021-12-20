using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyBindings", menuName = "ScriptableObjects/KeyBindings", order = 1)]
public class KeyBindings : ScriptableObject
{
    public KeyCode[] SteerLeft = { KeyCode.A, KeyCode.S };
    
    public KeyCode[] SteerRight = { KeyCode.D, KeyCode.F };
}
