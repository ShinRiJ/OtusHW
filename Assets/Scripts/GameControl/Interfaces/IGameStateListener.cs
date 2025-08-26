namespace ShootEmUp
{
    public interface IGameStateListener { }
    public interface IStartGameListener : IGameStateListener
    {
        public void StartGame();
    }
    public interface IFinishGameListener : IGameStateListener
    {
        public void FinishGame();
    }
    public interface IPauseGameListener : IGameStateListener
    {
        public void PauseGame();
    }
    public interface IResumeGameListener : IGameStateListener
    {
        public void ResumeGame();
    }


}