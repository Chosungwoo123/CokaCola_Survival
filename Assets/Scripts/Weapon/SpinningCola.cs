using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCola : MonoBehaviour
{
    [SerializeField] private int colaCount;

    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;

    [SerializeField] private GameObject colaPrefab;

    private ItemData weaponData;

    private int curLevel = 0;

    private Vector3 weaponSize;

    private void Update()
    {
        Spinning();
    }

    private void Spinning()
    {
        transform.position = GameManager.Instance.curPlayer.transform.position;

        transform.Rotate(Vector3.back * attackSpeed * Time.deltaTime);
    }

    public void Init(ItemData data)
    {
        weaponData = data;

        gameObject.name = "SpinningCola";

        colaCount = weaponData.levelData[curLevel].itemCount;
        attackSpeed = weaponData.levelData[curLevel].fireRate;
        attackDamage = weaponData.levelData[curLevel].damage;
        weaponSize = weaponData.levelData[curLevel].weaponSize;

        colaPrefab = weaponData.projectile;

        Batch();
    }

    public void LevelUp()
    {
        curLevel++;

        colaCount = weaponData.levelData[curLevel].itemCount;
        attackSpeed = weaponData.levelData[curLevel].fireRate;
        attackDamage = weaponData.levelData[curLevel].damage;
        weaponSize = weaponData.levelData[curLevel].weaponSize;

        Batch();
    }

    private void Batch()
    {
        for (int i = 0; i < colaCount; i++)
        {
            GameObject cola;

            if (i < transform.childCount)
            {
                cola = transform.GetChild(i).gameObject;
            }
            else
            {
                cola = Instantiate(colaPrefab, transform);
            }

            cola.transform.localPosition = Vector3.zero;
            cola.transform.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i / colaCount;

            cola.transform.Rotate(rotVec);

            cola.transform.Translate(cola.transform.up * 2f, Space.World);
            
            cola.GetComponent<SpinningColaWeapon>().InitSpainningColaWeapon(attackDamage, weaponSize);
        }
    }
}