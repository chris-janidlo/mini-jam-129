using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using crass;

public class MonsterSpawner : MonoBehaviour
{
    public int InitialSpawnBurst;
    public Vector2Int SpawnXRange, SpawnYRange;
    public int MinDistanceFromPlayer;

    public LayerMask AvoidLayers;
    public AnimationCurve DelayByDifficulty;
    public int MaxMonstersSpawned;

    public BagRandomizer<Monsters> MonsterBag;

    public SnakeMonster MonsterPrefab;
    public Vector2Variable PlayerPosition;

    public IntVariable Difficulty;

    float spawnTimer;

    void Start()
    {
        for (var _ = 0; _ < InitialSpawnBurst; _++)
        {
            StartCoroutine(spawnRoutine());
        }
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0 && SnakeMonster.NumSpawned <= MaxMonstersSpawned)
        {
            spawnTimer = DelayByDifficulty.Evaluate(Difficulty.Value);
            StartCoroutine(spawnRoutine());
        }
    }

    IEnumerator spawnRoutine()
    {
        Vector2Int target;
        do
        {
            yield return null;
            target = new Vector2Int(
                RandomExtra.Range(SpawnXRange),
                RandomExtra.Range(SpawnYRange)
            );
        }
        while (
            Vector2.Distance(PlayerPosition.Value, target) < MinDistanceFromPlayer
            || Physics2D.OverlapPoint(target, AvoidLayers) != null
        );

        var monster = Instantiate(MonsterPrefab, (Vector2)target, Quaternion.identity);
        monster.Init(MonsterBag.GetNext());
    }
}
