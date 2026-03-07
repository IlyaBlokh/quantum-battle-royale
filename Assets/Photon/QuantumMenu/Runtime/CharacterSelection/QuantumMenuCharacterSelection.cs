using UnityEngine;

namespace Quantum.Menu
{
  public class QuantumMenuCharacterSelection : QuantumMenuUIScreen
  {
    [SerializeField] private CharacterModel[] _characterModels;
    public override void Awake()
    {
    }
    
    public virtual void OnBackButtonPressed() {
      Controller.Show<QuantumMenuUIMain>();
    }
  }
}