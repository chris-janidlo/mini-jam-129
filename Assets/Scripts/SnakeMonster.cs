using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;
using DG.Tweening;
using UnityAtoms.BaseAtoms;

public class SnakeMonster : MonoBehaviour
{
    public static int NumSpawned { get; private set; }

    public Vector2 WanderTimeRange;
    public float MinDistanceFromPlayerToWander, MinDistanceFromOtherWanderersToWander;
    public LayerMask WanderAvoidLayers, OtherWanderersLayers;
    public float WanderMoveTime;
    public Ease WanderEase;

    public Vector2 DispersalDistanceRange;
    public Color DispersalColor;
    public float DisperseTime;
    public Ease DisperseEase;

    public float SpawnAnimTime;
    public Ease SpawnAnimEase;
    public float DespawnAnimTime;
    public Ease DespawnAnimEase;

    public string IdleLayerName, FollowLayerName;
    public float IdleColliderSize, FollowColliderSize;

    public AudioClip Grunt;
    public Vector2 DispersalGruntDelayRange;
    public SfxOptions GruntOptions;

    public LayerMask DispersalAvoidLayers;
    public Transform SpriteTransform;
    public SpriteRenderer Sprite;

    public new Rigidbody2D rigidbody;
    public new BoxCollider2D collider;

    public SoundEffectPlayer SoundEffectPlayer;

    public Vector2Variable PlayerPosition;

    public IconMap IconMap;

    public bool Following { get; private set; }
    public Monsters Type { get; private set; }

    float wanderTimer;
    private bool wandering;

    void OnDestroy()
    {
        NumSpawned--;
    }

    void Start()
    {
        wanderTimer = RandomExtra.Range(WanderTimeRange);
    }

    void Update()
    {
        wanderTimer -= Time.deltaTime;

        if (
            !Following
            && !wandering
            && wanderTimer <= 0
            && Vector2.Distance(rigidbody.position, PlayerPosition.Value) > MinDistanceFromPlayerToWander
            && !Physics2D.OverlapCircleAll(rigidbody.position, MinDistanceFromOtherWanderersToWander, OtherWanderersLayers).Any(c => c.GetComponent<SnakeMonster>().wandering)
        )
        {
            StartCoroutine(wanderRoutine());
        }
    }

    public void Init(Monsters monster)
    {
        NumSpawned++;
        Type = monster;
        Sprite.sprite = IconMap.GetSprite(monster);

        transform.localScale = Vector3.zero;
        transform.DOScale(1, SpawnAnimTime).SetEase(SpawnAnimEase);
    }

    public void StartFollow()
    {
        Following = true;
        gameObject.layer = LayerMask.NameToLayer(FollowLayerName);
        collider.size = Vector2.one * FollowColliderSize;
        SoundEffectPlayer.Play(Grunt, GruntOptions);
    }

    public void Disperse()
    {
        StartCoroutine(disperseRoutine());
        StartCoroutine(disperseGruntRoutine());
    }

    public void TurnInForQuest()
    {
        transform.DOScale(0, DespawnAnimTime)
            .SetEase(DespawnAnimEase)
            .OnComplete(() => Destroy(gameObject));
        
        this.enabled = false;
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
            .DOMove(rigidbody.position, DisperseTime)
            .SetEase(DisperseEase)
            .WaitForCompletion();

        Following = false;
        Sprite.color = oldColor;
    }

    IEnumerator disperseGruntRoutine()
    {
        yield return new WaitForSeconds(RandomExtra.Range(DispersalGruntDelayRange));
        SoundEffectPlayer.Play(Grunt, GruntOptions);
    }

    IEnumerator wanderRoutine()
    {
        wandering = true;

        var deltas = new List<Vector2Int>() { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
        deltas.ShuffleInPlace();

        Vector2? target = null;
        foreach (var delta in deltas)
        {
            var potentialTarget = rigidbody.position + (Vector2)delta;
            if (Physics2D.OverlapPoint(potentialTarget, WanderAvoidLayers) == null)
            {
                target = potentialTarget;
                break;
            }
        }

        if (target.HasValue)
        {
            yield return rigidbody
                .DOMove(target.Value, WanderMoveTime)
                .SetEase(WanderEase)
                .WaitForCompletion();
        }

        wandering = false;
        wanderTimer = RandomExtra.Range(WanderTimeRange);
    }
}
