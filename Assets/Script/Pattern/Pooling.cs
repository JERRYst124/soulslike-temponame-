using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class Trolling
{
    public string Key;
    public List<PoolingObject> ValueList;

    public Trolling(string key, List<PoolingObject> valueList)
    {
        this.Key = key;
        this.ValueList = valueList;
    }
}

public class Pooling : MonoBehaviour
{
    public static Pooling Instance { get { return instance; } }
    private static Pooling instance;

    public Dictionary<string, List<PoolingObject>> PoolingDictionary
                        = new Dictionary<string, List<PoolingObject>>();
    [SerializeField] private int InitCapacity;
    [SerializeField] private int MaxCapacity;
    public List<Trolling> ShowList;
    private void Awake()
    {
        instance = this;
    }
    public void InitPool(List<PoolingObject> poolingTypes)
    {
        foreach (var poolingType in poolingTypes)
        {
            if (!PoolingDictionary.ContainsKey(poolingType.name))
            {
                PoolingDictionary.Add(poolingType.name, new List<PoolingObject>());
            }
            for (int i = 0; i < InitCapacity; i++)
            {
                //     Create(poolingType);
            }
        }
    }
    public PoolingObject Create(PoolingObject PoolingType, Transform parent)
    {

        PoolingObject Instance = Instantiate(PoolingType, parent);
        Instance.name = PoolingType.name;
        Instance.pool = this;
        Instance.gameObject.SetActive(false);
        if (!PoolingDictionary.ContainsKey(PoolingType.name))
        {
            PoolingDictionary.Add(PoolingType.name, new List<PoolingObject>());
        }
        PoolingDictionary[PoolingType.name].Add(Instance);
        return Instance;
    }
    public PoolingObject GetPoolingObject(GameObject gameObject, Transform parent)
    {
        PoolingObject PoolingType = gameObject.GetComponent<PoolingObject>();
        PoolingObject instance;
        if (!PoolingDictionary.ContainsKey(PoolingType.name))
        {
            PoolingDictionary.Add(PoolingType.name, new List<PoolingObject>());
        }
        if (PoolingDictionary[PoolingType.name].Count == 0)
        {

            instance = Create(PoolingType, parent);
        }
        else
        {
            instance = PoolingDictionary[PoolingType.name].ElementAt(PoolingDictionary[PoolingType.name].Count - 1);
        }
        PoolingDictionary[PoolingType.name].Remove(instance);
        instance.gameObject.SetActive(true);
        return instance;
    }
    public PoolingObject ReleasePoolingObject(GameObject gameObject)
    {
        PoolingObject PoolingType = gameObject.GetComponent<PoolingObject>();
        if (!PoolingDictionary.ContainsKey(PoolingType.name))
        {
            PoolingDictionary.Add(PoolingType.name, new List<PoolingObject>());
        }
        PoolingType.gameObject.SetActive(false);
        PoolingDictionary[PoolingType.name].Add(PoolingType);
        return PoolingType;
    }
    void Start()
    {
        List<PoolingObject> poolingTypes = new List<PoolingObject>(); // Add your prefab references here
        InitPool(poolingTypes);
    }
    private float DestroyTime = 5f;
    private float MyTime;
    // Update is called once per frame
    void Update()
    {
        MyTime += Time.deltaTime;
        if (MyTime >= DestroyTime)
        {
            MyTime -= DestroyTime;
            ShowList.Clear();
            foreach (var target in PoolingDictionary)
            {
                ShowList.Add(new Trolling(target.Key, target.Value));
            }

        }
    }
}
