using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class Entity : MonoBehaviour
{
    // {<"RefName", EComponent>}
    public Dictionary<string, EComponent> Components;

    // In Child Classes, override and call base.Start()
    protected virtual void Start()
    {
        Components = new Dictionary<string, EComponent>();
        
        foreach (EComponent comp in gameObject.GetComponents<EComponent>())
        {
            // Error Checking ------------------------------------------------------------------
            if (string.IsNullOrEmpty(comp.RefName))
            {
                Debug.LogError(gameObject + " found component with invalid reference name");
                EditorApplication.isPlaying = false;
                return;
            }
            if (Components.ContainsKey(comp.RefName))
            {
                Debug.LogError(gameObject + " found duplicate component");
                EditorApplication.isPlaying = false;
                return;
            }
            // ---------------------------------------------------------------------------------

            Components.Add(comp.RefName, comp);
        }
    }
}
