using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class Walker : MonoBehaviour
{
    public Vector2 SpeedRange = new Vector2(140, 160);
    public Vector2 AmplitudeRange = new Vector2(7, 13);
    public Vector2 StartTimeRange = new Vector2(0, 10);

    public Rigidbody2D Rigidbody;

    [Tooltip("Should be positioned such that the local origin of the sprite is the center of its legs.")]
    public Transform SpritePivot;

    IEnumerator Start()
    {
        var speed = RandomExtra.Range(SpeedRange);
        var amplitude = RandomExtra.Range(AmplitudeRange);
        var timer = RandomExtra.Range(StartTimeRange);

        var prevPosition = Rigidbody.position;

        yield return null;

        while (true)
        {
            if (Rigidbody.position == prevPosition)
            {
                yield return null;
                continue;
            }

            prevPosition = Rigidbody.position;

            timer += Time.deltaTime;
            timer = Mathf.Repeat(timer, 360f);

            var angle = Mathf.Sin(timer * speed) * amplitude;
            SpritePivot.localEulerAngles = Vector3.forward * angle;

            yield return null;
        }
    }
}
