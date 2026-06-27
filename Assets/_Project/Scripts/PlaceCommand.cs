using UnityEngine;

public class PlaceCommand : ICommand
{
    private GameObject prefab;
    private Vector3 position;
    private GameObject placedObject;

    public PlaceCommand(GameObject prefab, Vector3 position)
    {
        this.prefab = prefab;
        this.position = position;
    }

    public void Execute()
    {
        placedObject = Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public void Undo()
    {
        Object.Destroy(placedObject);
    }
}