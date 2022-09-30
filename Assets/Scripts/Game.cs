using UnityEngine;
using System.Linq;

public class Game : MonoBehaviour
{
    // The reference to this class and all it's variables
    // within that instance can be accessed with this variable.
    public static Game Current { get; private set; }

    // Store class references.
    public static Background Background { get; private set; }
    public static Player Player { get; private set; }

    // Prefabs
    public static GameObject[] Groups;
    public static GameObject Weapon;
    
    public float Speed = 6f;
    public bool GameHasEnded = false;

    private void Awake()
    {
        Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Groups = Resources.LoadAll<GameObject>("Prefabs/Groups");
        Weapon = Resources.Load<GameObject>("Prefabs/Weapon");

        Enemy.Target = Player;
        Current = this;
    }

    // TODO: Make into generic give command by typing in weapon string.
    public static void GiveProjectileWeapon(Character ctr)
    {
        if (ctr.ProjectileWeapon != null)
            return;

        var wpn = Instantiate(Weapon, ctr.transform).GetComponent<Weapon>();
        ctr.EquipWeapon(wpn);
    }

    public static void GiveWeapon(string name, Character character)
    {
        var wpninfo = Resources.Load<WeaponInfo>($"ScriptableObjects/Weapons/{name}");
        if (character.CanEquipWeapon(wpninfo))
        {
            var wpn = Instantiate(Weapon, character.transform).GetComponent<Weapon>();
            character.EquipWeapon(wpn);
        }
    }
}