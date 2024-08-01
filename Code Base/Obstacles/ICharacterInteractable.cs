using UnityEngine;

namespace Code_Base.Obstacles
{
    public interface ICharacterInteractable
    {
        public GameObject GameObject {get;}
        public void Interact(Character сharacter);
    }
}