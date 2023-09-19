using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card/New", order = 1)]
public class CardData : ScriptableObject
{
    [SerializeField]
    private int id;

    [SerializeField] 
    private string cardName;

    [SerializeField] 
    private AssetReferenceT<Sprite> imageSpriteSoftReference;
}
