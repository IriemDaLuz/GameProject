using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ElectricLever : MonoBehaviour
{
    public Transform leverHandle;
    public Vector3 targetLocalPosition;
    public float speed = 1.5f;

    public Light[] lucesActivar;
    public UnityEvent onLeverActivated;

    private bool isActivated = false;

    private void OnMouseDown()
    {
        if (!isActivated)
        {
            StartCoroutine(ActivateLever());
        }
    }

    private IEnumerator ActivateLever()
    {
        isActivated = true;

        Vector3 start = leverHandle.localPosition;
        Vector3 end = targetLocalPosition;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            leverHandle.localPosition = Vector3.Lerp(start, end, t);
            yield return null;
        }

        // Activar luces
        foreach (var luz in lucesActivar)
        {
            if (luz != null)
                luz.enabled = true;
        }

        // Completar misión
        MissionManager.Instance.CompletarMisionElectricidad();

        onLeverActivated?.Invoke();
    }
}
