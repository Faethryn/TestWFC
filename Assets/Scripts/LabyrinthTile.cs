using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

using static UnityEditor.VersionControl.Asset;

public class LabyrinthTile 
{
    public Vector2Int Coordinates = new Vector2Int();

    public TileComponent currentID = null;

    public List<TileComponent> PossibleIds = new List<TileComponent>();

    public LabyrinthGenerator parent = null;

    public List<Vector2Int> AdjacentTilePositions { get; private set; }

    private Dictionary<LabyrinthTile, List<TileComponent>> _mapTileCashe = new Dictionary<LabyrinthTile,List<TileComponent>>();

    public bool _done = false;

    GameObject spawnedVersion = null;


    public LabyrinthTile(Vector2Int positionInMap)
    {
        Coordinates = positionInMap;
    }


    public LabyrinthTile(LabyrinthGenerator parentGenerator, Vector2Int positionInMap, List<TileComponent> possibleIds)
    {
        PossibleIds = new List<TileComponent>(possibleIds);
        Coordinates = positionInMap;
        parent = parentGenerator;
        AdjacentTilePositions = GetAdjacentCellsPositions(parent);
    }

    List<Vector2Int> GetAdjacentCellsPositions(LabyrinthGenerator map)
    {
        List<Vector2Int> cells = new List<Vector2Int>();
        if (Coordinates.x - 1 >= 0) cells.Add(new Vector2Int(Coordinates.x - 1, Coordinates.y));
        if (Coordinates.x + 1 < map.GridSize.x) cells.Add(new Vector2Int(Coordinates.x + 1, Coordinates.y));
        if (Coordinates.y - 1 >= 0) cells.Add(new Vector2Int(Coordinates.x, Coordinates.y - 1));
        if (Coordinates.y + 1 < map.GridSize.y) cells.Add(new Vector2Int(Coordinates.x, Coordinates.y + 1));
        return cells;
    }


    public bool TrySelectState()
    {
       
        var states = new List<TileComponent>(PossibleIds);
        while (states.Count > 0)
        {
            System.Random random = new System.Random();
            var selectState = PossibleIds[random.Next(0, PossibleIds.Count)];
            PossibleIds = new List<TileComponent>() { selectState };
            if (!TryUpdateAdjacentTiles(this))
            {
                states.Remove(selectState);
            }
            else return true;
        }
        return false;
    }


    public IEnumerator TrySelectStateCoroutine()
    {

        var states = new List<TileComponent>(PossibleIds);
        while (states.Count > 0)
        {
            System.Random random = new System.Random();
            var selectState = PossibleIds[random.Next(0, PossibleIds.Count)];
            PossibleIds = new List<TileComponent>() { selectState };
            if (!TryUpdateAdjacentTiles(this))
            {
                states.Remove(selectState);
            }
            else yield return true;
        }
       yield return false;
    }


    delegate bool TryUpdateAction();

    bool TryUpdateAdjacentTiles(LabyrinthTile cellWithSelectedModule)
    {
        List<TryUpdateAction> updateAdjacentTileActions = new List<TryUpdateAction>();
        bool updateSuccess = AdjacentTilePositions.All(tile =>
        {
            return parent.GetTileAtCoord(tile).TryUpdateStates(this, cellWithSelectedModule, updateAdjacentTileActions);
        });
        if (!updateSuccess)
        {
            ReverseStates(cellWithSelectedModule);
            return false;
        }
        else
            return updateAdjacentTileActions.All(action => action.Invoke());
    }




    bool TryUpdateStates(LabyrinthTile otherTile, LabyrinthTile tileWithSelectedState, List<TryUpdateAction> updateAdjacentCellsActions)
    {
        AddOrUpdateToMapCellCashe(tileWithSelectedState);

        int removeModuleCount = PossibleIds.RemoveAll(thisState =>
        {
            var directionToPreviusCell = otherTile.Coordinates - Coordinates;
           
            return !otherTile.PossibleIds.Any(otherState =>

                thisState.IsMatchingModules(otherState, directionToPreviusCell) &&
                thisState.DoExtraConditionsMatch(parent, this)

                );
        });

        if (PossibleIds.Count == 0)
            return false;

        if (removeModuleCount > 0)
            updateAdjacentCellsActions.Add(() => TryUpdateAdjacentTiles(tileWithSelectedState));

        return true;
    }


    public void ReverseStates(LabyrinthTile originallyUpdatedCell)
    {
        if (_mapTileCashe.ContainsKey(originallyUpdatedCell))
        {
            PossibleIds = new List<TileComponent>(_mapTileCashe[originallyUpdatedCell]);
            _mapTileCashe.Remove(originallyUpdatedCell);
            foreach (var cellPos in AdjacentTilePositions)
            {
               parent.GetTileAtCoord(cellPos).ReverseStates(originallyUpdatedCell);
            }
        }
    }



    void AddOrUpdateToMapCellCashe(LabyrinthTile originallyUpdatedTile)
    {
        if (_mapTileCashe.ContainsKey(originallyUpdatedTile)) _mapTileCashe[originallyUpdatedTile] = new List<TileComponent>(PossibleIds);
        else _mapTileCashe.Add(originallyUpdatedTile , new List<TileComponent>(PossibleIds));
    }


    public void CreateSelf()
    {
        var localPosition = new Vector3(Coordinates.x * parent._tileSize, 0, Coordinates.y * parent._tileSize);
      spawnedVersion =  GameObject.Instantiate(PossibleIds[0]._modulePrefab);

        spawnedVersion.transform.position = localPosition;
        spawnedVersion.transform.parent = parent.gameObject.transform;
    }

    public void CreateSelf(float tileSize, TileComponent forcedComponent, GameObject parent)
    {
        var localPosition = new Vector3(Coordinates.x * tileSize, 0, Coordinates.y * tileSize);
        spawnedVersion = GameObject.Instantiate(forcedComponent._modulePrefab);

        spawnedVersion.transform.position = localPosition;
        spawnedVersion.transform.parent = parent.gameObject.transform;
    }



    public bool TileComponentOverride(TileComponent tileComponent)
    {
        var states = new List<TileComponent>(PossibleIds);
        while (states.Count > 0)
        {
            var selectState = tileComponent;
            PossibleIds = new List<TileComponent>() { selectState };
            if (!TryUpdateAdjacentTiles(this))
            {
                states.Remove(selectState);
            }
            else return true;
        }
        return false;

    }


}




