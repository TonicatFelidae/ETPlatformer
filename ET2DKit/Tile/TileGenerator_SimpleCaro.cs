using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TileGenerator_SimpleCaro : MonoBehaviour
{
    public Dat_Rectangle_2D map_size;
    public bool start_zero;
    public List<Tile> list_tile;
    List<bool> state_grid = new List<bool>();
    public Tilemap tileMap;
    // Start is called before the first frame update
    void OnEnable()
    {
        Generate_tile_map();
    }
    public void Generate_tile_map()
    {
        state_grid = new List<bool>();
        if (start_zero)
        {
            state_grid.Add(false); state_grid.Add(false);
        }
        else
        {
            state_grid.Add(true); state_grid.Add(true);
        }
        int mapsize_x = map_size.maxX - map_size.minX;
        int mapsize_y = map_size.maxY - map_size.minY;
        for (int i = map_size.minX; i < map_size.maxX; i++)
        {
            for (int j = map_size.minY; j < map_size.maxY; j++)
            {
                tileMap.SetTile(new Vector3Int(i,j,0), list_tile[state_grid[1]?1:0]);
                state_grid[1] = state_grid[1] ? false : true;
            }
            state_grid[0] = state_grid[0] ? false : true;
            state_grid[1] = state_grid[0];
        }
    }

}
[Serializable]
public struct Dat_Rectangle_2D
{
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;
}
