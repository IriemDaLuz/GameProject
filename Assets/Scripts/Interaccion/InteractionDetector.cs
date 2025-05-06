using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [Header("Información del objeto detectado")]
    public GameObject currentObject;     
    public string currentTag = "";        
    public string currentName = "";     

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactuable"))
        {
            currentObject = other.gameObject;
            currentTag = other.tag;
            currentName = other.name;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentObject)
        {
            currentObject = null;
            currentTag = "";
            currentName = "";
        }
    }

    public GameObject GetDetectedObject()
    {
        return currentObject;
    }

    public string GetDetectedTag()
    {
        return currentTag;
    }

    public string GetDetectedName()
    {
        return currentName;
    }
}
