using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodSpawner : Singleton<FoodSpawner>
{
   public List<Food> Foods;
   
   [SerializeField] private MeshRenderer _mapRenderer;
   
   [SerializeField] private int _count;

   [SerializeField] private GameObject _prefab;

   [SerializeField] private List<Color> _colors;

   protected override void Awake()
   {
      base.Awake();
      Foods = new List<Food>();
      Spawn();
   }

   private void Respawn(Food food)
   {
      food.gameObject.SetActive(false);
      food.transform.position = GetRandomPoint();
      food.gameObject.SetActive(true);

   }

   private Vector3 GetRandomPoint()
   {
      var bounds = _mapRenderer.bounds;
      var randomX = Random.Range(bounds.max.x, bounds.min.x);
      var randomY = Random.Range(bounds.max.z, bounds.min.z);

      return new Vector3(randomX, -50, randomY);
   }
   private Color GetRandomColor() => _colors[Random.Range(0, _colors.Count - 1)];

   private void Spawn()
   {
      for (int i = 0; i < _count; i++)
      {
         var randomPoint = GetRandomPoint();
         var foodGameObject = Instantiate(_prefab, randomPoint, Quaternion.identity, transform);
         foodGameObject.GetComponentInChildren<MeshRenderer>().materials[0].color = GetRandomColor();
         foodGameObject.name = i.ToString(); 
         var food = foodGameObject.GetComponent<Food>();
         Foods.Add(food);
         food.OnEated += () => Respawn(food);
      }
   }
}
