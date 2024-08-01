using System;
using Unity.VisualScripting;

namespace Code_Base.Obstacles
{
    public interface IEatable: ICharacterInteractable
    {
        public float Force { get; set; } 
    }
}