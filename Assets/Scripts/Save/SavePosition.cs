using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class SavePort : MonoBehaviour
{
    [SerializeField]
    private string key;

    // Start is called before the first frame update
    private void Awake()
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
                var package = manager.Load(key);

                if(package != null)
                {
                    Load(package);
                }
                else
                {
                    Debug.LogWarning($"[Load] key ({key}) was not exists");
                }
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
