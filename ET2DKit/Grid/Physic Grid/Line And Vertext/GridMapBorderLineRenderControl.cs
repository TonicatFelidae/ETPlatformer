using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using Core.Extension;
using ET.SupportKit.Collection;

/// <summary>
/// Use for PHYSIC grid map only
/// </summary>
namespace ET.GridSystem.PhysicGrid
{

    public class GridMapBorderLineRenderControl : MonoBehaviour
    {

        [NonSerialized] public LineRenderer mainLine;
        public Color startC0;
        public Color startC1;
        public Color startC2;
        public Color startC3;
        public float startCTime;
        public float startH0;
        public float startH1;
        private List<Vector3> curDatPoint = null;
        public float archorYstart;
        public float archorYend;
        private float archorYm;
        public float startHSpeed;
        public LineProtocol[] lineProtocols;
        private void Awake()
        {
            mainLine = GetComponent<LineRenderer>();
        }
        public void Active(bool enable, List<Vector3> datPoints = null, LinePresent linePresent = LinePresent.Normal)
        {
            if (enable && !IsListEquals(curDatPoint, datPoints))
            {
                curDatPoint = datPoints;
                gameObject.SetActive(true);
                mainLine.positionCount = datPoints.Count;
                mainLine.SetPositions(datPoints.ToArray());
                mainLine.DOColor(new Color2(startC0, startC1), new Color2(startC2, startC3), startCTime);
                archorYm = archorYstart;
                SetLinePresent(linePresent);

            }
            else if (!enable)
            {
                gameObject.SetActive(false);
            }
        }
        private void Update()
        {
            if (mainLine.positionCount > 0)
            {
                if (archorYm < archorYend)
                    archorYm += Time.deltaTime * startHSpeed;
                for (int i = 0; i < mainLine.positionCount; i++)
                {
                    Vector3 n = mainLine.GetPosition(i); n.y = archorYm;
                    mainLine.SetPosition(i, n);
                }

            }
        }
        void SetLinePresent(LinePresent linePresent)
        {
            LineProtocol po = lineProtocols.Find(x => x.linePresent == linePresent);
            mainLine.material = po.mat;
            mainLine.textureMode = po.textureMode;
        }
        bool IsListEquals<T>(List<T> l1, List<T> l2)
        {
            if (l1 == null || l2 == null) return false;
            if (l1.Count != l2.Count) return false;
            for (int i = 0; i < l1.Count; i++)
            {
                if (!l1[i].Equals(l2[i])) return false;
            }
            return true;
        }

        private void OnEnable()
        {
        }
        [Serializable]
        public struct LineProtocol
        {
            public LinePresent linePresent;
            public Material mat;
            public LineTextureMode textureMode;
        }
    }
    public enum LinePresent
    {
        Normal,
        Dot,
    }
}
