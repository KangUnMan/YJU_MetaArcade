using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum contentChild
{
    Name = 0,
    Score,
    Time
}

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] GameObject _content;

    private List<List<TMP_Text>> _bindText;

    private void Awake()
    {
        bindingContent();


    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void bindingContent()
    {
        _bindText = new List<List<TMP_Text>>();

        for (int i = 0; i < _content.transform.childCount; i++)
        {
            Transform temp = _content.transform.GetChild(i);

            List<TMP_Text> t = new List<TMP_Text>();

            t.Add(temp.GetChild(1).GetComponentInChildren<TMP_Text>());
            t.Add(temp.GetChild(2).GetComponentInChildren<TMP_Text>());
            t.Add(temp.GetChild(3).GetComponentInChildren<TMP_Text>());

            _bindText.Add(t);
        }

        foreach (List<TMP_Text> t in _bindText)
        {
            t[0].text = "Player";
            t[1].text = "30";
            t[2].text = "2024";
        }
    }
}
