using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ET.GO.Behaviour
{
    public class BH_Rotaion : Biiihaviour
    {
        public float direction;
        public void Right()
        {
            direction = -1;
            Rotation2D();
        }
        public void Left()
        {
            direction = 1;
            Rotation2D();
        }
        #region inputManager
        /// <summary>
        /// For inputSystem
        /// </summary>
        /// <param name="value"></param>
        public void OnRotation(InputAction.CallbackContext value)
        {
            direction = value.ReadValue<float>();
        }
        public void OnCancleRotation(InputAction.CallbackContext value)
        {
            direction = 0;
        }
        #endregion
        //private void Rotation2D(Vector2 dirvec)
        //{
        //    if (dirvec != Vector2.zero)
        //    {
        //        Vector2 direct = dirvec;
        //        targetangle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg - 90f;
        //        if (curangle != targetangle)
        //        {
        //            float dist1 = Mathf.Abs(curangle - targetangle);
        //            D.addDtext("curangle", "curangle :" + curangle);
        //            D.addDtext("targetangle", "targetangle :" + targetangle);
        //            targetangle = targetangle + 360 * (int)(curangle / 360);
        //            if (ET_Math.isBetween(targetangle, curangle - 180, curangle) || ET_Math.isBetween(targetangle, curangle + 360 - 180, curangle + 360))
        //            {
        //                if (targetangle >= curangle)
        //
        //                {
        //                    Debug.Log("pass");
        //                    curangle += 360;
        //                }
        //            }
        //            else
        //            {
        //                if (targetangle < curangle) curangle -= 360;
        //            }
        //            //lept
        //            if (r_rot_speed != 0) curangle = ET_Math.lerpToFloat(curangle, targetangle, r_rot_speed * Time.deltaTime);
        //            else curangle = targetangle;
        //        }
        //        transform.eulerAngles = Vector3.forward * curangle;
        //    }
        //}
        //private void Rotation2D(Vector2 dirvec)
        //{
        //    if (curangle != targetangle) curangle = ET_Math.lerpToFloat(curangle, targetangle, data.rotspeed / 5);
        //    transform.eulerAngles = Vector3.forward * curangle;
        //}
        private void Rotation2D()
        {
            transformx.eulerAngles += Vector3.forward * 100 * Time.deltaTime * direction;
        }
        public override void Update()
        {
            transformx.eulerAngles += Vector3.forward * 100 * Time.deltaTime * direction;
        }
    }
}

