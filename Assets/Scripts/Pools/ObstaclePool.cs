public class ObstaclePool : BasePool
{
    public static ObstaclePool Get;

    private new void Awake()
    {
        Get = this;
        base.Awake();
    }
}