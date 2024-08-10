using System;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] new string name;
    [SerializeField] int hp;
    [SerializeField] int at;
    [SerializeField] GameObject hitEffect;

    Func<UniTask> tapAction;
    bool isProcessing = false;

    public string Name => name;
    public int HP => hp;

    public int Attack(PlayerManager player)
    {
        return player.Damage(at);
    }

    public int Damage(int damage)
    {
        Instantiate(hitEffect, this.transform, false);
        transform.DOShakePosition(0.3f, 0.5f, 20, 90, false, true).SetLink(gameObject);
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        return damage;
    }

    public void AddEventListenerOnTap(Func<UniTask> action)
    {
        tapAction += action;
    }


    public async void OnTap()
    {
        if (isProcessing) return;
        isProcessing = true;
        try
        {
            await ExecuteTapAction();
        }
        finally
        {
            isProcessing = false;
        }
    }

    async UniTask ExecuteTapAction()
    {
        if (tapAction != null)
        {
            await tapAction();
        }
    }
}
