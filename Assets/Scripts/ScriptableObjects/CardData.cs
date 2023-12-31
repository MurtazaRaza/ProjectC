using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card/New", order = 1)]
public class CardData : ScriptableObject
{
    public int id;
    public string cardName;
    public string spriteSheetResourcePath;
}
