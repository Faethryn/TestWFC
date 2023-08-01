using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{

    public Vector2 GridSize;

    public List<List<Tile>> TileGrid = new List<List<Tile>>();

    [SerializeField]
    public List<TileModule> TileModules = new List<TileModule>();

    public List<Tile> completedTiles = new List<Tile>();

    private bool _isDone= false;

    Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();

    // Start is called before the first frame update
    void Start()
    {
        

        for(int i = 0; i < GridSize.x; i++)
        {
            TileGrid.Add(new List<Tile>());

            for (int j = 0; j < GridSize.y; j++)
            {

                Tile newTile = new Tile();

                newTile.Coordinates = new Vector2(i, j);
                
                newTile.parent = this;
                foreach(TileModule module in TileModules)
                {
                    newTile.PossibleIds.Add(module._moduleID);
                }

                TileGrid[i].Add(newTile);


            }

        }


        int amountOfTiles = (int)(GridSize.x * GridSize.y);
       

        

        //foreach(List<Tile> tileRow in TileGrid) 
        //{
        
        //    foreach(Tile tile in tileRow)
        //    {
               
        //    }
        
        //}

        
    }


    IEnumerator NextTile()
    {


        yield return null;
    }

   
    public Tile GetTileAtCoord(Vector2 coord)
    {

        return TileGrid[(int)coord.x][(int)coord.y];
    }


    public void CompleteTile(Tile tile)
    {
        completedTiles.Add(tile);
    }

}
