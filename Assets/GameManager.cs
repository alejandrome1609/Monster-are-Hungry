using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int money = 30;
    public int bugerPrice = 10;
    public TMP_Text moneyText;
    public int bunStock = 10;
    public int tentacleStock = 5;
    public int dragonScaleStock = 5;
    public int boneStock = 5;
    public int meatStock = 5;
    public int cheeseStock = 5;
    public TMP_Text bunText;
    public TMP_Text tentacleText;
    public TMP_Text dragonScaleText;
    public TMP_Text boneText;
    public TMP_Text meatText;
    public TMP_Text cheeseText;
    public TMP_Text shopBunText;
    public TMP_Text shopTentacleText;
    public TMP_Text shopDragonScaleText;
    public TMP_Text shopBoneText;
    public TMP_Text shopMeatText;
    public TMP_Text shopCheeseText;
    public float dayTimeRemaining = 15f;
    public int dailyQuota = 25;
    public bool dayOpen = true;
    private bool daySettled = false;
    public PlayerScript playerScript;
    public FoodStation foodStation;
    public GameObject dayEndPanel;
    public TMP_Text dayEndText;
    public GameObject nextDayButton;
    public GameObject retryButton;
    public float dayDuration = 120f;
    public int quotaIncrease = 5;
    private int dayNumber = 1;

    private bool quotaPaid = false;
    public TMP_Text dayInfoText;
    public GameObject startPanel;
    public bool gameStarted = false;

    public void playGame()
    {
        if (gameStarted == true)
        {
            return;
        }
        gameStarted = true;
        dayOpen = true;
        dayTimeRemaining = dayDuration;
        startPanel.SetActive(false);
        playerScript.SetDayEndOpen(false);
        updateDayInfo();
    }

    void updateDayInfo()
    {
       string timeDisplay;

       if (dayOpen == true)
        {
            timeDisplay = "Time: " + Mathf.CeilToInt(dayTimeRemaining) + "s";
        }
        else
        {
            timeDisplay = "Closed.";
        }

        dayInfoText.text = "Day " + dayNumber + "\n" + timeDisplay + "\nDad's Payment: $" + dailyQuota;
    }

    public void SettleDay()
    {
        if (dayOpen == true || daySettled == true)
        {
            return;
        }

        daySettled = true;
        quotaPaid = money >= dailyQuota;

        if (quotaPaid == true)
        {   
            money = money - dailyQuota;
            moneyText.text = "Money: " + money;

            dayEndText.text = "Day complete! Paid Dad: $"
                + dailyQuota + "\nMoney remaining: $" + money;
        }
        else
        {
        dayEndText.text = "Game over! Dad wanted: $"
            + dailyQuota + "\nbut you only had $" + money;
        }

        foodStation.ClearIngredients();
        playerScript.SetDayEndOpen(true);

        nextDayButton.SetActive(quotaPaid);
        retryButton.SetActive(quotaPaid == false);
        dayInfoText.gameObject.SetActive(false);
        dayEndPanel.SetActive(true);
    }
    public void UpdateStockText()
    {
        bunText.text = "Bun: " + bunStock;
        tentacleText.text = "Tentacle: " + tentacleStock;
        dragonScaleText.text = "Dragon Scale: " + dragonScaleStock;
        boneText.text = "Bone: " + boneStock;
        meatText.text = "Meat: " + meatStock;
        cheeseText.text = "Cheese: " + cheeseStock;
        shopBunText.text = "Bun: " + bunStock;
        shopTentacleText.text = "Tentacle: " + tentacleStock;
        shopDragonScaleText.text = "Dragon Scale: " + dragonScaleStock;
        shopBoneText.text = "Bone: " + boneStock;
        shopMeatText.text = "Meat: " + meatStock;
        shopCheeseText.text = "Cheese: " + cheeseStock;
    }

    public void receiveMoney()
    {
        money = money + bugerPrice;
        moneyText.text = "Money: " + money;
    }

    public void NextDay()
    {
        if (daySettled == false || quotaPaid == false)
        {
            return;
        }

        dayNumber = dayNumber + 1;
        dailyQuota = dailyQuota + quotaIncrease;
        dayTimeRemaining = dayDuration;

        daySettled = false;
        quotaPaid = false;
        dayOpen = true;

        dayEndPanel.SetActive(false);
        playerScript.SetDayEndOpen(false);
        dayInfoText.gameObject.SetActive(true);
        updateDayInfo();
    }

    public void RetryDay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool TryUseIngredient(string ingredientName)
    {
        if (ingredientName == "Meat" && meatStock > 0)
        {
            meatStock = meatStock - 1;
            UpdateStockText();
            return true;
        }
        if (ingredientName == "Bun" && bunStock > 0)
        {
            bunStock = bunStock - 1;
            UpdateStockText();
            return true;
        }
        if (ingredientName == "Tentacle" && tentacleStock > 0)
        {
            tentacleStock = tentacleStock - 1;
            UpdateStockText();
            return true;
        }
        if (ingredientName == "Dragon Scale" && dragonScaleStock > 0)
        {
            dragonScaleStock = dragonScaleStock - 1;
            UpdateStockText();
            return true;
        }
        if (ingredientName == "Bone" && boneStock > 0)
        {
            boneStock = boneStock - 1;
            UpdateStockText();
            return true;
        }
        if (ingredientName == "Cheese" && cheeseStock > 0)
        {
            cheeseStock = cheeseStock - 1;
            UpdateStockText();
            return true;
        }

        return false;
    }

    public void buyBuns()
    {
        if (money < 1)
        {
            Debug.Log("not enough, get more!!!!");
            return;
        }
        money = money - 1;
        bunStock = bunStock + 1;
        moneyText.text = "Money: " + money;
        UpdateStockText();
    }

    public void buyMeat()
    {
        if (money < 2)
        {
            Debug.Log("not enough, get more!!!!");
            return;
        }
        money = money - 2;
        meatStock = meatStock + 1;
        moneyText.text = "Money: " + money;
        UpdateStockText();
    }
    public void buyCheese()
    {
        if (money < 2)
        {
            Debug.Log("not enough, get more!!!!");
            return;
        }
        money = money - 2;
        cheeseStock = cheeseStock + 1;
        moneyText.text = "Money: " + money;
        UpdateStockText();
    }
    public void buyBone()
    {
        if (money < 2)
        {
            Debug.Log("not enough, get more!!!!");
            return;
        }
        money = money - 2;
        boneStock = boneStock + 1;
        moneyText.text = "Money: " + money;
        UpdateStockText();
    }
    public void buyTentacle()
    {
        if (money < 3)
        {
            Debug.Log("not enough, get more!!!!");
            return;
        }
        money = money - 3;
        tentacleStock = tentacleStock + 1;
        moneyText.text = "Money: " + money;
        UpdateStockText();
    }
    public void buyDragonScale()
    {
        if (money < 3)
        {
            Debug.Log("not enough, get more!!!!");
            return;
        }
        money = money - 3;
        dragonScaleStock = dragonScaleStock + 1;
        moneyText.text = "Money: " + money;
        UpdateStockText();
    }

    void Start()
    {
        moneyText.text = "Money: " + money;
        UpdateStockText();
        dayEndPanel.SetActive(false);
        dayTimeRemaining = dayDuration;
        updateDayInfo();
        gameStarted = false;
        dayOpen = false;
        startPanel.SetActive(true);
        playerScript.SetDayEndOpen(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted == false)
        {
            return;
        }
        if (dayOpen == true)
        {
            dayTimeRemaining = dayTimeRemaining - Time.deltaTime;
            if (dayTimeRemaining <= 0f)
            {
                dayTimeRemaining = 0f;
                dayOpen = false;
                Debug.Log("day is over.");
            }
        }
        updateDayInfo();
    }
}
