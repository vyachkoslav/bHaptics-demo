using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictedObstaclePipeline : MonoBehaviour
{
    [SerializeField] ObjectCreator objectCreator;

    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector3 removePosition;
    [SerializeField] Vector3 moveDirection;
    [SerializeField] float speed;
    [SerializeField] float acceleration;

    [SerializeField] protected List<string> objectsToUse;
    [SerializeField] int maxActiveObjects;

    [SerializeField] float distanceBetweenObjects;

    List<GameObject> activeObjects;
    int currentIndex;

    void Start()
    {
        activeObjects = new List<GameObject>();
    }

    void Update()
    {
        MoveAllObjects();
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
                if (!newObject)
                    return;
                newObject.transform.position = spawnPosition;
                activeObjects.Add(newObject);
            }
        }

    }

    protected virtual string SelectNewObject()
    {
        if (currentIndex >= objectsToUse.Count)
            return string.Empty;

        return objectsToUse[currentIndex++];
    }
    GameObject GetNewObject()
    {
        string selected = SelectNewObject();
        return objectCreator.InstantiateObject(selected);
    }

    void MoveAllObjects()
    {
        foreach(GameObject obj in activeObjects)
        {
            obj.transform.position += speed * Time.deltaTime * moveDirection;
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
                if (activeObjects.Count == 0)
                    GameManager.WinGame();
                break;
            }
        }
    }
}
