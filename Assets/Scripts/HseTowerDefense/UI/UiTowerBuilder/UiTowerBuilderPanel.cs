using System.Collections.Generic;
using HseTowerDefense.BuildingSystem;
using UnityEngine;

namespace HseTowerDefense.UI.UiTowerBuilder
{
    public class UiTowerBuilderPanel : MonoBehaviour
    {
        [SerializeField] private List<TowerData> _towersList;
        [SerializeField] private UiTowerBuilderButton _towerButtonPrefab;
        [SerializeField] private Transform _towerButtonsList;

        private void Awake()
        {
            foreach (TowerData towerData in _towersList)
            {
                GameObject towerButtonGm = Instantiate(_towerButtonPrefab.gameObject, _towerButtonsList);
                UiTowerBuilderButton towerBuilderButton = towerButtonGm.GetComponent<UiTowerBuilderButton>();
                towerBuilderButton.Init(towerData);
                towerBuilderButton.gameObject.SetActive(true);
            }
        }
    }
}