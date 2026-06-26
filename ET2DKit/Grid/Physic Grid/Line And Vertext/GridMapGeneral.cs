using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.GridSystem;
using ET.GridSystem.PhysicGrid;

/// <summary>
/// Generate physic grid map, each tile is GTile game object
/// </summary>
public class GridMapGeneral : MonoBehaviour
{
    Dictionary<Vector2Int, XTile> _mappingTiles;
    public int rows = 7;
    public int columns = 10;
    public int attackRange = 1;
    public Vector2 SizeTile;
    public Vector2 Space;
    public Vector2Int OriLoc;
    public GameObject pp_GTile;
    public GridMapBorderLineRenderControl render;
    private GridMapRenderHighLight _pointGenerator = new();
    private GridMapInitData _dataGenerator = new();
    public int[,] map;
    MovableAttackableTiles data;

    private void Start()
    {
        CreateTileGrid(pp_GTile);
        map = new int[columns, rows];
        data = _dataGenerator.GetMovableAttackableTiles(OriLoc, map, 100, attackRange);
        ApplyEvent();
    }
    public void CreateTileGrid(GameObject prefabTile)
    {
        var startX = -0.5f * (columns * SizeTile.x + (columns - 1) * Space.x) + SizeTile.x * 0.5f;
        var startY = 0.5f * (rows * SizeTile.y + (rows - 1) * Space.y) - SizeTile.x * 0.5f;

        _mappingTiles = new Dictionary<Vector2Int, XTile>();
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < columns; c++)
            {
                var tileObj = GameObject.Instantiate(prefabTile, this.transform);
                tileObj.transform.position = new Vector3(startX + c * (SizeTile.x + Space.x), 0.0001f, startY - r * (SizeTile.y + Space.y));

                var tile = tileObj.GetComponent<XTile>();
                tile.Setup(c, r);
                tile.SetTile(HighlightType.Normal);
                _mappingTiles.Add(new Vector2Int(c, r), tile);
            }
        }
    }
    void ApplyEvent()
    {
        foreach (var item in _mappingTiles)
        {
            item.Value.OnMouseEnterEvent.RemoveAllListeners();
            item.Value.OnMouseEnterEvent.AddListener(() =>
            {
                List<List<Vector3>> points = _pointGenerator.GetPointProtocolX(data.borderPerPanel[item.Value.loc], _mappingTiles);
                foreach (var pan in _mappingTiles)
                {
                    pan.Value.TurnAllPointNum(false);
                    if (data.borderPerPanel[item.Value.loc].Contains(pan.Value.loc))
                    {
                        if (data.borderPerPanel[item.Value.loc].IndexOf(pan.Value.loc) == 0)
                            pan.Value.SetTile(HighlightType.OpponentRange);
                        else if (data.borderPerPanel[item.Value.loc].IndexOf(pan.Value.loc) == 1)
                            pan.Value.SetTile(HighlightType.CastSkill);
                        else if (data.borderPerPanel[item.Value.loc].IndexOf(pan.Value.loc) == 2)
                            pan.Value.SetTile(HighlightType.Attackable);
                        else pan.Value.SetTile(HighlightType.Moveable);
                    }
                    else
                    {
                        pan.Value.SetTile(HighlightType.Normal);

                    }
                }
                for (int i = 0; i < points[0].Count; i++)
                {
                    foreach (var item in _mappingTiles)
                    {
                        for (int j = 0; j < item.Value.points.Length; j++)
                        {
                            if (item.Value.points[j].position == points[0][i])
                            {
                                item.Value.SetPointNum(j, i);
                            }
                        }
                    }

                }
                render.Active(true, points[0]);
            }
            );
        }
    }


}
