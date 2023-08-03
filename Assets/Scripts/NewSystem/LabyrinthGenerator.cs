using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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


    [SerializeField]
    public float _tileSize = 2.0f;


    public List<GameObject> _SpawnedTiles = new List<GameObject>();

    [SerializeField] public TileComponent replacement;

 



    void Start()
    {


        for (int x = 0; x < GridSize.x; x++)
        {
            TileGrid.Add(new List<LabyrinthTile>());

            for (int y = 0; y < GridSize.y; y++)
            {

                LabyrinthTile newTile = new LabyrinthTile();

                newTile.Coordinates = new Vector2(x, y);

                newTile.parent = this;
                newTile.PossibleIds.AddRange( PossibleComponents);

                TileGrid[x].Add(newTile);


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
            LabyrinthTile chosenTile = null;


            chosenTile = ChooseLowestPossibleID();

           


            if (CompletedTiles.Contains(chosenTile))
            {
                Debug.LogError("you gave me a completed tile");
            }
            else
            {
                chosenTile.ChooseID();
            }


           
        }
        else
        {
            Debug.Log("done");
        }
        
     

        if(CompletedTiles.Count == (GridSize.x * GridSize.y))
        {
            _isDone = true;
            Debug.Log("done");
        }

       

    }

    private LabyrinthTile ChooseLowestPossibleID()
    {
        
        LabyrinthTile tempTile = null;

        List<LabyrinthTile> uncompletedTiles = new List<LabyrinthTile>();

        foreach (List<LabyrinthTile> tileList in TileGrid)
        {
            foreach (LabyrinthTile tile in tileList)
            {

                if ( !(CompletedTiles.Contains(tile)) )
                {
                   uncompletedTiles.Add(tile);
                }
            }
        }

        int lowestIDCount = uncompletedTiles.Min(tile => tile.PossibleIds.Count);

        tempTile = uncompletedTiles.First(tile => tile.PossibleIds.Count == lowestIDCount);


       

        return tempTile;
    }


   

    

    public LabyrinthTile GetTileAtCoord(Vector2 coord)
    {

        return TileGrid[(int)coord.x][(int)coord.y];
    }

    public void Restart()
    {
        TileGrid.Clear();
        CompletedTiles.Clear();

        foreach(GameObject tile in _SpawnedTiles)
        {
            Destroy(tile);
        }

        _SpawnedTiles.Clear();
        Debug.LogError("restarting");
      
        
        
        Start();
    }


}
