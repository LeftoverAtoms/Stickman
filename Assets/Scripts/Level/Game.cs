using UnityEngine;

namespace Stickman
{
    public static class Game
    {
        public static Background Background;
        public static Player Player;

        public static GameObject[] Groups;
        public static GameObject Item;

        public static bool IsGameOver = false;
        public static float Speed = 6f;

        static Game()
        {
            Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            Groups = Resources.LoadAll<GameObject>("Prefabs/Groups");
            Item = Resources.Load<GameObject>("Prefabs/Item");

            Enemy.Target = Player;
        }

        public static void Give(string name, Character chr)
        {
            if (!chr.Inventory.CanAdd()) return;

            var atr = Resources.Load<ScriptableItem>($"ScriptableObjects/Items/{name}");
            var obj = UnityEngine.Object.Instantiate(Item, chr.transform);

            HeldObject item;
            if (atr.Type == AttributeType.Item) item = obj.AddComponent<HeldObject>();
            else item = obj.AddComponent<Weapon>();

            item.SetAttributes(atr);
            chr.Equip(item, true);
        }

        public static void SpawnEnemyDebug()
        {
            var obj = Resources.Load<GameObject>("Prefabs/Enemy");
            obj = UnityEngine.Object.Instantiate(obj, new Vector2(15f, 1f), default);
        }
    }
}