using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PoemController : MonoBehaviour
{

    [SerializeField]
    Image nextPage;

    [SerializeField]
    Image prevPage;

    [SerializeField]
    Image background;

    [SerializeField]
    TMP_Text text;

    [SerializeField]
    GameObject DotText;
    const string path = "Background/PoemBackground/";

    //현재 타임이 밤인지, 낮인지는 GameManager가 가지고있음
    GameManager gameManager;

    private bool hasShownDotText = false;
    int currentPage;
    int totalPage;
    int chapter;


    private void OnEnable()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();

        if (gameManager)
        {
            nextPage.sprite = Resources.Load<Sprite>(path + "NextPage/" + gameManager.Time);
            prevPage.sprite = Resources.Load<Sprite>(path + "PrevPage/" + gameManager.Time);
            background.sprite = Resources.Load<Sprite>(path + gameManager.Time);
        }
        prevPage.gameObject.SetActive(false);
        currentPage = 0; //뜰 때 마다 첫번째 페이지로
        LoadPoem();
    }


    void LoadPoem()
    {
        if (gameManager)
        {
            //시 내용을 업데이트
            chapter = gameManager.Chapter;

            if (currentPage < DataManager.Instance.PoemData.poems[chapter].text.Count)
                text.text = DataManager.Instance.PoemData.poems[chapter].text[currentPage].textKor;

            totalPage = DataManager.Instance.PoemData.poems[chapter].text.Count; //가장 마지막 위치로드
            
            if(totalPage == currentPage)
            {
                nextPage.gameObject.SetActive(false);
            }
        }
    }


    public void NextPage()
    {

        currentPage++;

        if (currentPage >= totalPage)
        {
            currentPage = totalPage - 1;
            return;
        }
        //다음 페이지가 없을 경우에는 버튼을 없애버림
        if (currentPage + 1 >= totalPage)
        {
            //감상평 ON
            if (!hasShownDotText)
            {
                // 감상평 ON (한 번만 실행)
                DotTextOn();
                hasShownDotText = true;
            }

            nextPage.gameObject.SetActive(false);
        }

        prevPage.gameObject.SetActive(true);
        text.text = DataManager.Instance.PoemData.poems[chapter].text[currentPage].textKor;
    }

    public void PrevPage() 
    {

        currentPage--;

        if (currentPage < 0)
        {
            currentPage = 0;
            return;
        }
        if (currentPage - 1 < 0)
        {
            prevPage.gameObject.SetActive(false);
        }

        nextPage.gameObject.SetActive(true);
        text.text = DataManager.Instance.PoemData.poems[chapter].text[currentPage].textKor;
    }

    private void DotTextOn()
    {
        DotText.SetActive(true);
        DotText.GetComponent<DotTextReview>().StartReview();
    }

    public void Exit()
    {
        //manager에서 sleep 요청
        gameManager.GoSleep();
        Destroy(gameObject.transform.parent.gameObject);
        hasShownDotText = false;
    }

}
