using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneLoader : MonoBehaviour
{
    private const string PREFAB_PROGRESS = "Progress";

    [SerializeField]
    private SceneObject scene;
    [SerializeField]
    private LoadSceneMode mode = LoadSceneMode.Single;

    private AsyncOperation operation;

    public void LoadScene()
    {
        SceneManager.LoadScene(scene, mode);
    }

    public void LoadSceneAsync()
    {
        var p = GetComponentInParent<Canvas>().transform;

        ResourcesFactory
            .Instantiate<LoadProgress>(PREFAB_PROGRESS, p)
            .Loader(this);

        operation = SceneManager.LoadSceneAsync(scene, mode);
        //operation.allowSceneActivation = false;
    }

    public float Progress() => (operation == null) ? 0 : operation.progress / 0.9f;
}

public class ResourcesFactory
{
    public static GameObject Instantiate(string path, Transform transform)
    {
        var obj = (GameObject)Resources.Load(path);
        return Object.Instantiate(obj, transform);
    }

    public static T Instantiate<T>(string path, Transform transform) where T : MonoBehaviour
    {
        var obj = (GameObject)Resources.Load(path);
        return Object.Instantiate(obj.GetComponent<T>(), transform);
    }
}