using UnityEngine;
using UniRx;

public class EnemyHPPresenter : MonoBehaviour
{
    private IHPModel _hPModel;
    private IGetHitEventProvider _getHitEventProvider;

    private void Start()
    {
        _getHitEventProvider = this.GetComponent<IGetHitEventProvider>();
        _hPModel = this.GetComponent<IHPModel>();

        _getHitEventProvider.GetHit
            .Where(getHit => getHit == true)
            .Subscribe(_ =>
            {
                _hPModel.GetDamage(10);
            });
        
    }
}
