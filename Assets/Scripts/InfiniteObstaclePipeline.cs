using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteObstaclePipeline : PredictedObstaclePipeline
{
    /// <summary>
    /// How many same objects can spawn in a row, 0 = Infinity
    /// </summary>
    [SerializeField] int maxSameObjectsInARow;
    int sameInARow;
    string lastObject;

    protected new void Start()
    {
        base.Start();
        if (maxSameObjectsInARow == 0)
            maxSameObjectsInARow = int.MaxValue;
    }
    protected override string SelectNewObject()
    {
        string selected = objectsToUse[Random.Range(0, objectsToUse.Count)];
        if (selected == lastObject)
        {
            if (++sameInARow >= maxSameObjectsInARow)
            {
                while (selected == lastObject)
                    selected = objectsToUse[Random.Range(0, objectsToUse.Count)];

                lastObject = selected;
                sameInARow = 0;
            }
        }
        else
        {
            sameInARow = 0;
        }
        return selected;
    }
}
