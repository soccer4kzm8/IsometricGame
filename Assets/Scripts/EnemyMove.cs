using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _sword;
    [SerializeField] private float _moveSpeed = 3f;
    private NavMeshAgent _navMeshAgent = null; 

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;
        this.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(x => _navMeshAgent.isStopped = true);
        this.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(x => _navMeshAgent.isStopped = false);
    }

    void FixedUpdate()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
		}
    }
}
