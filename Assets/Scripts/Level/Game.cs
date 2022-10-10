using UnityEngine;

namespace Stickman
{
    public static class Game
    {
        public static Background Background;
        public static Player Player;

        public static GameObject[] Groups;
        public static GameObject Weapon;

        public static bool IsGameOver = false;
        public static float Speed = 6f;

        static Game()
        {
            Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            Groups = Resources.LoadAll<GameObject>("Prefabs/Groups");
            Weapon = Resources.Load<GameObject>("Prefabs/Weapon");

            Enemy.Target = Player;
        }

        public static void GiveWeapon(string name, Character chr)
        {
            if (!chr.Inventory.CanAdd()) return;

            var item = Resources.Load<ScriptableItem>($"ScriptableObjects/Items/{name}");
            var obj = UnityEngine.Object.Instantiate(Weapon, chr.transform);
            var wpn = obj.GetComponent<Weapon>();

            wpn.SetAttributes(item);
            chr.Equip(wpn, true);
        }

        public static void SpawnEnemyDebug()
        {
            var obj = Resources.Load<GameObject>("Prefabs/Enemy");
            obj = UnityEngine.Object.Instantiate(obj, new Vector2(15f, 1f), default);
        }
    }
}