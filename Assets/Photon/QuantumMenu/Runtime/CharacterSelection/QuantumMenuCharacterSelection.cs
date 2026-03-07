using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quantum.Menu
{
  public class QuantumMenuCharacterSelection : QuantumMenuUIScreen
  {
    [SerializeField] private CharacterModel[] characterModels;
    [SerializeField] private SelectableCharacter selectableCharacter;
    [SerializeField] private Transform selectionParent;
    [SerializeField] private QuantumMenuUIController _quantumMenuUIController;
    
    private readonly Dictionary<CharacterModel, SelectableCharacter> _selectableCharacters = new();
    private CharacterModel _selectedCharacterModel;
    
    public override void Awake()
    {
      SelectableCharacter.OnCharacterSelected += OnCharacterSelected;
      InitializeCharacterSelection();
    }

    private void InitializeCharacterSelection()
    {
      foreach (CharacterModel model in characterModels)
      {
        SelectableCharacter selectable = Instantiate(selectableCharacter, selectionParent);
        selectable.Initialize(model);
        _selectableCharacters.Add(model, selectable);
      }
      
      OnCharacterSelected(characterModels[0]);
    }

    private void OnCharacterSelected(CharacterModel characterModel)
    {
      if (_selectedCharacterModel == characterModel)
        return;
      
      if (_selectedCharacterModel != null)
        _selectableCharacters[_selectedCharacterModel].SetSelected(false);
      
      _selectedCharacterModel = characterModel;
      _selectableCharacters[_selectedCharacterModel].SetSelected(true);

      _quantumMenuUIController.ConnectArgs.RuntimePlayers[0].PlayerAvatar = _selectedCharacterModel.EntityPrototype;
    }

    public virtual void OnBackButtonPressed() {
      Controller.Show<QuantumMenuUIMain>();
    }

    private void OnDestroy()
    {
      SelectableCharacter.OnCharacterSelected -= OnCharacterSelected;
    }
  }
}