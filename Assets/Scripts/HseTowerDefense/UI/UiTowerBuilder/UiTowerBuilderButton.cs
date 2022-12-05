using System.Globalization;
using HseTowerDefense.BuildingSystem;
using HseTowerDefense.PlayerResources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HseTowerDefense.UI.UiTowerBuilder
{
    public class UiTowerBuilderButton : MonoBehaviour
    {
        [SerializeField] private Image _towerIconImage;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buildButton;
        [SerializeField] private TowerBuilder _towerBuilder;
        [SerializeField] private TowerData _tower;
        
        private void Awake()
        {
            _towerBuilder = FindObjectOfType<TowerBuilder>();
            _buildButton.onClick.AddListener(BuildTower);
            _towerBuilder.OnStartBuild += () => gameObject.SetActive(false);
            _towerBuilder.OnEndBuild += () => gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _buildButton.onClick.RemoveListener(BuildTower);
        }

        public void Init(TowerData tower)
        {
            _tower = tower;
            _priceText.SetText(tower.Price.ToString(CultureInfo.InvariantCulture));
            _towerIconImage.sprite = tower.TowerIcon;
        }

        private void BuildTower()
        {
            if (PlayerInventory.Instance.coins >= _tower.Price)
            {
                PlayerInventory.Instance.coins -= _tower.Price;
                _towerBuilder.SelectTower(_tower);
            }
        }
    }
}