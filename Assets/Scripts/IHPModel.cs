using UniRx;

public interface IHPModel
{
    /// <summary>
    /// 残っているHP
    /// </summary>
    IReadOnlyReactiveProperty<int> HP { get; }

    /// <summary>
    /// ダメージ受けた時の処理
    /// </summary>
    /// <param name="attackPoint">相手の攻撃力</param>
    void GetDamage(int attackPoint);
}
