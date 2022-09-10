using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _moveSpeed = 3f;
    private NavMeshAgent _navMeshAgent = null; 

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;
    }

    void Update()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }
    }
}
