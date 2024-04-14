using UniOwl.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public class UniSelectableAudio : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField] private Selectable _selectable;
        
        [SerializeField] private AudioClip hoverClip;
        [SerializeField] private AudioClip clickClip;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;
            AudioSFXSystem.PlayClip2D(hoverClip);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selectable != null && !_selectable.interactable) return;
            AudioSFXSystem.PlayClip2D(clickClip);
        }
    }
}