using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WFC/TileComponent")]
public class TileComponent : ScriptableObject
{
    [SerializeField]
    public GameObject _modulePrefab;

    [SerializeField]
    public ContactType _northContact;

    [SerializeField]
    public ContactType _eastContact;
    [SerializeField]
    public ContactType _southContact;
    [SerializeField]
    public ContactType _westContact;



    [SerializeField]
    public List<IExtraCondition> _extraConditions = new List<IExtraCondition>();



    public bool IsMatchingModules(TileComponent otherTileState, Vector2Int side)
    {
        if (side == new Vector2(0, 1))
        {

            return _northContact.IsMatchingContacts(otherTileState._southContact);

        }
        else
        {


            if (side == new Vector2Int(1, 0))
            {
                return _eastContact.IsMatchingContacts(otherTileState._westContact);

            }
            else
            {



                if (side == new Vector2Int(-1, 0))
                {
                    return _westContact.IsMatchingContacts(otherTileState._eastContact);

                }
                else
                {



                    if (side == new Vector2Int(0, -1))
                    {
                        return _southContact.IsMatchingContacts(otherTileState._northContact);

                    }
                    else
                    {

                        return false;

                    }



                }

            }




        }
    }

    public bool DoExtraConditionsMatch(LabyrinthGenerator generator, LabyrinthTile tileToApplyTo)
    {

        if(_extraConditions.Count > 0)
        {

            foreach (IExtraCondition condition in _extraConditions)
            {
                if(condition.ConditionCheck(generator, tileToApplyTo))
                {

                }
                else
                {
                    return false;
                }
            }

        }


        return true;
    }


   

}
