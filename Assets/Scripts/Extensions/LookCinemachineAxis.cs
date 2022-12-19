using System;
using Cinemachine;
using Enums;
using UnityEngine;

namespace Extensions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LookCinemachineAxis : CinemachineExtension
    {
        public CinemachineLockAxis LockAxis;
        public CinemachineCoreType CoreType;

        [Tooltip("Lock the camera's specific position to this value")]
        public float m_Position = 0f;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            switch (CoreType)
            {
                case (CinemachineCoreType)CinemachineCore.Stage.Body:
                    switch (LockAxis)
                    {
                        case CinemachineLockAxis.XValue:
                        {
                            var pos = state.RawPosition;
                            pos.x = m_Position;
                            state.RawPosition = pos;
                        }
                            break;
                        case CinemachineLockAxis.YValue:
                        {
                            var pos = state.RawPosition;
                            pos.y = m_Position;
                            state.RawPosition = pos;
                        }
                            break;
                        case CinemachineLockAxis.ZValue:
                        {
                            var pos = state.RawPosition;
                            pos.z = m_Position;
                            state.RawPosition = pos;
                        }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case CinemachineCoreType.Aim:
                    switch (LockAxis)
                    {
                        case CinemachineLockAxis.XValue:
                        {
                            var pos = state.RawOrientation;
                            pos.x = m_Position;
                            state.RawOrientation = pos;
                        }
                            break;
                        case CinemachineLockAxis.YValue:
                        {
                            var pos = state.RawOrientation;
                            pos.y = m_Position;
                            state.RawOrientation = pos;
                        }
                            break;
                        case CinemachineLockAxis.ZValue:
                        {
                            var pos = state.RawOrientation;
                            pos.z = m_Position;
                            state.RawOrientation = pos;
                        }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
            }
        }
    }
}