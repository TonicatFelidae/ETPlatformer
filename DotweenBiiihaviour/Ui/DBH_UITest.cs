using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DBH_UITest : DBHBase, IPointerEnterHandler, IPointerExitHandler
{
    public DBH_UIPoppy dBH_UIPoppy;
    Sequence _sequence1;
    Sequence _sequence2;
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayDOTweenSequence(ref _sequence1, dBH_UIPoppy.GrowBig);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayDOTweenSequence(ref _sequence2, dBH_UIPoppy.ReturnToForm);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
