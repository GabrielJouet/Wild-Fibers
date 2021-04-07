using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSquad", menuName = "Towers/Squad")]
public class Squad : ScriptableObject
{
    [SerializeField]
    private List<TowerData> _towers;

    public List<TowerData> Towers { get => _towers; }


    [SerializeField]
    private List<Augmentation> _augmentations;

    public List<Augmentation> Augmentations { get => _augmentations; }


    [SerializeField]
    private Sprite _spriteSquad;
    public Sprite SquadSprite { get => _spriteSquad; }
}