using UnityEngine;

public class XPOrb : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private int xpValue = 1;

    private Transform _player;
    private PlayerMagnet _playerMagnet;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            _player = playerObject.transform;
            _playerMagnet = playerObject.GetComponent<PlayerMagnet>();
        }
    }

    private void Update()
    {
        if (_player == null || _playerMagnet == null) return;

        float distance = Vector3.Distance(transform.position, _player.position);

        if (distance <= _playerMagnet.PickupRange)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _player.position,
                moveSpeed * Time.deltaTime
            );

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