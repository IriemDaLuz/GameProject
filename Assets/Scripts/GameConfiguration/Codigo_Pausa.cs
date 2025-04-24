using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Codigo_Pausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public bool Pausa=false;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Pausa==false){
                ObjetoMenuPausa.SetActive(true);
                Pausa=true;

                Time.timeScale=0;
                Cursor.lockState=CursorLockMode.None;
                Cursor.visible=true;
            } else if(Pausa==true);{
                Reanudar();
            }
        }
    }
    public void Reanudar(){
        ObjetoMenuPausa.SetActive(false);
        Pausa=false;
        Time.timeScale=1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void IrAlMenu(string nombreMenu){
        SceneManager.LoadScene(nombreMenu);
    }
}
