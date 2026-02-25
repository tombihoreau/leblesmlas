using UnityEngine;
using System.Collections.Generic;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float showTime = 2f;
    [SerializeField] private CanvasGroup ameliorationPanel;

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
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        CancelInvoke(nameof(Hide));
        Invoke(nameof(Hide), showTime);
        ShowAmeliorations();
    }

    private void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void HideInstant()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ShowAmeliorations()
    {
        int childCount = ameliorationPanel.transform.childCount;

        if (childCount == 0) return;

        List<int> indexes = new List<int>();
        for (int i = 0; i < childCount; i++)
        {
            indexes.Add(i);
        }

        for (int i = 0; i < indexes.Count; i++)
        {
            int randomIndex = Random.Range(i, indexes.Count);
            (indexes[i], indexes[randomIndex]) = (indexes[randomIndex], indexes[i]);
        }

        for (int i = 0; i < 3; i++)
        {
            int index = indexes[i];
            ameliorationPanel.transform.GetChild(index).gameObject.SetActive(true);
        }
    }
}