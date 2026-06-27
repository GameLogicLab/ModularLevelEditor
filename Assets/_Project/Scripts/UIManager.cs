using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public InputHandler inputHandler;

    [Header("Asset Buttons")]
    public Button[] assetButtons;
    public GameObject[] assetPrefabs;

    void Start()
    {
        for (int i = 0; i < assetButtons.Length; i++)
        {
            int index = i;
            assetButtons[index].onClick.AddListener(() => SelectAsset(index));
        }
    }

    void SelectAsset(int index)
    {
        if (index < assetPrefabs.Length)
        {
            inputHandler.objectToPlace = assetPrefabs[index];
            Debug.Log("Selected: " + assetPrefabs[index].name);
        }
    }
}