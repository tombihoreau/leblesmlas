using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [Header("Level")]
    public int level = 1;

    [Header("XP")]
    public int currentXP = 0;

    [Tooltip("XP nécessaire pour passer du niveau 1 au niveau 2")]
    public int baseMaxXP = 10;

    [Tooltip("Ex: 10 = +10% d'XP requis par niveau")]
    [Range(0f, 200f)]
    public float growthPercentPerLevel = 10f;

    [Header("UI")]
    [SerializeField] private XPBarUI xpBarUI;

    private int MaxXPForCurrentLevel()
    {
        float multiplier = Mathf.Pow(1f + growthPercentPerLevel / 100f, level - 1);
        return Mathf.Max(1, Mathf.RoundToInt(baseMaxXP * multiplier));
    }

    private void Start()
    {
        RefreshUI();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= MaxXPForCurrentLevel())
        {
            currentXP -= MaxXPForCurrentLevel();
            level++;
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        int maxXP = MaxXPForCurrentLevel();
        if (xpBarUI != null)
            xpBarUI.SetUI(level, currentXP, maxXP);

        Debug.Log($"LVL {level} | XP {currentXP}/{maxXP} (+{growthPercentPerLevel}%/lvl)");
    }
}