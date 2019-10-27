using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadProgress : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    private SceneLoader loader;
    private float progress = 0;

    public void Loader(SceneLoader loader) => this.loader = loader;

    private void Update()
    {
        if (loader == null) return;
        progress = Mathf.Lerp(progress, loader.Progress(), 0.5f);
        slider.value = progress;
        text.text = $"{(int)(progress * 100f)} %";

        Debug.Log($"[Load Progress] called in {progress}");
    }
}
