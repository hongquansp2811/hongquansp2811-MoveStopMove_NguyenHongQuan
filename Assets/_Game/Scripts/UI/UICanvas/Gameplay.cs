using TMPro;
using UnityEngine;

public class Gameplay : UICanvas
{
    [SerializeField] private TextMeshProUGUI aliveText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        if (LevelManager.Ins.currentMap.DataConfig.TimeToPlay > 0)
        {
            aliveText.gameObject.SetActive(true);
        }
        else
        {
            timeText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        UpdateAliveText();
        UpdateTimeText();
    }

    private void UpdateAliveText()
    {
        aliveText.text = $"Alive: {LevelManager.Ins.currentMap.GetAliveCount()}";
        coinText.text = LevelManager.Ins.GetCoint().ToString();
    }

    private void UpdateTimeText()
    {
        timeText.text = LevelManager.Ins.currentMap.timeText;
    }

    public void SettingButton()
    {
        GameManager.Ins.ChangeState(GameState.Pause);
    }
}
