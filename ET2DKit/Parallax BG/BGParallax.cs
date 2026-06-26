using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.SupportKit;
using System;

public class BGParallax : MonoBehaviour
{
    private List<float> _starposX;
    private List<float> _starposY;
    [SerializeField] private List<BGParallaxItem> _parallaxItems;
    [SerializeField] private bool _horizontal = true;
    [SerializeField] private bool _vertical = false;
    public bool Horizontal
    {
        get => _horizontal;
        set
        {
            _horizontal = value;
            _starposX = new List<float>();
            for (int i = 0; i < _parallaxItems.Count; i++)
            {
                _starposX.Add(_parallaxItems[i].layer.position.x);
            }
        }
    }
    public bool Vertical
    {
        get => _vertical;
        set
        {
            _vertical = value;
            _starposY = new List<float>();
            for (int i = 0; i < _parallaxItems.Count; i++)
            {
                _starposY.Add(_parallaxItems[i].layer.position.y);
            }
        }
    }
    void Start()
    {
        Horizontal = _horizontal;
        Vertical = _vertical;
    }
    void FixedUpdate()
    {
        if (Horizontal)
        {
            for (int i = 0; i < _parallaxItems.Count; i++)
            {
                float dis = Camera.main.transform.position.x * _parallaxItems[i].speedX;
                _parallaxItems[i].layer.position = _parallaxItems[i].layer.position.Set_x(_starposX[i] + dis);
            }
        }
        if (Vertical)
        {
            for (int i = 0; i < _parallaxItems.Count; i++)
            {
                float dis = Camera.main.transform.position.y * _parallaxItems[i].speedY;
                _parallaxItems[i].layer.position = _parallaxItems[i].layer.position.Set_y(_starposY[i] + dis);
            }
        }

    }
    [Serializable]
    public struct BGParallaxItem
    {
        public Transform layer;
        [Range(0, 1)] public float speedX;
        [Range(0, 1)] public float speedY;
    }
}
