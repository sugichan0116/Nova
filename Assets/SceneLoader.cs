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
        //var obj = (GameObject)Resources.Load(PREFAB_PROGRESS);
        //Instantiate(obj.GetComponent<LoadProgress>(), transform.parent).Loader(this);
        ResourcesFactory
            .Instantiate<LoadProgress>(PREFAB_PROGRESS, transform.parent)
            .Loader(this);

        operation = SceneManager.LoadSceneAsync(scene, mode);
        //operation.allowSceneActivation = false;
    }

    public float Progress() => (operation == null) ? 0 : operation.progress / 0.9f;

    //private int BuildIndexByName(string sceneName)
    //{
    //    var s = EditorBuildSettings.scenes
    //        .Where(scene => scene.path.Contains(sceneName))
    //        .FirstOrDefault();

    //    Debug.Log($"[Build Index] {sceneName} >> {s.path} >> {SceneManager.GetSceneByPath(s.path)}");
    //    return SceneManager.GetSceneByPath(s.path).buildIndex;
    //}
}

public class ResourcesFactory
{
    public static T Instantiate<T>(string path, Transform transform) where T : MonoBehaviour
    {
        var obj = (GameObject)Resources.Load(path);
        return Object.Instantiate(obj.GetComponent<T>(), transform.parent);
    }
}