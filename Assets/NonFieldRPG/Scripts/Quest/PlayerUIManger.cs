using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atText;

    public void SetUp(PlayerManager player)
    {
        hpText.text = $"HP：{player.HP}";
        atText.text = $"AT：{player.AT}";
    }

    public void UpdateUI(PlayerManager player)
    {
        hpText.text = $"HP：{player.HP}";
    }
}
