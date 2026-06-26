using ET.SupportKit.Collection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.GridSystem.PhysicGrid
{
    public class GridMapRenderHighLight
    {
        public List<List<Vector3>> GetPointProtocolX(List<Vector2Int> border, Dictionary<Vector2Int, XTile> physicGTiles)
        {
            List<Vector2Int> buffer = new() { border[0] };
            List<OrderAndPos> order = GetNearbyPannelProtocolX(border[0], buffer, border, 0, 0);
            return ConstructPointsGroup(order, border, physicGTiles);
        }
        List<OrderAndPos> GetNearbyPannelProtocolX(Vector2Int panel, List<Vector2Int> buffer, List<Vector2Int> border, int counter, int sweaperLocation)
        {
            List<OrderAndPos> ret = new List<OrderAndPos>();
            OrderAndPos nextPanel = new OrderAndPos(0, PositionPresents.None);

            if (counter < border.Count - 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    nextPanel = GetNextPannelProtocolX(panel, IncreaseSweaper(sweaperLocation, i), buffer, border);
                    if (nextPanel.position != PositionPresents.None)
                    {
                        buffer.Add(border[nextPanel.index]);
                        sweaperLocation = IncreaseSweaper(sweaperLocation, i);
                        break;
                    }
                }
                if (nextPanel.position != PositionPresents.None)
                {
                    counter += 1;
                    ret = GetNearbyPannelProtocolX(border[nextPanel.index], buffer, border, counter, sweaperLocation);
                    ret.Add(nextPanel);
                    return ret;
                }
                else
                {
                    Debug.Log("[HL] loop recursive not close case");
                    return ret;
                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    nextPanel = GetNextPannelProtocolX(panel, IncreaseSweaper(sweaperLocation, i), buffer, border, true);
                    if (nextPanel.position != PositionPresents.None && nextPanel.index == border.IndexOf(buffer[0]))
                    {
                        ret.Add(nextPanel);
                        return ret;
                    }
                }
                return ret;
            }
            int IncreaseSweaper(int sweaper, int i)
            {
                int val = sweaper + i;
                return val >= 8 ? val - 8 : val;
            }
        }
        OrderAndPos GetNextPannelProtocolX(Vector2Int panel, int ID, List<Vector2Int> buffer, List<Vector2Int> border, bool getOri = false)
        {
            OrderAndPos ret = new OrderAndPos();
            Vector2Int ori = panel;
            Vector2Int loc = panel;
            switch (ID)
            {
                case 0:
                    loc = new Vector2Int(ori.x, ori.y - 1);
                    ret.position = PositionPresents.TopCenter;
                    break;
                case 1:
                    loc = new Vector2Int(ori.x + 1, ori.y - 1);
                    ret.position = PositionPresents.TopRight;
                    break;
                case 2:
                    loc = new Vector2Int(ori.x + 1, ori.y);
                    ret.position = PositionPresents.MiddleRight;
                    break;
                case 3:
                    loc = new Vector2Int(ori.x + 1, ori.y + 1);
                    ret.position = PositionPresents.BottomRight;
                    break;
                case 4:
                    loc = new Vector2Int(ori.x, ori.y + 1);
                    ret.position = PositionPresents.BottomCenter;
                    break;
                case 5:
                    loc = new Vector2Int(ori.x - 1, ori.y + 1);
                    ret.position = PositionPresents.BottomLeft;
                    break;
                case 6:
                    loc = new Vector2Int(ori.x - 1, ori.y);
                    ret.position = PositionPresents.MiddleLeft;
                    break;
                case 7:
                    loc = new Vector2Int(ori.x - 1, ori.y - 1);
                    ret.position = PositionPresents.TopLeft;
                    break;
            }
            if (getOri)
            {
                if (buffer[0] == loc)
                {
                    ret.index = border.IndexOf(loc);
                    return ret;
                }
                else
                {
                    ret.position = PositionPresents.None;
                    return ret;
                }
            }
            else
            {
                if (border.Contains(loc) && !buffer.Contains(loc))
                {
                    ret.index = border.IndexOf(loc);
                    return ret;
                }
                else
                {
                    ret.position = PositionPresents.None;
                    return ret;
                }
            }

        }
        #region PointSorter

        public List<List<Vector3>> GetPoint(List<Vector2Int> border, Dictionary<Vector2Int, XTile> physicGTiles)
        {
            List<Vector2Int> buffer = new() { border[0] };
            List<OrderAndPos> order = GetNearbyPannel(border[0], buffer, border, 0);
            foreach (var item in order)
            {
                Debug.Log(item.position);
            }
            return ConstructPointsGroup(order, border, physicGTiles);
        }
        List<OrderAndPos> GetNearbyPannel(Vector2Int panel, List<Vector2Int> buffer, List<Vector2Int> border, int counter)
        {
            List<OrderAndPos> ret = new List<OrderAndPos>();
            OrderAndPos nextPanel = new OrderAndPos(0, PositionPresents.None);
            if (counter < border.Count - 1)
            {
                List<OrderAndPos> trial = new();
                List<OrderAndPos> trialExist = new();
                for (int i = 0; i < 8; i++)
                {
                    bool gotExistance = false;
                    OrderAndPos testPanel = GetNextPannel(panel, i, buffer, border, ref gotExistance);
                    if (testPanel.position == PositionPresents.None)
                    {
                        if (gotExistance) trialExist.Add(testPanel);
                    }
                    else trial.Add(testPanel);
                }
                if (trial.Count > 0)
                {
                    //if (buffer.Count == 1 )
                    //{
                    //    nextPanel = GetNextPannelMultiCase(trial, trialExist, border, panel);
                    //}
                    //else
                    //{
                    nextPanel = GetNextPannelMultiCase(trial, trialExist, border, panel);
                    //}
                    buffer.Add(border[nextPanel.index]);
                }
                if (nextPanel.position != PositionPresents.None)
                {
                    counter += 1;
                    ret = GetNearbyPannel(border[nextPanel.index], buffer, border, counter);
                    ret.Add(nextPanel);
                    return ret;
                }
                else
                {
                    Debug.Log("[HL] loop recursive not close case");
                    return ret;
                }
            }
            else
            {
                bool throwGotExistance = false;
                for (int i = 0; i < 8; i++)
                {
                    nextPanel = GetNextPannel(panel, i, buffer, border, ref throwGotExistance, true);
                    if (nextPanel.position != PositionPresents.None && nextPanel.index == border.IndexOf(buffer[0]))
                    {
                        ret.Add(nextPanel);
                        return ret;
                    }
                }
                return ret;
            }
        }

        OrderAndPos GetNextPannel(Vector2Int panel, int ID, List<Vector2Int> buffer, List<Vector2Int> border, ref bool gotExistance, bool getOri = false)
        {
            OrderAndPos ret = new OrderAndPos();
            Vector2Int ori = panel;
            Vector2Int loc = panel;
            switch (ID)
            {
                case 0:
                    loc = new Vector2Int(ori.x, ori.y - 1);
                    ret.position = PositionPresents.TopCenter;
                    break;
                case 1:
                    loc = new Vector2Int(ori.x - 1, ori.y);
                    ret.position = PositionPresents.MiddleLeft;
                    break;
                case 2:
                    loc = new Vector2Int(ori.x + 1, ori.y);
                    ret.position = PositionPresents.MiddleRight;
                    break;
                case 3:
                    loc = new Vector2Int(ori.x, ori.y + 1);
                    ret.position = PositionPresents.BottomCenter;
                    break;
                case 4:
                    loc = new Vector2Int(ori.x - 1, ori.y - 1);
                    ret.position = PositionPresents.TopLeft;
                    break;
                case 5:
                    loc = new Vector2Int(ori.x + 1, ori.y - 1);
                    ret.position = PositionPresents.TopRight;
                    break;
                case 6:
                    loc = new Vector2Int(ori.x - 1, ori.y + 1);
                    ret.position = PositionPresents.BottomLeft;
                    break;
                case 7:
                    loc = new Vector2Int(ori.x + 1, ori.y + 1);
                    ret.position = PositionPresents.BottomRight;
                    break;
            }
            if (getOri)
            {
                if (buffer[0] == loc)
                {
                    ret.index = border.IndexOf(loc);
                    return ret;
                }
                else
                {
                    ret.position = PositionPresents.None;
                    return ret;
                }
            }
            else
            {
                if (border.Contains(loc) && !buffer.Contains(loc))
                {
                    ret.index = border.IndexOf(loc);
                    return ret;
                }
                else
                {
                    if (buffer.Contains(loc)) gotExistance = true;
                    ret.position = PositionPresents.None;
                    return ret;
                }
            }

        }
        OrderAndPos GetNextPannelMultiCase(List<OrderAndPos> found, List<OrderAndPos> exist, List<Vector2Int> border, Vector2Int panel)
        {
            if (found.Count == 1)
            {
                return found[0];
            }
            else if (found.Count == 2)
            {
                if (isOppXY(found[0], found[1]) && isOppXY(found[1], found[0]))
                {
                    //if (exist.Count>0 && border[exist[0].index].y< panel.y && border[exist[0].index].x == panel.x) return found[1];
                    return found[0];
                }
                else if (isNextToH(found[0], found[1]) || isNextToV(found[0], found[1]))
                {
                    if (isNextToV(found[0], found[1]))
                    {
                        if (border[found[0].index].x > panel.x)
                        {
                            if (isIncreaseY(found[0], found[1]))
                                return found[0];
                            else return found[1];
                        }
                        else
                        {
                            if (isIncreaseY(found[0], found[1]))
                                return found[1];
                            else return found[0];
                        }
                    }
                    else
                    {
                        if (border[found[0].index].y > panel.y)
                        {

                            if (isIncreaseX(found[0], found[1]))
                                return found[1];
                            else return found[0];
                        }
                        else
                        {

                            if (isIncreaseX(found[0], found[1]))
                                return found[0];
                            else return found[1];
                        }

                    }
                }
                else if (((Mathf.Abs(border[found[0].index].x - border[found[1].index].x) != 1))
                    || ((Mathf.Abs(border[found[0].index].y - border[found[1].index].y) != 1))
                    )
                {
                    return found[0];
                }
                //else if ((Mathf.Abs(border[found[0].index].x - border[found[1].index].x) == 1)
                //    && (border[found[0].index].y == border[found[1].index].y)
                //    ||
                //    (Mathf.Abs(border[found[0].index].y - border[found[1].index].y) == 1)
                //    && (border[found[0].index].x == border[found[1].index].x))
                //{
                //    return found[1];
                //}
                else
                {
                    return found[0];

                }
            }
            else if (found.Count == 3)
            {
                bool caseS1 = isSameX(found[0], found[1])
                    && isSameX(found[1], found[2]);
                bool caseS2 = isSameY(found[0], found[1])
                    && isSameY(found[1], found[2]);
                if (caseS1 || caseS2)//1110000000
                {
                    if (exist.Count > 0)
                    {
                        if (caseS2 && !isSameX(exist[0], found[0]))
                        {
                            if (!isSameX(exist[0], found[1])) return found[1];
                            if (!isSameX(exist[0], found[2])) return found[2];
                        }
                        if (caseS1 && !isSameY(exist[0], found[0]))
                        {
                            if (!isSameY(exist[0], found[1])) return found[1];
                            if (!isSameY(exist[0], found[2])) return found[2];
                        }
                    }

                    return found[1];
                }
                else
                {
                    return found[0];
                }
            }
            else
            {
                Debug.Log("[HL] out of case");
                return found[0];
            }
            bool isSameX(OrderAndPos i1, OrderAndPos i2) => border[i1.index].x == border[i2.index].x;
            bool isSameY(OrderAndPos i1, OrderAndPos i2) => border[i1.index].y == border[i2.index].y;
            bool isOppXY(OrderAndPos i1, OrderAndPos i2) => Mathf.Abs(border[i1.index].x - border[i2.index].y) == 1;
            bool isNextToV(OrderAndPos i1, OrderAndPos i2) => isSameX(i1, i2) && Mathf.Abs(border[i1.index].y - border[i2.index].y) == 1;
            bool isNextToH(OrderAndPos i1, OrderAndPos i2) => isSameY(i1, i2) && Mathf.Abs(border[i1.index].x - border[i2.index].x) == 1;
            bool isIncreaseX(OrderAndPos i1, OrderAndPos i2) => border[i1.index].x < border[i2.index].x;
            bool isIncreaseY(OrderAndPos i1, OrderAndPos i2) => border[i1.index].y < border[i2.index].y;
        }
        OrderAndPos GetNextPannelMultiCaseFirstTime(Vector2Int panel, List<OrderAndPos> found, List<Vector2Int> border)
        {
            if (found.Count == 2)
            {
                if (border[found[0].index].y == panel.y && border[found[1].index].y != panel.y)
                {
                    return found[1];
                }
                return found[0];
            }
            else
            {
                return found[0];
                //bool caseS1 = (border[found[0].index].x == border[found[1].index].x)
                //    && (border[found[0].index].x == border[found[2].index].x);
                //bool caseS2 = (border[found[0].index].y == border[found[1].index].y)
                //    && (border[found[0].index].y == border[found[2].index].y);
                //
            }
            //else Debug.Log("[HL] out of case first time");

        }
        List<List<Vector3>> ConstructPointsGroup(List<OrderAndPos> dat, List<Vector2Int> border, Dictionary<Vector2Int, XTile> physicGTiles)
        {
            List<Vector3> list1 = new();
            List<Vector3> list2 = new();
            for (int i = 0; i < dat.Count; i++)
            {
                List<int> usedPoint = new() { 0, 1, 2, 3 };
                OrderAndPos val1 = dat[i - 1 < 0 ? dat.Count - 1 : i - 1];
                OrderAndPos val2 = dat[i];
                List<Vector3> localPoints = new();
                for (int j = 0; j < 4; j++)
                {
                    localPoints.Add(physicGTiles[border[val2.index]].points[j].position);
                }
                switch (val1.position)
                {
                    case PositionPresents.TopLeft:
                        list1.TryAdd(localPoints[3]);
                        list2.TryAdd(localPoints[3]);
                        usedPoint.Remove(3);
                        break;
                    case PositionPresents.TopCenter:
                        list1.TryAdd(localPoints[0]);
                        list2.TryAdd(localPoints[3]);
                        usedPoint.Remove(0);
                        usedPoint.Remove(3);
                        break;
                    case PositionPresents.TopRight:
                        list1.TryAdd(localPoints[0]);
                        list2.TryAdd(localPoints[0]);
                        usedPoint.Remove(0);
                        break;
                    case PositionPresents.MiddleLeft:
                        list1.TryAdd(localPoints[3]);
                        list2.TryAdd(localPoints[2]);
                        usedPoint.Remove(3);
                        usedPoint.Remove(2);
                        break;
                    case PositionPresents.MiddleRight:
                        list1.TryAdd(localPoints[1]);
                        list2.TryAdd(localPoints[0]);
                        usedPoint.Remove(1);
                        usedPoint.Remove(0);
                        break;
                    case PositionPresents.BottomLeft:
                        list1.TryAdd(localPoints[2]);
                        list2.TryAdd(localPoints[2]);
                        usedPoint.Remove(2);
                        break;
                    case PositionPresents.BottomCenter:
                        list1.TryAdd(localPoints[2]);
                        list2.TryAdd(localPoints[1]);
                        usedPoint.Remove(2);
                        usedPoint.Remove(1);
                        break;
                    case PositionPresents.BottomRight:
                        list1.TryAdd(localPoints[1]);
                        list2.TryAdd(localPoints[1]);
                        usedPoint.Remove(1);
                        break;
                    default:
                        break;
                }
                switch (val2.position)
                {
                    case PositionPresents.TopLeft:
                        RemoveAllCase1(usedPoint, localPoints, 1);
                        AddWithCase1(usedPoint, localPoints, 0, 3);
                        AddWithCase1(usedPoint, localPoints, 2, 3, true);
                        break;
                    case PositionPresents.TopCenter:
                        AddWithCase2(usedPoint, localPoints, 2, 1, 3, 0);
                        break;
                    case PositionPresents.TopRight:
                        RemoveAllCase1(usedPoint, localPoints, 2);
                        AddWithCase1(usedPoint, localPoints, 1, 0);
                        AddWithCase1(usedPoint, localPoints, 3, 0, true);
                        break;
                    case PositionPresents.MiddleLeft:
                        AddWithCase2(usedPoint, localPoints, 1, 0, 2, 3);
                        break;
                    case PositionPresents.MiddleRight:
                        AddWithCase2(usedPoint, localPoints, 3, 2, 0, 1);
                        break;
                    case PositionPresents.BottomLeft:
                        RemoveAllCase1(usedPoint, localPoints, 0);
                        AddWithCase1(usedPoint, localPoints, 3, 2);
                        AddWithCase1(usedPoint, localPoints, 1, 2, true);
                        break;
                    case PositionPresents.BottomCenter:
                        AddWithCase2(usedPoint, localPoints, 0, 3, 1, 2);
                        break;
                    case PositionPresents.BottomRight:
                        RemoveAllCase1(usedPoint, localPoints, 3);
                        AddWithCase1(usedPoint, localPoints, 2, 1);
                        AddWithCase1(usedPoint, localPoints, 0, 1, true);
                        break;
                    default:
                        break;
                }

            }
            List<List<Vector3>> ret = new List<List<Vector3>>();
            if (list1.Count > list2.Count)
            {
                ret.Add(list1);
                ret.Add(list2);
            }
            else
            {
                ret.Add(list2);
                ret.Add(list1);
            }
            return ret;
            void AddWithCase1(List<int> usedPoint, List<Vector3> localPoints, int sideVal, int oppVal, bool isList2 = false)
            {
                if (!isList2)
                {
                    if (usedPoint.Contains(sideVal)) if (usedPoint.Contains(oppVal)) { list1.TryAdd(localPoints[oppVal]); list1.TryAdd(localPoints[sideVal]); } else list1.TryAdd(localPoints[sideVal]);
                }
                else
                {
                    if (usedPoint.Contains(sideVal)) if (usedPoint.Contains(oppVal)) { list2.TryAdd(localPoints[oppVal]); list2.TryAdd(localPoints[sideVal]); } else list2.TryAdd(localPoints[sideVal]);
                }


            }
            void AddWithCase2(List<int> usedPoint, List<Vector3> localPoints, int l1Val, int l2Val, int l1x, int l2x)
            {

                if (usedPoint.Count < 3) // 0,1,2
                {
                    RemoveAllCase1(usedPoint, localPoints, l1Val);
                    RemoveAllCase1(usedPoint, localPoints, l2Val);
                    if (usedPoint.Contains(l1x)) list2.TryAdd(localPoints[l1x]);
                    if (usedPoint.Contains(l2x)) list1.TryAdd(localPoints[l2x]);

                }
                else if (usedPoint.Contains(l1Val)) //3,4
                {
                    RemoveAllCase1(usedPoint, localPoints, l2Val);
                    list2.TryAdd(localPoints[l2x]);
                    list2.TryAdd(localPoints[l1x]);
                }
                else
                {
                    RemoveAllCase1(usedPoint, localPoints, l1Val);
                    list1.TryAdd(localPoints[l1x]);
                    list1.TryAdd(localPoints[l2x]);
                }


            }
            void RemoveAllCase1(List<int> usedPoint, List<Vector3> localPoints, int removeVal)
            {

                usedPoint.Remove(removeVal);
                //list1.Remove(localPoints[removeVal]);
                //list2.Remove(localPoints[removeVal]);
            }
        }
        #endregion
    }
}


