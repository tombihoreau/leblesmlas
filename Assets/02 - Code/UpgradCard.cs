using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType
{
    AttackSpeed,
    AttackRange,
    AttackSize,
    CowboyDamage,
    CowBoyMoveSpeed,
    CowBoyRangeXP
}

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private UpgradeType upgradeType;
    private LevelUpUI _levelUpUI;
    private Button _button;

    private void Awake()
    {
        _levelUpUI = FindFirstObjectByType<LevelUpUI>();
        _button = GetComponentInChildren<Button>();
        Debug.Log("UpgradeCard: " + _button);
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

    private void OnClick()
    {
        Debug.Log("click" + _levelUpUI);
        if (_levelUpUI != null) _levelUpUI.HideNow();
    }
}