using TMPro;
using UnityEngine;


public class UiManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI scoreText;

    [SerializeField] 
    private TextMeshProUGUI comboText;

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateComboText(int combo)
    {
        comboText.text = combo.ToString();
    }

}
