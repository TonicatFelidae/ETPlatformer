//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using ET.GO.Behaviour;
//using ET.Module.ETInput;
//using ET.Module;
//using Cysharp.Threading.Tasks;
//
//public class ObjectControllerBase : MonoBehaviour
//{
//    Biiihaviour biiihaviour = new Biiihaviour();
//    BH_MoveTopDown moveA;
//    BH_Look lookK;
//    BH_Rotaion rotA;
//    byte moveA_updown_state;
//    byte moveA_leftright_state;
//    private void Awake()
//    {
//        moveA = new BH_MoveTopDown();
//        moveA.SetMultiplySpeed(1, 0.2f).Setup(gameObject).SetSpeed(10);
//        lookK = (BH_Look)new BH_Look().Setup(gameObject);
//        rotA = (BH_Rotaion)new BH_Rotaion().Setup(gameObject);
//    }
//    public MovermentBehaviour movermentBehaviour;
//    public RotationBehaviour rotationBehaviour;
//    // Start is called before the first frame update
//    void Start()
//    {
//       // UpdateByAI();
//    }
//    private void Update()
//    {
//        _inputManager.InvokeEvent_TwoKeyOpposite(ref moveA_updown_state,InputCode.Up, InputCode.Down,
//            () =>
//            {
//                moveA.MoveUp();
//            },
//            () =>
//            {
//                moveA.MoveDown();
//            }
//            );
//        _inputManager.InvokeEvent_TwoKeyOpposite(ref moveA_leftright_state, InputCode.Right, InputCode.Left,
//            () =>
//            {
//                moveA.MoveRight();
//            },
//            () =>
//            {
//                moveA.MoveLeft();
//            }
//            );
//        if (Input.GetKey(_inputManager.GetKey(InputCode.RotationRight)))
//        {
//            rotA.Right();
//        }
//        if (Input.GetKey(_inputManager.GetKey(InputCode.RotationLeft)))
//        {
//            rotA.Left();
//        }
//    }
//    // Update is called once per frame
//    void FixedUpdate()
//    {
//        moveA.FixedUpdate();
//        //mouse present :3 lol remember to write this code
//        //lookK.Lookpoint2D(Input.mousePosition);
//        //RotationAct();
//    }
//   // AI = new{
//   //     Event(PlayerNearby)  Act(MoveTo(x)) Con(PlayerNearby) 
//   //     }
//   // public UniTask UpdateByAI()
//   // {
//   //     moveA.MoveUp();
//   //     moveA.MoveDown();
//   //
//   // } 
//   // public async void MoveUpAsync()
//   // {
//   //     moveA.MoveUp().;
//   // }
//    //private void RotationAct()
//    //{
//    //    switch (rotationBehaviour.controlType)
//    //    {
//    //        case RotationControlType.LookAt:
//    //            Lookpoint2D(rotationBehaviour.point_lookAt);
//    //            break;
//    //        case RotationControlType.LookAtMouse:
//    //            Lookpoint2D(Input.mousePosition);
//    //            break;
//    //    }
//    //}
//    [Serializable]
//    public struct MovermentBehaviour
//    {
//        public MovermentControlType controlType;
//
//    }
//    [Serializable]
//    public struct RotationBehaviour
//    {
//        public RotationControlType controlType;
//        public Vector3 point_lookAt;
//    }
//}
