using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public int gold;
    public int gems;
    public event Action<int> OnGoldChange;
    public event Action<int> OnGemsChange;

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

            OnGoldChange?.Invoke(gold);
            OnGemsChange?.Invoke(gems);
            return true;
        }
        return false;
    }

    public void AddResources(int goldAmount, int gemsAmount)
    {
        gold += goldAmount;
        gems += gemsAmount;
        OnGoldChange?.Invoke(gold);
        OnGemsChange?.Invoke(gems);
    }
    public bool checkResources(int goldAmount, int gemsAmount)
    {
        if (gold >= goldAmount && gems >= gemsAmount)
        {
            return true;
        }
        return false;
    }
}
