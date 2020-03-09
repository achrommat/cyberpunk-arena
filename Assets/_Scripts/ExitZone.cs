using UnityEngine;
using UnityEngine.Events;

public class ExitZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().RestartLevel();
        }
    }
}
