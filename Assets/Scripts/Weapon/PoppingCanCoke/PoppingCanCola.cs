using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoppingCanCola : MonoBehaviour
{
    [SerializeField] private float scanRange;

    [SerializeField] private LayerMask targetLayer;

    private int curLevel;
    private int weaponPer;
    private int weaponCount;

    private float fireRate;
    private float damage;
    private float moveSpeed;
    private float fireTimer;

    public GameObject poppingCanColaPrefab;
    
    private RaycastHit2D[] targets;

    public Transform nearestTarget;

    private ItemData weaponData;

    private void Update()
    {
        if (GameManager.Instance.isStop)
        {
            return;
        }
        
        FireCanCola();
        MoveUpdate();
    }

    private void FixedUpdate()
    {
        CheckNearestTarget();
    }

    private void CheckNearestTarget()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);

        float diff = 100f;

        Transform temp = null;
        
        foreach (var item in targets)
        {
            float curDiff = Vector3.Distance(transform.position, item.transform.position);

            if (curDiff < diff)
            {
                diff = curDiff;
                temp = item.transform;
            }
        }

        nearestTarget = temp;
    }

    private void MoveUpdate()
    {
        transform.position = GameManager.Instance.curPlayer.transform.position;
    }

    public void Init(ItemData data, float scanRange, LayerMask targetLayer)
    {
        weaponData = data;

        gameObject.name = "PoppingCola";

        curLevel = 0;
        
        fireRate = weaponData.levelData[curLevel].fireRate;
        damage = weaponData.levelData[curLevel].damage;
        weaponPer = weaponData.levelData[curLevel].itemCount;
        moveSpeed = weaponData.levelData[curLevel].weaponMoveSpeed;
        weaponCount = (int)weaponData.levelData[curLevel].weaponSize.x;     // 콜라를 한번에 발사하는 수

        this.targetLayer = targetLayer;

        this.scanRange = scanRange;
        
        poppingCanColaPrefab = weaponData.projectile;

        fireTimer = fireRate;
    }

    public void LevelUp()
    {
        curLevel++;
        
        fireRate = weaponData.levelData[curLevel].fireRate;
        damage = weaponData.levelData[curLevel].damage;
        weaponPer = weaponData.levelData[curLevel].itemCount;
        moveSpeed = weaponData.levelData[curLevel].weaponMoveSpeed;
        weaponCount = (int)weaponData.levelData[curLevel].weaponSize.x;
    }

    private void FireCanCola()
    {
        if (fireTimer <= 0 && nearestTarget != null)
        {
            GameObject canCola =
                PoolManager.Instance.GetGameObejct(poppingCanColaPrefab, transform.position, quaternion.identity);

            Vector3 dir = nearestTarget.position - transform.position;
            
            canCola.SetActive(true);

            canCola.GetComponent<PoppingCanColaWeapon>()
                .InitPoppingCanCola(damage, weaponPer, dir.normalized, moveSpeed);

            for (int i = 1; i < weaponCount; i++)
            {
                canCola = PoolManager.Instance.GetGameObejct(poppingCanColaPrefab, transform.position,
                    Quaternion.identity);

                canCola.SetActive(true);

                canCola.GetComponent<PoppingCanColaWeapon>().InitPoppingCanCola(damage, weaponPer,
                    Random.insideUnitCircle.normalized, moveSpeed);
            }
            
            fireTimer = fireRate;
        }

        fireTimer -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}