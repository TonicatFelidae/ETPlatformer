using ET.GO.Behaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ET.GO.Behaviour
{
    /// <summary>
    /// Automatic determine 2D or 3D moverment
    /// DP:
    /// Chain of Responsibility
    /// </summary>
    [Serializable]
    public class Biiihaviour : IBiiihaviour //Nerver use sharing behavior
    {
        [NonSerialized] public float moveX; //spped moveRight
        [NonSerialized] public float moveY; //spped moveUp
        [NonSerialized] public bool isJump;
        public Transform transformx;
        public Transform gameObjectx;
        public MoveMethod moveMethod;   
        public Rigidbody2D Rigidbody2D
        {
            get
            {
                if (_rigidbody2D == null) _rigidbody2D = gameObjectx.GetComponent<Rigidbody2D>();
                return _rigidbody2D;
            }
        }
        private Rigidbody2D _rigidbody2D;
        public Rigidbody Rigidbody3D
        {
            get
            {
                if (_rigidbody == null) _rigidbody = gameObjectx.GetComponent<Rigidbody>();
                return _rigidbody;
            }
        }
        private Rigidbody _rigidbody;
        public RectTransform RectTransformObject
        {
            get
            {
                if (_rectTransformObject == null) _rectTransformObject = gameObjectx.GetComponent<RectTransform>();
                return _rectTransformObject;
            }
        }
        private RectTransform _rectTransformObject;
        protected float m_speed;
        protected float r_speed;
        public void Stop()
        {
            Debug.Log("Stop");
        }
        /// <summary>
        /// Set and bind gameObject to ninput
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObjectx"></param>
        /// <returns></returns>
        public virtual T Setup<T>(GameObject gameObjectx, MoveMethod moveMethod) where T : Biiihaviour
        {
            this.transformx = gameObjectx.transform;
            this.gameObjectx = gameObjectx.transform;
            this.moveMethod = moveMethod;
            return this as T;
        }
        #region Set
        public Biiihaviour SetSpeed(float speed)
        {
            this.m_speed = speed;
            return this;
        }
        public Biiihaviour SetRotationSpeed(float speed)
        {
            this.r_speed = speed;
            return this;
        }

        #endregion
        /// <summary>
        /// Require fix update to perform action
        /// </summary>
        public virtual void Update()
        {

        }
        /*
         * I don't know but this script is cute!
         */
        // Start is called before the first frame update
        //storagevariable

    }

    public enum MoveMethod
    {
        Ridgidbody,
        Translate,
    }

    public enum BHMoverment
    {
        None,
        Move,
    }
    public enum BHAction
    {
        None,
        Attack,
    }
}
public interface IBiiihaviour
{
   public abstract void Update();
}

