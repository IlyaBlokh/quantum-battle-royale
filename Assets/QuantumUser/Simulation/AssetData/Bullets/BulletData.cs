using Photon.Deterministic;

namespace Quantum
{
    public unsafe class BulletData : AssetObject
    {
        public FP Duration;
        public EntityPrototype Bullet;
        public FP Damage;
        public FP Speed;

        public virtual void CreateBullet(Frame f, WeaponBase weaponData, EntityRef owner)
        {
            EntityRef bulletEntity = f.Create(Bullet);
            
            Transform2D* bullerTransform = f.Unsafe.GetPointer<Transform2D>(bulletEntity);
            Transform2D ownerTransform = f.Get<Transform2D>(owner);
            bullerTransform->Position = ownerTransform.Position + FPVector2.Rotate(weaponData.Offset.XZ, ownerTransform.Rotation);
            bullerTransform->Rotation = ownerTransform.Rotation;
            
            Bullet* bullet = f.Unsafe.GetPointer<Bullet>(bulletEntity);
            bullet->Speed = Speed;
            bullet->Damage = Damage;
            bullet->HeightOffset = weaponData.Offset.Y;
            bullet->Owner = owner;
            bullet->Time = Duration;
            bullet->Direction = ownerTransform.Up;
        }
    }
}
