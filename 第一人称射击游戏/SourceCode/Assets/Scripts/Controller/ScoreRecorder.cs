using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder
{
    public int score;
    
    public ScoreRecorder()
    {
        score = 0;
    }
    public void RecordScore(GameObject o)
    {
        if(o.TryGetComponent<PreyData>(out PreyData preyData))
        {
            score += preyData.score;
        }
    }
}
