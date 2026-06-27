using UnityEngine;

public class DeleteCommand : ICommand
{
    private GameObject deletedObject;
    private Vector3 position;
    private Quaternion rotation;

    public DeleteCommand(GameObject obj)
    {
        deletedObject = obj;
        position = obj.transform.position;
        rotation = obj.transform.rotation;
    }

    public void Execute()
    {
        deletedObject.SetActive(false);
    }

    public void Undo()
    {
        deletedObject.SetActive(true);
    }
}