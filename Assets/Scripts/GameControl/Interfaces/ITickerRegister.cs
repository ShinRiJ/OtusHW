using System.Collections.Generic;

namespace ShootEmUp
{
    public interface ITickerRegister
    {
        public void RegisterTickers(IEnumerable<ITickable> bufferTickers);
        public void RegisterTicker(ITickable ticker);
        public void DeleteTickers(IEnumerable<ITickable> bufferTickers);
        public void DeleteTicker(ITickable ticker);
    }
}
