using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Dictionary that can be used in Unity Editor
/// </summary>
[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [Serializable]
    public struct Pair
    {
        public TKey Key;
        public TValue Value;
    }

    [SerializeField] private List<Pair> pairs = new();

    private Dictionary<TKey, TValue> dictionary;

    /// <summary>
    /// Builds a Dictionary
    /// </summary>
    public Dictionary<TKey, TValue> ToDictionary()
    {
        if (dictionary == null)
        {
            dictionary = new Dictionary<TKey, TValue>();
            foreach (var p in pairs)
                if (!dictionary.ContainsKey(p.Key))
                    dictionary.Add(p.Key, p.Value);
        }
        return dictionary;
    }
}
