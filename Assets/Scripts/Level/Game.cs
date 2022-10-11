using UnityEngine;
using System;

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
            if (!chr.Inventory.CanAdd())
                return;

            ScriptableItem info = Resources.Load<ScriptableItem>($"Items/{name}");

            Type type = null;
            if (info.Type == ScriptableItem.AttributeType.Weapon) type = typeof(Weapon);
            else if (info.Type == ScriptableItem.AttributeType.Item) type = typeof(Item);

            Item item = new GameObject("Item").AddComponent(type) as Item;

            item.SetAttributes(info);
            chr.Equip(item, true);
        }

        // TODO: public static void Spawn(string name, Vector2 pos, [optional equipment]) { }
        public static void SpawnObject(string name, Vector2 pos)
        {
            var obj = Resources.Load<GameObject>("Prefabs/Enemy");
            UnityEngine.Object.Instantiate(obj, new Vector2(15f, 1f), default);
        }
    }
}