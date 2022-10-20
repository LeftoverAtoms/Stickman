using UnityEngine;

namespace Stickman
{
    public static class Game
    {
        public static Background Background;
        public static Player Player;

        public static bool IsGameOver = false;

        public static float RelativeSpeed => Speed + Player.velocity.x;
        public static float Speed = 6f;

        static Game()
        {
            //Player = SpawnObject("Player").AddComponent<Player>();
            //Player = new GameObject().AddComponent<Player>();
            SpawnObject("Player").AddComponent<Player>();
            Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();

            Enemy.Target = Player; // Maybe enemies should be able to target other enemies.
        }

        public static void Give(string name, Character chr)
        {
            if(!chr.inventory.CanAppend)
                return;

            GameObject obj = SpawnObject(name);
            if(obj.TryGetComponent<Item>(out Item item))
            {
                chr.Equip(item, true);
            }
        }

        public static GameObject SpawnObject(string name, Vector2 pos = default)
        {
            // Locate the config file for this object by name.
            foreach(var file in Resources.LoadAll<ScriptableObject>("Config"))
            {
                Debug.Log($"[Type: {file.type.Name}] [File: {file.name}]");
                if(file.name == name)
                {
                    var obj = new GameObject(file.name);
                    obj.transform.position = pos;

                    var comp = obj.AddComponent(file.type);
                    (comp as Object).SetProperties(file as ScriptableCharacter);

                    return obj;
                }
            }
            return null;
        }
    }
}