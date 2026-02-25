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

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
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
        //UpgradeManager.Instance.Apply(upgradeType);
        //LevelUpUI.Hide();
    }
}