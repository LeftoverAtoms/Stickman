using UnityEngine;

namespace Stickman
{
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

        public static void GiveWeapon(string name, Character character)
        {
            var wpnAttribute = Resources.Load<ScriptableItem>($"ScriptableObjects/Weapons/{name}");

            var wpn = Instantiate(Weapon, character.transform).GetComponent<Weapon>();
            wpn.Attribute = wpnAttribute;
            if (!character.Equip(wpn))
            {
                Destroy(wpn);
            }
        }

        public static void SpawnEnemyDebug()
        {
            var enemy = Resources.Load<GameObject>("Prefabs/Enemy");
            Instantiate(enemy, new Vector2(15f, 1f), default).GetComponent<Weapon>();
        }
    }
}