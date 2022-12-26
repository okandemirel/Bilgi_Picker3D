using System.Collections.Generic;
using System.Linq;
using Data.ValueObjects;
using Interfaces;
using Managers;
using UnityEngine;

namespace Commands.Player
{
    public class ForceBallsToPoolCommand : ICommand
    {
        private PlayerManager _manager;
        private MovementData _movementData;

        public ForceBallsToPoolCommand(PlayerManager manager, MovementData movementData)
        {
            _manager = manager;
            _movementData = movementData;
        }

        public void Execute(int value)
        {
            //throw new System.NotImplementedException();
        }

        public void Execute()
        {
            var transform1 = _manager.transform;
            var position = transform1.position;
            var forcePos = new Vector3(position.x, position.y - 1.2f, position.z + 1);

            var collider = Physics.OverlapSphere(forcePos, 1.05f);

            var ballColliderList = collider.Where(col => col.CompareTag("Collectable")).ToList();

            foreach (var ball in ballColliderList)
            {
                if (ball.GetComponent<Rigidbody>() == null) continue;
                Rigidbody rb;
                rb = ball.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(0, _movementData.ForwardForceCounter, _movementData.ForwardForceCounter),
                    ForceMode.Impulse);
            }

            ballColliderList.Clear();
        }
    }
}