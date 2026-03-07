using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quantum.Menu
{
  public class SelectableCharacter : MonoBehaviour
  {
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterName;
    [SerializeField] private GameObject characterSelected;
    
    private CharacterModel _model;
    
    public static event Action<CharacterModel> OnCharacterSelected;

    public void Initialize(CharacterModel characterModel)
    {
      _model = characterModel;
      characterImage.sprite = _model.CharacterImage;
      characterName.text = _model.CharacterName;
    }

    public void SetSelected(bool isSelected)
    {
      characterSelected.SetActive(isSelected);
    }

    public void CharacterSelected()
    {
      OnCharacterSelected?.Invoke(_model);
    }
  }
}