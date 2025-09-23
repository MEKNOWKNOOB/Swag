using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // [NonSerialized] 
    public static AudioManager instance;

    [Header("Audio System")]
    [SerializeField] private int sourceCount = 1;


    [Header("Audio Reference")]
    [SerializeField] private List<AudioSource> sourceCache;
    [SerializeField] private List<Audio> audioClips;
    [NonSerialized] private Dictionary<string, Audio> audioReference;



    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;

        audioReference = new Dictionary<string, Audio>();

        for (int i = 1; i < sourceCount; i++)
        {
            sourceCache.Add(Instantiate(sourceCache[0], transform));
        }

        foreach (Audio entry in audioClips)
        {
            audioReference.Add(entry.name, entry);
        }
    }
    void Start()
    {
        AudioManager.instance.PlayLoop("Test", new Vector3(0,0,0), -0.8f);
    }



    private int FindFreeSource()
    {
        for (int i = 0; i < sourceCache.Count; i++)
        {
            if (!sourceCache[i].isPlaying)
            {
                return i;
            }
        }
        return -1;
    }

    // Hacky, but we need to get the audio source to fade it in and out
    public AudioSource GetAudioSource(int ad)
    {
        return sourceCache[ad];
    }

    // 2D Audio
    public int PlayGeneric(String audioName)
    {
        if (!audioReference.ContainsKey(audioName))
        {
            return -1;
        }

        int ad = FindFreeSource(); // Audio Descriptor

        if (ad == -1)
        {
            return -1;
        }

        AudioSource activateAudioSource = sourceCache[ad];
        activateAudioSource.loop = false;
        activateAudioSource.volume = audioReference[audioName].volume;
        activateAudioSource.clip = audioReference[audioName].clip;
        activateAudioSource.spatialBlend = 0;
        activateAudioSource.pitch = 1;
        activateAudioSource.Play();

        return ad;
    }
    
    public int StopGeneric(int ad)
    {
        if (!sourceCache[ad].isPlaying)
        {
            return -1;
        }
        sourceCache[ad].Stop();
        return -1;
    }

    public int PlayRandomPitch(String audioName)
    {
        int ad = PlayGeneric(audioName);
        if (ad == -1)
        {
            return -1;
        }
        sourceCache[ad].pitch = math.pow(0.9f, UnityEngine.Random.Range(-1f, 1f));
        return ad;
    }

    public int PlayLoop(String audioName)
    {
        int ad = PlayGeneric(audioName);

        if (ad == -1)
        {
            return -1;
        }

        sourceCache[ad].loop = true;
        return ad;
    }
    
    public int PlayLoop(String audioName, float pitch)
    {
        int ad = PlayLoop(audioName);
        if (ad == -1)
        {
            return -1;
        }
        sourceCache[ad].pitch = pitch;
        return ad;
    }



    // 3D Audio    
    public int PlayGeneric(String audioName, Vector3 position)
    {
        int ad = PlayGeneric(audioName);
        if (ad == -1)
        {
            return -1;
        }
        sourceCache[ad].transform.position = position;
        sourceCache[ad].spatialBlend = 1;
        return ad;
    }

    public int PlayRandomPitch(String audioName, Vector3 position)
    {
        int ad = PlayGeneric(audioName, position);
        if (ad == -1)
        {
            return -1;
        }
        sourceCache[ad].pitch = math.pow(0.9f, UnityEngine.Random.Range(-1f, 1f));
        return ad;
    }

    public int PlayLoop(String audioName, Vector3 position)
    {
        int ad = PlayLoop(audioName);
        if (ad == -1)
        {
            return -1;
        }
        sourceCache[ad].transform.position = position;
        sourceCache[ad].spatialBlend = 1;
        return ad;
    }
    
    public int PlayLoop(String audioName, Vector3 position, float pitch)
    {
        int ad = PlayLoop(audioName);
        if (ad == -1)
        {
            return -1;
        }
        sourceCache[ad].transform.position = position;
        sourceCache[ad].spatialBlend = 1;
        sourceCache[ad].pitch = pitch;
        return ad;
    }
}
