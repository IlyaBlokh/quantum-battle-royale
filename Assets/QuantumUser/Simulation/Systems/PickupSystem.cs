using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class PickupSystem : SystemMainThreadFilter<PickupSystem.Filter>, ISignalOnTriggerEnter2D, ISignalOnTriggerExit2D, ISignalOnComponentAdded<PickUpItem>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public PickUpItem* PickupItem;
        }

        public void OnAdded(Frame f, EntityRef entity, PickUpItem* component)
        {
            PickupItemBase baseConfig = f.FindAsset(component->PickupItemBase);
            component->PickupTime = baseConfig.PickupTime;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            if (filter.PickupItem->EntityPickingUp == EntityRef.None)
                return;
            
            filter.PickupItem->CurrentPickupTime += f.DeltaTime;
            if (filter.PickupItem->CurrentPickupTime >= filter.PickupItem->PickupTime)
            {
                PickupItemBase baseConfig = f.FindAsset(filter.PickupItem->PickupItemBase);
                baseConfig.Pickup(f, filter.Entity, filter.PickupItem->EntityPickingUp);
            }
        }

        public void OnTriggerEnter2D(Frame f, TriggerInfo2D info)
        {
            if (f.Has<PlayerLink>(info.Entity) &&
                f.Unsafe.TryGetPointer(info.Other, out PickUpItem* pickupItem) &&
                pickupItem->EntityPickingUp == EntityRef.None)
            {
                pickupItem->EntityPickingUp = info.Entity;
            }
        }

        public void OnTriggerExit2D(Frame f, ExitInfo2D info)
        {
            if (f.Has<PlayerLink>(info.Entity) &&
                f.Unsafe.TryGetPointer(info.Other, out PickUpItem* pickupItem) &&
                pickupItem->EntityPickingUp == info.Entity)
            {
                pickupItem->EntityPickingUp = EntityRef.None;
                pickupItem->CurrentPickupTime = 0;
            }
        }
    }
}
