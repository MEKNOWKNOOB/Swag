using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using Unity.Netcode;

public class NetworkEntity : NetworkBehaviour
{
    // {"RefName", NetworkComponent}
    public Dictionary<string, NetworkComponent> NetworkComponents;

    [Header("Base")]
    public string Name;
    [SerializeField] new public Transform transform = null;

    // In Child Classes, override and call base.Start()
    protected virtual void Start()
    {
        NetworkComponents = new Dictionary<string, NetworkComponent>();

        foreach (NetworkComponent comp in gameObject.GetComponents<NetworkComponent>())
        {
            // Error Checking ------------------------------------------------------------------
            if (string.IsNullOrEmpty(comp.RefName))
            {
                Debug.LogError(gameObject + " found component with invalid reference name");
                EditorApplication.isPlaying = false;
                return;
            }
            if (NetworkComponents.ContainsKey(comp.RefName))
            {
                Debug.LogError(gameObject + " found duplicate component");
                EditorApplication.isPlaying = false;
                return;
            }
            // ---------------------------------------------------------------------------------

            NetworkComponents.Add(comp.RefName, comp);
            comp.Entity = this;
        }

        if (transform == null)
        {
            transform = base.transform;
        }
    }
}
