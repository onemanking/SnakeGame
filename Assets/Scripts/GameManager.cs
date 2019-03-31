using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<GameManager>();
                    go.name = "GameManager";
                }
            }

            return _instance;
        }
    }

    public GameObject borderPrefab;

    [Header("PREFABS")]
    [SerializeField]
    private Snake snakePrefab;
    [SerializeField]
    private GameObject backgroundPrefab;
    [SerializeField]
    private GameObject logoDtacPrefab;
    [SerializeField]
    private GameObject boundPrefab;
    [SerializeField]
    private Food foodPrefab;
    [SerializeField]
    private GameObject botNormalSnakePrefab;
    [SerializeField]
    private GameObject[] botSpecialSnakePrefab;
    private GameObject topBorder;
    private GameObject bottomBorder;
    private GameObject leftBorder;
    private GameObject rightBorder;


    public int row;
    public int colume;
    public List<GameObject> backgroundList;
    public List<GameObject> gameBlockList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InitLevel();
        PoolManager.Instance.Init();
        SpawnPlayerSnake();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFood();
        }
    }

    public void InitLevel()
    {
        GameObject go = null;
        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < colume; y++)
            {
                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        go = Instantiate(backgroundList[0], new Vector2(1 * x, 1 * y), Quaternion.identity);
                    }
                    else
                    {
                        go = Instantiate(backgroundList[1], new Vector2(1 * x, 1 * y), Quaternion.identity);
                    }
                }
                else
                {
                    if (x % 2 == 0)
                    {
                        go = Instantiate(backgroundList[1], new Vector2(1 * x, 1 * y), Quaternion.identity);
                    }
                    else
                    {
                        go = Instantiate(backgroundList[0], new Vector2(1 * x, 1 * y), Quaternion.identity);
                    }
                }

                gameBlockList.Add(go);
            }
        }
        // Instantiate(backgroundPrefab, Vector2.zero, Quaternion.identity);
        // topBorder = Instantiate(borderPrefab, Vector3.zero, Quaternion.identity);
    }

    public void SpawnPlayerSnake()
    {
        Snake playerSnake = Instantiate(snakePrefab, Vector3.zero, Quaternion.identity);
        PlayerController.Instance.SetControllSnake(playerSnake);
    }

    public void SpawnFood()
    {
        Vector2 randomPosition = gameBlockList[Random.Range(0, gameBlockList.Count)].transform.position;
        Food food = PoolManager.Instance.GetFoodObject();
        food.transform.position = randomPosition;
        food.gameObject.SetActive(true);
    }

    public void AddScore(int score)
    {

    }
}
