using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public InteractionDetector detector; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject obj = detector.GetDetectedObject();
            if (obj != null)
            {
                if (detector.GetDetectedTag() == "Interactuable" && obj.GetComponent<DoorInteraction>())
                {
                    obj.GetComponent<DoorInteraction>().ToggleDoor();
                }

               
            }
        }
    }
}
