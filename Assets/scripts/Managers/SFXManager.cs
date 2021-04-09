using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    [SerializeField]
    List<SFXClip> clips;

    [SerializeField]
    int channelAmount = 5;

    List<AudioSource> sources = new List<AudioSource>();

    private void Start()
    {
        for(int i = 0; i < channelAmount; i++)
        {
            AudioSource src = this.gameObject.AddComponent<AudioSource>();
            src.minDistance = 10f;
            src.maxDistance = 20f;
            sources.Add(src);
        }
    }

    private void _PlaySFX(string name)
    {
        if(!clips.Exists(x => x.name == name))
        {
            Debug.LogWarning($"Clip {name} does not exist!");
            return;
        }

        AudioClip clip = clips.First(x => x.name == name).clip;
        AudioSource src = GetFirstEmptySrc();
        src.clip = clip;
        src.Play();
    }

    private void _PlaySFX(string name, Transform transform)
    {
        if(!clips.Exists(x => x.name == name))
        {
            Debug.LogWarning($"Clip {name} does not exist!");
            return;
        }

        AudioClip clip = clips.First(x => x.name == name).clip;
        
        Debug.Log("Playing clip");
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public static void PlaySFX(string name, Transform transform)
    {
        instance._PlaySFX(name, transform);
    }

    public static void PlaySFX(string name)
    {
        instance._PlaySFX(name);
    }

    private AudioSource GetFirstEmptySrc()
    {
        foreach(AudioSource src in sources)
        {
            if (!src.isPlaying) return src;
        }

        Debug.LogError($"Couldn't find empty audio channel. Please increase the channel amount.");
        return null;
    }
}

[System.Serializable]
public class SFXClip
{
    public string name;
    public AudioClip clip;
}