using TMPro;
using UnityEngine;

public class CollectibleTracker : MonoBehaviour
{
    public TextMeshProUGUI collectibleText;
    private int currentCollectibles;


    public void Update()
    {
        currentCollectibles = FindObjectsOfType<Collections>().Length;
        UpdateUI();
    }

    private void UpdateUI()
    {
        collectibleText.text = "Remaining: " + (currentCollectibles);
    }
}
