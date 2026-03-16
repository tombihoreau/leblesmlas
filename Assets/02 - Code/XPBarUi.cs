using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text levelText;

    private void OnEnable()
    {
        PlayerXP.OnXPChanged += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerXP.OnXPChanged -= UpdateUI;
    }

    private void UpdateUI(int level, int currentXP, int maxXP)
    {
        if (levelText is not null)
            levelText.text = "LVL " + level;

        float t = (float)currentXP / maxXP;
        fillImage.fillAmount = Mathf.Clamp01(t);
    }
}