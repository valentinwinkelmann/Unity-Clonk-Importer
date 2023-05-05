
using UnityEngine;


namespace UnityClonk
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class UnityClonkAnimation : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        public UnityClonkActMap actMap;
        public float speed = 1f;
        public string action;
        private float frame;

        public string fallback = "Walk";




        private string prevAction = null;
        private float prevFrame = -1000;
        private void Awake()
        {
            spriteRenderer= GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            if(speed != 0)
            {
                frame += Time.deltaTime * speed;
            }



            SetSprite(action, frame, fallback);

        }
        private void OnValidate()
        {   if (Application.isPlaying) return;
            SetSprite(action, frame, fallback);
        }

        public void SetSprite(string name, int frame, string fallbackName = "Walk")
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (actMap.Actions.ContainsKey(name) && actMap != null)
            {
                int frameIndex = Mathf.Abs(frame % actMap.Actions[name].Length);
                spriteRenderer.sprite = actMap.Actions[name][frameIndex];
            }
            else
            {
                int frameIndex = Mathf.Abs(frame % actMap.Actions[fallbackName].Length);
                spriteRenderer.sprite = actMap.Actions[fallbackName][frameIndex];
            }
        }
        public void SetSprite(string name, float time, string fallbackName = "Walk")
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (actMap.Actions.ContainsKey(name) && actMap != null)
            {
                int frame = Mathf.RoundToInt(actMap.Actions[name].Length * time);

                int frameIndex = Mathf.Abs(frame % actMap.Actions[name].Length);
                spriteRenderer.sprite = actMap.Actions[name][frameIndex];
            }
            else
            {
                int frame = Mathf.RoundToInt(actMap.Actions[fallbackName].Length * time);
                int frameIndex = Mathf.Abs(frame % actMap.Actions[fallbackName].Length);
                spriteRenderer.sprite = actMap.Actions[fallbackName][frameIndex];
            }
        }
    }
}
