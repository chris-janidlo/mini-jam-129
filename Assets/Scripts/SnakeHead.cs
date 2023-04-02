using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeHead : MonoBehaviour
{
    public float LerpSpeed;
    public int MaxFollowers;
    public Vector2Int InitialMoveDirection;

    public LayerMask MovementBlockingLayers;

    public new Rigidbody2D rigidbody;

    float lerp;
    Vector2Int latestInput, currentMoveDirection;

    LinkedList<Vector2> destinationBuffer;
    List<SnakeMonster> followers;
    
    void Start()
    {
        latestInput = currentMoveDirection = InitialMoveDirection;

        destinationBuffer = new LinkedList<Vector2>();
        followers = new List<SnakeMonster>(MaxFollowers);

        var start = Vector2Int.RoundToInt(rigidbody.position);
        rigidbody.MovePosition(start); // ensure we start on the grid

        destinationBuffer.AddFirst(start);
        destinationBuffer.AddFirst(start + currentMoveDirection);
    }

    void FixedUpdate()
    {
        moveSnake();

        lerp += LerpSpeed * Time.deltaTime;

        if (lerp >= 1)
        {
            updateDestination();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.GetComponent<SnakeMonster>() is { } follower
            && !follower.Following
            && followers.Count < MaxFollowers
        )
        {
            followers.Insert(0, follower);
            follower.StartFollow();
        }
    }

    void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        if (input == Vector2.zero) return;

        var cardinalizedInput = Mathf.Abs(input.x) > Mathf.Abs(input.y)
            ? new Vector2Int(Math.Sign(input.x), 0)
            : new Vector2Int(0, Math.Sign(input.y));

        // can't turn 180 degrees
        if (cardinalizedInput == -currentMoveDirection) return;

        var potentialNextDestination = destinationBuffer.First.Value + cardinalizedInput;
        if (!validDestination(potentialNextDestination)) return;
        
        latestInput = cardinalizedInput;
    }

    void moveSnake()
    {
        var node = destinationBuffer.First;
        for (var i = -1; i < followers.Count; i++)
        {
            var newPos = Vector2.Lerp(node.Next.Value, node.Value, lerp);

            var rbToMove = i == -1
                ? rigidbody
                : followers[i].rigidbody;

            rbToMove.MovePosition(newPos);

            node = node.Next;
        }
    }

    void updateDestination()
    {
        currentMoveDirection = latestInput;

        var nextDestination = destinationBuffer.First.Value + currentMoveDirection;
        if (!validDestination(nextDestination)) return;

        destinationBuffer.AddFirst(nextDestination);
        lerp = 0;

        // need two extra destinations because each segment (including the head) is
        // lerping between 2 destinations
        if (destinationBuffer.Count > MaxFollowers + 2)
        {
            destinationBuffer.RemoveLast();
        }
    }

    bool validDestination(Vector2 query)
    {
        if (Physics2D.OverlapPoint(query, MovementBlockingLayers) is { } collider)
        {
            var monsterSegment = collider.GetComponent<SnakeMonster>();

            // special case - you can walk through the last monster segment, because it will get out of your way
            var isLastMonster = monsterSegment != null && monsterSegment.Following && monsterSegment == followers[followers.Count - 1];
            
            return isLastMonster;
        }

        return true;
    }
}
