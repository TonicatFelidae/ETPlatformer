
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ET.GO.Behaviour
{
    public class BH_Move : Biiihaviour
    {
        //acceable
        public void ResetInput()
        {
            moveX = 0;
            moveY = 0;
        }
        public virtual void MoveLeft()
        {
            moveX += -1;
        }
        public virtual void MoveRight()
        {
            moveX += 1;
        }
        public virtual void MoveUp()
        {
            moveY += 1;
        }
        public virtual void MoveDown()
        {
            moveY += -1;
        }
        private Vector2 jumpDirection = Vector2.up;
        private float jumpPower = 1.0f;

        public virtual void Jump()
        {
            Jump(Vector2.up, 1.0f);
        }

        public virtual void Jump(Vector2 direction)
        {
            Jump(direction, 1.0f);
        }

        public virtual void Jump(Vector2 direction, float power)
        {
            isJump = true;
            jumpDirection = direction.normalized;
            jumpPower = Mathf.Max(power, 0.1f);
        }

        //hide function logic


        public virtual void MoveHorizontal(float direction)
        {
            Rigidbody2D.MovePosition(Rigidbody2D.position + direction * Vector2.right * m_speed * Time.fixedDeltaTime);
        }
        public virtual void MoveVertical(float direction)
        {
            Rigidbody2D.MovePosition(Rigidbody2D.position + direction * Vector2.up * m_speed * Time.fixedDeltaTime);
        }
        public virtual void MoveFourWay(float x, float y)
        {
            Vector2 dir = new Vector2(x, y);
            if (x != 0 && y != 0) dir = dir.normalized; // performance normolize
            switch (moveMethod)
            {
                case MoveMethod.Ridgidbody:
                    Rigidbody2D.MovePosition(Rigidbody2D.position + dir * m_speed * Time.fixedDeltaTime);
                    break;
                case MoveMethod.Translate:
                    transformx.Translate(dir * Time.deltaTime * Vector2.right * m_speed);
                    break;
                default:
                    break;
            }
        }
        public virtual void MoveToPoint2DLeftRight(int direction)
        {
            MoveFourWay(direction, 0);
        }
        private void act_MoveLeft()
        {
            MoveHorizontal(-1);
        }
        private void act_MoveRight()
        {
            MoveHorizontal(1);
        }
        private void act_MoveUp()
        {
            MoveVertical(1);
        }
        private void act_MoveDown()
        {
            MoveVertical(-1);
        }
        private void act_Jump()
        {
            Rigidbody2D.AddForce(jumpDirection * 10 * jumpPower, ForceMode2D.Impulse);
            jumpDirection = Vector2.up;
            jumpPower = 1.0f;
            Debug.Log("jump");
        }
        //update
        /// <summary>
        /// Put in fix update if the move is physic
        /// </summary>
        public override void Update()
        {
            if (moveX != 0 || moveY != 0) MoveFourWay(moveX, moveY);
            if (isJump) act_Jump();
            moveX = 0;
            moveY = 0;
            isJump = false;
        }

    } // move platform horizon
    public class BH_MoveTopDown : Biiihaviour
    {
        float speed_verticle;
        float speed_horizontal;
        float stopMoveRange;
        public Vector3[] PathPositions
        {
            get
            {
                if (listPos != null && listPos.Count > 0)
                {
                    Vector2[] dat = (listPos).ToArray();
                    Vector3[] ret = new Vector3[dat.Length + 1];
                    ret[0] = transformx.position;
                    ret[0].z = 0;
                    for (int i = 0; i < dat.Length; i++)
                    {
                        ret[i + 1] = dat[i];
                    }
                    return ret;
                }
                return new Vector3[0];
            }
        }
        Queue<Vector2> listPos;
        public BH_MoveTopDown SetMultiplySpeed(float verticle, float horizontal)
        {
            speed_verticle = verticle;
            speed_horizontal = horizontal;
            return this;
        }
        public BH_MoveTopDown SetStopMoveRangeForAIMove(float stopMoveRange)
        {
            this.stopMoveRange = stopMoveRange;
            return this;
        }
        public void MoveHorizontal(float direction)
        {
            transformx.Translate(direction * Time.fixedDeltaTime * Vector2.up * m_speed * speed_horizontal);
        }
        public void MoveVertical(float direction)
        {
            transformx.Translate(direction * Time.fixedDeltaTime * Vector2.right * m_speed * speed_verticle);

        }
        public void Move()
        {
            Vector2 dir = new Vector2(moveX, moveY);
            transformx.Translate(Time.fixedDeltaTime * dir.normalized * m_speed);
        }
        public void Move(Vector2 dir)
        {
            transformx.Translate(Time.fixedDeltaTime * dir.normalized * m_speed);
        }
        //acceable
        public void MoveLeft()
        {
            moveX = -1;
        }
        public void MoveRight()
        {
            moveX = 1;
        }
        public void MoveUp()
        {
            moveY = 1;
        }
        public void MoveDown()
        {
            moveY = -1;
        }
        //hide function logic
        private void Act_move()
        {
            moveX *= speed_horizontal;
            moveY *= speed_verticle;
            Move();
        }
        //update
        public override void Update()
        {
            Act_move();
            moveX = moveY = 0;
        }
        public IEnumerator MoveOnPath(Queue<Vector2> listPosDat, BH_Look bh_look, Action moveStart = null, Action moveEnd = null)
        {
            listPos = listPosDat;
            if (listPos.Count != 0)
            {
                Vector2 curPos = listPos.Peek();
                Vector2 dir = curPos - (Vector2)transformx.position;
                Vector2 velocity = Time.fixedDeltaTime * dir.normalized * m_speed;
                float dirM = dir.magnitude;
                float velocityM = velocity.magnitude;
                moveStart?.Invoke();
                while (listPos.Count > 0)
                {
                    bh_look.LookDir2D(velocity);
                    transformx.Translate(velocity);
                    dirM -= velocityM;
                    if ((dirM < stopMoveRange))
                    {
                        listPos.Dequeue();
                        if (listPos.Count == 0) break;
                        curPos = listPos.Peek();
                        dir = curPos - (Vector2)transformx.position;
                        velocity = Time.fixedDeltaTime * dir.normalized * m_speed;
                        dirM = dir.magnitude;
                        velocityM = velocity.magnitude;
                    }
                    yield return new WaitForFixedUpdate();
                }
            }
            moveEnd?.Invoke();
            yield return null;
        }
    }
    public class BH_MoveTopDown_Physic : Biiihaviour
    {
        float speed_verticle;
        float speed_horizontal;
        public BH_MoveTopDown_Physic SetMultiplySpeed(float verticle, float horizontal)
        {
            speed_verticle = verticle;
            speed_horizontal = horizontal;
            return this;

        }
        public void MoveHorizontal(float direction)
        {
            transformx.Translate(direction * Time.fixedDeltaTime * Vector2.up * m_speed * speed_horizontal);
        }
        public void MoveVertical(float direction)
        {
            transformx.Translate(direction * Time.fixedDeltaTime * Vector2.right * m_speed * speed_verticle);

        }
        public void Move()
        {
            Vector3 dir = new Vector3(moveX, moveY, 0);
            Rigidbody2D.MovePosition(transformx.position + Time.deltaTime * dir.normalized * m_speed);
        }
        public void Move(Vector2 dir)
        {
            Rigidbody2D.MovePosition(transformx.position + Time.deltaTime * (Vector3)dir.normalized * m_speed);
        }
        //hide function logic
        private void Act_move()
        {
            moveX *= speed_horizontal;
            moveY *= speed_verticle;
            Move();
        }
        //update
        public override void Update()
        {
            Act_move();
            moveX = moveY = 0;
        }
    }
    public class BH_MoveTopDown_Script : Biiihaviour
    {
        public void Move(Vector2 velocity)
        {
            transformx.position += Time.deltaTime * (Vector3)velocity * m_speed;
        }
    }

    public class BH_MoveTopDown_Velocity : Biiihaviour
    {
        Vector2 velocity;
        MoveType moveType;
        public void SetMoveType(MoveType moveType)
        {
            this.moveType = moveType;
        }
        #region inputManager
        /// <summary>
        /// For inputSystem
        /// </summary>
        /// <param name="value"></param>
        public void OnMove(InputAction.CallbackContext value)
        {
            velocity = value.ReadValue<Vector2>();
        }
        public void OnCancleMove(InputAction.CallbackContext value)
        {
            velocity = Vector2.zero;
        }
        #endregion
        public void Move(Vector2 velocity, float moveSpeed)
        {
            m_speed = moveSpeed;
            Move(velocity);
        }
        public void Move(Vector2 velocity)
        {
            this.velocity = velocity * m_speed;
        }
        public override void Update()
        {
            switch (moveType)
            {
                case MoveType.FourWay:
                    Rigidbody2D.linearVelocity = velocity * m_speed;
                    break;
                case MoveType.Forward:
                    Vector2 forwardDirection = gameObjectx.transform.right;
                    Vector2 rightDirection = gameObjectx.transform.up;

                    // Calculate the velocity aligned with the forward direction
                    Vector2 alignedVelocity = (forwardDirection * velocity.x) + (rightDirection * velocity.y);
                    Rigidbody2D.linearVelocity = alignedVelocity.normalized * m_speed;
                    break;
                default:
                    break;
            }

        }
    }
    public enum MoveType
    {
        FourWay,
        Forward

    }
}