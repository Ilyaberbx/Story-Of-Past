using Better.Locators.Runtime;
using Odumbrata.Behaviour.Player;
using Odumbrata.Services.Camera;
using UnityEngine;

namespace Odumbrata
{
    public class TestBootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour _player;

        private CameraService _cameraService;

        private void Start()
        {
            _cameraService = ServiceLocator.Get<CameraService>();
            _cameraService.SetActive(0);
            _cameraService.SetTarget(_player);
        }
    }
}