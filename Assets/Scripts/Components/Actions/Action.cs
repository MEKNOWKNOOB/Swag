using UnityEngine;

public class Action : NetworkComponent
{
    // Last Time the Action Started (Cannot be used again until Time > useStartTime + useTime)
    [SerializeField] protected float useStartTime = 0.0f;

    void Awake()
    {
        RefName = "Action";
    }

    /// <summary>
    /// Runs Active
    /// </summary>
    /// <param name="tool"> referencing Tool being used</param>
    /// <param name="user"> user is NOT the Item class, it's the actual User, Tool should work without an Item class as long its part of an Entity</param>
    public virtual bool Active(Tool tool, NetworkEntity user)
    {
        if (Time.time < useStartTime + tool.UseTime) return false; // Still Using
        useStartTime = Time.time;
        return true;

        // Make a child class with what you need (animations, hurtboxes, turning left at the crossroads), just call base first like this {if(!base.Active(tool, user)) return false;}
    }

    /// <summary>
    /// Runs Active
    /// </summary>
    /// <param name="tool"> referencing Tool being used</param>
    /// <param name="user"> user is NOT the Item class, it's the actual User, Tool should work without an Item class as long its part of an Entity</param>
    /// <param name="direction"> direction of use</param>
    public virtual bool Active(Tool tool, NetworkEntity user, Vector2 direction)
    {
        if (Time.time < useStartTime + tool.UseTime) return false; // Still Using
        useStartTime = Time.time;
        return true;

        // Make a child class with what you need (animations, hurtboxes, turning left at the crossroads), just call base first like this {if(!base.Active(tool, user)) return false;}
    }
}
