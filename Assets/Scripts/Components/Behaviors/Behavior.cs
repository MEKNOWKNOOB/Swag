using UnityEngine;

public class Behavior : NetworkComponent
{
    public LocalPlayer TargetPlayer = null;
    public float AttackRange = 1.0f;

    void Awake()
    {
        RefName = "Behavior";
    }

    public Vector2 NextDirection()
    {
        foreach (LocalPlayer player in GameManager.Instance.Players)
        {
            if (TargetPlayer == null || (TargetPlayer.transform.position - Entity.transform.position).sqrMagnitude > (player.transform.position - Entity.transform.position).sqrMagnitude)
            {
                TargetPlayer = player;
            }
        }

        return PathToPos(TargetPlayer.transform.position);
    }

    public bool NextAttack()
    {
        if (TargetPlayer == null) return false;
        if((TargetPlayer.transform.position - Entity.transform.position).sqrMagnitude > Mathf.Pow(AttackRange, 2))
        {
            return false;
        }
        return true;
    }

    public Vector2 PathToPos(Vector2 pos /*, Dictionary<int, int> heuristics*/)
    {
        return ((Pathing)Entity.NetworkComponents["Pathing"]).PathToTarget(pos, 3, 100);
    }
}
