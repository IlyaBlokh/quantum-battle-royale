using Photon.Deterministic;
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
        }

        public override void Update(Frame frame, ref Filter filter)
        {
        }

        public void CreateBullet(Frame f, EntityRef owner, WeaponData weaponData)
        {
            BulletData bulletData = weaponData.Bullet;
            EntityRef bulletEntity = f.Create(bulletData.Bullet);
            
            Transform2D* bullerTransform = f.Unsafe.GetPointer<Transform2D>(bulletEntity);
            Transform2D ownerTransform = f.Get<Transform2D>(owner);
            bullerTransform->Position = ownerTransform.Position + FPVector2.Rotate(weaponData.Offset.XZ, ownerTransform.Rotation);
            bullerTransform->Rotation = ownerTransform.Rotation;
            
            Bullet* bullet = f.Unsafe.GetPointer<Bullet>(bulletEntity);
            bullet->Speed = bulletData.Speed;
            bullet->Damage = bulletData.Damage;
            bullet->Owner = owner;
            bullet->Time = bulletData.Duration;
            bullet->Direction = ownerTransform.Up;
        }
    }
}
