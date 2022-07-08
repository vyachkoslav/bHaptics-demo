using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePipeline : MonoBehaviour
{
    [SerializeField] ObjectCreator objectCreator;

    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector3 removePosition;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] float speed;
    [SerializeField] float acceleration;

    [SerializeField] List<string> objectsToUse;
    [SerializeField] int maxActiveObjects;
    [SerializeField] int maxSameObjectsInARow;
    int sameInARow;
    string lastObject;

    [SerializeField] float distanceBetweenObjects;

    List<GameObject> activeObjects;

    void Start()
    {
        activeObjects = new List<GameObject>();
    }

    void Update()
    {
        DeleteFarObjects();

        int activeCount = activeObjects.Count;
        if (activeCount < maxActiveObjects)
        {
            float distanceFromLastToSpawn;
            if (activeCount == 0)
                distanceFromLastToSpawn = distanceBetweenObjects;
            else
                distanceFromLastToSpawn = Vector3.Distance(activeObjects[activeCount - 1].transform.position, spawnPosition);

            if(distanceFromLastToSpawn >= distanceBetweenObjects)
            {
                GameObject newObject = GetNewObject();
                newObject.transform.position = spawnPosition;
                activeObjects.Add(newObject);
            }
        }

        MoveAllObjects();
    }

    GameObject GetNewObject()
    {
        string selected = objectsToUse[Random.Range(0, objectsToUse.Count)];
        if (selected == lastObject)
        {
            if (++sameInARow > maxSameObjectsInARow)
            {
                while (selected == lastObject)
                    selected = objectsToUse[Random.Range(0, objectsToUse.Count)];
                sameInARow = 0;
            }
        }
        else
        {
            sameInARow = 0;
        }

        return objectCreator.InstantiateObject(selected);
    }

    void MoveAllObjects()
    {
        foreach(GameObject obj in activeObjects)
        {
            obj.transform.position += moveDirection * speed * Time.deltaTime;
        }
    }
    void DeleteFarObjects()
    {
        foreach (GameObject obj in activeObjects)
        {
            if(Vector3.Distance(obj.transform.position, removePosition) < 0.1f)
            {
                activeObjects.Remove(obj);
                objectCreator.DestroyObject(obj);
                speed += acceleration;
                break;
            }

        }
    }
}
