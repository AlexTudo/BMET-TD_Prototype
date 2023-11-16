using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    //private bool doMovement = false;

    [Header("Attributes")]
    public float panSpeed = 30;
    public float panBorderThickness = 10;
    public float scrollSpeed = 8;
    public float minY = 15;
    public float maxY = 45;
    public float minX = -40;
    public float maxX = -30;
    public float minZ = -20;
    public float maxZ = 30;

    private GameFlow playerStats;

    private void Start()
    {
        playerStats = FindObjectOfType<GameFlow>();    
    }

    void Update()
    {
        if (playerStats.IsGameOver)
        {
            enabled = false;
            return;
        }

        //if(Keyboard.current.escapeKey.isPressed)
        //{
        //    doMovement = !doMovement;
        //}

        //if (!doMovement)
         //   return;

        if (Keyboard.current.wKey.isPressed) //|| Mouse.current.position.y.ReadValue() >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Keyboard.current.sKey.isPressed) //|| Mouse.current.position.y.ReadValue() <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Keyboard.current.dKey.isPressed) //|| Mouse.current.position.x.ReadValue() >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Keyboard.current.aKey.isPressed) //|| Mouse.current.position.x.ReadValue() <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Mouse.current.scroll.y.ReadValue();
        Vector3 pos = transform.position;
        pos.y -= scroll * scrollSpeed * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}
