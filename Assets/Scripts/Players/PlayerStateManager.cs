using UnityEngine;
using UniRx;

public class PlayerStateManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPModel _playerHPModel = null;
    #endregion SerializeField
    #region private変数
    /// <summary>
    /// プレイヤーステートを監視
    /// </summary>
    private readonly ReactiveProperty<PlayerState> _state = new ReactiveProperty<PlayerState>(PlayerState.Alive);
    #endregion private変数

    #region public変数
    /// <summary>
    /// プレイヤーステート
    /// </summary>
    public IReadOnlyReactiveProperty<PlayerState> State => _state;
    #endregion public変数

    void Start()
    {
        _state.AddTo(this);

        // 体力が0以下になったら、プレイヤーステートをDeadに変更
        _playerHPModel.HP
            .Where(hp => hp <= 0)
            .Take(1)
            .Subscribe(_ =>
            {
                _state.Value = PlayerState.Dead;
            })
            .AddTo(this);
    }
}
