using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityLogic.GamePlay.Pool;
using Random = UnityEngine.Random;

namespace UnityLogic.GamePlay.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyBase> enemies;
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private float delayBtwSpawn;

        private Dictionary<Type, EnemyPool> _pools;
        private Coroutine _spawnCoroutine;
        private new Transform transform;
        private GamePlayManager _gamePlayManager;

        private List<EnemyBase> _spawnedEnemies;

        private class EnemyPool
        {
            public readonly EnemyBase EnemyPrefab;
            public readonly Pool<EnemyBase> Pool;

            public EnemyPool(EnemyBase enemyPrefab)
            {
                EnemyPrefab = enemyPrefab;
                Pool = new Pool<EnemyBase>(EnemyPrefab);
            }
        }
        private void Awake()
        {
            transform = GetComponent<Transform>();
            _gamePlayManager = GameCore.Instance.GetManager<GamePlayManager>();
            InitializePool();
            _spawnedEnemies = new List<EnemyBase>();
        }
        private void InitializePool()
        {
            _pools = new Dictionary<Type, EnemyPool>();
            foreach (var enemy in enemies)
            {
                var type = enemy.GetType();
                if (_pools.ContainsKey(type))
                {
                    continue;
                }
                var newPool = new EnemyPool(enemy);
                _pools.Add(type, newPool);
            }
        }
        public void StartSpawn()
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
        public void StopSpawn()
        {
            StopCoroutine(_spawnCoroutine);
            // Return spawned enemies
            var spawnedEnemies = new List<EnemyBase>(_spawnedEnemies);
            foreach (var enemy in spawnedEnemies)
            {
                ReturnEnemyToPool(enemy);
            }
        }
        public void SpawnSmallAsteroids(in Vector3 position)
        {
            var pool = _pools[typeof(SmallAsteroid)];
            var count = Random.Range(2, 3);
            for (int i = 0; i < count; i++)
            {
                SpawnEnemyFromPool(pool, position);
            }
        }
        private IEnumerator SpawnCoroutine()
        {
            var delay = new WaitForSeconds(delayBtwSpawn);
            yield return delay;
            while (true)
            {
                var randomPool = GetRandomPool();
                var spawnPoint = GetRandomSpawnPoint();
                SpawnEnemyFromPool(randomPool, spawnPoint.position, spawnPoint);
                yield return delay;
            }
        }
        private void SpawnEnemyFromPool(in EnemyPool pool, in Vector3 position,
            in Transform exclusiveSpawnPoint = null)
        {
            var enemy = pool.Pool.Get();
            enemy.transform.position = position;
            enemy.transform.SetParent(transform);
            enemy.SetTarget(GetEnemyTargetTransform(enemy, exclusiveSpawnPoint), ReturnEnemyToPool);
            enemy.gameObject.SetActive(true);
            _spawnedEnemies.Add(enemy);
        }
        private Transform GetEnemyTargetTransform(in EnemyBase enemy, in Transform spawnPoint = null)
        {
            switch (enemy.FollowType)
            {
                case EnemyBase.FollowingType.PlayerFollower:
                    return _gamePlayManager.GetPlayerTransform();
                case EnemyBase.FollowingType.RandomPoint:
                    return GetRandomSpawnPoint(spawnPoint);
                default:
                    return null;
            }
        }
        private Transform GetRandomSpawnPoint(in Transform exclusiveTransform = null)
        {
            if (spawnPoints.Count <= 1)
            {
                throw new Exception("Spawn point array not specified");
            }

            Transform result = GetRandomPoint();
            while (result == exclusiveTransform)
            {
                result = GetRandomPoint();
            }
            return result;
            
            Transform GetRandomPoint() => spawnPoints[Random.Range(0, spawnPoints.Count)];
        }
        private EnemyPool GetRandomPool()
        {
            var pools = _pools.Values.ToList();
            return pools[Random.Range(0, pools.Count)];
        }
        private void ReturnEnemyToPool(EnemyBase enemy)
        {
            var type = enemy.GetType();
            if (_pools.TryGetValue(type, out var pool))
            {
                pool.Pool.Add(enemy);
            }
            _spawnedEnemies.Remove(enemy);
        }
    }
}