using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;

    [Header("Costs")]
    public int punchPowerCost = 100;
    public int moreBurgersCost = 150;
    public int juiceCost = 200;
    public int bouncingCost = 250;
    public int rainCost = 300;

    [Header("UI Text")]
    public TextMeshProUGUI punchPowerText;
    public TextMeshProUGUI moreBurgersText;
    public TextMeshProUGUI juiceText;
    public TextMeshProUGUI bouncingText;
    public TextMeshProUGUI rainText;

    [Header("References")]
    public BurgerPunch burgerPunch;
    public BurgerBounce burgerBounce;

    int maxLevel = 5;
    int punchLevel = 0;
    int burgerCountLevel = 0;
    int juiceLevel = 0;
    int bounceLevel = 0;
    int rainLevel = 0;

    void Start()
    {
        UpdatePriceText();
    }

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        UpdatePriceText();
    }

    public void BuyPunchPower()
    {
        if (punchLevel >= maxLevel || !CanAfford(punchPowerCost))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cantAfford);
            return;
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.shopBuy);
        ScoreManager.Instance.AddScore(-punchPowerCost);

        punchLevel++;
        PunchStats.Instance.punchMultiplier += 0.3f;
        punchPowerCost = Mathf.RoundToInt(punchPowerCost * 1.5f);

        UpdatePriceText();
    }

    public void BuyMoreBurgers()
    {
        if (burgerCountLevel >= maxLevel || !CanAfford(moreBurgersCost))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cantAfford);
            return;
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.shopBuy);
        ScoreManager.Instance.AddScore(-moreBurgersCost);

        burgerCountLevel++;
        BurgerSpawner.Instance.burgerCount++;
        BurgerSpawner.Instance.SpawnBurgers();
        moreBurgersCost = Mathf.RoundToInt(moreBurgersCost * 1.6f);

        UpdatePriceText();
    }

    public void BuyJuice()
    {
        if (juiceLevel >= maxLevel || !CanAfford(juiceCost))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cantAfford);
            return;
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.shopBuy);
        ScoreManager.Instance.AddScore(-juiceCost);

        juiceLevel++;
        JuiceManager.Instance.vfxScale += 0.25f;
        JuiceManager.Instance.screenShake += 0.15f;
        juiceCost = Mathf.RoundToInt(juiceCost * 1.7f);

        UpdatePriceText();
    }

    public void BuyBouncing()
    {
        if (BounceStats.Instance.bounceLevel >= maxLevel || !CanAfford(bouncingCost))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cantAfford);
            return;
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.shopBuy);
        ScoreManager.Instance.AddScore(-bouncingCost);

        BounceStats.Instance.bounceLevel++;
        bouncingCost = Mathf.RoundToInt(bouncingCost * 1.6f);

        UpdatePriceText();
    }


    public void BuyRain()
    {
        if (rainLevel >= maxLevel || !CanAfford(rainCost))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.cantAfford);
            return;
        }

        AudioManager.Instance.PlaySFX(AudioManager.Instance.shopBuy);
        ScoreManager.Instance.AddScore(-rainCost);

        RainManager.Instance.UpgradeRain();

        rainLevel++;
        rainCost = Mathf.RoundToInt(rainCost * 1.7f);

        UpdatePriceText();
    }

    void UpdatePriceText()
    {
        punchPowerText.text = punchLevel >= maxLevel ? "Punch Power MAX" : $"Punch Power {punchPowerCost}";
        moreBurgersText.text = burgerCountLevel >= maxLevel ? "More Burgers MAX" : $"More Burgers {moreBurgersCost}";
        juiceText.text = juiceLevel >= maxLevel ? "Juice MAX" : $"Juice {juiceCost}";
        bouncingText.text = bounceLevel >= maxLevel ? "Bouncing MAX" : $"Bouncing {bouncingCost}";
        rainText.text = rainLevel >= maxLevel ? "Rain MAX" : $"Rain {rainCost}";

        punchPowerText.color = ScoreManager.Instance.score >= punchPowerCost ? Color.white : Color.red;
        moreBurgersText.color = ScoreManager.Instance.score >= moreBurgersCost ? Color.white : Color.red;
        juiceText.color = ScoreManager.Instance.score >= juiceCost ? Color.white : Color.red;
        bouncingText.color = ScoreManager.Instance.score >= bouncingCost ? Color.white : Color.red;
        rainText.color = ScoreManager.Instance.score >= rainCost ? Color.white : Color.red;
    }

    bool CanAfford(int cost)
    {
        return ScoreManager.Instance.score >= cost;
    }
}
