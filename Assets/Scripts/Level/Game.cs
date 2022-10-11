using UnityEngine;

namespace Stickman
{
    public static class Game
    {
        public static Background Background;
        public static Player Player;

        public static GameObject[] Groups;

        public static bool IsGameOver = false;
        public static float Speed = 6f;

        static Game()
        {
            Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            Groups = Resources.LoadAll<GameObject>("Prefabs/Groups");

            Enemy.Target = Player;
        }

        public static void Give(string name, Character chr)
        {
            if (!chr.Inventory.CanAdd()) return;

            var config = Resources.Load<ScriptableItem>($"Items/{name}");
            var obj = new GameObject("Item");

            Item item;
            if (config.Type == AttributeType.Item) item = obj.AddComponent<Item>();
            else item = obj.AddComponent<Weapon>();

            item.SetAttributes(config);
            chr.Equip(item, true);
        }

        // TODO: public static void Spawn(string name, Vector2 pos, [optional equipment]) { }
        public static void SpawnEnemyDebug()
        {
            var obj = Resources.Load<GameObject>("Prefabs/Enemy");
            UnityEngine.Object.Instantiate(obj, new Vector2(15f, 1f), default);
        }
    }
}