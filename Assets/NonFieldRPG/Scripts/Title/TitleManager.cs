using UnityEngine;

public class TownManager : MonoBehaviour
{
    [SerializeField] SceneTransitionManager sceneTransitionManager;

    public void OnToQuestButton()
    {
        SoundManager.instance.PlaySE(SE.Button);
        sceneTransitionManager.LoadTo(SceneName.Town);
    }

}

