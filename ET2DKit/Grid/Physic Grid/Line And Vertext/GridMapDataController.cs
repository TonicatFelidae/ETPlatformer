using ET.SupportKit.Collection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.GridSystem;

namespace ET.GridSystem.PhysicGrid
{
    /// <summary>
    /// Generate rule: 
    /// [OriMap]
    /// -1 ignoreObstacle cant move
    /// [MoveMap]
    /// 0,1,2,3,.. can move also distance
    /// 9999 veryfar
    /// -1 ignoreObstacle cant move
    /// -2 emptyslot that can move
    /// </summary>
    public class GridMapInitData
    {
        public MovableAttackableTiles GetMovableAttackableTiles(Vector2Int heroTile, int[,] map, int moveRange, int attackRange)
        {
            //if (GridMap.useTestRange) moveRange = GridMap.testRange;
            MovableAttackableTiles ret = new MovableAttackableTiles();
            ret.Init();
            //moveable
            var result = new List<Vector2Int>();
            var distances = GenerateMoveMap(heroTile, map);
            var w = map.GetLength(0);
            var h = map.GetLength(1);
            for (var x = 0; x < w; x++)
            {
                for (var y = 0; y < h; y++)
                {
                    if (distances[x, y] <= moveRange + 1 && map[x, y] == 0)
                        result.Add(new Vector2Int(x, y));
                }
            }
            ret.moveable = result;
            //attackable
            result = new List<Vector2Int>();
            int[,] mapMod = GenMapMod();
            //if (GridMap.currentRangeHighlightOnly)
            //{
            GetCurrentRangeProtocol();
            //}
            //else
            //{
            //    GetAllPossipleRangeProtocol();
            //}
            //border
            ret.border = GenBorder(ret.attackable, true);
            foreach (var item in ret.attackablePerPanel)
            {
                ret.borderPerPanel.Add(item.Key, GenBorder(item.Value, false, item.Key));
            }
            return ret;
            List<Vector2Int> GenBorder(List<Vector2Int> input, bool useMapMod, Vector2Int ori = new())
            {
                List<Vector2Int> retxx = new();
                for (int i = 0; i < input.Count; i++)
                {
                    if (useMapMod) distances = GenerateMoveMap(input[i], mapMod);
                    else distances = GenerateMoveMap(input[i], ret.mapmodPerPanel[ori]);

                    int cur_x = input[i].x;
                    int cur_y = input[i].y;
                    if (cur_x == 0 || cur_y == 0 || cur_x == w - 1 || cur_y == h - 1)
                    {
                        retxx.TryAdd(input[i]);
                    }
                    else
                    {
                        for (var x = 0; x < w; x++)
                        {
                            for (var y = 0; y < h; y++)
                            {
                                if (useMapMod)
                                {
                                    if (distances[x, y] <= 2 && mapMod[x, y] == 0)
                                    {
                                        retxx.TryAdd(input[i]);
                                        break;
                                    }

                                }
                                else
                                {
                                    if (distances[x, y] <= 2 && (ret.mapmodPerPanel[ori])[x, y] == 0)
                                    {
                                        retxx.TryAdd(input[i]);
                                        break;
                                    }

                                }
                            }
                        }
                    }

                }
                return retxx;
            }
            void GetAllPossipleRangeProtocol()
            {

                for (int i = 0; i < ret.moveable.Count; i++)
                {
                    int[,] localMapMod = GenMapMod();
                    List<Vector2Int> localResult = new List<Vector2Int>();
                    distances = GenerateMoveMap(ret.moveable[i], map, true);
                    for (var x = 0; x < w; x++)
                    {
                        for (var y = 0; y < h; y++)
                        {
                            if (distances[x, y] <= attackRange + 1)
                            {
                                result.TryAdd(new Vector2Int(x, y));
                                localResult.TryAdd(new Vector2Int(x, y));
                                mapMod[x, y] = -1;
                                localMapMod[x, y] = -1;
                            }
                        }
                    }
                    ret.attackablePerPanel.Add(ret.moveable[i], localResult);
                    ret.mapmodPerPanel.Add(ret.moveable[i], localMapMod);
                }
                ret.attackable = result;
            }
            void GetCurrentRangeProtocol()
            {
                distances = GenerateMoveMap(heroTile, mapMod, true);
                for (var x = 0; x < w; x++)
                {
                    for (var y = 0; y < h; y++)
                    {
                        if (distances[x, y] <= attackRange + 1)
                        {
                            result.TryAdd(new Vector2Int(x, y));
                            mapMod[x, y] = -1;
                        }
                    }
                }
                ret.attackable = result;
                for (int i = 0; i < ret.moveable.Count; i++)
                {
                    int[,] localMapMod = GenMapMod();
                    List<Vector2Int> localResult = new List<Vector2Int>();
                    distances = GenerateMoveMap(ret.moveable[i], localMapMod, true);
                    for (var x = 0; x < w; x++)
                    {
                        for (var y = 0; y < h; y++)
                        {
                            if (distances[x, y] <= attackRange + 1)
                            {
                                localResult.TryAdd(new Vector2Int(x, y));
                                localMapMod[x, y] = -1;
                            }
                        }
                    }
                    ret.attackablePerPanel.Add(ret.moveable[i], localResult);
                    ret.mapmodPerPanel.Add(ret.moveable[i], localMapMod);
                }
            }
            int[,] GenMapMod()
            {
                return new int[w, h];
            }

        }
        public int[,] GenerateMoveMap(Vector2Int ori, int[,] map, bool ignoreObstacle = false)
        {
            int w = map.GetLength(0);
            int h = map.GetLength(1);
            var distances = new int[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    distances[i, j] = 10000;
                }
            }
            var dx = new int[] { 0, 1, 0, -1 };
            var dy = new int[] { 1, 0, -1, 0 };
            var q = new Queue<Vector2Int>();
            q.Enqueue(new Vector2Int(ori.x, ori.y));
            distances[ori.x, ori.y] = 1;
            while (q.Count > 0)
            {
                var curTile = q.Dequeue();
                for (var i = 0; i < 4; i++)
                {
                    var nx = curTile.x + dx[i];
                    var ny = curTile.y + dy[i];

                    if (nx >= 0 && nx < w && ny >= 0 && ny < h)
                    {
                        if (map[nx, ny] == -1 && !ignoreObstacle) distances[nx, ny] = 10000;
                        else
                        {
                            if (distances[nx, ny] == 10000)
                            {
                                distances[nx, ny] = distances[curTile.x, curTile.y] + 1;
                                q.Enqueue(new Vector2Int(nx, ny));
                            }
                        }
                    }
                }
            }

            return distances;
        }
    }
}

