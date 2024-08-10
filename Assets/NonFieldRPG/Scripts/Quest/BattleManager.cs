using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    [SerializeField] PlayerUIManager playerUI;
    [SerializeField] EnemyUIManager enemyUI;
    EnemyManager enemy;
    Action endBattleAction;
    Func<UniTask> gameOverAction;


    void Start()
    {
        enemyUI.gameObject.SetActive(false);
    }

    public void SetUp(EnemyManager enemy, Action endBattleAction, Func<UniTask> gameOverAction)
    {
        this.enemy = enemy;
        enemyUI.SetUp(enemy);
        enemyUI.gameObject.SetActive(true);
        enemy.AddEventListenerOnTap(PlayerTurn);
        this.endBattleAction += endBattleAction;
        this.gameOverAction += gameOverAction;
    }

    public async UniTask PlayerTurn()
    {
        SoundManager.instance.PlaySE(SE.Attack);
        var damage = player.Attack(enemy);
        enemyUI.UpdateUI(enemy);
        DialogTextManager.instance.SetScenarios(new string[] { $"プレイヤーの攻撃。\nモンスターに{damage}ダメージを与えた！" });
        if (enemy.HP <= 0)
        {
            await EndBattle();
        }
        else
        {
            await EnemyTurn();
        }
    }

    async UniTask EnemyTurn()
    {
        await UniTask.Delay(1000);
        SoundManager.instance.PlaySE(SE.Attack);
        int damage = enemy.Attack(player);
        playerUI.UpdateUI(player);
        DialogTextManager.instance.SetScenarios(new string[] { $"モンスターの攻撃。\nプレイヤーは{damage}ダメージを受けた。" });
        if (player.HP <= 0)
        {
            await UniTask.Delay(2000);
            await gameOverAction();
        }
        else
        {
            await UniTask.Delay(1000);
        }
    }

    async UniTask EndBattle()
    {
        await UniTask.Delay(1000);
        enemyUI.gameObject.SetActive(false);
        Destroy(enemy.gameObject);
        endBattleAction();
    }
}
