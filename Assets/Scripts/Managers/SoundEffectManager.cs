using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    [NonSerialized] public static SoundEffectManager Instance; //singleton for SoundEffectManager
    public enum SoundEffectKey {none, placeCable, good, bad, electrified}; // for easier sound playing
    [SerializeField] private AudioClip[] _sounds; //for inputing sounds inside the inspector. Must be in the same order as above (Unity hates dictionarys(Won't Serialize them))
    private Dictionary<SoundEffectKey, AudioClip> _soundToPlay;
    private AudioSource _source;

    
    
    private void Awake() //Create Instance for singleton
    {//Create singleton for SoundEffectManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);
        InitSounds();
    }
    private void InitSounds() // Add sounds to the Dictionary with their respective enum as the key
    {
        _soundToPlay = new Dictionary<SoundEffectKey, AudioClip>
        {
            {SoundEffectKey.placeCable, _sounds[0]},
            {SoundEffectKey.good, _sounds[1]},
            {SoundEffectKey.bad, _sounds[2]},
            {SoundEffectKey.electrified, _sounds[3]}
        };
        _source = GetComponent<AudioSource>();
    }
    public void PlaySoundEffect(SoundEffectKey _sound)
    {
        _source.clip = _soundToPlay[_sound];
        _source.Play();
    }
}
