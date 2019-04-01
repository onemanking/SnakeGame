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
    private float speed = 10;
    [SerializeField]
    private float moveDelay = 0.1f;
    [Header ("BODYPARTS")]
    public List<Transform> bodyPartsList;

    [Header ("HEAD")]
    public SpriteRenderer headSprite;

    [SerializeField]
    private GameObject bodyPrefab;

    private Direction direction;

    private float currentMoveDelay;
    // Start is called before the first frame update
    void Start ()
    {
        direction = Direction.Right;
        currentMoveDelay = moveDelay;
    }

    // Update is called once per frame
    void Update ()
    {
        currentMoveDelay -= Time.deltaTime;
        if (currentMoveDelay <= 0)
            MoveForward ();
    }

    void MoveForward ()
    {
        currentMoveDelay = moveDelay;
        transform.Translate (Vector2.right * speed * Time.deltaTime);
    }

    public void SetDirection (Direction dir)
    {
        if ((direction == Direction.Right && dir == Direction.Left || direction == Direction.Left && dir == Direction.Right) ||
            (direction == Direction.Up && dir == Direction.Down || direction == Direction.Down && dir == Direction.Up))
            return;

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

    void GrowthUp ()
    {
        GameObject bodyPart = GameObject.Instantiate (bodyPrefab) as GameObject;
        bodyPart.transform.position = bodyPartsList[bodyPartsList.Count - 1].position;
        bodyPart.transform.SetParent (transform.parent);
        bodyPartsList.Add (bodyPart.transform);
    }

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag == "Food")
        {
            int foodScore = coll.gameObject.GetComponent<Food> ().Score;
            GameManager.Instance.AddScore (foodScore);
            // GrowthUp ();
            coll.gameObject.SetActive (false);
        }

    }
}