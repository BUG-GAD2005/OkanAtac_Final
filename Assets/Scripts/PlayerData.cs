using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int gold;
    public int gems;

    public void Initialize(int initialGold, int initialGems)
    {
        gold = initialGold;
        gems = initialGems;
    }

    public bool DeductResources(int goldAmount, int gemsAmount)
    {
        if (gold >= goldAmount && gems >= gemsAmount)
        {
            gold -= goldAmount;
            gems -= gemsAmount;
            return true;
        }

        return false;
    }

    public void AddResources(int goldAmount, int gemsAmount)
    {
        gold += goldAmount;
        gems += gemsAmount;
    }

    void DebugResources()
    {
        if(Input.GetKeyDown(KeyCode.D))
        DeductResources(2,1);
        if(Input.GetKeyDown(KeyCode.A))
        AddResources(2,1);
    }

    void Update()
    {
        DebugResources();
    }
}
