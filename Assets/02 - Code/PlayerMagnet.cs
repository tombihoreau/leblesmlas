using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] private float pickupRange = 4f;

    public float PickupRange => pickupRange;

    public void IncreasePickupRange(float amount)
    {
        pickupRange += amount;
    }
}