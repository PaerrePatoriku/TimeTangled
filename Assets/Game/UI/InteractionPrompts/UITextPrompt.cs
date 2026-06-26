using TMPro;
using UnityEngine;

namespace Game.UI.InteractionPrompts
{
    public class UITextPrompt : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI textContainer;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void SetText(string text)
        {
            textContainer.text = text;
        }
    }
}
