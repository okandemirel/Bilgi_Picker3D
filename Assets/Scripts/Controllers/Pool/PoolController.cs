using System;
using System.Collections.Generic;
using Data.UnityObjects;
using Data.ValueObjects;
using DG.Tweening;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Controllers.Pool
{
    public class PoolController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<DOTweenAnimation> tweens = new List<DOTweenAnimation>();
        [SerializeField] private TextMeshPro poolText;
        [SerializeField] private byte stageID;
        [SerializeField] private new Renderer renderer;

        #endregion

        #region Private Variables

        [ShowInInspector] private PoolData _data;
        [ShowInInspector] private byte _collectedCount;

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPoolData();
        }

        private PoolData GetPoolData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level")
                .Levels[(int)CoreGameSignals.Instance.onGetLevelValue?.Invoke()]
                .PoolList[stageID];
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful += OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful += OnChangeThePoolColor;
        }

        private void OnActivateTweens(int stageValue)
        {
            if (stageValue != stageID) return;
            foreach (var tween in tweens)
            {
                tween.DOPlay();
            }
        }

        private void OnChangeThePoolColor(int stageValue)
        {
            renderer.sharedMaterial.DOColor(new Color(0.1607842f, 0.6039216f, 0.1766218f), 1).SetEase(Ease.Linear);
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnActivateTweens;
            CoreGameSignals.Instance.onStageAreaSuccessful -= OnChangeThePoolColor;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            SetRequiredAmountToText();
        }

        public bool TakeStageResult(byte stageValue)
        {
            if (stageValue == stageID)
            {
                return _collectedCount >= _data.RequiredObjectCount;
            }

            return false;
        }

        private void SetRequiredAmountToText()
        {
            poolText.text = $"0/{_data.RequiredObjectCount}";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Collectable")) return;
            IncreaseCollectedCount();
            SetCollectedCountToText();
        }

        private void SetCollectedCountToText()
        {
            poolText.text = $"{_collectedCount}/{_data.RequiredObjectCount}";
        }

        private void IncreaseCollectedCount()
        {
            _collectedCount++;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Collectable")) return;
            DecreaseTheCollectedCount();
            SetCollectedCountToText();
        }

        private void DecreaseTheCollectedCount()
        {
            _collectedCount--;
        }
    }
}