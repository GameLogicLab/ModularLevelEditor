using System.Collections.Generic;

[System.Serializable]
public class ObjectData
{
    public string prefabID;
    public float posX, posY, posZ;
    public float rotY;
}

[System.Serializable]
public class LevelData
{
    public string levelName;
    public List<ObjectData> objects = new List<ObjectData>();
}