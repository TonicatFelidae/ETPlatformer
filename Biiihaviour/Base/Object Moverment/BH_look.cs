using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.GO.Behaviour
{
    public class BH_Look : Biiihaviour
    {
        public void Move_Freely_FourDirection(Vector3 dir, float m_speed)
        {
            transformx.Translate(m_speed * Time.deltaTime * dir.normalized);
        }
        //Rotation
        public void LookPoint2D_Instant(Vector3 point)
        {
            Vector2 look = point - Camera.main.WorldToScreenPoint(gameObjectx.transform.position);
            float an = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
            transformx.rotation = Quaternion.AngleAxis(an, Vector3.forward);
        }
        public void LookMouse2D(Vector3 pointMouse)
        {
            Vector2 look = pointMouse - Camera.main.WorldToScreenPoint(gameObjectx.transform.position);
            //var rotation = Quaternion.LookRotation(look);
            float an = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(an, Vector3.forward);
            transformx.rotation = Quaternion.Slerp(transformx.rotation, rotation, Time.deltaTime * r_speed);
        }
        public void LookPoint2D(Vector3 point)
        {
            Vector2 look = point - gameObjectx.transform.position;
            //var rotation = Quaternion.LookRotation(look);
            float an = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(an, Vector3.forward);
            transformx.rotation = Quaternion.Slerp(transformx.rotation, rotation, Time.deltaTime * r_speed);
        }
        public void LookDir2D(Vector2 look)
        {
            //var rotation = Quaternion.LookRotation(look);
            float an = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(an, Vector3.forward);
            transformx.rotation = Quaternion.Slerp(transformx.rotation, rotation, Time.deltaTime * r_speed);
        }
        public void UnityInput(float r_speed)
        {
            float inputRot = Input.GetAxis("Rotation");
            transformx.eulerAngles += new Vector3(0, 0, -inputRot * r_speed);
        }
        //Forward
        public void LookForward2D(ETDirection direction)
        {
			switch (direction)
			{
				case ETDirection.Up:
					break;
				case ETDirection.Down:
					break;
				case ETDirection.Left:
                    transformx.localScale = new (-Mathf.Abs(transformx.localScale.x), transformx.localScale.y, transformx.localScale.z);

                    break;
				case ETDirection.Right:
                    transformx.localScale = new(Mathf.Abs(transformx.localScale.x), transformx.localScale.y, transformx.localScale.z);
                    break;
				default:
					break;
			}
		}
    }
}

