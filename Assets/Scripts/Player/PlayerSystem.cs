using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;
using System;

public partial struct PlayerSystem : ISystem
{
    private Entity playerEntity;
    private Entity inputEntity;
    private EntityManager entityManager;
    private PlayerComponent playerComponent;
    private InputComponent inputComponent;
    public void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
        inputEntity = SystemAPI.GetSingletonEntity<InputComponent>();

        playerComponent = entityManager.GetComponentData<PlayerComponent>(playerEntity);
        inputComponent = entityManager.GetComponentData<InputComponent>(inputEntity);

        Move(ref state);
        Shoot(ref state);
    }

    private void Move(ref SystemState state)
    {
        LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);
        playerTransform.Position += new float3(inputComponent.movement * playerComponent.MoveSpeed * SystemAPI.Time.DeltaTime, 0);

        Vector2 direction = (Vector2)inputComponent.mousePos - (Vector2)Camera.main.WorldToScreenPoint(playerTransform.Position) ;
        float angle = math.degrees(math.atan2(direction.y, direction.x));
        playerTransform.Rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);

        entityManager.SetComponentData(playerEntity, playerTransform);
    }

    private float TimeTillNextShot;

    private void Shoot(ref SystemState state)
    {
        if (inputComponent.pressingLMB && TimeTillNextShot < SystemAPI.Time.ElapsedTime) 
        {
            EntityCommandBuffer ECB = new EntityCommandBuffer(Allocator.Temp);

            Entity bulletEntity = entityManager.Instantiate(playerComponent.BulletPrefab);

            ECB.AddComponent(bulletEntity, new BulletComponent { Speed = 10 });

            LocalTransform bulletTransform = entityManager.GetComponentData<LocalTransform>(bulletEntity);
            bulletTransform.Rotation = entityManager.GetComponentData<LocalTransform>(playerEntity).Rotation;
            LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);
            bulletTransform.Position = playerTransform.Position + playerTransform.Up(); 
            ECB.SetComponent(bulletEntity, bulletTransform);

            ECB.Playback(entityManager);

            TimeTillNextShot = (float)SystemAPI.Time.ElapsedTime + playerComponent.ShootCooldown;
        }
    }
}
