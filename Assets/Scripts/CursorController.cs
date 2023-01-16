using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    [Header("Cursor Sprites")]
    [SerializeField] private Texture2D _basicCursor;
    [SerializeField] private Texture2D _inspectCursor;
    //[SerializeField] private SpriteRenderer _activeSprite;

    private Vector2 _mousePosition;
    [SerializeField] private float _cursorSpeed = 20f;

    [SerializeField] private Character _player;
    private Vector2 _cusorHotspot;

    private void Awake()
    {
        ChangeCursor(_basicCursor);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width/2,cursorType.height/2);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
        
    }
    //void Awake()
    //{
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Confined;
    //}

    ////not working
    //private void OnMouseEnter()
    //{
    //    if (gameObject.CompareTag("Interactable"))
    //    {
    //        _activeSprite.sprite = _inspectCursor;
    //    }
    //    else
    //    {
    //        _activeSprite.sprite = _basicCursor;
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider.tag == "Interactable")
    //    {
    //        _activeSprite.sprite = _inspectCursor;
    //    }
    //    else
    //    {
    //        _activeSprite.sprite = _basicCursor;
    //    }
    //}

    //void Update()
    //{
    //    _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    //    transform.position = _mousePosition;
    //    //transform.position = Vector2.MoveTowards(transform.position, _mousePosition, _cursorSpeed * Time.deltaTime);
    //    if (_player.isMouseAiming)
    //    {
    //        gameObject.GetComponent<Renderer>().enabled = false;
    //    }
    //    else
    //    {
    //        gameObject.GetComponent<Renderer>().enabled = true;
    //    }
    //}
}
