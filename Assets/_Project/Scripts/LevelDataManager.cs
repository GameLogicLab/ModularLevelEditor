using System.IO;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    public string levelName = "Level_01";

    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath;
    }

    public void SaveLevel()
    {
        string path = Path.Combine(savePath, levelName + ".json");

        LevelData levelData = new LevelData();
        levelData.levelName = levelName;

        GameObject[] placeables = GameObject.FindGameObjectsWithTag("Placeable");

        foreach (GameObject obj in placeables)
        {
            if (!obj.activeInHierarchy) continue;

            ObjectData data = new ObjectData();
            data.prefabID = obj.name.Replace("(Clone)", "").Trim();
            data.posX = obj.transform.position.x;
            data.posY = obj.transform.position.y;
            data.posZ = obj.transform.position.z;
            data.rotY = obj.transform.eulerAngles.y;

            levelData.objects.Add(data);
        }

        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(path, json);

        Debug.Log("Level saved: " + path);
    }

    public void LoadLevel()
    {
        string path = Path.Combine(savePath, levelName + ".json");

        if (!File.Exists(path))
        {
            Debug.Log("No save file found at: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        GameObject[] existing = GameObject.FindGameObjectsWithTag("Placeable");
        foreach (GameObject obj in existing)
        {
            Destroy(obj);
        }

        foreach (ObjectData data in levelData.objects)
        {
            Vector3 position = new Vector3(data.posX, data.posY, data.posZ);
            Quaternion rotation = Quaternion.Euler(0, data.rotY, 0);

            GameObject prefab = Resources.Load<GameObject>(data.prefabID);
            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, position, rotation);
                obj.tag = "Placeable";
            }
        }

        Debug.Log("Level loaded: " + levelData.levelName);
    }

    public void SetLevelName(string name)
    {
        levelName = name;
    }
}