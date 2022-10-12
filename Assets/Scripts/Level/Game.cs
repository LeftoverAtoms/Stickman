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

            ScriptableItem info = Resources.Load<ScriptableItem>($"Objects/Items/{name}");

            Type type = null;
            if (info.Type == ScriptableItem.AttributeType.Weapon) type = typeof(Weapon);
            else if (info.Type == ScriptableItem.AttributeType.Item) type = typeof(Item);

            Item item = (Item)Spawn(type);

            item.SetAttributes(info);
            chr.Equip(item, true);
        }

        public static Component Spawn(Type type, Vector2 pos = default)
        {
            var obj = new GameObject(type.Name).AddComponent(type);
            obj.gameObject.transform.position = pos;

            return obj;
        }
    }
}