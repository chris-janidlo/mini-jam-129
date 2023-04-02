using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMonster : MonoBehaviour
{
    public string IdleLayerName, FollowLayerName;
    public float IdleColliderSize, FollowColliderSize;

    public new Rigidbody2D rigidbody;
    public new BoxCollider2D collider;

    public bool Following { get; private set; }

    public void StartFollow()
    {
        Following = true;
        gameObject.layer = LayerMask.NameToLayer(FollowLayerName);
        collider.size = Vector2.one * FollowColliderSize;
    }

    public void StopFollow()
    {
        gameObject.layer = LayerMask.NameToLayer(IdleLayerName);
        collider.size = Vector2.one * IdleColliderSize;
        throw new System.NotImplementedException();
    }
}
