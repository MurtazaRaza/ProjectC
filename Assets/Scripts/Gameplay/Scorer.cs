using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.BaseClasses;

public class Scorer : UnitySingleton<Scorer>
{
    [SerializeField] 
    private UiManager uiManager;

    public int Score => _score;
    public int ComboStreak => _comboStreak;
    public int PerSuccessPoint => _perSuccessPoint;
    
    private int _comboStreak = 0;
    private int _perSuccessPoint = 1;
    private bool _hasComboMultiplier;

    private int _score;

    public void SetScoreRules(int perSuccessPoint, bool hasComboMultiplier = true)
    {
        _perSuccessPoint = perSuccessPoint;
    }

    public void SetScoreFromState(int score, int combo)
    {
        _score = score;
        _comboStreak = combo;
    }
    
    public void Scored(bool val)
    {
        if (val)
        {
            _comboStreak += 1;
            _score += _perSuccessPoint * (_hasComboMultiplier ? _comboStreak : 1);
        }
        else
        {
            _comboStreak = 0;
        }

        uiManager.UpdateScore(_score);
        uiManager.UpdateComboText(_comboStreak);
    }

}
