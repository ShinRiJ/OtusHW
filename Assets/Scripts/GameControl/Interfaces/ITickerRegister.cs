using System.Collections.Generic;

namespace ShootEmUp
{
    public interface ITickerRegister
    {
        public void RegisterTickers(IEnumerable<ITickableCustom> bufferTickers);
        public void RegisterTicker(ITickableCustom ticker);
        public void DeleteTickers(IEnumerable<ITickableCustom> bufferTickers);
        public void DeleteTicker(ITickableCustom ticker);
    }
}
