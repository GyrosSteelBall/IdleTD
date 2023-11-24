using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Unit,
    Enemy,
    // ... add other entity types here
}

public class EntityPoolManager : MonoBehaviour
{
    public static EntityPoolManager Instance { get; private set; }

    [System.Serializable]
    public struct EntityPoolConfig
    {
        public EntityType entityType;
        public GameObject prefab;
        public int initialPoolSize;
    }

    [SerializeField]
    private EntityPoolConfig[] entityPoolConfigs;

    private Dictionary<EntityType, Queue<GameObject>> poolDictionary = new Dictionary<EntityType, Queue<GameObject>>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePools();
        }
    }

    private void InitializePools()
    {
        foreach (EntityPoolConfig pool in entityPoolConfigs)
        {
            Queue<GameObject> entityQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.initialPoolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                entityQueue.Enqueue(obj);
            }
            poolDictionary.Add(pool.entityType, entityQueue);
        }
    }

    public GameObject SpawnFromPool(EntityType entityType, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(entityType))
        {
            Debug.LogError($"Pool with EntityType '{entityType}' doesn't exist.");
            return null;
        }

        if (poolDictionary[entityType].Count == 0)
        {
            // Find the pool corresponding to the entityType
            foreach (var pool in entityPoolConfigs)
            {
                if (pool.entityType == entityType)
                {
                    // Instantiate a new object because the pool is empty
                    GameObject newObj = Instantiate(pool.prefab);
                    newObj.transform.position = position;
                    newObj.transform.rotation = rotation;
                    newObj.SetActive(true);

                    // Optionally add the new item to the pool for future reuse
                    poolDictionary[entityType].Enqueue(newObj);  // Comment this line if you don't want to add it back to the pool

                    // Return the newly instantiated object
                    return newObj;
                }
            }

            // Handle case where the entityType does not have a prefab defined
            Debug.LogError($"No prefab defined for EntityType '{entityType}'");
            return null;
        }

        // Normal pool behavior, extract an object from the pool
        GameObject entityObject = poolDictionary[entityType].Dequeue();
        entityObject.SetActive(true);
        entityObject.transform.position = position;
        entityObject.transform.rotation = rotation;

        // Initialize if necessary
        IPoolable poolableEntity = entityObject.GetComponent<IPoolable>();
        poolableEntity?.OnSpawn();

        return entityObject;
    }

    public void ReturnToPool(EntityType entityType, GameObject entityObject)
    {
        entityObject.SetActive(false);
        poolDictionary[entityType].Enqueue(entityObject);
    }
}
