using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DBHBase : MonoBehaviour
{
    public Sequence sequence01;
    public Sequence sequence02;
    public Sequence sequence03;
    public Sequence sequence04;
    public Sequence sequence05;
    public bool IsABuildinSequencePlaying()
    {
        bool b1 = sequence01 != null && sequence01.IsPlaying();
        bool b2 = sequence02 != null && sequence02.IsPlaying();
        bool b3 = sequence03 != null && sequence03.IsPlaying();
        bool b4 = sequence04 != null && sequence04.IsPlaying();
        bool b5 = sequence05 != null && sequence05.IsPlaying();
        if (b1 && b2 && b3 && b4 && b5)
        {
            return true;
        }
        return false;
    }
        ///// <summary>
        ///// sequence linked together in a dictionary (not pool),with ID
        ///// </summary>
        //Dictionary<int,Sequence> linkedSequences = new();
        //public Sequence GetSequence(int ID)
        //{
        //    if (!linkedSequences.ContainsKey(ID)) linkedSequences.Add(ID, null);
        //    return linkedSequences[ID];    
        //}
        /// <summary>
        /// Wait for sequence finish before doing it.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="sequenceGenerateMethod"></param>
        public void PlayDOTweenSequence(ref Sequence sequence, Action<Sequence> sequenceGenerateMethod)
    {
        if (sequence != null)
        {
            if (!sequence.IsPlaying())
            {
                sequence.Restart();
            }
        }
        else
        {
            sequence = DOTween.Sequence();
            sequence.SetAutoKill(false);
            sequenceGenerateMethod.Invoke(sequence);
        }
    }
    /// <summary>
    /// Restart sequense before play everytime even trigger it
    /// </summary>
    /// <param name="sequence"></param>
    /// <param name="sequenceGenerateMethod"></param>
    public void PlayDOTweenSequenceForceRestart(ref Sequence sequence, Action<Sequence> sequenceGenerateMethod)
    {
        if (sequence != null)
        {

            sequence.Restart();
        }
        else
        {
            sequence = DOTween.Sequence();
            sequence.SetAutoKill(false);
            sequenceGenerateMethod.Invoke(sequence);
        }
    }
}
