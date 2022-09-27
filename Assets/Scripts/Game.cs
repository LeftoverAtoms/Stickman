using UnityEngine;

public class Game : MonoBehaviour
{
    // The reference to this class and all it's variables
    // within that instance can be accessed with this variable.
    public static Game Current { get; private set; }

    // Store class references.
    public Background Background { get; private set; }
    public Player Player { get; private set; }

    // Prefabs
    public static GameObject WeaponGO;
    //public GameObject[] ObstaclePrefabs;

    public float Speed = 6f;
    public bool GameHasEnded = false;

    private void Awake()
    {
        Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        WeaponGO = Resources.Load<GameObject>("Prefabs/Weapon");
        Enemy.Target = Player;
        Current = this;
    }

    public static void GiveProjectileWeapon(Character ctr)
    {
        if (ctr.ProjectileWeapon != null) return;

        var obj = Instantiate(WeaponGO, ctr.transform);
        var wpn = obj.GetComponent<Weapon>();
        wpn.WeaponType = WeaponType.Projectile;
        ctr.EquipWeapon(wpn);
    }
}