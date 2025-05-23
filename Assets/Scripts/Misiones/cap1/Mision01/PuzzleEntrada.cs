using UnityEngine;
using TMPro;

public class PuzzleEntrada : MonoBehaviour
{
    public Camera puzzleCam;
    public TMP_Text interactionText;
    public GameObject player;
    public MonoBehaviour playerMovementScript; 
    public GameObject panelAyuda;

    private bool jugadorDentro = false;
    private bool enPuzzle = false;
    private Camera camaraJugador;

    void Start()
    {
        camaraJugador = Camera.main;
        puzzleCam.gameObject.SetActive(false);
        interactionText.enabled = false;
        if (panelAyuda != null) panelAyuda.SetActive(false);
    }

    void Update()
    {
        if (jugadorDentro && Input.GetKeyDown(KeyCode.E))
        {
            if (!enPuzzle) ActivarPuzzle();
            else SalirDelPuzzle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = true;
            interactionText.text = "Arreglar electricidad [E]";
            interactionText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDentro = false;
            interactionText.enabled = false;
            if (enPuzzle) SalirDelPuzzle();
        }
    }

    void ActivarPuzzle()
    {
        enPuzzle = true;
        camaraJugador.enabled = false;
        puzzleCam.gameObject.SetActive(true);
        interactionText.text = "Salir [E]";
        if (playerMovementScript) playerMovementScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (panelAyuda != null) panelAyuda.SetActive(true);
    }

    void SalirDelPuzzle()
    {
        enPuzzle = false;
        puzzleCam.gameObject.SetActive(false);
        camaraJugador.enabled = true;
        interactionText.text = "Arreglar electricidad [E]";
        if (playerMovementScript) playerMovementScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (panelAyuda != null) panelAyuda.SetActive(false);
    }
}