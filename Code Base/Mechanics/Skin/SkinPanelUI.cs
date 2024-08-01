using System;
using System.Collections.Generic;
using Code_Base.Data;
using Code_Base.Interface.Money;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanelUI : MonoBehaviour
{
    [SerializeField] private List<Image> SkinImages;
    [SerializeField] private TextMeshProUGUI ButtonText;
    [SerializeField] private List<TextMeshProUGUI> PricesText;
    
    private Player _player;
    private Skin _selectedSkin;
    private Color _defaultColor;

    private void OnEnable()
    {
        SetPrices();
    }
    
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _defaultColor = SkinImages[0].color;
    }

    public void SetPlayerSkin(int num)
    {
        if (_selectedSkin != null)
        {
            SkinImages[SkinManager.Instance.Skins.IndexOf(_selectedSkin)].color = _defaultColor;
        }
        
        SkinImages[num].color = Color.cyan;
        _selectedSkin = SkinManager.Instance.Skins[num];

        if (SavingSystem.Instance.Data.BuyedSkins.Contains(_selectedSkin.Name))
        {
            ButtonText.text = "Выбрать";
        }
        else
        {
            ButtonText.text = "Купить";
        }
    }

    public void BuySkin()
    {
        if (_selectedSkin != null && !SavingSystem.Instance.Data.BuyedSkins.Contains(_selectedSkin.Name) && Money.Instance.TryToBuy(_selectedSkin.Price))
        {
            SkinManager.Instance.SetSkin(_player, _selectedSkin.Name);
            SavingSystem.Instance.Data.BuyedSkins.Add(_selectedSkin.Name);
            SavingSystem.Instance.Save();
            ButtonText.text = "Выбрать";
            SetPrices();
        } else if (_selectedSkin != null && SavingSystem.Instance.Data.BuyedSkins.Contains(_selectedSkin.Name))
        {
            SkinManager.Instance.SetSkin(_player, _selectedSkin.Name);
        }
    }

    private void SetPrices()
    {
        for (var i = 0; i < PricesText.Count; i++)
        {
            var currentSkin = SkinManager.Instance.Skins[i];
            if (SavingSystem.Instance.Data.BuyedSkins.Contains(currentSkin.Name))
            {
                PricesText[i].text = "Куплено";
            }
            else
            {
                PricesText[i].text = currentSkin.Price.ToString();
            }
        }
    }
}

