using UnityEngine;
using System;

public class PlayerXP : MonoBehaviour
{
    public int level = 1;
    public int currentXP = 0;

    public int baseMaxXP = 10;
    [Range(0f, 200f)]
    public float growthPercentPerLevel = 10f;

    public static event Action<int, int, int> OnXPChanged;
    public static event Action<int> OnLevelUp;

    private int MaxXPForCurrentLevel()
    {
        float multiplier = Mathf.Pow(1f + growthPercentPerLevel / 100f, level - 1);
        return Mathf.Max(1, Mathf.RoundToInt(baseMaxXP * multiplier));
    }

    private void Start()
    {
        NotifyXPChanged();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= MaxXPForCurrentLevel())
        {
            currentXP -= MaxXPForCurrentLevel();
            level++;
            OnLevelUp?.Invoke(level);
        }

        NotifyXPChanged();
    }

    private void NotifyXPChanged()
    {
        OnXPChanged?.Invoke(level, currentXP, MaxXPForCurrentLevel());
    }
}