namespace Analytics
{
    public interface IAnalytics
    {
        void SendGoal(string goalName, string goalValue);
        void SendGoal(string goalName, int goalValue);
    }
}