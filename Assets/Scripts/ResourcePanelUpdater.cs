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
    }

    private void Update()
    {
        UpdateResourceTexts();
    }

    private void UpdateResourceTexts()
    {
        goldText.text = playerData.gold.ToString();
        gemsText.text = playerData.gems.ToString();
    }

    /* Call this method whenever the player's resources change
    public void OnResourceChange()
    {
        UpdateResourceTexts();
    }*/
}
