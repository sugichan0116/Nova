using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using System.Linq;
using NaughtyAttributes;

public class TargetDetector : MonoBehaviour
{
    public enum State
    {
        IDLE,
        DETECT,
        WARNING,
    }


    [SerializeField]
    protected Collider2D searhArea;
    [SerializeField]
    protected Personality personality = Personality.CAUTIONARY;
    [SerializeField]
    protected float warningTime = 6f;

    protected State state;

    public Transform Target { get; protected set; } = null;

    protected void Start()
    {
        //State Transition by triggers
        searhArea
            .OnTriggerEnter2DAsObservable()
            .Where(c => IsTarget(c.gameObject))
            .Subscribe(c =>
            {
                //Debug.Log($"[Move AI] detected! {this}");
                state = State.DETECT;
                Target = c.transform;
            })
            .AddTo(this);

        searhArea
            .OnTriggerExit2DAsObservable()
            .Where(c => IsTarget(c.gameObject))
            .Subscribe(_ =>
            {
                if(warningTime < 1f)
                {
                    state = State.IDLE;
                } else
                {
                    state = State.WARNING;
                }
            })
            .AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => state == State.WARNING && warningTime > 1f)
            .Sample(TimeSpan.FromSeconds(1f))
            .Buffer((int)warningTime)
            .Subscribe(_ =>
            {
                state = State.IDLE;
            });
    }

    protected bool IsTarget(GameObject obj)
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

    public static bool IsFriend(string tagA, string tagB)
    {
        return Compare(tagA, tagB) == Relation.FRIEND;
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
