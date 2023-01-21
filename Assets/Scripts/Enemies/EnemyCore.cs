using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyCore : MonoBehaviour, IEnemyCore
{
    public void Initialize()
    {
        var enemyAnimation = this.GetComponent<EnemyAnimation>();
        // 死亡アニメーション終了後、自身を消す
        this.UpdateAsObservable()
            .Where(_ => enemyAnimation.AnimationDie.Value == true)
            .Take(1)
            .Subscribe(_ =>
            {
                Destroy(this.gameObject);
            });
    }
}
