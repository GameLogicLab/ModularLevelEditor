using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;
    public Transform previewObject;
    public CommandInvoker commandInvoker;

    [Header("Placement")]
    public GameObject objectToPlace;

    private Camera mainCamera;
    private int placementLayerMask;

    void Start()
    {
        mainCamera = Camera.main;
        placementLayerMask = ~LayerMask.GetMask("Preview");
    }

    void Update()
    {
        MovePreviewToMouse();

        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }

        if (commandInvoker == null) return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            commandInvoker.Undo();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            commandInvoker.Redo();
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeleteObject();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            FindAnyObjectByType<LevelDataManager>().SaveLevel();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            FindAnyObjectByType<LevelDataManager>().LoadLevel();
        }
    }

    void MovePreviewToMouse()
    {
        if (mainCamera == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
        {
            Vector3 snappedPos = gridManager.GetSnappedPosition(hit.point);

            if (previewObject != null)
                previewObject.position = snappedPos;
        }
    }

    void PlaceObject()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        if (commandInvoker == null) return;

        if (commandInvoker == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
        {
            Vector3 snappedPos = gridManager.GetSnappedPosition(hit.point);
            PlaceCommand command = new PlaceCommand(objectToPlace, snappedPos);
            commandInvoker.ExecuteCommand(command);
        }
    }

    void DeleteObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
        {
            if (hit.collider.gameObject.CompareTag("Placeable"))
            {
                DeleteCommand command = new DeleteCommand(hit.collider.gameObject);
                commandInvoker.ExecuteCommand(command);
            }
        }
    }
}