using HseTowerDefense.Towers;
using UnityEngine;

namespace HseTowerDefense.BuildingSystem
{
    public class TowerBuilder : MonoBehaviour
    {
        [SerializeField] private Tower _selectedTower;

        private void Update()
        {
            if (_selectedTower != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Vector3 mousePosition = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                    Debug.DrawRay(ray.origin, ray.direction * 1000, Color.magenta, 2);
                    if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out BuildingCell cell))
                        {
                            if (cell.CanBuilding)
                            {
                                Vector3 towerPosition = cell.transform.position;
                                GameObject tower = Instantiate(_selectedTower.gameObject);
                                tower.transform.position = towerPosition;
                            }   
                        }
                    }
                }
            }
        }
    }
}