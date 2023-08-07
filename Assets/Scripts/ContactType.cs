using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SideContactType
{
  ground,
  wallNorth,
  wallEast,
  wallSouth,
  wallWest,
  
 
  empty
}


public enum Side
{
    north,
    east,
    south,
    west
}


public static class ContactDirectionInMap
{
    public static  Vector2Int North = new Vector2Int(0, 1);
    public static Vector2Int South = new Vector2Int(0, -1);
    public static Vector2Int East = new Vector2Int(1, 0);
    public static Vector2Int West = new Vector2Int(-1, 0);

     
}




[CreateAssetMenu(menuName = "WFC/ContactType")]
public class ContactType :ScriptableObject
{

  [SerializeField]  private SideContactType _sideContactType;

    [SerializeField]
    private List<SideContactType> _compatibleContacts;

    public SideContactType SideContactType => _sideContactType;

    public List<SideContactType> CompatibleContacts => _compatibleContacts;


    public bool IsMatchingContacts(ContactType other)
    {
        return other.CompatibleContacts.Contains(SideContactType) &&
               CompatibleContacts.Contains(other.SideContactType);
    }





}