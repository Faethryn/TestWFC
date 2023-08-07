using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LabyrinthGenerator : MonoBehaviour
{
    [SerializeField]
  public List<TileComponent> PossibleComponents = new List<TileComponent>();

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



        InizializeMap();
        FillCells();
        CreateMap();


        //foreach(List<Tile> tileRow in TileGrid) 
        //{

        //    foreach(Tile tile in tileRow)
        //    {

        //    }

        //}


    }



  private void  InizializeMap()
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

   


}
