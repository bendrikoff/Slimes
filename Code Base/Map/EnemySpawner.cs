using System;
using System.Collections;
using System.Collections.Generic;
using Code_Base.Map;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Spawner
{
    public List<Enemy> Enemies;

    protected override void Awake()
    {
        base.Awake();
        Enemies = new List<Enemy>();
        Spawn();
    }

    private void Respawn(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.position = GetRandomPoint();
        enemy.gameObject.SetActive(true);
    }

    public override void Spawn()
    {
        for (int i = 0; i < _count; i++)
        {
            var randomPoint = GetRandomPoint();
            var foodGameObject = Instantiate(_prefab, randomPoint, Quaternion.identity, transform);
            foodGameObject.GetComponentInChildren<MeshRenderer>().materials[0].color = GetRandomColor();
            foodGameObject.name = i.ToString(); 
            var enemy = foodGameObject.GetComponent<Enemy>();
            Enemies.Add(enemy);
            enemy.OnDeath += () => Respawn(enemy);
        }
    }
}