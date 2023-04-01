using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeHead : MonoBehaviour
{
    public float MoveSpeed;

    Vector2Int latestInput = Vector2Int.right;
    Vector2Int lockedInput = Vector2Int.right;

    void Start()
    {
        StartCoroutine(MovementRoutine());
    }

    void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        if (input == Vector2.zero) return;

        var candidateInput = Mathf.Abs(input.x) > Mathf.Abs(input.y)
            ? new Vector2Int(Math.Sign(input.x), 0)
            : new Vector2Int(0, Math.Sign(input.y));

        // can't turn 180 degrees
        if (candidateInput != -lockedInput) latestInput = candidateInput;
    }

    IEnumerator MovementRoutine()
    {
        Vector2Int destination = Vector2Int.RoundToInt(transform.position);
        transform.position = (Vector2)destination; // ensure we start on the grid

        while (true)
        {
            lockedInput = latestInput;
            destination += lockedInput;

            while ((Vector2)transform.position != destination)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, MoveSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
