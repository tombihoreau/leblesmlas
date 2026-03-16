using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class UpgradeCard : MonoBehaviour
{
    public enum UpgradeType
    {
        AttackSpeed,
        AttackRange,
        AttackSize,
        CowboyDamage,
        CowboyMoveSpeed,
        CowboyRangeXP,

        NewExplosionWeapon,
        ExplosionAttackRange,
        ExplosionSize
    }
    [SerializeField] private UpgradeType upgradeType;

    [Header("Valeurs d'amélioration")]
    [SerializeField] private float attackSpeedPercent = 0.15f;
    [SerializeField] private float attackRangeBonus = 1f;
    [SerializeField] private float attackSizeBonus = 0.3f;
    [SerializeField] private float damageBonus = 2f;
    [SerializeField] private float moveSpeedBonus = 0.5f;
    [SerializeField] private float pickupRangeBonus = 1f;

    [Header("Weapons")]
    [SerializeField] private SwordWeapon swordWeapon;
    [SerializeField] private ExplosionWeapon explosionWeapon;

    private LevelUpUI _levelUpUI;
    private Button _button;
    private ThirdPersonController _thirdPersonController;
    private PlayerMagnet _playerMagnet;

    private void Awake()
    {
        _levelUpUI = FindFirstObjectByType<LevelUpUI>();
        _button = GetComponentInChildren<Button>();

        if (swordWeapon == null)
            swordWeapon = FindFirstObjectByType<SwordWeapon>();

        _thirdPersonController = FindFirstObjectByType<ThirdPersonController>();
        _playerMagnet = FindFirstObjectByType<PlayerMagnet>();

        if (_button != null)
        {
            _button.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("UpgradeCard: pas de Button sur " + gameObject.name);
        }
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnClick);
    }

    public bool CanBeShown()
    {
        bool result = false;

        switch (upgradeType)
        {
            case UpgradeType.NewExplosionWeapon:
                result = explosionWeapon != null && !explosionWeapon.enabled;
                break;

            case UpgradeType.ExplosionAttackRange:
            case UpgradeType.ExplosionSize:
                result = explosionWeapon != null && explosionWeapon.enabled;
                break;

            default:
                result = true;
                break;
        }

        return result;
    }

    private void OnClick()
    {
        ApplyUpgrade();

        if (_levelUpUI != null)
            _levelUpUI.HideNow();
    }

    private void ApplyUpgrade()
    {
        switch (upgradeType)
        {
            case UpgradeType.AttackSpeed:
                if (swordWeapon != null)
                    swordWeapon.IncreaseAttackSpeed(attackSpeedPercent);
                break;

            case UpgradeType.AttackRange:
                if (swordWeapon != null)
                    swordWeapon.IncreaseAttackRange(attackRangeBonus);
                break;

            case UpgradeType.AttackSize:
                if (swordWeapon != null)
                    swordWeapon.IncreaseAttackSize(attackSizeBonus);
                break;

            case UpgradeType.CowboyDamage:
                if (swordWeapon != null)
                    swordWeapon.IncreaseDamage(damageBonus);
                break;

            case UpgradeType.CowboyMoveSpeed:
                if (_thirdPersonController != null)
                    _thirdPersonController.IncreaseMoveSpeed(moveSpeedBonus);
                break;

            case UpgradeType.CowboyRangeXP:
                if (_playerMagnet != null)
                    _playerMagnet.IncreasePickupRange(pickupRangeBonus);
                break;

            case UpgradeType.NewExplosionWeapon:
                if (explosionWeapon != null)
                {
                    explosionWeapon.enabled = true;
                    Debug.Log("ExplosionWeapon débloquée");
                }
                break;

            case UpgradeType.ExplosionAttackRange:
                if (explosionWeapon != null)
                    explosionWeapon.IncreaseAttackRange(attackRangeBonus);
                break;

            case UpgradeType.ExplosionSize:
                if (explosionWeapon != null)
                    explosionWeapon.IncreaseAttackSize(attackSizeBonus);
                break;
        }

        Debug.Log("Upgrade appliquée : " + upgradeType);
    }
}