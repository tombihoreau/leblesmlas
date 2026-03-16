using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject levelUpCanvas;
    public void SetUI(int level, int currentXP, int maxXP)
    {
        if (levelText is not null)
            levelText.text = "LVL " + level;

        float t = (float)currentXP / maxXP;
        fillImage.fillAmount = Mathf.Clamp01(t);
    }
}