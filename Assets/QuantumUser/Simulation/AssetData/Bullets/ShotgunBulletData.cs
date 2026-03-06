using Photon.Deterministic;

namespace Quantum
{
  public unsafe class ShotgunBulletData : BulletData
  {
    public int NumBullets;
    public FP SpreadAngle;

    public override void CreateBullet(Frame f, WeaponBase weaponData, EntityRef owner)
    {
      var ownerTransform = f.Get<Transform2D>(owner);
      var spreadAngleRad = FP.Deg2Rad * SpreadAngle;
      for (int i = 0; i < NumBullets; i++)
      {
        EntityRef bulletEntity = f.Create(Bullet);
        Bullet* bullet = f.Unsafe.GetPointer<Bullet>(bulletEntity);
        Transform2D* bullerTransform = f.Unsafe.GetPointer<Transform2D>(bulletEntity);
        
        bullerTransform->Position = ownerTransform.Position + FPVector2.Rotate(weaponData.Offset.XZ, ownerTransform.Rotation);
        bullerTransform->Rotation = ownerTransform.Rotation + FPMath.Lerp(-spreadAngleRad, spreadAngleRad, (FP)i / (NumBullets - 1));
        
        bullet->Speed = Speed;
        bullet->Damage = Damage;
        bullet->HeightOffset = weaponData.Offset.Y;
        bullet->Owner = owner;
        bullet->Time = Duration;
        bullet->Direction = bullerTransform->Up;
      }
    }
  }
}