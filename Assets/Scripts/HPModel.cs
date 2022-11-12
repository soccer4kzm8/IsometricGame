using UniRx;

public class HPModel
{
    /// <summary>
    /// ç≈ëÂHP
    /// </summary>
    public readonly int hpMax;

    public IntReactiveProperty hpRP = new IntReactiveProperty();

    public int HP
    {
        get { return hpRP.Value; }
        set { hpRP.Value = value; }
    }

    public HPModel(int hpMax)
    {
        this.hpMax = hpMax;
    }
}
