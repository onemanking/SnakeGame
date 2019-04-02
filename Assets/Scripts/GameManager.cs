using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentState
{
    Standby,
    Playing,
    Gameover
}

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

    private GameObject blockParent;
    private GameObject borderParent;

    public int row;
    public int colume;
    public List<GameObject> backgroundList;
    public List<GameObject> gameBlockList = new List<GameObject> ();

    public CurrentState currentState;

    private int score;

    // Start is called before the first frame update
    void Start ()
    {
        InitLevel ();
        PoolManager.Instance.Init ();
        SpawnPlayerSnake ();
        SpawnFood (gameBlockList[Defines.FOOD_BLOCK_NUMBER]);
    }

    public void StartGame ()
    {
        currentState = CurrentState.Playing;
    }

    public void Gameover ()
    {
        currentState = CurrentState.Gameover;
        MenuManager.Instance.Gameover ();
    }

    public void Replay ()
    {
        InitNewGame ();
        StartGame ();
    }

    void InitNewGame ()
    {
        score = 0;
        MenuManager.Instance.ScoreTextChange (score);
        SpawnPlayerSnake ();
        ClearAllFood ();
        SpawnFood (gameBlockList[Defines.FOOD_BLOCK_NUMBER]);
		AudioManager.Instance.PlayBGM ("MainMenu");
    }

    public void InitLevel ()
    {
        // SPAWN BACKGROUND
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

        // SPAWN BORDER
        borderParent = new GameObject ();
        borderParent.name = "BorderParent";
        for (int y = -1; y < colume + 1; y++)
        {
            for (int x = -1; x < row + 1; x++)
            {
                if (x == -1 || x == row + 1 || y == -1 || y == colume + 1 || x == row || y == row)
                {
                    go = Instantiate (borderPrefab, new Vector2 (1 * x, 1 * y), Quaternion.identity);
                    go.transform.SetParent (borderParent.transform);
                }
            }
        }
    }

    private void SpawnPlayerSnake ()
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
        Vector2 position = gameBlockList.Find (x => x == gameBlock).transform.position;
        Food food = PoolManager.Instance.GetFoodObject ();
        food.transform.position = position;
        food.gameObject.SetActive (true);
    }

    public void ClearAllFood ()
    {
        PoolManager.Instance.DisableAllObject ();
    }

    public void AddScore (int _score)
    {
        score += _score;
        MenuManager.Instance.ScoreTextChange (score);
        AudioManager.Instance.PlaySoundEffect ("Reward");
    }
}