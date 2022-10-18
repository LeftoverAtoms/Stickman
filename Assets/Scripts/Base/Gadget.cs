using UnityEngine;

namespace Stickman
{
    public class Gadget : Item
    {
        public ScriptableItem ItemAttribute;

        protected override void Start()
        {
            base.Start();

            ItemAttribute = Attribute as ScriptableItem;
        }
    }
}