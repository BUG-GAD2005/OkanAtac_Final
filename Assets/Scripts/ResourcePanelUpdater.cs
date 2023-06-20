using UnityEngine;
using UnityEngine.UI;

public class ResourcePanelUpdater : MonoBehaviour
{
    public Text goldText;
    public Text gemsText;

    [SerializeField] PlayerData playerData;

    private void Start()
    {
        UpdateResourceTexts();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        if (playerData != null)
        {
            playerData.OnGoldChange += HandleGoldChange;
            playerData.OnGemsChange += HandleGemsChange;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (playerData != null)
        {
            playerData.OnGoldChange -= HandleGoldChange;
            playerData.OnGemsChange -= HandleGemsChange;
        }
    }

    private void HandleGoldChange(int newGoldAmount)
    {
        goldText.text = newGoldAmount.ToString();
    }

    private void HandleGemsChange(int newGemsAmount)
    {
        gemsText.text = newGemsAmount.ToString();
    }

    private void UpdateResourceTexts()
    {
        if (playerData != null)
        {
            goldText.text = playerData.gold.ToString();
            gemsText.text = playerData.gems.ToString();
        }
    }

}
