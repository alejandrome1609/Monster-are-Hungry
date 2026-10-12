using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public class Customer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<string> requestedIngredients = new List<string>()
    {
        "Bun","Meat","Bun"
    };
    public TMP_Text orderText;
    private bool orderTaken = false;
    public Transform serviceSpot;
    public float moveSpeed = 2f;
    private bool hasArrived = false;
    public Transform player;
    public float interactionDistance = 3f;
    public bool CheckOrder(List<Food> meal)
    {
        if (orderTaken == false || orderCompleted == true)
        {
            return false;
        }
        if (meal.Count != requestedIngredients.Count)
        {
            return false;
        }
        for (int i = 0; i < meal.Count; i++)
        {
            if (meal[i].ingredientName != requestedIngredients[i])
            {
                return false;
            }
        }
        return true;
    }
    private bool orderCompleted = false;
    public void CompleteOrder()
    {
        orderCompleted = true;
        orderText.text = "Thanks!";
    }
    public Transform exitSpot;  
    void Start()
    {
        orderText.text = "Customer approching, please wait";
    }
    // Update is called once per frame
    void Update()
    {
        if (orderCompleted == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitSpot.position, moveSpeed * Time.deltaTime);
            if (transform.position == exitSpot.position)
            {
                Destroy(gameObject);
            }
            return;
        }
        
        if (hasArrived == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, serviceSpot.position, moveSpeed * Time.deltaTime);
            if (transform.position == serviceSpot.position)
            {
                hasArrived = true;
                orderText.text = "Customer has arrived, take their order by pressing E!";
            }
        }
        
        if (hasArrived == true && orderTaken == false)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactionDistance && Keyboard.current.eKey.wasPressedThisFrame)
            {
                orderText.text = "Order: " + string.Join(", ", requestedIngredients);
                orderTaken = true;
            }
        }
    }
}
