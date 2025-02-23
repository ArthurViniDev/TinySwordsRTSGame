using System;
using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private Vector3 hotSpot;
    [SerializeField] private  Sprite sprite;
    [SerializeField] private Image image;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        image = GetComponent<Image>();
    }

    private void Update()
    {
        Cursor.visible = false;
        transform.position = Input.mousePosition + hotSpot;
    }
}
