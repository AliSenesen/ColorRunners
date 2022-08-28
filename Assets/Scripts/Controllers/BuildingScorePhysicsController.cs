﻿using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingScorePhysicsController : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        
        #endregion

        #region Serialized Variables

        [SerializeField] private BuildingManager buildingManager;
        [SerializeField] private int ObjetType;
        #endregion

        #region Private Variables

        private float _timer = 0f;

        #endregion

        #endregion

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("ScorePhysics"))
            {
                 _timer -= Time.fixedDeltaTime;
                
              if (_timer <= 0)
              {
                  _timer = 0.2f;
  
                  if (buildingManager.buildingsData.BuildingMarketPrice > buildingManager.buildingsData.PayedAmount)
                  {
                      buildingManager.UpdatePayedAmount();
                  }
                  else
                  {
                      gameObject.SetActive(false);
                      if (buildingManager.buildingsData.idleLevelState == IdleLevelStateType.Uncompleted)
                      {
                          transform.gameObject.SetActive(false);
                          buildingManager.OpenSideObject();
                          buildingManager.UpdateBuildingStatus(IdleLevelStateType.Completed);
                      }
                  }
              }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ScorePhysics"))
            {
                _timer = 0f;
                buildingManager.Save(buildingManager.BuildingAddressID);
            }
        }
    }
}