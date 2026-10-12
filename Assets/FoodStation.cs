using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class FoodStation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Food foodPrefab;
    public Transform layer2;
    public Transform layer3;
    public Transform layer4;
    private List<Food> preparedIngredients = new List<Food>();
    public void AddIngredient(Food ingredientPrefab)
    {
        if (preparedIngredients.Count >= 4)
        {
            return;
        }
        Transform spawnPoint = transform;
        if (preparedIngredients.Count == 1)
        {
            spawnPoint = layer2;
        }
        else if (preparedIngredients.Count == 2)
        {
            spawnPoint = layer3;
        }
        else if (preparedIngredients.Count == 3)
        {
            spawnPoint = layer4;
        }
        Food newIngredient = Instantiate(ingredientPrefab, spawnPoint.position, spawnPoint.rotation);
        preparedIngredients.Add(newIngredient);

    }
    public void ClearIngredients()
    {
        foreach (Food ingredient in preparedIngredients)
        {
            Destroy(ingredient.gameObject);
        }
        preparedIngredients.Clear();
    }
    public Customer customer;
    public void ServeCustomer()
    {
        if (customer.CheckOrder(preparedIngredients))
        {
            customer.CompleteOrder();
            Debug.Log("Correct Order!");
            ClearIngredients();
        }
        else
        {
            Debug.Log("Order is incorrect!");
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ServeCustomer();
        }
    }}

