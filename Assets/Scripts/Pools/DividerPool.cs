public class DividerPool : BasePool
{
    public static DividerPool Get;
    private new void Awake()
    {
        Get = this;
        base.Awake();
    }
}