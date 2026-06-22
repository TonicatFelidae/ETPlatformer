using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBHTest : DBHBase
{
    Sequence sequence;
    Sequence sequence2;
    public DBH_UIPoppy dBH_UIPoppy;
    private void OnMouseEnter()
    {
        if(!IsABuildinSequencePlaying())
        {

            PlayDOTweenSequenceForceRestart(ref sequence01, dBH_UIPoppy.GrowBig);
        }

    }
    private void OnMouseExit()
    {
        if (!IsABuildinSequencePlaying())
        {

            PlayDOTweenSequenceForceRestart(ref sequence02, dBH_UIPoppy.ReturnToForm);
        }

    }
}
