using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class PageSequencer : MonoBehaviour
{
    [ReorderableList]
    public List<PageContent> pages;
    public bool isLoop = false;
    public int index;

    [Button]
    public void NextPage()
    {
        index++;
        UpdatePages();
    }

    [Button]
    public void BackPage()
    {
        index--;
        UpdatePages();
    }

    private void UpdatePages()
    {
        index = (index + pages.Count) % pages.Count; //ここゴミ mathf.clamp????

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
