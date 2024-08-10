using UnityEngine;

public enum BGM
{
    Title,
    Town,
    Quest,
    Battle,
}

public enum SE
{
    Button,
    Attack,
    Clear,
}

public class SoundManager : MonoBehaviour
{


    public static SoundManager instance;

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource seSource;
    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] AudioClip[] seClips;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGM bgm)
    {
        StopBGM();
        bgmSource.clip = bgmClips[(int)bgm];
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(SE se)
    {
        StopSE();
        seSource.clip = seClips[(int)se];
        seSource.PlayOneShot(seSource.clip);
    }

    public void StopSE()
    {
        seSource.Stop();
    }

}
