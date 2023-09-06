using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Transporter : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;
    [SerializeField] int moveX=0, moveY=0;
    [SerializeField] float delay = 1f;
    [SerializeField] GameObject[] toDisappear;

    // Start is called before the first frame update
    void Start()
    {
        /*
        if (slots.Length > 0)
        {
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { CheckAndMove(); }) ;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        Camera.main.GetComponent<Mover>().SetTarget(new Vector3(Camera.main.transform.position.x + moveX, Camera.main.transform.position.y + moveY, -10));
        FindObjectOfType<AudioManager>().Play("Flip");
    }

    public void CheckAndMove()
    {
        bool canCont = true;
        foreach (GameObject obj in toDisappear)
        {
            obj.SetActive(false);
        }
        foreach (ItemSlot slot in slots)
        {
            if (!slot.CheckMe())
            {
                canCont = false;
                //FindObjectOfType<AudioManager>().Play("Error");
                //return;
            }       
        }
        if (canCont)
        {
            FindObjectOfType<AudioManager>().Play("Success");
            StartCoroutine(DelayMove());
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Error");
        }
    }

    IEnumerator DelayMove()
    {
        yield return new WaitForSeconds(delay);
        Move();
    }

    public ItemSlot[] AllSlots()
    {
        return slots;
    }


}
