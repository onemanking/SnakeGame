using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Food : MonoBehaviour
{
    [SerializeField]
    private int score;
    public int Score { get => score; }

    private SpriteRenderer spriteRenderer;
    private float currentRotation;
    private Vector3 currentSize;
    private float endSize;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        currentSize = Vector3.Slerp(currentSize, Vector3.one * 2, Time.deltaTime * 5);
        transform.localScale = currentSize;
    }
}
