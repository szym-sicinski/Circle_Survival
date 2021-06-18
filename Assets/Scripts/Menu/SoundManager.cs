using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType
{
    CLICK,
    BOOM,
    SPAWN,
}
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip boomClip;
    [SerializeField] private AudioClip spawnClip;
    
    [SerializeField] private AudioClip mainThemeClip;

    [SerializeField] private GameObject soundEffectPrefab;

    private void Awake()
    {
        // SINGLETON
        if (instance == null)
            instance = this;
        else
            if (instance != this)
                Destroy(gameObject);
        DontDestroyOnLoad(this);
    }
    public void PlaySound(SoundType type)
    {
        AudioClip chosenClip = null;

        switch (type)
        {
            case SoundType.CLICK:
                chosenClip = clickClip;
                break;
            case SoundType.BOOM:
                chosenClip = boomClip;
                break;
            case SoundType.SPAWN:
                chosenClip = spawnClip;
                break;
        }

        AudioSource soundEffectAS = Instantiate(soundEffectPrefab, transform).GetComponent<AudioSource>();

        soundEffectAS.clip = chosenClip;
        soundEffectAS.Play();

        Destroy(soundEffectAS.gameObject, chosenClip.length);
    }

}
