using UnityEngine;

public class Action : NetworkComponent
{
    // How Long the Action Takes
    [SerializeField] private float useTime = 0.0f;

    // Last Time the Action Started (Cannot be used again until Time > useStartTime + useTime)
    [SerializeField] private float useStartTime = 0.0f;

    void Awake()
    {
        RefName = "Action";
    }

    /// <summary>
    /// Runs Active
    /// </summary>
    /// <param name="tool"> referencing Tool being used</param>
    /// <param name="user"> user is NOT the Item class, it's the actual User, Tool should work without an Item class as long its part of an Entity</param>
    public virtual void Active(Tool tool, NetworkEntity user)
    {
        if (Time.time < useStartTime + useTime) return; // Still Using
        useStartTime = Time.time;

        // Make a child class with what you need (animations, hurtboxes, turning left at the crossroads), just call base first
    }

    /// <summary>
    /// Runs Active
    /// </summary>
    /// <param name="tool"> referencing Tool being used</param>
    /// <param name="user"> user is NOT the Item class, it's the actual User, Tool should work without an Item class as long its part of an Entity</param>
    /// <param name="direction"> direction of use</param>
    public virtual void Active(Tool tool, NetworkEntity user, Vector2 direction)
    {
        if (Time.time < useStartTime + useTime) return; // Still Using
        useStartTime = Time.time;

        // Make a child class with what you need (animations, hurtboxes, turning left at the crossroads), just call base first
    }
}
