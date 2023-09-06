using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private int answerNum;
    public bool original = true;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool placed = false;
    [SerializeField] Transporter myTransporter;
    [SerializeField] float maxDistance = 0.7f;
    [SerializeField] GameObject[] toDisappear;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (placed)
            return;
        if (original)
        {
            GameObject thisClone = Instantiate(this.gameObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            thisClone.transform.parent = gameObject.transform.parent;
            thisClone.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            original = false;
        }
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (placed)
            return;
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //rectTransform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //rectTransform.localPosition = Input.mousePosition - canvas.transform.position;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        canvas.transform as RectTransform,
        Input.mousePosition, canvas.worldCamera,
        out pos);
        rectTransform.position = canvas.transform.TransformPoint(pos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*
        if (placed)
            return;
        */
        ///
        ItemSlot chosenSlot = null;
        float minDist = Mathf.Infinity;
        ItemSlot[] allSlots = myTransporter.AllSlots();
        foreach(ItemSlot slot in allSlots)
        {
            Debug.Log("X");
            float newDist = Vector3.Distance(slot.transform.position, this.transform.position);
            if (newDist < minDist)
            {
                minDist = newDist;
                chosenSlot = slot;
            }
        }
        Debug.Log(minDist);
        if (minDist > maxDistance)
        {
            Destroy(this.gameObject);
            return;
        }
                foreach (GameObject obj in toDisappear)
        {
            obj.SetActive(false);
        }
        chosenSlot.HandlePlacement(this.gameObject);



        ///
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (placed)
            return;
    }

    public int GetAnswerNum()
    {
        return answerNum;
    }

    public void SetPlaced()
    {
        placed = true;
    }



}
