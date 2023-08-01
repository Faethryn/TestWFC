using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WFC/TileComponent")]
public class TileComponent : ScriptableObject
{
    [SerializeField]
    public GameObject _modulePrefab;

    [SerializeField]
  public  SideContactType _northContact;

    [SerializeField]
 public   SideContactType _eastContact;
    [SerializeField]
  public  SideContactType _southContact;
    [SerializeField]
 public    SideContactType _westContact;
}
