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
    public bool PlayerIsNear()
    {
        return Vector3.Distance(transform.position, player.position) <= interactionDistance;
    }
    public bool CheckOrder(List<Food> meal)
    {
        if (orderTaken == false || orderCompleted == true || isLeaving == true)
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
        orderText.text = "Nice!";
        isLeaving = true;
    }
    public Transform exitSpot;
    private string[] fillings =
    {
        "Tentacle", "Dragon Scale", "Bone", "Meat", "Cheese"
    };
    private string orderName;
    private bool isLeaving = false;
    private float patienceRemaining = 20f;
    public TMP_Text patienceText;

    void generateOrder()
    {
        requestedIngredients.Clear();
        requestedIngredients.Add("Bun");
        int fillingCount = Random.Range(1, 3);
        orderName = "";
        for (int i = 0; i < fillingCount; i++)
        {
            string filling = fillings[Random.Range(0, fillings.Length)];
            requestedIngredients.Add(filling);
            
            if (i > 0)
            {
                orderName = orderName + " and ";
            }
            orderName = orderName + filling;
        }
        requestedIngredients.Add("Bun");
        orderName = orderName + "Burger";
    

    }
    void Start()
    {
        orderText.text = "Waiting for order.";
        generateOrder();
        patienceText.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        if (isLeaving == true)
        {
            patienceText.text = "";
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
                orderText.text = "Waiting for order.";
            }
        }
        
        if (hasArrived == true)
        {
            patienceRemaining = patienceRemaining - Time.deltaTime;
            patienceText.text = "Patience: " + Mathf.RoundToInt(patienceRemaining).ToString();
            if (patienceRemaining <= 0f)
            {
                isLeaving = true;
                orderText.text = "Bruh.";
                return;
            }
        }
        if (hasArrived == true && orderTaken == false)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= interactionDistance && Keyboard.current.eKey.wasPressedThisFrame)
            {
                orderText.text = "Order: " + orderName;
                orderTaken = true;
                patienceRemaining = 10f;
            }
        }
    }
}
