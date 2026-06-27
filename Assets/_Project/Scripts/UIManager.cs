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

    [Header("Level List")]
    public GameObject levelListPanel;
    public Transform levelListContent;
    public Button levelListButtonPrefab;
    public Button showLevelsButton;

    void Start()
    {
        for (int i = 0; i < assetButtons.Length; i++)
        {
            int index = i;
            assetButtons[index].onClick.AddListener(() => SelectAsset(index));
        }

        levelListPanel.SetActive(false);
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

    public void ToggleLevelList()
    {
        Debug.Log("Toggle called! Panel active: " + levelListPanel.activeSelf);
        bool isActive = levelListPanel.activeSelf;
        levelListPanel.SetActive(!isActive);
        Debug.Log("Panel now: " + levelListPanel.activeSelf);

        if (!isActive)
            PopulateLevelList();
    }

    void PopulateLevelList()
    {
        foreach (Transform child in levelListContent)
            Destroy(child.gameObject);

        string[] levels = levelDataManager.GetSavedLevels();

        Debug.Log("Save path: " + levelDataManager.savePath);
        Debug.Log("Levels found: " + levels.Length);

        if (levels.Length == 0)
        {
            Debug.Log("No saved levels found!");
            return;
        }

        foreach (string levelName in levels)
        {
            Button btn = Instantiate(levelListButtonPrefab, levelListContent);
            btn.GetComponentInChildren<TMP_Text>().text = levelName;

            string name = levelName;
            btn.onClick.AddListener(() =>
            {
                levelNameInput.text = name;
                levelDataManager.SetLevelName(name);
                levelDataManager.LoadLevel();
                levelListPanel.SetActive(false);
            });
        }
    }
}