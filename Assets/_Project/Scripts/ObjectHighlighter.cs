using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    private GameObject selectedObject;
    private Material originalMaterial;
    public Material highlightMaterial;

    public void SelectObject(GameObject obj)
    {
        // Pehle purana object deselect karo
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material = originalMaterial;
        }

        selectedObject = obj;

        if (selectedObject != null)
        {
            Renderer rend = selectedObject.GetComponent<Renderer>();
            originalMaterial = rend.material;
            rend.material = highlightMaterial;
        }
    }

    public void DeselectAll()
    {
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Renderer>().material = originalMaterial;
            selectedObject = null;
        }
    }
}