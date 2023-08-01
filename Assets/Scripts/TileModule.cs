using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WFC/TileModule")]
public class TileModule : ScriptableObject
{

    [SerializeField]
    public GameObject _modulePrefab;

    [SerializeField]
    public int _moduleID;

    [SerializeField]
    public List<int> _possibleNeighBoursNorth = new List<int>();

    [SerializeField]
    public List<int> _possibleNeighBoursSouth = new List<int>();

    [SerializeField]
    public List<int> _possibleNeighBoursWest = new List<int>();

    [SerializeField]
    public List<int> _possibleNeighBoursEast = new List<int>();

}
