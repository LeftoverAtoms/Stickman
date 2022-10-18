using UnityEngine;

namespace Stickman
{
    public class Gadget : Item
    {
        public ScriptableItem ItemAttribute;

        public override void Start()
        {
            base.Start();

            ItemAttribute = Attribute as ScriptableItem;
        }
    }
}