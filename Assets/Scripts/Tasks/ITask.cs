namespace Tasks
{
    public interface ITask
    {
        int Priority { get; }
        bool CanExecute();
        void Execute();
    }
}
