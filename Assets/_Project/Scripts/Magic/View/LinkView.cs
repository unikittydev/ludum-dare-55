using UniOwl.Audio;
using UnityEngine;

namespace Game.Magic.Elements
{
    public class LinkView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
		[SerializeField] private Material[] _materials;

		[SerializeField] private AudioCue _connectCue;
		
		private void Awake()
		{
			_line.material = _materials[Random.Range(0, _materials.Length)];
			AudioSFXSystem.PlayCue2D(_connectCue);
		}
    }
}