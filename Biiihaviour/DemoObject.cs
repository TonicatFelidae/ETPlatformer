//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Zenject;
//using ET;
//using ET.Module;
//using ET.Module.ETInput;
//using ET.GO.Behaviour;
//
//public class DemoObject : MonoBehaviour
//{
//    BH_Move moveA = new ();
//    BH_Rotaion rotA = new ();
//    BH_Look avatarLook = new ();
//    InputManager _inputManager;
//    public GameObject avatar;
//    public Animator avatarAnimator;
//    //logic
//    byte _moveLeftRightState_isRight = 0;
//    [Inject]
//    public void Init(InputManager inputManager)
//    {
//        _inputManager = inputManager;
//        Debug.Log("Pazz");
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        avatarAnimator = avatar.GetComponent<Animator>();
//        moveA.Setup(gameObject);
//        rotA.Setup(gameObject);
//        avatarLook.Setup(avatar);
//        // if (key left) move left
//        // if (key right) move right
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        if (_inputManager.InvokeEvent_TwoKeyOpposite(ref _moveLeftRightState_isRight, InputCode.Right, InputCode.Left,
//            () =>
//            {
//                moveA.MoveRight();
//                avatarLook.LookForward2D(ETDirection.Right);
//            },
//            () =>
//            {
//                moveA.MoveLeft();
//                avatarLook.LookForward2D(ETDirection.Left);
//            }
//            ))
//        {
//            avatarAnimator.SetInteger("state", 1);
//        }
//        else
//        {
//            avatarAnimator.SetInteger("state", 0);
//        }
//
//        //if (Input.GetKey(_inputManager.GetKey(InputCode.Right)) && Input.GetKey(_inputManager.GetKey(InputCode.Left)))
//        //{
//        //    if (Input.GetKeyDown(_inputManager.GetKey(InputCode.Right)))
//        //    {
//        //        _moveLeftRightControl_isRight = 2;
//        //    }
//        //    else if (Input.GetKeyDown(_inputManager.GetKey(InputCode.Left)))
//        //    {
//        //        _moveLeftRightControl_isRight = 1;
//        //    }
//        //}
//        //else
//        //{
//        //    _moveLeftRightControl_isRight = 0;
//        //    if (Input.GetKey(_inputManager.GetKey(InputCode.Right)))
//        //    {
//        //        _moveLeftRightControl_isRight = 2;
//        //    }
//        //    if (Input.GetKey(_inputManager.GetKey(InputCode.Left)))
//        //    {
//        //        _moveLeftRightControl_isRight = 1;
//        //    }
//        //}
//        //if (_moveLeftRightControl_isRight == 2)
//        //{
//        //    moveA.MoveRight();
//        //    avatarLook.LookForward2D(ETDirection.Right);
//        //}
//        //else if (_moveLeftRightControl_isRight == 1)
//        //{
//        //    moveA.MoveLeft();
//        //    avatarLook.LookForward2D(ETDirection.Left);
//        //}
//        if (Input.GetKey(_inputManager.GetKey(InputCode.RotationRight)))
//        {
//            rotA.Right();
//        }
//        if (Input.GetKey(_inputManager.GetKey(InputCode.RotationLeft)))
//        {
//            rotA.Left();
//        }
//        if (Input.GetKeyDown(_inputManager.GetKey(InputCode.Jump)))
//        {
//            moveA.Jump();
//        }
//    }
//    private void FixedUpdate()
//    {
//        moveA.FixedUpdate();
//    }
//}
