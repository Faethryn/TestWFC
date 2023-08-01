using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyrinthGenerator : MonoBehaviour
{
    [SerializeField]
  public List<TileComponent> PossibleComponents = new List<TileComponent>();

    public Vector2 GridSize;

    private int currentUpdate = 0;

    public List<List<LabyrinthTile>> TileGrid = new List<List<LabyrinthTile>>();

    public List<LabyrinthTile> CompletedTiles = new List<LabyrinthTile>();


    private int amountOfTiles = 0;

    private bool _isDone = false;

    void Start()
    {


        for (int i = 0; i < GridSize.x; i++)
        {
            TileGrid.Add(new List<LabyrinthTile>());

            for (int j = 0; j < GridSize.y; j++)
            {

                LabyrinthTile newTile = new LabyrinthTile();

                newTile.Coordinates = new Vector2(i, j);

                newTile.parent = this;
                foreach (TileComponent module in PossibleComponents)
                {
                    newTile.PossibleIds.Add(module);
                }

                TileGrid[i].Add(newTile);


            }

        }


         amountOfTiles = (int)(GridSize.x * GridSize.y);




        //foreach(List<Tile> tileRow in TileGrid) 
        //{

        //    foreach(Tile tile in tileRow)
        //    {

        //    }

        //}


    }

    private void Update()
    {
        if(!_isDone)
        {
            LabyrinthTile chosenTile = TileGrid[0][0];


            if(currentUpdate != 0)
            {
            chosenTile = ChooseLowestPossibleID(chosenTile);

            }

            currentUpdate++;


            if (CompletedTiles.Contains(chosenTile))
            {

            }
            else
            {
                chosenTile.ChooseID();
            }


           
        }
        
     

        if(CompletedTiles.Count == amountOfTiles)
        {
            _isDone = true;
        }


    }

    private LabyrinthTile ChooseLowestPossibleID(LabyrinthTile startTile)
    {
        

        foreach (List<LabyrinthTile> tileList in TileGrid)
        {
            foreach (LabyrinthTile tile in tileList)
            {
                if (tile.PossibleIds.Count >= startTile.PossibleIds.Count  )
                {
                    startTile = tile;
                }
            }
        }

        return startTile;
    }

    public LabyrinthTile GetTileAtCoord(Vector2 coord)
    {

        return TileGrid[(int)coord.x][(int)coord.y];
    }

    public void Restart()
    {
        TileGrid.Clear();
        CompletedTiles.Clear();
        Start();
    }


}
