using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "ScriptableObjects/SoundConfig", order = 1)]
public class SoundConfig : ScriptableObject
{
    public float engineStage1Start = 2.544F;
    public float engineStage1End = 4.717F;

    public float engineStage2Start = 4.8F + 0.144F;
    public float engineStage2End = 7.2F - 0.083F;

    public float engineStage3Start = 7.2F + 0.144F;
    public float engineStage3End = 9.6F - 0.083F;
    public float engineStage4Start = 9.6F + 0.144F;
    public float engineStage4End = 12F - 0.083F;
}
