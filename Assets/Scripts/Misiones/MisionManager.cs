using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    [Header("Mission UI Elements")]
    public TMP_Text missionTextUI;   
    public TMP_Text optionalMissionUI;  

    private string currentMainMission;
    private string currentOptionalMission;
    public bool misionElectricidadCompletada = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetMainMission("Restablece la electricidad.");
        SetOptionalMission("");
    }

    public void SetMainMission(string newMission)
    {
        currentMainMission = newMission;
        UpdateMainMissionUI();
    }

    public void SetOptionalMission(string newOptionalMission)
    {
        currentOptionalMission = newOptionalMission;
        UpdateOptionalMissionUI();
    }

    private void UpdateMainMissionUI()
    {
        if (missionTextUI != null){
            missionTextUI.text = currentMainMission;
        }
    }

    private void UpdateOptionalMissionUI()
    {
        if (optionalMissionUI != null)
        {
            if (string.IsNullOrEmpty(currentOptionalMission)){
                optionalMissionUI.text = ""; 
                }
            else {
                optionalMissionUI.text = "Optional: " + currentOptionalMission;
                }
        }
    }

    public void CompletarMisionElectricidad()
    {
        Debug.Log("Electricidad restaurada.");
        misionElectricidadCompletada = true;
        SetMainMission("Encuentra una salida del pasillo.");
    }

    public string GetCurrentMainMission()
    {
        return currentMainMission;
    }

    public string GetCurrentOptionalMission()
    {
        return currentOptionalMission;
    }
}