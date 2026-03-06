using System.Collections.Generic;
using System.Linq;

namespace Quantum
{
    public partial class SimulationConfig : AssetObject
    {
        public AssetRef<EntityPrototype> HealthPickupItem;
        public List<WeaponTypeToEntityPrototype> WeaponTypeToEntityPrototypes;
        
        private Dictionary<WeaponType, EntityPrototype> _weaponPrototypes;
        
        public EntityPrototype GetWeaponPrototype(WeaponType weaponType)
        {
            _weaponPrototypes ??= WeaponTypeToEntityPrototypes.ToDictionary(x => x.WeaponType, x => x.EntityPrototype);
            return _weaponPrototypes[weaponType];
        }
        
    }
}