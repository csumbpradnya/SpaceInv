using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int points;
    public TMP_Text score;
    public void scoreIncEnemy1()
    {
        points = points + 100;
        
    }
    
    public void scoreIncEnemy2()
    {
        points = points + 150;
        
    }
    
    public void scoreIncEnemy3()
    {
        points = points + 200;
        
    }
    

    public void setScore()
    {
        if (points < 1000)
        {
            score.SetText("Score: " + "0" + points + "  HighScore: ");
        }
        else if (points > 1000)
        {
            score.SetText("Score: " + points + "  HighScore: ");
        }
    }
}
