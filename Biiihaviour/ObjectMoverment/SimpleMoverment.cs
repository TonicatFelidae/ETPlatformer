using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET;
using ET.GO.Behaviour;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class SimpleMoverment : MonoBehaviour
{
    public bool move_topDown = true;
    public MoveType moveType;
    public bool rotation;
    BiiihaviourInput inputActions;
    BH_MoveTopDown_Velocity bh_move;
    BH_Rotaion bh_rotation;
    //logic
    private void Awake()
    {
        inputActions = new BiiihaviourInput();
    }
    // Start is called before the first frame update
    void Start()
    {
        bh_move = new BH_MoveTopDown_Velocity().Setup<BH_MoveTopDown_Velocity>(this.gameObject, MoveMethod.Translate);
        bh_move.SetSpeed(4);
        bh_move.SetMoveType(moveType);
        bh_rotation = new BH_Rotaion().Setup<BH_Rotaion>(this.gameObject, MoveMethod.Translate);
        bh_rotation.SetSpeed(20);
        inputActions.Player.Moverment.performed += bh_move.OnMove;
        inputActions.Player.Moverment.canceled += bh_move.OnCancleMove;
        inputActions.Player.Rotation.performed += bh_rotation.OnRotation;
        inputActions.Player.Rotation.canceled += bh_rotation.OnCancleRotation;


    }
    private void OnEnable()
    {
        inputActions.Player.Moverment.Enable();
        inputActions.Player.Rotation.Enable();

    }
    private void OnDisable()
    {
        inputActions.Player.Moverment.Disable();
        inputActions.Player.Rotation.Disable();

    }
    private void FixedUpdate()
    {
        if (move_topDown) bh_move.Update();
        if (rotation) bh_rotation.Update();
    }
}