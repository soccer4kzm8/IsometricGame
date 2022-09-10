using UnityEngine;
using UnityEngine.AI;
using UniRx;
using UniRx.Triggers;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _attackRange;
    [SerializeField] private float _moveSpeed = 3f;
    private NavMeshAgent _navMeshAgent = null; 

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;

        _attackRange.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _player.name)
            .Subscribe(_ => Debug.LogError("îÕàÕÇ…ì¸Ç¡ÇΩ"));
        _attackRange.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.name == _player.name)
            .Subscribe(_ => Debug.LogError("îÕàÕÇ©ÇÁèoÇΩ"));
    }

    void Update()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }
    }
}
