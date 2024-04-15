using TMPro;
using UnityEngine;

// Used for shadows; had issues with material underline and material variants in TMP_Text
public class TextProxy : MonoBehaviour
{
    [SerializeField] private TMP_Text _self;
    [SerializeField] private TMP_Text _target;


    private void Update()
    {
        _self.text = _target.text;
    }
}
