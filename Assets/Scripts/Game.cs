using UnityEngine;

namespace Stickman
{
    public static class Game
    {
        public static Background Background;
        public static Character Player;

        public static bool IsGameOver = false;

        public static float RelativeSpeed => Speed + Player.velocity.x;
        public static float Speed = 6f;

        static Game()
        {
            Debug.Log("aye");

            Player = SpawnObject<Character>("Player");
            //Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();

            Enemy.Target = Player; // Maybe enemies should be able to target other enemies.
        }

        public static void Give(string name, Character chr)
        {
            if(!chr.inventory.CanAppend)
                return;

            var item = SpawnObject<Item>(name);
            chr.Equip(item, true);
        }

        public static T SpawnObject<T>(string name, Vector2 pos = default) where T : Object
        {
            // Locate the config file for this object by name.
            foreach(var file in Resources.LoadAll<ScriptableObject>("Config"))
            {
                Debug.Log($"[Type: {file.type.Name}] [File: {file.name}]");
                if(file.name == name)
                {
                    var obj = new GameObject(file.name);
                    obj.transform.position = pos;

                    var comp = obj.AddComponent<T>();
                    comp.SetProperties(file);

                    return comp;
                }
            }
            return null;
        }
    }
}