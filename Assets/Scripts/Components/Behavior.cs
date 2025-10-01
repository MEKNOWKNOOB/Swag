using System.Collections.Generic;
using UnityEngine;

public class Behavior : NetworkComponent
{
    void Awake()
    {
        RefName = "Movement";
    }

    public Vector2 PathFindToPos(Vector2 pos, Dictionary<int, int> heuristics)
    {


        return Vector2.zero;
    }
}
