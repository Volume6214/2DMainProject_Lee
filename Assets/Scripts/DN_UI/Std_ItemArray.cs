using System.Collections.Generic;
using UnityEngine;

public class Std_ItemArray : MonoBehaviour
{
    [SerializeField] private float _spacing = 20f;
    [SerializeField] private float _startY = 0f;

    public void ArrayItem()
    {
        List<RectTransform> children = new List<RectTransform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf) 
                children.Add(child.GetComponent<RectTransform>());
        }

        float currentX = 0f;
        foreach (var child in children)
        {
            float width = child.sizeDelta.x;

            child.anchoredPosition = new Vector2(currentX, _startY);

            currentX += width + _spacing;
        }

        GetComponent<RectTransform>().sizeDelta = new Vector2(currentX, GetComponent<RectTransform>().sizeDelta.y);
    }
}