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
    public static GameObject[] Groups;
    public static GameObject WeaponGO;
    
    public float Speed = 6f;
    public bool GameHasEnded = false;

    private void Awake()
    {
        Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Groups = Resources.LoadAll<GameObject>("Prefabs/Groups");
        WeaponGO = Resources.Load<GameObject>("Prefabs/Weapon");

        Enemy.Target = Player;
        Current = this;
    }

    // TODO: Make into generic give command by typing in weapon string.
    public static void GiveProjectileWeapon(Character ctr)
    {
        if (ctr.ProjectileWeapon != null)
            return;

        var wpn = Instantiate(WeaponGO, ctr.transform).GetComponent<Weapon>();
        wpn.WeaponType = WeaponType.Projectile;
        ctr.EquipWeapon(wpn);
    }
}