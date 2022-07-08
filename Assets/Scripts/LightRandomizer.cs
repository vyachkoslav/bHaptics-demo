using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRandomizer : MonoBehaviour
{
    [SerializeField] List<Color> possibleColors;
    private void OnEnable()
    {
        GetComponent<Light>().color = possibleColors[Random.Range(0, possibleColors.Count)];
    }
}