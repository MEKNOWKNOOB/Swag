using System;
using UnityEngine;
using Unity.Netcode;

public class NetworkComponent : NetworkBehaviour
{
    /// <summary>
    /// The reference name in the Dictionary of NetworkComponents
    /// </summary>
    [NonSerialized] public string RefName = null;

    /// <summary>
    /// The reference to the attached parent NetworkEntity
    /// </summary>
    [NonSerialized] public NetworkEntity Entity = null;
}
