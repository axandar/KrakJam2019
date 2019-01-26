namespace Code.Enemy{
	public class EnemySpawnInfo{
		public delegate void SpawnEnemyFunction();

		public int IntervalInMillis{ get; set; }
		public SpawnEnemyFunction SpawnEnemy{ get; }

		public EnemySpawnInfo(int intervalInMillis, SpawnEnemyFunction spawnEnemy){
			IntervalInMillis = intervalInMillis;
			SpawnEnemy = spawnEnemy;
		}
	}
}