using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObjects;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerMeshController meshController;

        #endregion

        #region Private Variables

        [ShowInInspector] private PlayerData _data;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPlayerData();
            SendDataToControllers();
        }

        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }

        private void SendDataToControllers()
        {
            movementController.GetMovementData(_data.MovementData);
            meshController.GetMeshData(_data.ScaleData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnInputTaken;
            InputSignals.Instance.onInputReleased += OnInputReleased;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered += OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnStageAreaSuccessful;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnInputTaken;
            InputSignals.Instance.onInputReleased -= OnInputReleased;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onStageAreaEntered -= OnStageAreaEntered;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnStageAreaSuccessful;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
        }

        private void OnInputTaken()
        {
            movementController.IsReadyToMove(true);
        }
        private void OnInputDragged(HorizontalnputParams inputParams)
        {
            movementController.UpdateInputParams(inputParams);
        }

        private void OnInputReleased()
        {
            movementController.IsReadyToMove(false);
        }

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
        }
        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnStageAreaEntered()
        {
            movementController.IsReadyToPlay(false);
        }
        private void OnStageAreaSuccessful()
        {
            movementController.IsReadyToPlay(true);
        }

        private void OnReset()
        {
            movementController.OnReset();
            meshController.OnReset();
            physicsController.OnReset();
        }
    }
}