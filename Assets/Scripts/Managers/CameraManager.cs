using System;
using Cinemachine;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        #endregion

        #region Private Variables

        private Vector3 _initialPosition;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnSetCameraTarget()
        {
            virtualCamera.Follow = FindObjectOfType<PlayerManager>().transform;
        }

        private void UnSubscribeEvents()
        {
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            GetTheInitialPosition();
        }

        private void GetTheInitialPosition()
        {
            _initialPosition = transform.localPosition;
        }

        private void OnReset()
        {
            transform.localPosition = _initialPosition;
            virtualCamera.Follow = null;
        }
    }
}