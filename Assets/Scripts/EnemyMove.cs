using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _sword;
    private NavMeshAgent _navMeshAgent = null; 

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        //this.OnTriggerEnterAsObservable()
        //    .Where(x => x.gameObject.name == _sword.name)
        //    .Subscribe(x => _navMeshAgent.isStopped = true);
        //this.OnTriggerExitAsObservable()
        //    .Where(x => x.gameObject.name == _sword.name)
        //    .Subscribe(x => _navMeshAgent.isStopped = false);
    }

    void FixedUpdate()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
		}
    }
}
