namespace ShootEmUp
{
    public interface IEnemyUpdateInstaller
    {
        public void RegisterEnemyTicker(EnemyComponentProvider enemyComponentProvider);
        public void DeleteEnemyTicker(EnemyComponentProvider enemyComponentProvider);
    }
}
