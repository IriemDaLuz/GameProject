using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesplazamientoCreditos : MonoBehaviour
{
public float velocidad = 30f;
    public float tiempoExtra = 1f; 
    private RectTransform rectTransform;
    private float alturaViewport;
    private bool escenaCargada = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        alturaViewport = transform.parent.GetComponent<RectTransform>().rect.height;
    }

    void Update()
    {
        if (escenaCargada) return;

        transform.Translate(Vector3.up * velocidad * Time.deltaTime);

        if (rectTransform.anchoredPosition.y >= rectTransform.rect.height - alturaViewport)
        {
            escenaCargada = true;
            Invoke("IrAlMenu", tiempoExtra);
        }
    }

    void IrAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
} 