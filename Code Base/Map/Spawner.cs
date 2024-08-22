using System.Collections.Generic;
using UnityEngine;

namespace Code_Base.Map
{
    public abstract class Spawner: Singleton<EnemySpawner>
    {
        [SerializeField] protected MeshRenderer _mapRenderer;
   
        [SerializeField] protected int _count;

        [SerializeField] protected GameObject _prefab;

        [SerializeField] protected List<Color> _colors;

        public abstract void Spawn();
        
        protected Vector3 GetRandomPoint()
        {
            var bounds = _mapRenderer.bounds;
            var randomX = Random.Range(bounds.max.x, bounds.min.x);
            var randomY = Random.Range(bounds.max.z, bounds.min.z);

            return new Vector3(randomX, -50, randomY);
        }
        protected Color GetRandomColor() => _colors[Random.Range(0, _colors.Count - 1)];
    }
}