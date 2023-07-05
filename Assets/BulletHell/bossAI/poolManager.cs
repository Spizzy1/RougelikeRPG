using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class poolManager : MonoBehaviour
{
    
    [SerializeField]
    List<bulletPoolConfig> bulletPoolConfigs = new List<bulletPoolConfig>();
    Dictionary<GameObject, bulletPool> bulletPools = new Dictionary<GameObject, bulletPool> ();

    // Start is called before the first frame update
    private void Start()
    {
        foreach(bulletPoolConfig config in bulletPoolConfigs)
        {
            bulletPools.Add(config.bulletPrefab, new bulletPool(config));
        }
    }
    public GameObject requestBullet(GameObject prefab)
    {
        return bulletPools[prefab].requestBullet();
    }
    public void unrequestBullet(GameObject removeObject)
    {
        bulletPools[removeObject.GetComponent<moveBullet>().prefab].unrequestBullet(removeObject);
    }
    public void resetAll()
    {
        foreach(bulletPool pools in bulletPools.Values)
        {
            pools.resetAll();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public class bulletPool
    {
        int size;
        List<GameObject> pool;
        int usedBullets = 0;
        GameObject prefab;
        public bulletPool(bulletPoolConfig poolConfigs)
        {
            size = poolConfigs.bulletCount;
            pool = new List<GameObject>();
            for (int i = 0; i < poolConfigs.bulletCount; i++)
            {
                prefab = poolConfigs.bulletPrefab;
                pool.Add(Instantiate(prefab));
                pool[i].GetComponent<moveBullet>().prefab = prefab;
                DontDestroyOnLoad(pool[i]);
                pool[i].SetActive(false);
            }
        }
        public GameObject requestBullet()
        {
            GameObject saveBullet;
            if (usedBullets >= size)
            {
                pool.Add(Instantiate(prefab));
                pool[usedBullets].GetComponent<moveBullet>().prefab = prefab;
                DontDestroyOnLoad(pool[usedBullets]);
                pool[usedBullets].SetActive(true);
                size++;
                print(size);
            }
            saveBullet = pool[usedBullets];
            saveBullet.GetComponent<moveBullet>().index = usedBullets;
            usedBullets++;
            saveBullet.SetActive(true);
            return saveBullet;
        }
        public void unrequestBullet(GameObject removeObject)
        {
            if (removeObject.activeSelf == true)
            {
                int bulletIndex = removeObject.GetComponent<moveBullet>().index;
                removeObject.SetActive(false);
                usedBullets--;
                (pool[bulletIndex], pool[usedBullets]) = (pool[usedBullets], pool[bulletIndex]);
                pool[bulletIndex].GetComponent<moveBullet>().index = bulletIndex;
            }
        }
        public void resetAll()
        {
            for (int i = 0; i < usedBullets; i++)
            {
                pool[i].SetActive(false);
            }
            usedBullets = 0;
        }
    }
    [System.Serializable]
    public class bulletPoolConfig
    {
        public GameObject bulletPrefab;
        public int bulletCount;
    }
}
