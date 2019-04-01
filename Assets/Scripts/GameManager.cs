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
                _instance = FindObjectOfType (typeof (GameManager)) as GameManager;
                if (_instance == null)
                {
                    GameObject go = new GameObject ();
                    _instance = go.AddComponent<GameManager> ();
                    go.name = "GameManager";
                }
            }

            return _instance;
        }
    }

    public GameObject borderPrefab;

    [Header ("PREFABS")]
    [SerializeField]
    private Snake snakePrefab;
    [SerializeField]
    private GameObject boundPrefab;
    [SerializeField]
    private Food foodPrefab;

    private GameObject topBorder;
    private GameObject bottomBorder;
    private GameObject leftBorder;
    private GameObject rightBorder;

    private GameObject blockParent;

    public int row;
    public int colume;
    public List<GameObject> backgroundList;
    public List<GameObject> gameBlockList = new List<GameObject> ();
    // Start is called before the first frame update
    void Start ()
    {
        InitLevel ();
        PoolManager.Instance.Init ();
        SpawnPlayerSnake ();
        int foodBlockNumber = 114;
        SpawnFood (gameBlockList[foodBlockNumber]);
    }

    public void InitLevel ()
    {
        blockParent = new GameObject ();
        blockParent.name = "BlockParent";
        GameObject go = null;
        for (int y = 0; y < colume; y++)
        {
            for (int x = 0; x < row; x++)
            {
                if (x % 2 == 0)
                {
                    if (y % 2 == 0)
                    {
                        go = Instantiate (backgroundList[0], new Vector2 (1 * x, 1 * y), Quaternion.identity);
                    }
                    else
                    {
                        go = Instantiate (backgroundList[1], new Vector2 (1 * x, 1 * y), Quaternion.identity);
                    }
                }
                else
                {
                    if (y % 2 == 0)
                    {
                        go = Instantiate (backgroundList[1], new Vector2 (1 * x, 1 * y), Quaternion.identity);
                    }
                    else
                    {
                        go = Instantiate (backgroundList[0], new Vector2 (1 * x, 1 * y), Quaternion.identity);
                    }
                }

                gameBlockList.Add (go);
                go.transform.SetParent (blockParent.transform);
            }
        }
        // Instantiate(backgroundPrefab, Vector2.zero, Quaternion.identity);
        // topBorder = Instantiate(borderPrefab, Vector3.zero, Quaternion.identity);
    }

    public void SpawnPlayerSnake ()
    {
        Snake playerSnake = Instantiate (snakePrefab, new Vector2 (4, 7), Quaternion.identity);
        playerSnake.Init ();
        PlayerController.Instance.SetControllSnake (playerSnake);
    }

    public void SpawnFood ()
    {
        Vector2 randomPosition = gameBlockList[Random.Range (0, gameBlockList.Count)].transform.position;
        Food food = PoolManager.Instance.GetFoodObject ();
        food.transform.position = randomPosition;
        food.gameObject.SetActive (true);
    }

    public void SpawnFood (GameObject gameBlock)
    {
        Vector2 position = gameBlockList.Find(x => x == gameBlock).transform.position;
        Food food = PoolManager.Instance.GetFoodObject ();
        food.transform.position = position;
        food.gameObject.SetActive (true);
    }

    public void AddScore (int score)
    {
        
    }
}