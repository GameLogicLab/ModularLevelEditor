using UnityEngine;
using TMPro;

public class TransformPanel : MonoBehaviour
{
    [Header("Selected Object")]
    private GameObject selectedObject;
    private bool isUpdating = false;

    [Header("Position Fields")]
    public TMP_InputField posX, posY, posZ;

    [Header("Rotation Fields")]
    public TMP_InputField rotX, rotY, rotZ;

    [Header("Scale Fields")]
    public TMP_InputField scaleX, scaleY, scaleZ;

    void Start()
    {
        posX.onValueChanged.AddListener(_ => ApplyPosition());
        posY.onValueChanged.AddListener(_ => ApplyPosition());
        posZ.onValueChanged.AddListener(_ => ApplyPosition());

        rotX.onValueChanged.AddListener(_ => ApplyRotation());
        rotY.onValueChanged.AddListener(_ => ApplyRotation());
        rotZ.onValueChanged.AddListener(_ => ApplyRotation());

        scaleX.onValueChanged.AddListener(_ => ApplyScale());
        scaleY.onValueChanged.AddListener(_ => ApplyScale());
        scaleZ.onValueChanged.AddListener(_ => ApplyScale());

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (selectedObject != null && !isUpdating)
            UpdateFields();
    }

    public void SetSelectedObject(GameObject obj)
    {
        selectedObject = obj;
        gameObject.SetActive(obj != null);
    }

    void UpdateFields()
    {
        isUpdating = true;

        Vector3 pos = selectedObject.transform.position;
        posX.text = pos.x.ToString("F2");
        posY.text = pos.y.ToString("F2");
        posZ.text = pos.z.ToString("F2");

        Vector3 rot = selectedObject.transform.eulerAngles;
        rotX.text = rot.x.ToString("F2");
        rotY.text = rot.y.ToString("F2");
        rotZ.text = rot.z.ToString("F2");

        Vector3 scale = selectedObject.transform.localScale;
        scaleX.text = scale.x.ToString("F2");
        scaleY.text = scale.y.ToString("F2");
        scaleZ.text = scale.z.ToString("F2");

        isUpdating = false;
    }

    void ApplyPosition()
    {
        if (selectedObject == null || isUpdating) return;
        if (!float.TryParse(posX.text, out float x)) return;
        if (!float.TryParse(posY.text, out float y)) return;
        if (!float.TryParse(posZ.text, out float z)) return;

        selectedObject.transform.position = new Vector3(x, y, z);
    }

    void ApplyRotation()
    {
        if (selectedObject == null || isUpdating) return;
        if (!float.TryParse(rotX.text, out float x)) return;
        if (!float.TryParse(rotY.text, out float y)) return;
        if (!float.TryParse(rotZ.text, out float z)) return;

        selectedObject.transform.eulerAngles = new Vector3(x, y, z);
    }

    void ApplyScale()
    {
        if (selectedObject == null || isUpdating) return;
        if (!float.TryParse(scaleX.text, out float x)) return;
        if (!float.TryParse(scaleY.text, out float y)) return;
        if (!float.TryParse(scaleZ.text, out float z)) return;

        selectedObject.transform.localScale = new Vector3(x, y, z);
    }
}