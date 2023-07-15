using UnityEngine;

public class MagnetCard : ItemCard
{
    [SerializeField] private ItemData itemData;
        
    private void Start()
    {
        InitLevelCard();
    }

    protected override void InitLevelCard()
    {
        weaponIcon.sprite = itemData.itemIcon;
        weaponDesc.text = itemData.levelData[curLevel].itemDesc;
        weaponName.text = itemData.itemName;
        
        levelText.text = "NEW!!";

        gameObject.SetActive(false);
    }

    public override void CardLevelUp()
    {
        GameManager.Instance.curPlayer.GetComponent<PlayerPistol>().LevelUpMagnet(itemData.levelData[curLevel].damage);
        
        curLevel++;

        if (curLevel >= itemData.levelData.Length)
        {
            levelText.text = $"Lv.MAX";
            weaponDesc.text = "최대 레벨입니다.";

            return;
        }

        levelText.text = $"Lv.{curLevel:F0}";
        weaponDesc.text = itemData.levelData[curLevel].itemDesc;
    }

    public override bool CheckMaxLevel()
    {
        return (curLevel >= itemData.levelData.Length);
    }
}
