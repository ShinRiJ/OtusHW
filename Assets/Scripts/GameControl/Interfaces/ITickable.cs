namespace ShootEmUp
{
    public interface ITickable { }
    public interface ICommonTickable : ITickable
    {
        public void Tick();
    }

    public interface IFixedTickable : ITickable
    {
        public void FixedTick();
    }
}