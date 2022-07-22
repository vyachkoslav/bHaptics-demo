using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object creator which creates all objects on load and activates them when needed.
/// </summary>
public class ObjectPooler : ObjectCreator
{
    [Serializable]
    protected class ObjectCount
    {
        public string Name;
        public GameObject Instance;
        public uint Count;
    }
    /// <summary>
    /// Objects to spawn on load
    /// </summary>
    [SerializeField] List<ObjectCount> objectsToPool;
    protected List<ObjectCount> pooledObjects = new List<ObjectCount>();

    void Start()
    {
        foreach(ObjectCount obj in objectsToPool)
        {
            for (int i = 0; i <= obj.Count; ++i)
            {
                ObjectCount newObject = new ObjectCount()
                { 
                    Name = obj.Name, 
                    Instance = Instantiate(obj.Instance), 
                    Count = 1 
                };
                newObject.Instance.SetActive(false);
                pooledObjects.Add(newObject);
            }
        }
    }

    public override GameObject InstantiateObject(string name)
    {
        foreach(ObjectCount obj in pooledObjects)
        {
            if(obj.Name == name && !obj.Instance.activeSelf)
            {
                obj.Instance.SetActive(true);
                return obj.Instance;
            }
        }
        return null;
    }
    public override void DestroyObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    public override void DestroyAllCreatedObjects()
    {
        pooledObjects.ForEach(x => x.Instance.SetActive(false));
    }
}
