using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using UnityEngine;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    public List<AudioSource> audioSources;
    public List<AudioClip> audioClips;

    void Awake()
    {
        audioSources = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();
        InitializeSoundFX();
    }

    private void InitializeSoundFX()
    {
        // preload effects
        audioClips.Add(Resources.Load<AudioClip>("Audio/jump-sound")); // 0
        audioClips.Add(Resources.Load<AudioClip>("Audio/hurt-sound")); // 1
        audioClips.Add(Resources.Load<AudioClip>("Audio/gem-sound"));  // 2
        audioClips.Add(Resources.Load<AudioClip>("Audio/death-sound"));  // 3
        audioClips.Add(Resources.Load<AudioClip>("Audio/bullet-sound"));  // 4
        audioClips.Add(Resources.Load<AudioClip>("Audio/growl-sound"));  // 5

        // preload music here
        audioClips.Add(Resources.Load<AudioClip>("Audio/main-soundtrack"));  // 6
    }

    public void PlaySoundFX(Channel channel, SoundFX sound)
    {
        audioSources[(int)channel].clip = audioClips[(int)sound]; // loads the clip
        audioSources[(int)channel].Play();
    }

    public void StopSoundFX(Channel channel, SoundFX sound)
    {
        audioSources[(int)channel].clip = audioClips[(int)sound]; 
        audioSources[(int)channel].Stop();
    }

    public void PlayMusic()
    {
        audioSources[(int)Channel.MUSIC].clip = audioClips[(int)SoundFX.MUSIC];
        audioSources[(int)Channel.MUSIC].volume = 0.1f;
        audioSources[(int)Channel.MUSIC].loop = true;
        audioSources[(int)Channel.MUSIC].Play();
    }


}
