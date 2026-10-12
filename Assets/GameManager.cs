using UnityEngine;
using TMPro;

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

    public void UpdateStockText()
    {
        bunText.text = "Bun: " + bunStock;
        tentacleText.text = "Tentacle: " + tentacleStock;
        dragonScaleText.text = "Dragon Scale: " + dragonScaleStock;
        boneText.text = "Bone: " + boneStock;
        meatText.text = "Meat: " + meatStock;
        cheeseText.text = "Cheese: " + cheeseStock;
    }

    public void receiveMoney()
    {
        money = money + bugerPrice;
        moneyText.text = "Money: " + money;
        Debug.Log("Money: " + money);
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
    void Start()
    {
        moneyText.text = "Money: " + money;
        UpdateStockText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
