using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WFC/Conditions/MinimumOfType")]

public class MinimumOfTyleType : ExtraCondition
{

    public bool ApplyMinimum = true;

    public TileComponent neighBourComponent;

    public int minimumOfType = 1;


    public override bool ConditionCheck(LabyrinthGenerator generator, LabyrinthTile tileToApplyTo)
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
        else
        {
        return false;

        }




    }

   
}
