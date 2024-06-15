public class MultiplyPool : BasePool
{
    public static MultiplyPool Get;
    private new void Awake()
    {
        Get = this;
        base.Awake();
    }
}