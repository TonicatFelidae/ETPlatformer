using ET.GO.Behaviour;
using ET.Module;
using UnityEngine;
using Zenject;
using ET.Module.ETInput;
using ET.SupportKit;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace Game
{
    public class PlayerControl : CreatureControl
    {

        [Inject] InputManager _inputManager;
        [Inject] GameData _gameData;
        [Inject] Camera _camera;
        [Inject] SpeechBoxManager _speechBoxManager;
        [Inject] GameInputManager _gameInputManager;
        PlayerData PlayerData => _gameData.playerData;
        public BH_Move bH_Move;
        private float? _targetX;
        private float _targetXDir;
        private bool _suppressClick;
        private float _mouseTargetX;
        private int _lookDirection;

        private bool lookRight = false;
       

      [Header("REFERENCES")]
        public Player player;
        public Rigidbody2D rigidbody2D;
        public Collider2D collider2D;
        public Transform cameraTrackingPoint;
        //public Biihav
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            bH_Move = new BH_Move();
            bH_Move.Setup<BH_Move>(gameObject,MoveMethod.Translate);
            bH_Move.SetSpeed(PlayerData.playerMoveSpeed);
            _gameInputManager.OnWorldObjectClicked.AddListener(OnWorldObjectClicked);
            _gameInputManager.OnWorldPositionClicked.AddListener(OnWorldObjectPosition);
            _gameInputManager.OnCancleMove.AddListener(OnCancleMove);
        }
        private void OnWorldObjectClicked(WorldObject worldObject)
        {

        }
        private void OnWorldObjectPosition(Vector2 vector2)
        {

        }
        private void OnCancleMove()
        {

        }
        // Update is called once per frame
        void Update()
        {
            bH_Move.ResetInput();
            if (Input.GetKey(_inputManager.GetKey(InputCode.Left)))
            {
                ResetMoveTarget();
                bH_Move.MoveLeft();
                SetLookDirection(false);
                _suppressClick = true;
            }
            if (Input.GetKey(_inputManager.GetKey(InputCode.Right)))
            {
                ResetMoveTarget();
                bH_Move.MoveRight();
                SetLookDirection(true);
                _suppressClick = true;
            }
            //if (Input.GetKey(_inputManager.GetKey(InputCode.Up)))
            //{
            //    bH_Move.MoveUp();
            //}
            //if (Input.GetKey(_inputManager.GetKey(InputCode.Down)))
            //{
            //    bH_Move.MoveDown();
            //}
            //bH_Move.Update();
            BH_ClickMove();
            Act_Move();
            if (_targetX != null)
            {
                // Check if arrived at interaction distance, then invoke next action
                if (Mathf.Abs(DistanceToTargetX) <= PlayerData.interactableDistance)
                {
                    if (player.InvokePendingAction())
                    {
                        Debug.Log("InvokePendingAction");  
                        _targetX = null;
                    }
                }
            }
        }
        float DistanceToTargetX
        {
            get
            {
                if (!_targetX.HasValue) return 99999;
                Debug.Log($"{_targetX.Value}-{transform.position.x}={Mathf.Abs(_targetX.Value - transform.position.x)}");
                return Mathf.Abs(_targetX.Value - transform.position.x);
            }
        }
        void BH_ClickMove()
        {
            if (_suppressClick)
            {
                _suppressClick = false;
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {

            }
            bool isInteraction = false;
            if (Input.GetMouseButton(0)) //onclick and hold
            {
                float distance = _camera.WorldToScreenPoint(transform.position).z;
                Vector3 worldPoint = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
                _mouseTargetX = worldPoint.x;
                _targetXDir = _mouseTargetX - transform.position.x;
                _targetXDirection = _targetXDir < 0 ? -1 : 1;
                UpdateLookDirection();
                

                if (!K.IsMouseOverObject) //NOT over object
                {
                    _targetX = _mouseTargetX;
                    player.ShowSpeech(false);
                    player.CancleNextAction();
                    Debug.Log("Move, mouse not over object");
                }
                else if (!K.IsMouseClickDownLocked) //NOT lock by direction change, lock after click object
                {
                    _targetX = _mouseTargetX;
                    player.ShowSpeech(false);
                    player.CancleNextAction();
                    Debug.Log("Move, mouse not locked by direction change");
                }
                else
                {
                    _targetX = null;
                    if (Mathf.Abs(_targetXDir) < PlayerData.interactableDistance)
                    {
                        player.InvokeNextAction();
                        isInteraction = true;
                    }
                    else
                    {
                        //move closer to interaction distance
                        // Calculate the target position at the edge of interaction distance from the clicked point
                        float direction = Mathf.Sign(_targetXDir);
                        float targetEdgeX = _mouseTargetX - PlayerData.interactableDistance * direction;

                        // Set the new target X to stop at interaction distance
                        _targetX = targetEdgeX;

                        // Start moving toward the new target
                        //bH_Move.MoveToPoint2DLeftRight(_targetXDirection);
                        Debug.Log("Move, to interaction");

                    }
                }
            }
            //float minimumDistance =  PlayerData.moveableMinimumDistance;

            

        }
        private void Act_Move()
        {
            if (_targetX.HasValue)
            {
                float minimumDistance = PlayerData.moveableMinimumDistance;
                _targetXDir = _targetX.Value - transform.position.x;
                if (Mathf.Abs(_targetXDir) < minimumDistance) return;
                int direction = _targetXDir < 0 ? -1 : 1;

                if (direction != _targetXDirection)
                {
                    ResetMoveTarget();

                    return;
                }

                bH_Move.MoveToPoint2DLeftRight(_targetXDirection);
            }
        }
        
        public void ResetMoveTarget()
        {
            _targetX = null ;
            player.ShowSpeech(false);
        }
        public void ResetData()
        {
            _targetX = null;
            _suppressClick = true;
        }
        #region Look Direction
        public override void OnDirectionChange()
        {
            K.LockMouse();
            player.ShowSpeech(false);
        }
        #endregion
        public void TeleportToLocation()
        {
            
        }
        private void FixedUpdate()
        {
        }
    }
}