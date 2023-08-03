using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LabyrinthTile 
{
    public Vector2 Coordinates = new Vector2();

    public TileComponent currentID = null;

    public List<TileComponent> PossibleIds = new List<TileComponent>();

    public LabyrinthGenerator parent = null;

    private bool _alreadyInWave = false;

    public bool _done = false;

    GameObject spawnedVersion = null;

   

    public void ChooseID()
    {
        if(!_done)
        {
            int id = Random.Range(0, PossibleIds.Count);

            if(PossibleIds.Count == 0)
            {
                Debug.LogWarning("Something went wrong Here");
              
                //RestartNeeded();
                ReAdjustSelf();
                ReAdjustNeighbours();

            }
            else
            {
                TileComponent tempComponent = PossibleIds[id];

                PossibleIds.Clear();

                PossibleIds.Add(tempComponent);

                currentID = PossibleIds[0];


                RefreshNeighBours();
                _done = true;
                CompleteYourself();
            }

            Debug.Log(id.ToString());

           

            Debug.Log(Coordinates.x.ToString() + ", " + Coordinates.y.ToString());

            
        }
       

    }

    private void RestartNeeded()
    {
        PossibleIds.Add(parent.replacement);

        _done = true;

       


        CompleteYourself();
        parent.Restart();
    }

    public void ReAdjustSelf()
    {
        PossibleIds.Clear();

        PossibleIds.AddRange(parent.PossibleComponents);

        AdjustPossibleIds(Side.north);
        AdjustPossibleIds(Side.south);
        AdjustPossibleIds(Side.east);
        AdjustPossibleIds(Side.west);

        if(parent.CompletedTiles.Contains(this))
        {
            parent.CompletedTiles.Remove(this);
        }
       


    }

    private void ReAdjustNeighbours()
    {
        ReAdjustNeighbour(Side.east);
        UpdateNeighbour(Side.north);
        UpdateNeighbour(Side.west);

        UpdateNeighbour(Side.south);

    }




    public void AdjustPossibleIds(Side sourceSide)
    {

        LabyrinthTile tempTile = null;
        List<TileComponent> removeList = new List<TileComponent>();

        if (_done)
        {
            currentID = PossibleIds[0];
        }
        else
        {





            switch (sourceSide)
            {

                case Side.north:
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(0, 1));
                        foreach (TileComponent tileComponent in tempTile.PossibleIds)
                        {
                            foreach (TileComponent tileComponent1 in PossibleIds)
                            {

                                if (tileComponent._southContact != tileComponent1._northContact)
                                {
                                    removeList.Add(tileComponent1);
                                }

                            }

                        }

                        foreach (TileComponent tileComponent in removeList)
                        {
                            PossibleIds.Remove(tileComponent);
                          

                        }

                        

                        break;
                    }
                case Side.east:
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(1, 0));
                        foreach (TileComponent tileComponent in tempTile.PossibleIds)
                        {
                            foreach (TileComponent tileComponent1 in PossibleIds)
                            {

                                if (tileComponent._westContact != tileComponent1._southContact)
                                {
                                    removeList.Add(tileComponent1);
                                }

                            }

                        }

                        foreach (TileComponent tileComponent in removeList)
                        {
                            PossibleIds.Remove(tileComponent);
                          

                        }

                        break;
                    }
                case Side.south:
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(0, -1));
                        foreach (TileComponent tileComponent in tempTile.PossibleIds)
                        {
                            foreach (TileComponent tileComponent1 in PossibleIds)
                            {

                                if (tileComponent._northContact != tileComponent1._southContact)
                                {
                                    removeList.Add(tileComponent1);
                                }

                            }

                        }

                        foreach (TileComponent tileComponent in removeList)
                        {
                            PossibleIds.Remove(tileComponent);

                        }


                        break;
                    }
                case Side.west:
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(-1, 0));
                        foreach (TileComponent tileComponent in tempTile.PossibleIds)
                        {
                            foreach (TileComponent tileComponent1 in PossibleIds)
                            {

                                if (tileComponent._eastContact != tileComponent1._westContact)
                                {
                                    removeList.Add(tileComponent1);
                                }

                            }

                        }

                        foreach (TileComponent tileComponent in removeList)
                        {
                            PossibleIds.Remove(tileComponent);

                        }




                        break;
                    }


                    

            }



            //if(PossibleIds.Count == 0)
            //{
            //    parent.Restart();
            //}


            if (PossibleIds.Count == 1)
            {
                currentID = PossibleIds[0];
                CompleteYourself();

                _done = true;
            }

            if (PossibleIds.Count == 0)
            {
                Debug.LogWarning("Something went wrong Here");
                Debug.Log(sourceSide.HumanName() );
                Debug.Log(Coordinates.x.ToString() + " , " + Coordinates.y.ToString());
                Debug.Log("Removed Tiles");
                foreach(TileComponent tileComponent in removeList)
                {
                    Debug.Log(tileComponent.name);
                }

                Debug.Log("endOfList");

                RestartNeeded();
              


            }



           
           

        }
    }

    public void RefreshNeighBours()
    {
        UpdateNeighbour(Side.east);
        UpdateNeighbour(Side.north);
        UpdateNeighbour(Side.west);

        UpdateNeighbour(Side.south);
    }
    private void RefreshNeighBour(Side IgnoredSide)
    {
        switch (IgnoredSide)
        {
            case Side.west:
                {
                    UpdateNeighbour(Side.east);
                    UpdateNeighbour(Side.north);

                    UpdateNeighbour(Side.south);

                    break;
                }
                case Side.east:
                {
                    UpdateNeighbour(Side.west);
                    UpdateNeighbour(Side.north);
                    UpdateNeighbour(Side.south);

                    break;
                }
                case Side.north:
                {
                    UpdateNeighbour(Side.south);
                    UpdateNeighbour(Side.east);
                    UpdateNeighbour(Side.west);


                    break;
                }
                case Side.south:
                {
                    UpdateNeighbour(Side.north);
                    UpdateNeighbour(Side.east);
                    UpdateNeighbour(Side.west);


                    break;
                }

        }
    }

    private void ReAdjustNeighbour(Side sideToUpdate)
    {
        LabyrinthTile tempTile = null;
        switch (sideToUpdate)
        {

            case Side.west:
                {
                    if (Coordinates.x > 0)
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(-1, 0));

                        tempTile.ReAdjustSelf();

                    }
                    break;
                }
            case Side.east:
                {
                    if (Coordinates.x != (parent.GridSize.x - 1))
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(1, 0));
                        tempTile.ReAdjustSelf();


                    }

                    break;
                }
            case Side.north:
                {
                    if (Coordinates.y != (parent.GridSize.y - 1))
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(0, 1));
                        tempTile.ReAdjustSelf();

                    }


                    break;
                }
            case Side.south:
                {
                    if (Coordinates.y > 0)
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(0, -1));
                        tempTile.ReAdjustSelf();


                    }


                    break;
                }


        }
    }

    private void UpdateNeighbour(Side sideToUpdate)
    {
        LabyrinthTile tempTile = null;
        switch (sideToUpdate)
        {

            case Side.west:
                {
                    if(Coordinates.x > 0 )
                    {
                    tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(-1, 0));
                   
                    tempTile.AdjustPossibleIds(Side.east);

                    }
                    break;
                }
            case Side.east:
                {
                    if(Coordinates.x != (parent.GridSize.x - 1))
                    {
                    tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(1, 0));
                    tempTile.AdjustPossibleIds(Side.west);

                    }

                    break;
                }
            case Side.north:
                {
                    if(Coordinates.y != (parent.GridSize.y -1))
                     {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(0, 1));
                        tempTile.AdjustPossibleIds(Side.south);
                    }
                  

                    break;
                }
            case Side.south:
                {
                    if (Coordinates.y > 0)
                    {
                        tempTile = parent.GetTileAtCoord(Coordinates + new Vector2(0, -1));
                    tempTile.AdjustPossibleIds(Side.north);

                    }


                    break;
                }


        }
    }

    private void CompleteYourself()
    {
        parent.CompletedTiles.Add(this);

        spawnedVersion = GameObject.Instantiate(PossibleIds[0]._modulePrefab, new Vector3(Coordinates.x * parent._tileSize, 0, Coordinates.y * parent._tileSize), new Quaternion());

        parent._SpawnedTiles.Add(spawnedVersion);
    }

    

}




