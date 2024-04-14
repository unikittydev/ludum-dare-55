using System;
using UnityEngine;

namespace Game.Magic
{
    [Serializable]
    public class SummonButtonState
    {
        public Color _outerTint = Color.white;
        public Color _innerTint = Color.white;
        public Color _handTint = Color.white;
        
        public float _outerScale = 3.5f;
        public float _innerScale = 2.5f;
        public float _handScale = 1f;
    }
}