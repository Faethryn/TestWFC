using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MinimumOfTyleType : IExtraCondition
{
    public TileComponent neighBourComponent;

    public int minimumOfType = 1;


    public bool ConditionCheck(LabyrinthGenerator generator, LabyrinthTile tileToApplyTo)
    {

        int numberOfMatchingTiles = 0;

        foreach(var neighbour in tileToApplyTo.AdjacentTilePositions)
        {

            LabyrinthTile tempTile = generator.GetTileAtCoord(neighbour);

            Vector2 difference = tileToApplyTo.Coordinates - tempTile.Coordinates;

            foreach(var possibleID in tempTile.PossibleIds)
            {

                if(possibleID == neighBourComponent)
                {
                    numberOfMatchingTiles++;
                }


            }


        }

        if(numberOfMatchingTiles >= minimumOfType)
        {

           
            return true;
        }

        return false;
    }
}
