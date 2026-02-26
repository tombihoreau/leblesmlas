using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroup ameliorationPanel;
    [SerializeField] private MonoBehaviour cameraController;
    private float _previousTimeScale;

    private void OnEnable()
    {
        PlayerXP.OnLevelUp += Show;
    }

    private void OnDisable()
    {
        PlayerXP.OnLevelUp -= Show;
    }

    private void Start()
    {
        HideInstant();
    }

    private void Show(int newLevel)
    {
        // Affiche UI
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Pause + curseur
        PauseGame();

        // Afficher 3 améliorations (ta méthode)
        ShowAmeliorationsRandom();
    }

    public void HideNow()
    {
        // Cache UI
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        ResumeGame();
    }

    private void PauseGame()
    {
        _previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (cameraController != null)
            cameraController.enabled = false;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (cameraController != null)
            cameraController.enabled = true;
    }

    private void HideInstant()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ShowAmeliorationsRandom()
    {
        int childCount = ameliorationPanel.transform.childCount;
        if (childCount == 0) return;

        // Tout cacher
        for (int i = 0; i < childCount; i++)
            ameliorationPanel.transform.GetChild(i).gameObject.SetActive(false);

        // Indices
        var indexes = new System.Collections.Generic.List<int>(childCount);
        for (int i = 0; i < childCount; i++) indexes.Add(i);

        // Shuffle
        for (int i = 0; i < indexes.Count; i++)
        {
            int r = Random.Range(i, indexes.Count);
            (indexes[i], indexes[r]) = (indexes[r], indexes[i]);
        }

        // Afficher 3
        int n = Mathf.Min(3, childCount);
        for (int i = 0; i < n; i++)
            ameliorationPanel.transform.GetChild(indexes[i]).gameObject.SetActive(true);
    }
}