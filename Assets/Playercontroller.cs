//Pretty much copied this script from my 3D Basics.
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{
    public Transform playerCamera;
    public float mouseSensitivity = 1f;
//Added this because you'll see later something really cool you can do to give a limit to how much you can look up and down.
    private float limit = 0f;
    public CharacterController controller;
    public float moveSpeed = 4f;
    public GameObject ingredientMenu;
    public GameObject shopMenu;
    public Collider shopTrigger;
    private bool shopMenuOpen = false;
    private bool menuOpen = false;
    private bool dayEndOpen = false;

    public void SetDayEndOpen(bool isOpen)
    {
        dayEndOpen = isOpen;

        menuOpen = false;
        ingredientMenu.SetActive(false);
        shopMenu.SetActive(false);

        if (isOpen == false)
        {
            shopMenu.SetActive(shopMenuOpen);
        }

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == shopTrigger)
        {
            menuOpen = false;
            ingredientMenu.SetActive(false);
            shopMenuOpen = true;
            shopMenu.SetActive(dayEndOpen == false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == shopTrigger)
        {
            shopMenuOpen = false;
            shopMenu.SetActive(false);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        ingredientMenu.SetActive(menuOpen);
        shopMenu.SetActive(shopMenuOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (dayEndOpen == true)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            return;
        }
        
        if (Keyboard.current.tabKey.wasPressedThisFrame && shopMenuOpen == false)
        {
            menuOpen = !menuOpen;
            ingredientMenu.SetActive(menuOpen);
        }

        if (menuOpen == true || shopMenuOpen == true)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }

        if(menuOpen == true)
        {
            return;
        }

        if (shopMenuOpen == false)
        {
            Vector2 mouseMovement = Mouse.current.delta.ReadValue();
            transform.Rotate(0f, mouseMovement.x * mouseSensitivity, 0f);

        //Here, we make the camera move rather than the player so they dont fall, now the cool thing comes, we can add limits to how much it can reach, decided to keep it 80 and -80, these numbers because we then add it to quaternions euler, which from what I understood, it takes that number in degrees and turns it into degrees, so that the camera wont be able to past 80 or -80 degrees anymore!
            limit = limit - mouseMovement.y * mouseSensitivity;

            if (limit > 80f)
            {
                limit = 80f;
            }

            if (limit < -80f)
            {
                limit = -80f;
            }

            playerCamera.localRotation = Quaternion.Euler(limit, 0f, 0f);
        }

        //Simple movement script based on where player is facing.

        if (Keyboard.current.wKey.isPressed)
        {
            controller.Move(transform.forward * moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.sKey.isPressed)
        {
            controller.Move(transform.forward * -moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.aKey.isPressed)
        {
            controller.Move(transform.right * -moveSpeed * Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            controller.Move(transform.right * moveSpeed * Time.deltaTime);
        }

    }}
