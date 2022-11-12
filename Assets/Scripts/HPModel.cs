using UniRx;

public class HPModel
{
    /// <summary>
    /// ç≈ëÂHP
    /// </summary>
    public readonly int hpMax = 100;

    public IntReactiveProperty hpRP = new IntReactiveProperty();

    public int HP
    {
        get { return hpRP.Value; }
        set { hpRP.Value = value; }
    }
}
