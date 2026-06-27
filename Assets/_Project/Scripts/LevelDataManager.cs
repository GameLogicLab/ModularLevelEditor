using System.IO;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    public string levelName = "Level_01";

    private string SavePath => Application.persistentDataPath + "/" + levelName + ".json";

    public void SaveLevel()
    {
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
        File.WriteAllText(SavePath, json);

        Debug.Log("Level saved to: " + SavePath);
    }

    public void LoadLevel()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found!");
            return;
        }

        string json = File.ReadAllText(SavePath);
        LevelData levelData = JsonUtility.FromJson<LevelData>(json);

        // Pehle sab active placeables delete karo
        GameObject[] existing = GameObject.FindGameObjectsWithTag("Placeable");
        foreach (GameObject obj in existing)
        {
            Object.Destroy(obj);
        }

        // Ab JSON se objects spawn karo
        foreach (ObjectData data in levelData.objects)
        {
            Vector3 position = new Vector3(data.posX, data.posY, data.posZ);
            Quaternion rotation = Quaternion.Euler(0, data.rotY, 0);

            GameObject prefab = Resources.Load<GameObject>(data.prefabID);
            if (prefab != null)
            {
                GameObject obj = Object.Instantiate(prefab, position, rotation);
                obj.tag = "Placeable";
            }
            else
            {
                Debug.Log("Prefab not found: " + data.prefabID);
            }
        }

        Debug.Log("Level loaded: " + levelData.levelName);
    }
}