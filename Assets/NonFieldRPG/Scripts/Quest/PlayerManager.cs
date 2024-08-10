using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int at;
    [SerializeField] Transform playerDamagePanel;

    public int HP => hp;
    public int AT => at;

    public int Attack(EnemyManager enemy)
    {
        return enemy.Damage(at);
    }

    public int Damage(int damage)
    {
        playerDamagePanel.DOShakePosition(0.3f, 0.5f, 20, 90, false, true).SetLink(gameObject);
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        return damage;
    }
}
