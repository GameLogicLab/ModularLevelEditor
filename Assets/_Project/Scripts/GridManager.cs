using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float cellSize = 2f;

    public Vector3 GetSnappedPosition(Vector3 worldPosition)
    {
        float x = Mathf.Round(worldPosition.x / cellSize) * cellSize;
        float y = 0f;
        float z = Mathf.Round(worldPosition.z / cellSize) * cellSize;

        return new Vector3(x, y, z);
    }
}