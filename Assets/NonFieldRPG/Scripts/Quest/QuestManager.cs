using UnityEngine;
using DG.Tweening;
using System.Collections;
using Cysharp.Threading.Tasks;

public class QuestManager : MonoBehaviour
{
    [SerializeField] StageUIManager stageUI;
    [SerializeField] PlayerManager player;
    [SerializeField] PlayerUIManager playerUI;
    [SerializeField] EnemyManager enemyPrefab;
    [SerializeField] BattleManager battleManager;
    [SerializeField] SceneTransitionManager sceneTransitionManager;
    [SerializeField] GameObject questBG;

    readonly int[] encounterTable = { -1, -1, 0, -1, 0, -1 };
    int currentStage = 0;

    private void Start()
    {
        stageUI.UpdateUI(currentStage);
        playerUI.SetUp(player);
        DialogTextManager.instance.SetScenarios(new string[] { "これより探索を開始する。" });
    }

    async UniTask Search()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "探索中..." });
        questBG.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f).OnComplete(() =>
        {
            questBG.transform.DOScale(new Vector3(1f, 1f, 1f), 0);
        });
        var questBGSprite = questBG.GetComponent<SpriteRenderer>();
        questBGSprite.DOFade(0, 2f).OnComplete(() =>
        {
            questBGSprite.DOFade(1, 0);
        });

        await UniTask.Delay(2000);

        currentStage++;
        stageUI.UpdateUI(currentStage);

        if (encounterTable.Length <= currentStage)
        {
            QuestClear();
        }
        else if (encounterTable[currentStage] == 0)
        {
            EncounterEnemy();
        }
        else
        {
            stageUI.ToggleButton(true);
        }
    }

    public void OnNextButton()
    {
        SoundManager.instance.PlaySE(SE.Button);
        stageUI.ToggleButton(false);
        Search().Forget();
    }

    public void OnToTownButton()
    {
        SoundManager.instance.PlaySE(SE.Button);
        sceneTransitionManager.LoadTo(SceneName.Town);
    }

    void EncounterEnemy()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "モンスターに遭遇した！" });
        SoundManager.instance.PlayBGM(BGM.Battle);
        stageUI.ToggleButton(false);
        var enemy = Instantiate(enemyPrefab);
        battleManager.SetUp(enemy, EndBattle, GameOver);
    }

    void EndBattle()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "モンスターを撃破した！" });
        SoundManager.instance.PlayBGM(BGM.Quest);
        stageUI.ToggleButton(true);
    }

    void QuestClear()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "宝箱を手に入れた！\n街に戻りましょう。" });
        SoundManager.instance.StopBGM();
        SoundManager.instance.PlaySE(SE.Clear);
        stageUI.ShowClearUI();
    }

    async UniTask GameOver()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "プレイヤーは力尽きた..." });
        await UniTask.Delay(2000);
        sceneTransitionManager.LoadTo(SceneName.Town);
    }

}

