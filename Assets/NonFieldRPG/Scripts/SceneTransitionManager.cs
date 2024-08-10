using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    Title,
    Town,
    Quest,
}

public class SceneTransitionManager : MonoBehaviour
{
    readonly Dictionary<SceneName, BGM> sceneBGMMap = new()
    {
        {SceneName.Title, BGM.Title},
        {SceneName.Town, BGM.Town},
        {SceneName.Quest, BGM.Quest},
    };

    public void LoadTo(SceneName sceneName)
    {
        FadeIOManager.instance.FadeOutToIn(() =>
        {
            SceneManager.LoadScene(sceneName.ToString());
            SoundManager.instance.PlayBGM(sceneBGMMap[sceneName]);
        });
    }
}
