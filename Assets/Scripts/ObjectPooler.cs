using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : ObjectCreator
{
    [Serializable]
    class ObjectCount
    {
        public string Name;
        public GameObject Instance;
        public uint Count;
    }
    [SerializeField] List<ObjectCount> objectsToPool;
    List<ObjectCount> pooledObjects = new List<ObjectCount>();

    public static ObjectPooler Instance { get; private set; }

    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this);
    }

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
}
