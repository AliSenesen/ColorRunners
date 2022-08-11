using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands.Stack;
using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        
        #region Self Variables
        
        #region Public Variables
        
        public GameObject TempHolder;
        
        #endregion
        
        #region Serialized Variables
          
        [SerializeField] private List<GameObject> Collected = new List<GameObject>();
        
        [SerializeField] private GameObject collectorMeshRenderer;
        
        [SerializeField] private Transform playerManager;

        #region Private Variables

        [ShowInInspector] private StackData Data;

        private StackLerpMovementCommand _stackLerpMovementCommand;

        private CollectableScaleUpCommand _collectableScaleUpCommand;
        
        #endregion
        
        #endregion
        
        #endregion
        
        private void Awake()
        { 
            Data = GetStackData();
            Init();
        } 
        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_Stack").StackData;

        private void Init()
        {
            _stackLerpMovementCommand = new StackLerpMovementCommand();
            _collectableScaleUpCommand = new CollectableScaleUpCommand();
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
        }
        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        private void Update()
        {
            StackLerpMovement();
        }
        private void StackLerpMovement()
        { 
            _stackLerpMovementCommand.StackLerpMove(ref Collected, playerManager, Data.LerpSpeed, ref Data.StackDistanceZ);
        }
        private void OnIncreaseStack(GameObject other)
        {
            AddOnStack(other);
            CollectableScaleUp();
        }
        private void OnDecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            DecreaseStack(obstacleCollisionGOParams);
        }
        private void DecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            int CollidedObjectIndex = obstacleCollisionGOParams.Collected.transform.GetSiblingIndex();
            Collected[CollidedObjectIndex].SetActive(false);
            Collected[CollidedObjectIndex].transform.SetParent(TempHolder.transform);
            Collected.RemoveAt(CollidedObjectIndex);
            Collected.TrimExcess();
        }
        private void AddOnStack(GameObject other)
        { 
            other.transform.parent = transform;
            Collected.Add(other.gameObject);
        }
        private void CollectableScaleUp()
        {
            StartCoroutine(_collectableScaleUpCommand.CollectableScaleUp(Collected, Data.ScaleFactor, Data.StackScaleUpDelay));
        }
    }
}    
