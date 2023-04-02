using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;
using DG.Tweening;

public class SnakeMonster : MonoBehaviour
{
    public static int NumSpawned { get; private set; }

    public Vector2 DispersalDistanceRange;
    public Color DispersalColor;
    public float DisperseTime;
    public Ease DisperseEase;

    public float SpawnAnimTime;
    public Ease SpawnAnimEase;

    public string IdleLayerName, FollowLayerName;
    public float IdleColliderSize, FollowColliderSize;

    public LayerMask DispersalAvoidLayers;
    public Transform SpriteTransform;
    public SpriteRenderer Sprite;

    public new Rigidbody2D rigidbody;
    public new BoxCollider2D collider;

    public IconMap IconMap;

    public bool Following { get; private set; }

    void OnDestroy()
    {
        NumSpawned--;
    }

    public void Init(Monsters monster)
    {
        NumSpawned++;
        Sprite.sprite = IconMap.GetSprite(monster);

        transform.localScale = Vector3.zero;
        transform.DOScale(1, SpawnAnimTime).SetEase(SpawnAnimEase);
    }

    public void StartFollow()
    {
        Following = true;
        gameObject.layer = LayerMask.NameToLayer(FollowLayerName);
        collider.size = Vector2.one * FollowColliderSize;
    }

    public void Disperse()
    {
        StartCoroutine(disperseRoutine());
    }

    public void TurnInForQuest()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator disperseRoutine()
    {
        var oldColor = Sprite.color;
        Sprite.color = DispersalColor;
        collider.enabled = false;

        Vector2Int target;
        while (true)
        {
            var range = RandomExtra.Range(DispersalDistanceRange);
            var floatPoint = rigidbody.position + Random.insideUnitCircle.normalized * range;
            var gridPoint = Vector2Int.RoundToInt(floatPoint);

            if (Physics2D.OverlapPoint(gridPoint, DispersalAvoidLayers))
            {
                yield return null;
                continue;
            }
            else
            {
                target = gridPoint;
                break;
            }
        }

        var startPos = rigidbody.position;
        rigidbody.position = target;

        gameObject.layer = LayerMask.NameToLayer(IdleLayerName);
        collider.enabled = true;
        collider.size = Vector2.one * IdleColliderSize;
        
        SpriteTransform.position = startPos;
        yield return SpriteTransform
            .DOMove((Vector2)target, DisperseTime)
            .SetEase(DisperseEase)
            .WaitForCompletion();

        Following = false;
        Sprite.color = oldColor;
    }
}
