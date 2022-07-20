using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectCreator : MonoBehaviour
{
    public abstract GameObject InstantiateObject(string name);
    public abstract void DestroyObject(GameObject obj);
    public abstract void DestroyAllCreatedObjects();
}