using UnityEngine;
using UniRx;

public class PlayerHPPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private IHPModel _hPModel;
    private HPView _hPView;
    private IGetHitEventProvider _getHitEventProvider;

    void Start()
    {
        _getHitEventProvider = _player.GetComponent<IGetHitEventProvider>();
        _hPModel = this.GetComponent<IHPModel>();
        _hPView = this.GetComponent<HPView>();

        _getHitEventProvider.GetHit
            .Where(getHit => getHit == true)
            .Subscribe(_ => _hPModel.GetDamage(10));
        _hPModel.HP
            .Subscribe(hp => _hPView.SetGuage(_hPModel.MaxHP, hp)).AddTo(this);
    }
}
