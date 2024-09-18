using Unity.Entities;
using Random = Unity.Mathematics.Random;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class EnemySpawnerSystem : SystemBase
{
    private EnemySpawnerComponent enemySpawnerComponent;
    private EnemyDataContainer enemyDataContainerComponent;
    private Entity enemySpawnerEntity;
    private float nextSpawnTime;

    protected override void OnCreate()
    {
        
    }

    protected override void OnUpdate()
    {
        if(!SystemAPI.TryGetSingletonEntity<EnemySpawnerComponent>(out enemySpawnerEntity))
        {
            return;
        }
    }
}
