using Unity.Entities;
using System.Collections.Generic;
public class EnemyDataContainer : IComponentData
{
    public List<EnemyData> enemies;
}

public struct EnemyData
{
    public int level;
    public Entity prefab;
    public float health;
    public float damage;
    public float moveSpeed;
}
