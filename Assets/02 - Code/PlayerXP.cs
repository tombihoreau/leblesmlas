using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int currentXP;

    public void AddXP(int amount)
    {
        currentXP += amount;
        Debug.Log("XP: " + currentXP);
    }
}