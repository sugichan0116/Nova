using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;
using UnityEngine.Events;

public class PageSequencer : MonoBehaviour
{
    [ReorderableList]
    public List<PageContent> pages;

    public UnityEvent onFinish;

    public bool isLoop = false;
    public int index;

    [Button]
    public void NextPage()
    {
        index++;
        if(index == pages.Count)
        {
            if (isLoop) index = 0;
            else index--;

            onFinish.Invoke();
        }
        UpdatePages();
    }

    [Button]
    public void BackPage()
    {
        index--;
        if (index == -1)
        {
            if (isLoop) index = pages.Count - 1;
            else index++;
        }
        UpdatePages();
    }

    private void UpdatePages()
    {
        int n = 0;
        foreach (var page in pages)
        {
            page.gameObject.SetActive(n == index);
            n++;
        }
    }

    public string PageToString()
    {
        return $"{index + 1} / {pages.Count}";
    }

#if UNITY_EDITOR
    [Button]
    public void InitPages()
    {
        //直系の子孫のみとりたい
        //pages = transform.GetComponentsInChildren<PageContent>().ToList();
        pages = transform
            .Cast<Transform>()
            .Select(t => t.GetComponent<PageContent>())
            .Where(p => p != null)
            .ToList();

        UpdatePages();
    }
#endif
}
