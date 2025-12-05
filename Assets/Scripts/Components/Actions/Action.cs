using UnityEngine;

public class Action : NetworkComponent
{
    void Awake()
    {
        RefName = "Action";
    }

    /// <summary>
    /// Runs Active
    /// </summary>
    /// <param name="user"> user is NOT the Item class, it's the actual User, Tool should work without an Item class as long its part of an Entity</param>
    public virtual void Active(NetworkEntity user)
    {
        // Make a child class with what you need
    }
}
