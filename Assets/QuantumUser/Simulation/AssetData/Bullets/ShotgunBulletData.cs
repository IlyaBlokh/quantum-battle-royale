using Photon.Deterministic;

namespace Quantum
{
  public class ShotgunBulletData : BulletData
  {
    public int NumBullets;
    public FP SpreadAngle;

    public override void CreateBullet(Frame f, WeaponBase weaponData, EntityRef owner)
    {
      var spreadAngleRad = FP.Deg2Rad * SpreadAngle;
      for (int i = 0; i < NumBullets; i++)
      {
        FP rotationOffset = FPMath.Lerp(-spreadAngleRad, spreadAngleRad, (FP)i / (NumBullets - 1));
        CreateSingleBullet(f, weaponData, owner, rotationOffset);
      }
    }
  }
}