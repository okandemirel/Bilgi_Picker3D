using System;

namespace Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public MovementData MovementData;
        public ScaleData ScaleData;
    }

    [Serializable]
    public struct MovementData
    {
        public float ForwardSpeed;
        public float SidewaysSpeed;
        public float ForwardForceCounter;

        public MovementData(float forwardSpeed, float sidewaysSpeed, float forwardForceCounter)
        {
            ForwardSpeed = forwardSpeed;
            SidewaysSpeed = sidewaysSpeed;
            ForwardForceCounter = forwardForceCounter;
        }
    }

    [Serializable]
    public struct ScaleData
    {
        public float ScaleCounter;

        public ScaleData(float scaleCounter)
        {
            ScaleCounter = scaleCounter;
        }
    }
}