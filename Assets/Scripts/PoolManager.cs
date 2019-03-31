using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance = null;
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PoolManager)) as PoolManager;
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<PoolManager>();
                    go.name = "PoolManager";
                }
            }
            return _instance;
        }
    }

    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private int pooledAmount = 15;

    private List<Food> foodList;

    public void Init()
    {
        foodList = new List<Food>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject go = Instantiate(foodPrefab);
            go.SetActive(false);
            Food food = go.GetComponent<Food>();
            foodList.Add(food);
        }
    }

    public Food GetFoodObject()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            if (!foodList[i].gameObject.activeInHierarchy)
            {
                return foodList[i];
            }
        }

        GameObject go = Instantiate(foodPrefab);
        Food food = go.GetComponent<Food>();
        foodList.Add(food);
        return food;
    }

    // Update is called once per frame
    void Update()
    {

    }
}