using UnityEngine;
using UniRx;

public class PlayerHPPresenter : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private GameObject _player;
    #endregion SerializeField

    #region private変数
    private IHPModel _hPModel;
    private HPView _hPView;
    private IGetHitEventProvider _getHitEventProvider;
    #endregion private変数

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
