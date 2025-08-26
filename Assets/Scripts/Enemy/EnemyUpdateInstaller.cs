using TNRD;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyUpdateInstaller : MonoBehaviour, IEnemyUpdateInstaller
    {
        [SerializeField] SerializableInterface<ITickerRegister> _ticketRegister;

        public void RegisterEnemyTicker(EnemyComponentProvider enemyComponentProvider)
        {
            _ticketRegister.Value.RegisterTickers(enemyComponentProvider.GetTickableComponets());
        }

        public void DeleteEnemyTicker(EnemyComponentProvider enemyComponentProvider)
        {
            _ticketRegister.Value.DeleteTickers(enemyComponentProvider.GetTickableComponets());
        }
    }
}
