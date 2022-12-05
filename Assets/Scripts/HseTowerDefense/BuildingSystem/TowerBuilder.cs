using System;
using HseTowerDefense.Towers;
using UnityEngine;

namespace HseTowerDefense.BuildingSystem
{
    public class TowerBuilder : MonoBehaviour
    {
        [SerializeField] private TowerData _selectedTower;
        [SerializeField] private GameObject _towerPreviewGm;

        public event Action OnStartBuild;
        public event Action OnEndBuild;

        public bool IsSelected => _selectedTower != null;

        public void SelectTower(TowerData tower)
        {
            _selectedTower = tower;
            OnStartBuild?.Invoke();
        }

        public void ResetTower()
        {
            _selectedTower = null;
        }

        private void ClearPreview()
        {
            if (_towerPreviewGm != null)
            {
                Destroy(_towerPreviewGm);
                _towerPreviewGm = null;
            }
        }
        
        private void Update()
        {
            if (_selectedTower != null)
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                { 
                    ClearPreview();
                    Debug.DrawRay(ray.origin, ray.direction * 1000, Color.magenta, 2);
                    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out BuildingCell cell))
                        {
                            if (cell.CanBuilding)
                            {
                                Vector3 towerPosition = cell.transform.position;
                                GameObject tower = Instantiate(_selectedTower.TowerPrefab.gameObject);
                                tower.transform.position = towerPosition;
                                ResetTower();
                                OnEndBuild?.Invoke();
                            }   
                        }
                    }
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * 1000, Color.magenta, 2);
                    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out BuildingCell cell))
                        {
                            if (cell.CanBuilding)
                            {
                                if (_towerPreviewGm == null)
                                {
                                    Vector3 towerPosition = cell.transform.position;
                                    _towerPreviewGm = Instantiate(_selectedTower.TowerPreviewPrefab);
                                    _towerPreviewGm.transform.position = towerPosition;
                                }
                                else
                                {
                                    Vector3 towerPosition = cell.transform.position;
                                    _towerPreviewGm.transform.position = towerPosition;
                                }
                            }   
                        }
                    }
                }
            }
        }
    }
}