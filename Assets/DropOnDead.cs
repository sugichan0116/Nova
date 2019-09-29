using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using NaughtyAttributes;

public class DropOnDead : MonoBehaviour
{
    [SerializeField]
    DropSelection list;

    // Start is called before the first frame update
    void Start()
    {
        var drops = list.combinations;
        if (drops.Count() <= 0) return;

        var box = new LotteryBox(drops.Select(i => i.weight));
        var body = GetComponent<Body>();

        body.onDestroy
            .Subscribe(_ =>
            {
                foreach(var original in drops.ElementAt(box.GetIndex()).Objects())
                {
                    if (original == null) continue;
                    var obj = Instantiate(original, transform.position, transform.rotation);

                    Diffusion(obj);
                }
            })
            .AddTo(this);
    }

    private void Diffusion(GameObject obj)
    {
        var rd = obj.GetComponent<Rigidbody2D>();
        if (rd == null) return;

        var rotate = Quaternion.Euler(0, 0, Random.Range(0, 360));
        rd.velocity = rotate * Vector2.up * Random.Range(0, 10);
    }
}

[System.Serializable]
public class DropSelection
{
    public List<DropSet> combinations = new List<DropSet>();
}

[System.Serializable]
public class DropSet
{
    public string name = "Combination";
    public float weight;

    public List<ObjectWithVolume> objects;

    public IEnumerable<GameObject> Objects()
    {
        foreach (var item in objects)
        {
            var times = Random.Range(item.volume.x, item.volume.y);
            for (int i = 0; i < times; i++)
            {
                yield return item.obj;
            }
        }
    }
}

[System.Serializable]
public class ObjectWithVolume
{
    [SerializeField]
    public GameObject obj;

    [SerializeField, MinMaxSlider(0f, 10f)]
    public Vector2 volume;
}