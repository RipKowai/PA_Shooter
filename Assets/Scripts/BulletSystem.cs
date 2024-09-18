using System.Collections;
using Unity.Transforms;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;

public partial struct BulletSystem : ISystem
{
    private void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = state.EntityManager;
        NativeArray<Entity> allEntities = entityManager.GetAllEntities();

        foreach (Entity entity in allEntities)
        {
            if (entityManager.HasComponent<BulletComponent>(entity)) 
            {
                LocalTransform bulletTransform = entityManager.GetComponentData<LocalTransform>(entity);
                BulletComponent bulletComponent = entityManager.GetComponentData<BulletComponent>(entity);
                bulletTransform.Position += bulletComponent.Speed * SystemAPI.Time.DeltaTime * bulletTransform.Up();
                entityManager.SetComponentData(entity, bulletTransform);
            }
        }
    }
}
