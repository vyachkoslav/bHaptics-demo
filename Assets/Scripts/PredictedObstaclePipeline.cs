using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns known amount of obstacles
/// </summary>
public class PredictedObstaclePipeline : MonoBehaviour
{
    [SerializeField] ObjectCreator objectCreator;

    /// <summary>
    /// Obstacle spawn position
    /// </summary>
    [SerializeField] Vector3 spawnPosition;

    /// <summary>
    /// Obstacle destroy position
    /// </summary>
    [SerializeField] Vector3 removePosition;

    /// <summary>
    /// Obstacle global move direction
    /// </summary>
    [SerializeField] Vector3 moveDirection;

    /// <summary>
    /// Obstacle speed
    /// </summary>
    [SerializeField] float speed;

    /// <summary>
    /// Speed increase after obstacle destroy
    /// </summary>
    [SerializeField] float acceleration;

    /// <summary>
    /// List of obstacles to spawn. 
    /// </summary>
    [SerializeField] protected List<string> objectsToUse;

    /// <summary>
    /// Distance between objects' spawn
    /// </summary>
    [SerializeField] float distanceBetweenObjects;

    List<GameObject> activeObjects;
    int currentIndex;

    protected void Start()
    {
        activeObjects = new List<GameObject>();
    }

    void Update()
    {
        MoveAllObjects();
        DeleteFarObjects();

        float distanceFromLastToSpawn;
        if (activeObjects.Count == 0)
            distanceFromLastToSpawn = distanceBetweenObjects;
        else
            distanceFromLastToSpawn = Vector3.Distance(activeObjects[^1].transform.position, spawnPosition);

        if (distanceFromLastToSpawn >= distanceBetweenObjects)
        {
            GameObject newObject = GetNewObject();
            if (!newObject)
                return;
            newObject.transform.position = spawnPosition;
            activeObjects.Add(newObject);
        }

    }

    /// <summary>
    ///  Selects next obstacle to spawn
    /// </summary>
    protected virtual string SelectNewObject()
    {
        if (currentIndex >= objectsToUse.Count)
            return string.Empty;

        return objectsToUse[currentIndex++];
    }
    /// <summary>
    /// Spawns new obstacle
    /// </summary>
    GameObject GetNewObject()
    {
        string selected = SelectNewObject();
        return objectCreator.InstantiateObject(selected);
    }

    /// <summary>
    /// Moves all obstacles
    /// </summary>
    void MoveAllObjects()
    {
        foreach(GameObject obj in activeObjects)
        {
            obj.transform.position += speed * Time.deltaTime * moveDirection;
        }
    }
    /// <summary>
    /// Deletes obstacle near the removePosition
    /// </summary>
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
