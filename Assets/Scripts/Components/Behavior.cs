using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class Behavior : NetworkComponent
{
    void Awake()
    {
        RefName = "Behavior";
    }

    public Vector2 NextMove()
    {
        LocalPlayer targetPlayer = null;
        foreach(LocalPlayer player in GameManager.Instance.Players)
        {
            if(targetPlayer == null || (targetPlayer.transform.position-Entity.transform.position).sqrMagnitude > (player.transform.position-Entity.transform.position).sqrMagnitude)
            {
                targetPlayer = player;
            }
        }

        return PathToPos(targetPlayer.transform.position);
    }

    public Vector2 PathToPos(Vector2 pos /*, Dictionary<int, int> heuristics*/)
    {
        return ((Pathing)Entity.NetworkComponents["Pathing"]).PathToTarget(pos, 3, 100);
    }
}
