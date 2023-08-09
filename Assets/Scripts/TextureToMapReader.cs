using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class TextureToMapReader : MonoBehaviour
{
    [SerializeField] Texture2D inputMap;

    [SerializeField]
    public List<TileComponent> PossibleComponents = new List<TileComponent>();

    public Vector2Int GridSize;
    [SerializeField]
    public float _tileSize = 2.0f;

    private int amountOfTiles = 0;

    public List<LabyrinthTile> TileGrid = new List<LabyrinthTile>();

   

    

    // Start is called before the first frame update
    void Start()
    {
         GridSize = new Vector2Int(inputMap.width, inputMap.height);

     
        InitializeMap();
       
     
       

    }

    private void InitializeMap()
    {
        for (int x = 0; x < GridSize.x; x++)
        {


            for (int y = 0; y < GridSize.y; y++)
            {

                LabyrinthTile newTile= new LabyrinthTile(new Vector2Int(x,y));

              

                
                TileGrid.Add(newTile);

               


            }

        }

        for(int i = 0; i < TileGrid.Count; i++)
        {

            int id = Mathf.RoundToInt(inputMap.GetPixel(TileGrid[i].Coordinates.x, TileGrid[i].Coordinates.y).r * (float)(PossibleComponents.Count - 1f));
            Debug.Log(id.ToString());
             TileGrid[i].CreateSelf(_tileSize, PossibleComponents[id], this.gameObject);

        }


        amountOfTiles = (int)(GridSize.x * GridSize.y);
    }

   


}
