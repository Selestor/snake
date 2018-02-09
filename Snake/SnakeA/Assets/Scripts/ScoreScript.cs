using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple static script holding player score
public static class ScoreScript
{
    private static int points;
    
    public static int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }
}
