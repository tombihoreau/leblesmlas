using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private float pickupRange = 2f;

    public float PickupRange => pickupRange;

    public void IncreasePickupRange(float amount)
    {
        pickupRange += amount;
        Debug.Log("Nouvelle portée de collecte : " + pickupRange);
    }
}