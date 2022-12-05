using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace HseTowerDefense.PlayerResources
{
    public class CoinsIndicator : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Update()
        {
            _text.SetText(PlayerInventory.Instance.coins.ToString(CultureInfo.InvariantCulture));
        }
    }
}