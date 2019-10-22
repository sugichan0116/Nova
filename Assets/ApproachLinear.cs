using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using NaughtyAttributes;

public class TagManager
{
    public static readonly string PLAYER = "A:Player";
    public static readonly string ENEMY = "B:Enemy";
    public static readonly string NEUTRAL = "C:Neutral";
    public static string[] TAGS = new string[] { PLAYER, ENEMY, NEUTRAL };
}

public class RelationshipManager
{
    public enum Relation
    {
        NONE,
        FRIEND,
        ENEMY,
        NEUTRAL,
    }

    public static Relation Compare(string tagA, string tagB)
    {
        if (tagA == "Untagged" || tagB == "Untagged") return Relation.NONE;

        if (tagA == tagB) return Relation.FRIEND;
        if (tagA != TagManager.NEUTRAL && tagB != TagManager.NEUTRAL) return Relation.ENEMY;
        return Relation.NEUTRAL;
    }
}

public enum Personality
{
    PEACEFUL, //攻撃しない
    CAUTIONARY, //敵に攻撃する
    AGGRESSIVE, //敵と中立に攻撃する
    FEARFUL, //敵から逃げる
    //攻撃を受けると敵対
    //周りを回る
    //ストーカー一定距離を保つ
}

public class ApproachLinear : MonoBehaviour
{
    public enum State
    {
        IDLE,
        DETECT,
        WARNING,
    }


    [SerializeField]
    private Collider2D searhArea;
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private float rotateSpeed = 0.01f;

    [SerializeField]
    private float keepDistanceRadius = 4f;

    //[SerializeField]
    //[Dropdown("tags")] string targetTag;
    //private readonly string[] tags = TagManager.TAGS;
    [SerializeField]
    private Personality personality = Personality.CAUTIONARY;

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        var body = GetComponent<Rigidbody2D>();
        Transform target = null;


        //State Transition by triggers
        searhArea
            .OnTriggerEnter2DAsObservable()
            .Where(c => IsTarget(c.gameObject))
            .Subscribe(c =>
            {
                Debug.Log($"[Move AI] detected! {this}");
                state = State.DETECT;
                target = c.transform;
            })
            .AddTo(this);

        searhArea
            .OnTriggerExit2DAsObservable()
            .Where(c => IsTarget(c.gameObject))
            .Subscribe(_ =>
            {
                state = State.WARNING;
            })
            .AddTo(this);


        //Move
        this.UpdateAsObservable()
            .Where(_ => state == State.DETECT && target != null)
            .Subscribe(_ =>
            {
                body.velocity = Velocity(target);
            });

        this.UpdateAsObservable()
            .Where(_ => state == State.IDLE)
            .Subscribe(_ =>
            {
                body.velocity = Vector2.zero;
            });

        this.UpdateAsObservable()
            .Where(_ => state == State.WARNING)
            .Sample(TimeSpan.FromSeconds(1f))
            .Buffer(6)
            .Subscribe(_ =>
            {
                state = State.IDLE;
            });
    }

    private Vector2 Velocity(Transform target)
    {
        Vector2 velocity = Vector2.zero;
        var distance = target.position - transform.position;

        if (personality == Personality.FEARFUL)
        {
            distance *= -1;
        }

        if (distance.magnitude > 0f)//keepDistanceRadius)
        {
            var direction = transform.rotation * Vector3.up * speed;
            var r = Vector3.Cross(distance, direction).z;
            velocity = Quaternion.Euler(0, 0, -rotateSpeed * r) * direction;
        }

        return velocity;
    }

    private bool IsTarget(GameObject obj)
    {
        var relation = RelationshipManager.Compare(obj.tag, tag);

        switch (personality)
        {
            case Personality.PEACEFUL:
                return false;
            case Personality.CAUTIONARY:
                return relation == RelationshipManager.Relation.ENEMY;
            case Personality.AGGRESSIVE:
                return relation != RelationshipManager.Relation.FRIEND;
            case Personality.FEARFUL:
                return relation == RelationshipManager.Relation.ENEMY;

            default:
                return false;
        }
    }
}

//TODO：見えている場合だけにしたいときはこれを実装
//if (Physics.Linecast(transform.position, target.position))
//{
//    Debug.Log("blocked");
//}