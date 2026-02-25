using UnityEngine;

public class XPOrb : MonoBehaviour
{
    [SerializeField] private float pickupRange = 2f;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private int xpValue = 1;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_player == null) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= pickupRange)
        {
            // déplacement vers le joueur
            transform.position = Vector3.MoveTowards(
                transform.position,
                _player.position,
                moveSpeed * Time.deltaTime
            );

            // si très proche → collecter
            if (distance <= 0.2f)
            {
                Collect();
            }
        }
    }

    private void Collect()
    {
        PlayerXP playerXP = _player.GetComponent<PlayerXP>();
        if (playerXP != null)
        {
            playerXP.AddXP(xpValue);
        }

        Destroy(gameObject);
    }
}