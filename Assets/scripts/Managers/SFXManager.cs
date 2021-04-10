using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource _theme;
    
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    [SerializeField]
    List<SFXClip> clips;

    [SerializeField]
    int channelAmount = 5;

    [SerializeField]
    private float volumeIncreaseOnChase = .5f;

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

        // instance.PlayMainTheme();
    }

    #region Main theme

    private void PlayMainTheme()
    {
        AudioClip clip = clips.First(x => x.name == "Theme").clip;
        AudioSource src = GetFirstEmptySrc();

        src.clip = clip;
        src.loop = true;

        _theme = src;
    }

    public static void IncreaseMainThemeVolume()
    {
        instance._IncreaseMainThemeVolume();
    }

    public static void DecreaseMainThemeVolume()
    {
        instance._DecreaseMainThemeVolume();
    }
    
    private void _IncreaseMainThemeVolume()
    {
        instance._theme.volume += volumeIncreaseOnChase;
    }

    private void _DecreaseMainThemeVolume()
    {
        instance._theme.volume -= volumeIncreaseOnChase;
    }

    #endregion
    
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