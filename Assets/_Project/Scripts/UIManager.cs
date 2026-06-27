using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    public InputHandler inputHandler;
    public LevelDataManager levelDataManager;

    [Header("Asset Buttons")]
    public Button[] assetButtons;
    public GameObject[] assetPrefabs;

    [Header("Save Load")]
    public TMP_InputField levelNameInput;

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

    public void SaveLevel()
    {
        if (levelNameInput.text != "")
            levelDataManager.SetLevelName(levelNameInput.text);

        levelDataManager.SaveLevel();
    }

    public void LoadLevel()
    {
        if (levelNameInput.text != "")
            levelDataManager.SetLevelName(levelNameInput.text);

        levelDataManager.LoadLevel();
    }
}