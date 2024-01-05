using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightTouch : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    private bool isRight;
    private Player player;

    public float moviment;
    private IPointerDownHandler _pointerDownHandlerImplementation;
    private IPointerDownHandler _pointerDownHandlerImplementation1;
    private IPointerEnterHandler _pointerEnterHandlerImplementation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    void Update()
    {
        if (Input.GetMouseButton(0) && isRight)
        {
            moviment += Time.deltaTime;

            if (moviment > 1f)
            {
                moviment = 1f;
            }
        }
        player.moviment = moviment;
    }
    
    //É acionado quando clicamos (ou Touch) no elemento da UI
    public void OnPointerDown(PointerEventData eventData)
    {
        isRight = true;
    }
    //É acionado quando tiramos o click (Touch) do elemento
    public void OnPointerEnter(PointerEventData eventData)
    {
        isRight = false;
        moviment = 0f;
    }
}
