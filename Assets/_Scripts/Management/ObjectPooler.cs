using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;


    private void Awake() 
    {
        Instance = this;
    }
    #endregion


    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    private void OnEnable() 
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) 
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) 
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(0, 0, 0);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);

        }
    }


    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent) 
    {
        if (!poolDictionary.ContainsKey(tag)) 
        {
            Debug.LogWarning("Key doesn't exist in objectPooler:" + tag);
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.SetParent(parent, false);
        objectToSpawn.transform.localPosition = new Vector3(position.x, 50, position.z);
        objectToSpawn.transform.DOLocalMove(position, .5f);
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }


    public void ReturnToPool(string tag, GameObject objectToPool) 
    {
        if (!poolDictionary.ContainsKey(tag)) 
        {
            Debug.LogWarning("Key doesn't exist in poolDictionary:" + tag);
        }

        poolDictionary[tag].Enqueue(objectToPool);

        objectToPool.SetActive(false);
        objectToPool.transform.SetParent(transform, false);
        objectToPool.transform.localPosition = new Vector3(0, 0, 0);
        objectToPool.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}