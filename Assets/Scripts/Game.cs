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

            GameObject obj = Spawn(name);
            if (obj.TryGetComponent<Item>(out Item item))
            {
                chr.Equip(item, true);
            }
        }

        // Note: "Nifty Command" GameObject.GetComponent<Object>().GetType();
        public static GameObject Spawn(string name, Vector2 pos = default)
        {
            // Find file.
            ScriptableObject info = null;
            foreach (var file in Resources.LoadAll<ScriptableObject>("ScriptableObjects"))
            {
                if (file.name == name)
                {
                    info = file;
                    break;
                }
            }

            if (info == null)
                return null;

            var obj = new GameObject(info.Type.Name);
            obj.transform.position = pos;
            var cmp = obj.AddComponent(info.Type) as Object; // TODO: Typeof(scriptableobject type)!!!
            cmp.SetAttributes(info);

            return obj;
        }
    }
}