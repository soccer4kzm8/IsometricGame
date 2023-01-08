using UniRx;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private HPModel _playerHPModel;
    #endregion SerializeField

    #region private変数
    /// <summary>
    /// ゲームステートを監視
    /// </summary>
    private readonly ReactiveProperty<GameState> _state = new ReactiveProperty<GameState>(GameState.Playing);
    #endregion private変数

    #region public変数
    /// <summary>
    /// ゲームステート
    /// </summary>
    public IReadOnlyReactiveProperty<GameState> State => _state;
    #endregion public変数

    private void Start()
    {
        _state.AddTo(this);

        // プレイヤーのHPが0以下になったら、リザルト画面を表示
        _playerHPModel.HP
            .Skip(1)
            .Where(hp => hp <= 0)
            .Subscribe(_ =>
            {
                _state.Value = GameState.Result;
            })
            .AddTo(this);
    }
}
