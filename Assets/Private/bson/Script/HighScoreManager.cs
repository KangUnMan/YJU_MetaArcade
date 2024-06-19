using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

enum contentChild
{
    Name = 0,
    Score,
    Time
}

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] GameObject _Board;
    [SerializeField] GameObject _content;

    private List<List<TMP_Text>> _bindText;
    private bool showPopUp = false;

    private float _currentTime;

    private void Awake()
    {
        bindingContent();


    }
    void Start()
    {
        
    }

    void Update()
    {
        keyShortCut();
    }

    private void keyShortCut()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Input.GetKey(KeyCode.LeftControl))
        {
            showPopUp ^= true;

            updateBoard();

            _Board.SetActive(showPopUp);

            if (showPopUp)
            {
                StartCoroutine(pollingRanking());
            }
        }
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

        initBoard();
    }

    private void initBoard()
    {
        if (_bindText == null)
        {
            return;
        }

        foreach (List<TMP_Text> t in _bindText)
        {
            t[0].text = "Player";
            t[1].text = "30";
            t[2].text = "2024";
        }
    }

    private void updateBoard()
    {
        if (_bindText == null)
        {
            return;
        }

        WebServerManager.Instance.UpdateRanking((resList) =>
        {
            initBoard();

            for (int i = 0; i < resList.Count; i++)
            {
                _bindText[i][0].text = resList[i].AccountName;
                _bindText[i][1].text = resList[i].Score.ToString();
                _bindText[i][2].text = resList[i].Date.ToString("yyyy-MM-dd HH:mm:dd");
            }
        });
    }

    private IEnumerator pollingRanking()
    {
        while (showPopUp)
        {
            yield return new WaitForSeconds(5);
            updateBoard();
        }
    }
}
