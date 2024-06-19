public class BossPool : BasePool
{
    public static BossPool Get;
    private new void Awake()
    {
        Get = this;
        base.Awake();
    }
}