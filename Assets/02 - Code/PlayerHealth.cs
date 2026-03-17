using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Obligatoire pour TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    [Header("Paramètres de Vie")]
    [SerializeField] private float maxHp = 100f;
    [SerializeField] private Image barreDeVieImage;
    [SerializeField] private TextMeshProUGUI hpTexte;
    [SerializeField] public bool estMort = false;

    [Header("Couleurs du Texte")]
    [SerializeField] private Color couleurNormale = Color.white;
    [SerializeField] private Color couleurDanger = Color.yellow; // Jaune quand la vie est basse
    [SerializeField] private float seuilDanger = 0.3f; // 30% de vie restante
    
    [Header("Game Over")]
    [SerializeField] private GameObject ecranGameOver;

    [Header("Paramètres de Combat")]
    [SerializeField] private float forceRecul = 5f;
    [SerializeField] private Material materialDegats;

    private Rigidbody _rb;
    private SkinnedMeshRenderer _skinnedRenderer;
    private Material _materialOriginal;
    private float _hp;

    private void Awake()
    {
        _hp = maxHp;
        _rb = GetComponent<Rigidbody>();
        _skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (_skinnedRenderer != null)
        {
            _materialOriginal = _skinnedRenderer.material;
        }

        MettreAJourInterface();
        if (ecranGameOver != null) ecranGameOver.SetActive(false);
    }

    public void TakeDamage(float dmg, Vector3 positionAgresseur)
    {
        if (estMort) return;
        
        _hp -= dmg;
        _hp = Mathf.Max(0, _hp);

        MettreAJourInterface();
        
        if (_hp > 0f)
        {
            AppliquerRecul(positionAgresseur);
            StopAllCoroutines();
            StartCoroutine(EffetFlashDegats());
        }
        else
        {
            Mourir();
        }
    }

    private void MettreAJourInterface()
    {
        float ratio = _hp / maxHp;

        // 1. Mise à jour de la barre
        if (barreDeVieImage is not null)
        {
            barreDeVieImage.fillAmount = ratio;
        }

        // 2. Mise à jour du texte et de sa couleur
        if (hpTexte is not null)
        {
            hpTexte.text = $"{Mathf.CeilToInt(_hp)} / {maxHp}";

            // Si on est en dessous du seuil (ex: 30%), on passe en jaune
            if (ratio <= seuilDanger)
            {
                hpTexte.color = couleurDanger;
            }
            else
            {
                hpTexte.color = couleurNormale;
            }
        }
    }

    private void AppliquerRecul(Vector3 source)
    {
        if (_rb != null)
        {
            Vector3 direction = (transform.position - source).normalized;
            direction.y = 0.5f;
            _rb.AddForce(direction * forceRecul, ForceMode.Impulse);
        }
    }

    private IEnumerator EffetFlashDegats()
    {
        if (_skinnedRenderer is null || materialDegats is null) yield break;

        Material[] matsAvecFlash = new Material[2];
        matsAvecFlash[0] = _materialOriginal;
        matsAvecFlash[1] = materialDegats;

        _skinnedRenderer.materials = matsAvecFlash;
        yield return new WaitForSeconds(0.15f);
        _skinnedRenderer.materials = new Material[] { _materialOriginal };
    }
    

    private void Mourir()
    {
        estMort = true;
        
        if (TryGetComponent(out Animator anim)) anim.enabled = false;
        var controller = GetComponent("ThirdPersonController") as MonoBehaviour;
        if (controller != null) controller.enabled = false;
        
        var weapon = GetComponent("SwordWeapon")  as MonoBehaviour;
        if (weapon is not null) weapon.enabled = false;
        
        Time.timeScale = 0.5f;
        
        if (ecranGameOver != null)
        {
            ecranGameOver.SetActive(true);
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
    }
}