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

            Enemy.Target = Player; // Maybe enemies should be able to target other enemies.

            SpawnObject("a");
        }

        public static void Give(string name, Character chr)
        {
            if(!chr.inventory.CanAppend)
                return;

            Debug.Log("a");

            GameObject obj = SpawnObject(name);
            if(obj.TryGetComponent<Item>(out Item item))
            {
                chr.Equip(item, true);
            }
        }

        public static GameObject SpawnObject(string name, Vector2 pos = default)
        {
            // Locate the config file for this object by name.
            ScriptableObject info = null;
            foreach(var file in Resources.LoadAll<ScriptableObject>("Config"))
            {
                Debug.Log($"{file.type.Name} {file.name}");
                if(file.name == name) { info = file; break; }
            }
            Debug.Log(info);
            if(info == null) return null;

            var obj = new GameObject(info.name);
            obj.transform.position = pos;

            var cmp = obj.AddComponent(info.type) as Object;
            cmp.SetProperties(info);

            Debug.Log(obj);

            return obj;
        }
    }
}