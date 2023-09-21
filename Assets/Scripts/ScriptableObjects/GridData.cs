using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGridLayoutData", menuName = "Setup/NewGridLayoutData", order = 2)]
public class GridData : ScriptableObject
{
    public int numberOfRows;
    public Vector2 cardSize;
    public Vector2 spacing;
    public int maxCardsThatCanFit;
}
