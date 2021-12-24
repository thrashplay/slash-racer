using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EngineSound
{
    Low,
    High,
    None
}

public class SoundManager : MonoBehaviour
{
    public AudioSource engine;
    public AudioSource explosion;
    public AudioSource itemPickedUp;
    public AudioSource lowTimeWarning;

    public BooleanValue isPaused;
    public SoundConfig soundConfig;

    private EngineSound _engineSound = EngineSound.None;

    public void PlayEngine(EngineSound sound)
    {
        if (_engineSound == EngineSound.None && sound != EngineSound.None)
        {
            SetEngineSoundPlaying(true);
        } else if (_engineSound != EngineSound.None && sound == EngineSound.None)
        {
            SetEngineSoundPlaying(false);
        }

        _engineSound = sound;
    }

    public void PlayExplosion()
    {
        explosion.PlayOneShot(explosion.clip);
    }
    
    public void PlayItemPickedUp()
    {
        itemPickedUp.PlayOneShot(itemPickedUp.clip);
    }

    public void PlayLowTimeWarning()
    {
        lowTimeWarning.PlayOneShot(lowTimeWarning.clip);
    }

    private void LateUpdate()
    {
        if (_engineSound == EngineSound.None)
        {
            return;
        }

        if (isPaused.Value)
        {
            engine.Pause();
        }
        else
        {
            engine.UnPause();

            var start = _engineSound switch {
                EngineSound.Low => soundConfig.engineStage2Start,
                EngineSound.High => soundConfig.engineStage4Start,
                _ => 0
            };

            var end = _engineSound switch {
                EngineSound.Low => soundConfig.engineStage2End,
                EngineSound.High => soundConfig.engineStage4End,
                _ => 0
            };

            if (engine.time < start || engine.time >= end)
            {
                engine.time = start;
                engine.Play();
            }
        }
    }

    private void SetEngineSoundPlaying(bool playing)
    {
        if (!playing)
        {
            engine.Stop();
        }
    }
}
