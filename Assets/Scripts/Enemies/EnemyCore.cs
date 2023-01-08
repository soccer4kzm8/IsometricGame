using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyCore : MonoBehaviour
{
    void Start()
    {
        var enemyAnimation = this.GetComponent<EnemyAnimation>();
        this.UpdateAsObservable()
            .Where(_ => enemyAnimation.AnimationDie.Value == true)
            .Take(1)
            .Subscribe(_ => 
            {
                Debug.LogError("死亡アニメーション終了");
                Destroy(this.gameObject); 
            });
    }
}
