using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
// Start is called once before the first execution of Update after the MonoBehaviour is created
    public Customer customerPrefab;
    public Transform spawnPoint;
    public Transform serviceSpot;
    public Transform exitSpot;
    public Transform player;
    public TMPro.TMP_Text orderText;
    public FoodStation foodStation;
    private Customer currentCustomer;
    public GameManager gameManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (currentCustomer == null && gameManager.dayOpen == true)
        {
            spawnCustomer();
        }
        if (currentCustomer == null && gameManager.dayOpen == false)
        {
            gameManager.SettleDay();
        }
    }

    void spawnCustomer()
    {
        currentCustomer = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        currentCustomer.serviceSpot = serviceSpot;
        currentCustomer.exitSpot = exitSpot;
        currentCustomer.player = player;
        currentCustomer.orderText = orderText;
        foodStation.customer = currentCustomer;
    }
}
