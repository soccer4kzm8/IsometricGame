using UniRx;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private HPModel _hPModel;
    /// <summary>
    /// ゲームステートを監視
    /// </summary>
    private readonly ReactiveProperty<GameState> _state = new ReactiveProperty<GameState>(GameState.Playing);
    public IReadOnlyReactiveProperty<GameState> State => _state;

    private void Start()
    {
        _state.AddTo(this);

        _hPModel.HP
            .Skip(1)
            .Where(hp => hp <= 0)
            .Subscribe(_ =>
            {
                _state.Value = GameState.Result;
                Debug.LogError("死亡");
            })
            .AddTo(this);
    }
}
