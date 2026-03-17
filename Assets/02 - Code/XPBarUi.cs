using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text killCounterText;

    private int killCount = 0;

    private void OnEnable()
    {
        PlayerXP.OnXPChanged += UpdateUI;
        EnemyHealth.OnEnemyKilled += OnEnemyKilled;
    }
    private void OnDisable()
    {
        PlayerXP.OnXPChanged -= UpdateUI;
        EnemyHealth.OnEnemyKilled -= OnEnemyKilled;
    }

    private void UpdateUI(int level, int currentXP, int maxXP)
    {
        if (levelText is not null)
            levelText.text = "LVL " + level;

        float t = (float)currentXP / maxXP;
        fillImage.fillAmount = Mathf.Clamp01(t);
    }

    private void OnEnemyKilled()
    {
        killCount++;
        if (killCounterText != null)
            killCounterText.text = "Enemis achevés : " + killCount;
    }
}