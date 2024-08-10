using UnityEngine;
using DG.Tweening;


public class FadeIOManager : MonoBehaviour
{
    public static FadeIOManager instance;
    [SerializeField] float fadeTime = 1f;
    [SerializeField] CanvasGroup canvasGroup;

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

    public void FadeOutToIn(TweenCallback action)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, fadeTime).OnComplete(() =>
        {
            action();
            canvasGroup.DOFade(0, fadeTime).OnComplete
            (() =>
            {
                canvasGroup.blocksRaycasts = false;
            });
        });
    }

}
