using UnityEngine;

namespace Quantum.Menu
{
  [CreateAssetMenu(menuName = "Configs/CharacterModel", fileName = "Character", order = 0)]
  public class CharacterModel : ScriptableObject
  {
    [field:SerializeField] public Sprite CharacterImage { get; private set; }
    [field:SerializeField] public string CharacterName { get; private set; }
    [field:SerializeField] public EntityPrototype EntityPrototype { get; private set; }
    [field:SerializeField] public CharacterConfig CharacterConfig { get; private set; }
  }
}