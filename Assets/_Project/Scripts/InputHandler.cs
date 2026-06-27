using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;
    public Transform previewObject;
    public CommandInvoker commandInvoker;

    [Header("Placement")]
    public GameObject objectToPlace;

    [Header("Transform Panel")]
    public TransformPanel transformPanel;

    [Header("Highlight")]
    public ObjectHighlighter objectHighlighter;

    private Camera mainCamera;
    private int placementLayerMask;
    private GameObject selectedObject;

    void Start()
    {
        mainCamera = Camera.main;
        placementLayerMask = ~LayerMask.GetMask("Preview");
    }

    void Update()
    {
        MovePreviewToMouse();

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
            PlaceObject();

        if (Input.GetMouseButtonDown(1))
            SelectOrDelete();

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
            commandInvoker.Undo();

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y))
            commandInvoker.Redo();

        if (Input.GetKeyDown(KeyCode.F) && selectedObject != null)
            FocusOnObject();
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

    void SelectOrDelete()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayerMask))
        {
            if (hit.collider.gameObject.CompareTag("Placeable"))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    objectHighlighter.DeselectAll();
                    transformPanel.SetSelectedObject(null);
                    selectedObject = null;
                    DeleteCommand command = new DeleteCommand(hit.collider.gameObject);
                    commandInvoker.ExecuteCommand(command);
                }
                else
                {
                    selectedObject = hit.collider.gameObject;
                    objectHighlighter.SelectObject(selectedObject);
                    transformPanel.SetSelectedObject(selectedObject);
                }
            }
            else
            {
                selectedObject = null;
                objectHighlighter.DeselectAll();
                transformPanel.SetSelectedObject(null);
            }
        }
    }

    void FocusOnObject()
    {
        Vector3 targetPos = selectedObject.transform.position;
        mainCamera.transform.position = targetPos + new Vector3(0, 5, -8);
        mainCamera.transform.LookAt(targetPos);
    }
}