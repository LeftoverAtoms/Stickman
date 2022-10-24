using UnityEngine;

namespace Stickman
{
    public class Game : MonoBehaviour
    {
        public static Background Background;
        public static Player Player;

        public static bool IsGameOver = false;

        public static float RelativeSpeed => Player.velocity.x + Speed;
        public static float Speed = 6f;

        public void Start()
        {
            Player = Spawn<Player>("Player");
            Background = GameObject.FindGameObjectWithTag("Background").GetComponent<Background>();

            Enemy.Target = Player;
        }

        public static void Give(string name, Character chr)
        {
            if(!chr.inventory.CanAppend)
                return;

            var item = Spawn<Item>(name);
            chr.Equip(item, true);
        }

        public static T Spawn<T>(string name, Vector2 pos = default) where T : Object
        {
            foreach(var file in Resources.LoadAll<ScriptableObject>("Config"))
            {
                if(file.name != name) continue;

                var obj = new GameObject(file.name);
                obj.transform.position = pos;

                var cpt = obj.AddComponent<T>();
                cpt.SetProperties(file);

                return cpt;
            }
            return null;
        }
    }
}