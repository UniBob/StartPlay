using UnityEngine;

public static class PrefsKeys
{
    public static string plantedKey = "whichPlantIsPlantedInTheSpot";
    public static string plantedKey2 = "whichPlantIsPlantedInTheSpot2";
    public static string clearLocationKey = "healthOnLocationEnd";
    public static string maxHealthKey = "maxHealth";
    public static string currentHealthKey = "currentHealth";
    public static string fireRateKey = "fireRate";
    public static string bulletDamageKey = "bulletDamage";
    public static string currentGoldKey = "currentGold";
    public static string stage = "currentStage";
    public static string enemyesOnStage = "enemyesOnStage";
    public static string textDataPage = "textDataPage";
    public static string playerMovementSpeed = "playerMovementSpeed";

    public static int gardenSceneTag = 1;
    public static int fightSceneTag = 2;
    public static int startPageTag = 0;
    public static int dialogueSceneTag = 3;

    public static void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
    public static string inventorySlots = "inventorySlots";
}

