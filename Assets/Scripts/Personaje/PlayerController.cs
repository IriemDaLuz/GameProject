using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float velocidadCorrer = 8f;
    [SerializeField] private float velocidadAgacharse = 2f;
    [SerializeField] private float velocidadRotacion = 2f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform transformPersonaje;
    [SerializeField] private Camera camaraPersonaje;

    [SerializeField] private float alturaNormal = 2f;
    [SerializeField] private float alturaAgachado = 1f;

    private Vector3 movimiento;
    private float rotacionX;
    private bool estaAgachado = false;

    private void Start()
    {
        ManejarCursor();
    }

    private void Update()
    {
        MovimientoDelPersonaje();
        MovimientoDeCamara();
    }

    private void ManejarCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void MovimientoDelPersonaje()
    {
        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");

        float velocidadActual = velocidadMovimiento;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidadActual = velocidadCorrer;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            velocidadActual = velocidadAgacharse;
            if (!estaAgachado)
            {
                Agacharse(true);
            }
        }
        else
        {
            if (estaAgachado)
            {
                Agacharse(false);
            }
        }

        movimiento = transform.right * movX + transform.forward * movZ;
        characterController.SimpleMove(movimiento * velocidadActual);
    }

    void MovimientoDeCamara()
    {
        float ratonX = Input.GetAxis("Mouse X") * velocidadRotacion;
        float ratonY = Input.GetAxis("Mouse Y") * velocidadRotacion;

        rotacionX -= ratonY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        camaraPersonaje.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0);
        transformPersonaje.Rotate(Vector3.up * ratonX);
    }

    void Agacharse(bool agachar)
    {
        estaAgachado = agachar;
        characterController.height = agachar ? alturaAgachado : alturaNormal;

        Vector3 camPos = camaraPersonaje.transform.localPosition;
        camPos.y = agachar ? alturaAgachado / 2f : alturaNormal / 2f;
        camaraPersonaje.transform.localPosition = camPos;
    }
}
