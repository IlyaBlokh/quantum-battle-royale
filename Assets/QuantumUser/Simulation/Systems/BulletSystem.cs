using Photon.Deterministic;
using Quantum.Physics2D;
using UnityEngine.Scripting;

namespace Quantum.QuantumUser.Simulation.Systems
{
    [Preserve]
    public unsafe class BulletSystem : SystemMainThreadFilter<BulletSystem.Filter>, ISignalCreateBullet
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Bullet* Bullet;
            public Transform2D* Transform;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            FPVector2 nextPosition = filter.Bullet->Direction * filter.Bullet->Speed * f.DeltaTime;
            if (CheckForCollisions(f, filter, nextPosition, out EntityRef entityHit))
            {
                if (f.Unsafe.TryGetPointer(entityHit, out Damageable* damageable))
                    f.Signals.DamageableHit(entityHit, filter.Bullet->Owner, filter.Bullet->Damage, damageable);
                
                f.Destroy(filter.Entity);
                return;
            }

            CheckBulletTimeExpiration(f, filter);
            
            filter.Transform->Position += nextPosition;
        }

        private bool CheckForCollisions(Frame frame, Filter filter, FPVector2 nextPosition, out EntityRef entityHit)
        {
            entityHit = EntityRef.None;
             
            EntityRef owner = filter.Bullet->Owner;
            Transform2D bulletTransform = frame.Get<Transform2D>(filter.Entity);
            HitCollection collisions = frame.Physics2D.LinecastAll(
                bulletTransform.Position,
                bulletTransform.Position + nextPosition,
                int.MaxValue,
                QueryOptions.HitAll & ~QueryOptions.HitTriggers);

            for (int i = 0; i < collisions.Count; i++)
            {
                Hit collision = collisions[i];
                if (collision.Entity == owner || collision.Entity == filter.Entity)
                    continue;
                
                entityHit = collision.Entity;
                return true;
            }
            
            return false;
        }

        private void CheckBulletTimeExpiration(Frame frame, Filter filter)
        {
            filter.Bullet->Time -= frame.DeltaTime;
            if (filter.Bullet->Time <= 0)
                frame.Destroy(filter.Entity);
        }
        
        public void CreateBullet(Frame f, EntityRef owner, FiringWeapon weaponData)
        {
            BulletData bulletData = weaponData.Bullet;
            bulletData.CreateBullet(f, weaponData, owner);
        }
    }
}
