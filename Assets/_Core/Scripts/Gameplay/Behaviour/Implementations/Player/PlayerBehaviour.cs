using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Behaviour.Common.Door;
using Odumbrata.Behaviour.Player.States;
using Odumbrata.Services.Camera;
using UnityEngine;
using UnityEngine.AI;

namespace Odumbrata.Behaviour.Player
{
    public sealed class PlayerBehaviour : BaseHumanoidBehaviour<BaseEntityState>, ICameraTarget, IDoorHandler
    {
        [SerializeField] private Transform _cameraFollowPoint;
        [SerializeField] private Transform _cameraLookAt;

        private PlayerMoveState _moveState;
        private PlayerWaitForCallState _waitForCallState;
        
        private bool _waitingForDoor;
        public Transform CameraFollow => _cameraFollowPoint;
        public Transform CameraLookAt => _cameraLookAt;


        protected override void Start()
        {
            base.Start();
            
            _moveState = new PlayerMoveState(this);
            _waitForCallState = new PlayerWaitForCallState(this);

            _moveState.OnReachDestination += OnDestinationReached;
            _waitForCallState.OnValidPath += OnValidPath;

            SetStateAsync(_waitForCallState).Forget();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _moveState.OnReachDestination -= OnDestinationReached;
            _waitForCallState.OnValidPath -= OnValidPath;
        }

        private void OnValidPath(NavMeshPath path)
        {
            if (_waitingForDoor)
            {
                return;
            }

            SetStateAsync(_moveState).Forget();
        }

        private void OnDestinationReached()
        {
            SetStateAsync(_waitForCallState).Forget();
        }

        public async Task HandleDoorPreOpening(DoorHandleData data)
        {
            _waitingForDoor = true;

            var openDoorData = new OpenDoorData(Transform,
                data.InteractionPosition,
                data.LookAtPosition);

            var openDoorState = new PlayerOpenDoorState(this);

            await SetStateAsync(openDoorState, openDoorData);
            await SetStateAsync(_waitForCallState);

            _waitingForDoor = false;
        }
    }
}