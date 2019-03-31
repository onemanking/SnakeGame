using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance = null;
    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PlayerController)) as PlayerController;
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<PlayerController>();
                    go.name = "PlayerController";
                }
            }
            return _instance;
        }
    }

    private Direction direction;

    private Snake controllSnake;
    public Snake ControllSnake { get => controllSnake; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetAxis("Horizontal") > 0)
        // {
        //     direction = Direction.Right;
        // }
        // else if (Input.GetAxis("Horizontal") < 0)
        // {
        //     direction = Direction.Left;
        // }

        // if (Input.GetAxis("Vertical") > 0)
        // {
        //     direction = Direction.Up;
        // }
        // else if (Input.GetAxis("Vertical") < 0)
        // {
        //     direction = Direction.Down;
        // }

        if (Input.GetKey(KeyCode.D))
        {
            direction = Direction.Right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            direction = Direction.Left;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction = Direction.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = Direction.Down;
        }

        if (controllSnake)
        {
            controllSnake.SetDirection(direction);
        }
    }

    public void SetControllSnake(Snake snake)
    {
        controllSnake = snake;
    }
}
