using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code_Base.Data;
using UnityEngine;

public class SkinManager : Singleton<SkinManager>
{
    public List<Skin> Skins;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        if (SavingSystem.Instance.Data.SelectedSkin != null)
        {
            SetSkin(_player, SavingSystem.Instance.Data.SelectedSkin);
        }
    }

    public void SetSkin(Character character, SkinName skin)
    {
        var material = new Material[1] { Skins.FirstOrDefault(x=>x.Name == skin)?.Material};
        character.GameObject.GetComponentInChildren<MeshRenderer>().materials = material;

        if (character == _player)
        {
            SavingSystem.Instance.Data.SelectedSkin = skin;
            SavingSystem.Instance.Save();
        }
    }

}