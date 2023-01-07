using UnityEngine;
using UnityEngine.UI;

public class HPView : MonoBehaviour
{
    /// <summary>
    /// HPゲージ
    /// </summary>
    private Slider _hpGuage;

    private void Start()
    {
        _hpGuage = this.GetComponent<Slider>();
    }

    /// <summary>
    /// HPゲージの設定
    /// </summary>
    /// <param name="maxHP">最大HP</param>
    /// <param name="hp">残っているHP</param>
    public void SetGuage(int maxHP, float hp)
    {
        _hpGuage.value = hp / maxHP;
    }
}
