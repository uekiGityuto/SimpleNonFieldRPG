using TMPro;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI hpText;

    public void SetUp(EnemyManager enemy)
    {
        nameText.text = enemy.Name;
        hpText.text = $"HP：{enemy.HP}";
    }

    public void UpdateUI(EnemyManager enemy)
    {
        hpText.text = $"HP：{enemy.HP}";
    }
}
