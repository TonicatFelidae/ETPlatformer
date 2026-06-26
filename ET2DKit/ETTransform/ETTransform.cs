using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET;
using ET.SupportKit;

/// <summary>
/// ETTransform solution for transform 2D object (not canvas UI) with anchor system
/// </summary>
[RequireComponent(typeof(Renderer))]
public class ETTransform : MonoBehaviour
{
    public AnchorTypeW2D anchorType;
    //
    public Transform anchorTransformObject;
    public ETTransform anchorETTransform;



    public Vector3 anchorWorldPosition;
    //
    public Vector2 pivot;
    public Vector2 pivotOffset;
    public bool ShowGizmo_objectFixedPos;
    public bool ShowGizmo_objectPivot;
    public PositionPresents PivotPositionPresents
    {
        get => pivotPositionPresents;
        set
        {
            pivotPositionPresents = value;
            AdjustPivotPosition();
        }
    }
    public PositionPresents pivotPositionPresents;
    Vector2[] all2DPivotPosition;
    public void AdjustPosition()
    {
        switch (anchorType)
        {
            case AnchorTypeW2D.World:
                transform.position = anchorWorldPosition - GetPivot().ToVector3(anchorWorldPosition);
                break;
            case AnchorTypeW2D.AtTransformPosition:
                transform.position = anchorTransformObject.position - GetPivot().ToVector3(anchorTransformObject.position);
                break;
            case AnchorTypeW2D.AtETTransformPivot:
                transform.position = anchorETTransform.GetPivot().ToVector3(transform.position) - GetPivot().ToVector3(transform.position);
                break;
            case AnchorTypeW2D.PointZero:
                transform.position = Vector3.zero - GetPivot();
                break;
            default:
                break;
        }
    }
    public void AdjustPivotPosition()
    {
        all2DPivotPosition = transform.GetAllPivotPositionOn2DObject();
        pivot =
            all2DPivotPosition[(int)pivotPositionPresents - 1].ToVector3(transform.position);
    }
    public Vector3 GetPivot()
    {
        return pivot.ToVector3(transform.position) + pivotOffset.ToVector3(transform.position);

    }
    public Vector3 GetPivotWorldPos()
    { 
        return pivot.ToVector3(transform.position) + pivotOffset.ToVector3(transform.position) + transform.position;

    }
    private void OnDrawGizmosSelected()
    {
        if (ShowGizmo_objectFixedPos)
        {
            Gizmos.color = Color.green;
            Vector2[] allBoundPositionOn2DObject = transform.GetAllBoundPositionOn2DObject();
            foreach (var item in allBoundPositionOn2DObject)
            {
                Gizmos.DrawWireSphere(item, 0.1f);
            }
        }
        if (ShowGizmo_objectPivot)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere((Vector3)GetPivotWorldPos(), 0.2f);
        }
    }
    
}
