using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveToTexture 
{
 static public void SaveTilesToTexture(Vector2Int TextureSize, List<LabyrinthTile> tileGrid, LabyrinthGenerator parent)
    {

        var texture = new Texture2D(TextureSize.x, TextureSize.y, TextureFormat.ARGB32, false);

        for(int i = 0; i < tileGrid.Count; i++)
        {

            var index = parent.PossibleComponents.IndexOf(tileGrid[i].PossibleIds[0]);
            var brightness = index / (parent.PossibleComponents.Count -1f) ;
            Debug.Log(index.ToString());

            texture.SetPixel(tileGrid[i].Coordinates.x, tileGrid[i].Coordinates.y, new Color(brightness,0,0));

        }


        texture.Apply();

        SaveTextureToFileUtility.SaveTexture2DToFile(texture, (Application.dataPath + "/Textures/Grid"), SaveTextureToFileUtility.SaveTextureFileFormat.PNG);

    }
}
