using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _body;

    private NavMeshAgent _navMeshAgent = null; 

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _body.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => KnockBack());
        _body.OnTriggerExitAsObservable()
            .Where(x => x.gameObject.name == _sword.name)
            .Subscribe(_ => _navMeshAgent.isStopped = false);
    }

    void FixedUpdate()
    {
        if(_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
		}
    }

    private void KnockBack()
	{
        _navMeshAgent.isStopped = true;
        //いきなり位置移動させるんじゃなくて、Update()内で位置移動するようにする。UniRx使える？
        // ノックバックが終わったら、SetGetHit(true)
    }
}
