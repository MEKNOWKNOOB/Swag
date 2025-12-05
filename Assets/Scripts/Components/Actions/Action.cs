using UnityEngine;

public class Action : NetworkComponent
{
    // How Long the Action Takes
    [SerializeField] float useTime = 0.0f;

    // Last Time the Action Started (Cannot be used again until Time > useStartTime + useTime)
    [SerializeField] float useStartTime = 0.0f;

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

        // Make a child class with what you need (animations, hurtboxes, turning left at the crossroads), just call base first
    }
}
