using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DBH poppypop
/// </summary>
public class DBH_UIPoppy : MonoBehaviour
{
    public Transform target;
    [Range(1f, 5)]
    public float intensityGrowBig = 1;
    [Range(0.1f, 5)]
    public float lenghtGrowBig = 1;
    [Range(0.1f, 5)]
    public float intensityPopDown = 1;
    [Range(0.1f, 5)]
    public float lenghtPopDown = 1;
    [Range(0.1f, 5)]
    public float intensityPopUpABit = 1;
    [Range(0.1f,5)]
    public float lenghtPopUpABit = 1;
    public void GrowBig(Sequence sequence)
    {
        sequence.Append(target.DOScale((1 + 0.1f * intensityGrowBig), 0.2f* lenghtGrowBig));
    }
    public void ReturnToForm(Sequence sequence)
    {
        sequence.Append(target.DOScale((1-0.2f* intensityPopDown) *Vector3.one, 0.2f* lenghtPopDown));
        sequence.Append(target.DOScale((1 + 0.1f * intensityPopUpABit) * Vector3.one, 0.1f* lenghtPopUpABit));
        sequence.Append(target.DOScale(1f * Vector3.one, 0.05f* lenghtPopUpABit));
    }
}
