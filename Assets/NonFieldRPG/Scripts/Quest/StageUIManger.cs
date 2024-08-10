using TMPro;
using UnityEngine;

public class StageUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stageText;
    [SerializeField] GameObject stageClear;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject toTownButton;

    void Start()
    {
        stageClear.SetActive(false);
    }


    public void UpdateUI(int currentStage)
    {
        stageText.text = $"ステージ：{currentStage + 1}";
    }

    public void ToggleButton(bool isShow)
    {
        nextButton.SetActive(isShow);
        toTownButton.SetActive(isShow);
    }

    public void ShowClearUI()
    {
        stageClear.SetActive(true);
        nextButton.SetActive(false);
        toTownButton.SetActive(true);
    }
}
