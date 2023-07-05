using UnityEngine;

public class HealPackItemCard : ItemCard
{
    [SerializeField] private ItemData itemData;
    
    private void Start()
    {
        InitLevelCard();
    }

    protected override void InitLevelCard()
    {
        weaponIcon.sprite = itemData.itemIcon;
        weaponName.text = itemData.itemName;
        weaponDesc.text = itemData.levelData[0].itemDesc;
        levelText.text = "";
        
        gameObject.SetActive(false);
    }

    public override void CardLevelUp()
    {
        GameManager.Instance.curPlayer.GetComponent<PlayerPistol>().HealHP(itemData.levelData[0].damage);
    }

    public override bool CheckMaxLevel()
    {
        // 계속 먹을수 있는 아이템
        return false;
    }
}