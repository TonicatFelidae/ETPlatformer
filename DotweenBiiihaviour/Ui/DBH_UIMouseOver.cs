using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(RectTransform))]
public class DBH_UIMouseOver : DBHBase, IPointerEnterHandler, IPointerExitHandler
{
    public Transform target;
    Sequence sequence;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayDOTweenSequenceForceRestart(ref sequence, GenerateSequenceExample);
        //if (sequence != null) 
        //{ 
        //    if (!sequence.IsPlaying())
        //    {
        //        sequence.Restart();
        //    }
        //}
        //else
        //{
        //    sequence = DOTween.Sequence();
        //    sequence.SetAutoKill(false);    
        //    sequence.Append(target.DOPunchScale(new Vector3(1, 1, 1), 1f));
        //    sequence.Play();
        //}
        //if (!sequence.IsPlaying())
        //{
        //    sequence.Play();
        //}
    }
    public void GenerateSequenceExample(Sequence sequence)
    {
        // Add tweens to the sequence
        sequence.SetAutoKill(false);
        sequence.Append(target.DOPunchScale(new Vector3(1, 1, 1), 1f));
        // ... add more tweens as needed
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        //sequence = DOTween.Sequence();
        //sequence.Append(target.DOPunchScale(new Vector3(1, 1, 1), 1f));
        //sequence.Append(sequence.Pause());
        //sequence.SetLoops(-1);
    }

}
