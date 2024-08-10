using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] SceneTransitionManager sceneTransitionManager;

    void Start()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "街に着いた！" });
    }

    public void OnToQuestButton()
    {
        SoundManager.instance.PlaySE(SE.Button);
        sceneTransitionManager.LoadTo(SceneName.Quest);
    }

}

