using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for object creation and destruction
/// </summary>
public abstract class ObjectCreator : MonoBehaviour
{
    /// <summary>
    /// Spawns object
    /// </summary>
    public abstract GameObject InstantiateObject(string name);
    /// <summary>
    /// Destroys object
    /// </summary>
    public abstract void DestroyObject(GameObject obj);
    /// <summary>
    /// Destroys all spawned objects
    /// </summary>
    public abstract void DestroyAllCreatedObjects();
}