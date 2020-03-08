using UnityEngine;
using UnityEngine.Events;

public class PlayerEnterChecker : MonoBehaviour
{
    public Transform SpawnPoints;

    [SerializeField] private UnityEvent _onPlayerEntered;
    private bool _playerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_playerEntered)
        {
            _playerEntered = true;
            EnemyManager.SpawnPoints = this.SpawnPoints;
            _onPlayerEntered.Invoke();
        }
    }

    public void Reset()
    {
        _playerEntered = false;
    }
}
