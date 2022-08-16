using Enums;
using UnityEngine;

namespace Controllers
{
    public class DroneAreaColorController : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        public ColorType ColorType;
       
        #endregion
    
        #region Private Variables

        private MeshRenderer _meshRenderer;
        
        #endregion
    
        #endregion
        private void Awake()
        {
            GetReferences();
        }
        private void Start()
        {
            SetGateMaterial(ColorType);
        }
        private void GetReferences()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        public void SetGateMaterial(ColorType colorType)
        {
            _meshRenderer.material = Resources.Load<Material>($"Materials/{colorType}");
        }
    }
}

