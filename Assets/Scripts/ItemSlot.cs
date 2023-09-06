using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private GameObject currentDrag;
    private int currentNum=-1;
    [SerializeField] private int correctAnswerNum;
    [SerializeField] Color right, wrong;
    private GameObject myChildKnob;

    public void Start()
    {
        myChildKnob = this.gameObject.transform.GetChild(0).gameObject;
    }

    public void OnDrop(PointerEventData eventData)
    {
        /*
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            RectTransform rect1 = GetComponent<RectTransform>();
            RectTransform rect2 = eventData.pointerDrag.GetComponent<RectTransform>();

            if (!rect1.rect.Overlaps(rect2.rect))
            {
                return;
            }
            Debug.Log(this.gameObject.name+","+ eventData.pointerDrag.gameObject.name);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if(eventData.pointerDrag.GetComponent<DragDrop>() != null )
            {
                if (currentDrag != null)
                    Destroy(currentDrag.gameObject);
                FindObjectOfType<AudioManager>().Play("Snap");
                DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
                dd.SetPlaced();
                currentDrag = eventData.pointerDrag.gameObject;
                currentNum = (dd.GetAnswerNum());
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        */
    }

    public void HandlePlacement(GameObject item)
    {
        item.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        if (item.GetComponent<DragDrop>() != null)
        {
            if (currentDrag != null)
                Destroy(currentDrag.gameObject);
            FindObjectOfType<AudioManager>().Play("Snap");
            DragDrop dd = item.GetComponent<DragDrop>();
            dd.SetPlaced();
            currentDrag = item.gameObject;
            currentNum = (dd.GetAnswerNum());
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public bool CheckMe()
    {
        bool equal= (currentNum == correctAnswerNum);
        GameObject child = this.gameObject.transform.GetChild(0).gameObject;
        if (equal)
            myChildKnob.GetComponent<Image>().color = right;
        else
            myChildKnob.GetComponent<Image>().color = wrong;
        myChildKnob.GetComponent<Animator>().SetTrigger("Click");
        return currentNum == correctAnswerNum;
    }
}
