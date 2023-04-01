using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeHead : MonoBehaviour
{
    public float MoveSpeed;
    public Vector2Int InitialMoveDirection;

    public new Rigidbody2D rigidbody;

    Vector2Int latestInput, lockedInput, destination;

    void Start()
    {
        latestInput = lockedInput = InitialMoveDirection;

        destination = Vector2Int.RoundToInt(rigidbody.position);
        rigidbody.MovePosition(destination); // ensure we start on the grid
    }

    void FixedUpdate()
    {
        if (rigidbody.position == destination)
        {
            lockedInput = latestInput;
            destination += lockedInput;
        }

        var newPos = Vector2.MoveTowards(rigidbody.position, destination, MoveSpeed * Time.deltaTime);
        rigidbody.MovePosition(newPos);
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
}
