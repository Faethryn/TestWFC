using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tile
{


    public Vector2 Coordinates = new Vector2();

    public int currentID = -1;

    public List<int> PossibleIds = new List<int>();

    public BoardGenerator parent = null;

    private bool _alreadyInWave = false;

    public Tile(Vector2 coordinate, List<int> PossibleIds, BoardGenerator parent)
        {

        this.Coordinates = coordinate;
        this.PossibleIds = PossibleIds;
        this.parent = parent;

        }
    public Tile()
    {

       

    }

    public void ChooseTile()
    {
        int id = 1;
       
      
      if(PossibleIds.Count > 0)
        {

        int randomIndex = Random.Range(0, PossibleIds.Count);
      //  Debug.Log(randomIndex);
          
        id = PossibleIds[randomIndex];
        }



        currentID = id;

        PossibleIds.Clear();

        PossibleIds.Add(currentID);

        parent.CompleteTile(this);


       


        UpdatePossibleIds();
    }

    public void UpdatePossibleIds()
    {
       





            Tile tempTile = null;

            //north
            if (parent.GridSize.y - 1 != Coordinates.y)
            {

            Debug.Log("north");
                tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(0, 1));
              if (tempTile.PossibleIds.Count > 1)
              {
            

                tempTile.UpdatePossibleList(parent.TileModules[currentID]._possibleNeighBoursNorth);
              
                 }
               
            }



            //south
            if (0 != Coordinates.y)
            {
                  Debug.Log("South");

              tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(0, -1));
               if(tempTile.PossibleIds.Count > 1)
              {
              

                tempTile.UpdatePossibleList(parent.TileModules[currentID]._possibleNeighBoursSouth);
               
                 }

        }
        //East
        if (parent.GridSize.x - 1 != Coordinates.x)
            {
            Debug.Log("East");

            tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(1, 0));
            if (tempTile.PossibleIds.Count > 1)
            {
               

                tempTile.UpdatePossibleList(parent.TileModules[currentID]._possibleNeighBoursEast);
               

            }


        }
        //West
        if (0 != Coordinates.x)
        {
            Debug.Log("West");

            tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(-1, 0));
            if (tempTile.PossibleIds.Count > 1)
            {
               

                tempTile.UpdatePossibleList(parent.TileModules[currentID]._possibleNeighBoursWest);

               

            }

        }

        _alreadyInWave = true;


    }

    public void UpdatePossibleList(List<int> possibleIds)
    {
      
        
      
       
            List<int> removePossibleIds = new List<int>();

        string idsInPossible = "ids in possibilities: ";

            foreach (int id in PossibleIds)
            {
            idsInPossible += (id + ",");


                if (possibleIds.Contains(id) == false)
                {
                    removePossibleIds.Add(id);
                   

                }
            }

        string removedIds = "removed ids: ";

          foreach(int id in removePossibleIds)
        {
            removedIds += (id + ",");


            PossibleIds.Remove(id);
        }



      


    }


}
