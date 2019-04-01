using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class Snake : MonoBehaviour
{
    private const int Y = 0;
    [Header ("MOVEMENT")]
    [SerializeField]
    private float moveDelay = 0.08f;
    [Header ("BODYPARTS")]
    public List<Transform> bodyPartsList;

    [SerializeField]
    private GameObject bodyPrefab;

    private Direction direction;

    private float currentMoveDelay;

    // Start is called before the first frame update
    void Start ()
    {
        direction = Direction.Right;
    }

    // Update is called once per frame
    void Update ()
    {
        currentMoveDelay += Time.deltaTime;

        if (currentMoveDelay >= moveDelay)
        {
            MoveForward ();
        }
    }

    private List<Vector2> snakeMovePositionList = new List<Vector2> ();
    void MoveForward ()
    {
        currentMoveDelay = 0;
        transform.Translate (Vector2.right);

        snakeMovePositionList.Insert (0, transform.position);
        if (snakeMovePositionList.Count > bodyPartsList.Count + 1)
        {
            snakeMovePositionList.RemoveAt (snakeMovePositionList.Count - 1);
        }

        if (bodyPartsList.Count > 0)
        {
            for (int i = 0; i < bodyPartsList.Count; i++)
            {
                bodyPartsList[i].position = snakeMovePositionList[i];
            }
        }
    }

    public void Init ()
    {
        if (bodyPartsList.Count != Defines.DEFAULT_NUMBER_OF_SNAKE_BODY)
        {
            bodyPartsList = new List<Transform> ();
            bodyPartsList.Add (transform);
            snakeMovePositionList.Add (transform.position);
            for (int i = 0; i < Defines.DEFAULT_NUMBER_OF_SNAKE_BODY; i++)
            {
                GameObject bodyPart = GameObject.Instantiate (bodyPrefab) as GameObject;

                bodyPart.transform.position = new Vector2 (transform.position.x - (i + 1), transform.position.y);
                snakeMovePositionList.Add (bodyPart.transform.position);
                bodyPartsList.Add (bodyPart.transform);
            }
        }
    }

    public void SetDirection (Direction dir)
    {
        if ((direction == Direction.Right && dir == Direction.Left || direction == Direction.Left && dir == Direction.Right) ||
            (direction == Direction.Up && dir == Direction.Down || direction == Direction.Down && dir == Direction.Up))
        {
            return;
        }

        direction = dir;
        switch (dir)
        {
            case Direction.Up:
                transform.localRotation = Quaternion.Euler (0, 0, 90);
                break;
            case Direction.Down:
                transform.localRotation = Quaternion.Euler (0, 0, -90);
                break;
            case Direction.Left:
                transform.localRotation = Quaternion.Euler (0, 0, 180);
                break;
            case Direction.Right:
                transform.localRotation = Quaternion.Euler (0, 0, 0);
                break;
        }
    }

    void Death ()
    {
        
    }

    void EatFood (Vector2 position)
    {
        GameManager.Instance.SpawnFood ();
        GameObject bodyPart = GameObject.Instantiate (bodyPrefab) as GameObject;
        if (bodyPartsList.Count == 0)
        {
            bodyPart.transform.position = position;
        }
        else
        {
            bodyPart.transform.position = bodyPartsList[bodyPartsList.Count - 1].position;
        }
        bodyPartsList.Add (bodyPart.transform);
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Snake")
        {
            Death();
        }
        else if (coll.gameObject.tag == "Food")
        {
            int foodScore = coll.gameObject.GetComponent<Food> ().Score;
            GameManager.Instance.AddScore (foodScore);
            EatFood (coll.transform.position);
            coll.gameObject.SetActive (false);
        }
    }
}