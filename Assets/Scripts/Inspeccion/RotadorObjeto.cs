using UnityEngine;

public class RotadorDeObjeto : MonoBehaviour
{
    public float velocidad = 100f;

    void Update()
    {
        float rotX = Input.GetAxis("Mouse X") * velocidad * Time.deltaTime;
        float rotY = Input.GetAxis("Mouse Y") * velocidad * Time.deltaTime;

        transform.Rotate(Vector3.up, -rotX, Space.World);
        transform.Rotate(Vector3.right, rotY, Space.World);
    }
}
