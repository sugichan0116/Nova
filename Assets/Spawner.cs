using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    float lotteryPerSecond = 1f;
    [SerializeField]
    int lotteryTimes = 10;
    private int lotteryResidue = 0;

    [SerializeField]
    List<SpawnData> list = new List<SpawnData>();
    [SerializeField]
    Collider2D spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        Reflesh();

        var box = new LotteryBox(list.Select(i => i.weight));

        var collider = GetComponent<Collider2D>();
        spawnArea = spawnArea ?? collider;
        var player = Player.Instance.GetComponent<Collider2D>();

        // Bounds of the sphere
        Bounds b = spawnArea.bounds;

        Observable
            .Interval(TimeSpan.FromSeconds(1f / lotteryPerSecond))
            .Where(_ => lotteryResidue > 0)
            .Where(_ => player != null && collider.IsTouching(player))
            .Subscribe(_ => {
                // Get a random point inside the bounds
                var target = new Vector2(
                    Random.Range(b.min.x, b.max.x),
                    Random.Range(b.min.y, b.max.y)
                );

                Vector3 offset = spawnArea.ClosestPoint(target);

                var original = list.ElementAt(box.GetIndex()).obj;
                Instantiate(original, offset, transform.rotation);

                lotteryResidue--;
            })
            .AddTo(this);
    }

    public void Reflesh()
    {
        lotteryResidue = lotteryTimes;
    }
}

[System.Serializable]
public class SpawnData
{
    public GameObject obj;
    public float weight;
}

public class LotteryBox
{
    private IEnumerable<float> weight;

    public LotteryBox(IEnumerable<float> w)
    {
        weight = w;
    }

    public int GetIndex()
    {
        return Random.Range(0, weight.Count());
    }
}