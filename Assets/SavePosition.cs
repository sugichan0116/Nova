using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class SavePort : MonoBehaviour
{
    [SerializeField]
    private string key;

    // Start is called before the first frame update
    private void Start()
    {
        var manager = GameStateManager.Instance;

        manager.onSave
            .Subscribe(_ =>
            {
                Debug.Log($"[Save] {key} ({this})");
                manager.Save(key, Save());
            })
            .AddTo(this);

        manager.onLoad
            .Subscribe(_ =>
            {
                Debug.Log($"[Load] {key} ({this})");
                Load(manager.Load(key));
            })
            .AddTo(this);
    }

    protected abstract PackageObject Save();

    protected abstract void Load(PackageObject package);
}

public class SavePosition : SavePort
{
    protected override PackageObject Save()
    {
        return new PackageObject(typeof(Vector3), transform.position);
    }

    protected override void Load(PackageObject package)
    {
        var obj = (Vector3)package.data;
        transform.position = obj;
    }
}
