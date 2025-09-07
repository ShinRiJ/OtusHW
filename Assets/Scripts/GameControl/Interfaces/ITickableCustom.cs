namespace ShootEmUp
{
    public interface ITickableCustom { }
    public interface ICommonTickableCustom : ITickableCustom
    {
        public void Tick();
    }

    public interface IFixedTickableCustom : ITickableCustom
    {
        public void FixedTick();
    }
}