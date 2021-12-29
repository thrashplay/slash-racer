using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableConfig : ScriptableObject
{
    // whether this object type is enabled or not
    public bool Enabled = true;

    // minimize vertical space between spawned objects
    public float FrequencyMin = 6;

    // maximize vertical space between spawned objects
    public float FrequencyMax = 20;

    // the initial distance before this object can spawn at all
    public float InitialDelay = 0;
}
