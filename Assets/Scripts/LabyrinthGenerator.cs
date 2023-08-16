using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading;
using System;
using JetBrains.Annotations;
using System.Diagnostics.Tracing;

public class LabyrinthGenerator : MonoBehaviour
{
    [SerializeField]
  public List<TileComponent> PossibleComponents = new List<TileComponent>();
    [SerializeField]
    private TileComponent _topComponent;
    [SerializeField]
    private TileComponent _bottomComponent;
    [SerializeField]
    private TileComponent _rightComponent;
    [SerializeField]
    private   TileComponent _leftComponent;

    public Vector2Int GridSize;

    private int currentUpdate = 0;

    public List<LabyrinthTile> TileGrid = new List<LabyrinthTile>();

   
    public List<LabyrinthTile> CompletedTiles = new List<LabyrinthTile>();


    private int amountOfTiles = 0;

    private bool _isDone = false;


    [SerializeField]
    public float _tileSize = 2.0f;


    public List<GameObject> _SpawnedTiles = new List<GameObject>();

    [SerializeField] public TileComponent replacement;

 




    void Start()
    {



      

        InitializeMap();
        ExtraStartRules();


        StartCoroutine(ThreadRun());
       

        
      

      
       
       


        //foreach(List<Tile> tileRow in TileGrid) 
        //{

        //    foreach(Tile tile in tileRow)
        //    {

        //    }

        //}


    }

  


    IEnumerator ThreadRun()
    {

        Thread myThread = new Thread(FillCells);

        myThread.Start();
        
        yield return new WaitUntil(() => (myThread.IsAlive == false));

        ThreadDone();

        yield return null;

    }

   
    


    private void ThreadDone()
    {
        CreateMap();
        SaveToTexture.SaveTilesToTexture(GridSize, TileGrid, this);

    }



    private void  InitializeMap()
    {
        for (int x = 0; x < GridSize.x; x++)
        {


            for (int y = 0; y < GridSize.y; y++)
            {

                LabyrinthTile newTile = new LabyrinthTile(this, new Vector2Int(x, y), PossibleComponents);



                TileGrid.Add(newTile);


            }

        }


        amountOfTiles = (int)(GridSize.x * GridSize.y);
    }


    void ExtraStartRules()
    {

        SetEdges();

    }

    void SetEdges()
    {
        LabyrinthTile tile = null;
        for(int x = 0; x < GridSize.x ; x++)
        {
            for( int y = 0; y < GridSize.y ; y++  )
            {

                if(y == 0 && (x > 0 && x < (GridSize.x -1)))
                {
                    tile = GetTileAtCoord(new Vector2Int(x, y));

                    tile.TileComponentOverride(_bottomComponent);
                }

                if(y == (GridSize.y -1) && (x > 0 && x < (GridSize.x - 1))) 
                {

                    tile = GetTileAtCoord(new Vector2Int(x, y));

                    tile.TileComponentOverride(_topComponent);

                }

                if (x == (GridSize.x - 1) && (y > 0 && y < (GridSize.y - 1)))
                {

                    tile = GetTileAtCoord(new Vector2Int(x, y));

                    tile.TileComponentOverride(_rightComponent);

                }
                if (x == 0 && (y > 0 && y < (GridSize.y - 1)))
                {

                    tile = GetTileAtCoord(new Vector2Int(x, y));

                    tile.TileComponentOverride(_leftComponent);

                }

            }

        }

    }

    void FillCells()
    {
        LabyrinthTile cell = null;

        do
        {
            var cellsWithUnselectedState = TileGrid.Where(c => c.PossibleIds.Count() > 1).ToArray();

            if (cellsWithUnselectedState.Length == 0)
                return;

            var minStatesCount = cellsWithUnselectedState.Min(c => c.PossibleIds.Count());

            cell = cellsWithUnselectedState.First(c => c.PossibleIds.Count() == minStatesCount);
        }
        while (cell.TrySelectState( ));
    }


   







    void CreateMap()
    {
        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                
               
               GetTileAtCoord(new Vector2Int(i,j)).CreateSelf();
            }
        }
    }





    public LabyrinthTile GetTileAtCoord(Vector2Int coord)
    {
        int index =  (coord.x * GridSize.x + coord.y );

        return TileGrid[index];
    }

   


    public class ThreadFinishedEventargs : EventArgs
    {
        public ThreadFinishedEventargs()
        {

        }
        

    }

    public EventHandler<ThreadFinishedEventargs> ThreadFinished;

    protected virtual void OnThreadFinished(ThreadFinishedEventargs eventargs)
    {
        var handler = ThreadFinished;
        handler?.Invoke(this, eventargs);
    }


}
