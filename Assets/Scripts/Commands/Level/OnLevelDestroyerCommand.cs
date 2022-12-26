using Interfaces;
using UnityEngine;

namespace Commands.Level
{
    public class OnLevelDestroyerCommand : ICommand
    {
        private readonly Transform _levelHolder;

        public OnLevelDestroyerCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }

        public void Execute()
        {
            Object.Destroy(_levelHolder.GetChild(0).gameObject);
        }

        public void Execute(int value)
        {
        }
    }
}