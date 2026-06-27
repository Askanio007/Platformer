using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimationComponent : MonoBehaviour
    {
        [SerializeField] private bool _isPlaying;
        [SerializeField] private string _firstClipName;
        [SerializeField] private AnimationState[] states;

        private SpriteRenderer _spriteRenderer;
        private float _secondsPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;
        private Dictionary<string, AnimationState> _states = new();
        private AnimationState _currentState;

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            foreach (var state in states)
            {
                _states.Add(state.Name, state);
            }

            if (_isPlaying)
            {
                SetClip(_firstClipName);
            }
        }

        void Update()
        {
            if (!_isPlaying || _nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= _currentState.Sprites.Length)
            {
                if (_currentState.Loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    _isPlaying = false;
                    _spriteRenderer.sprite = null; 
                    _currentState.OnComplete?.Invoke();
                    return;
                }
            }

            _spriteRenderer.sprite = _currentState.Sprites[_currentSpriteIndex];
            _nextFrameTime += _secondsPerFrame;
            _currentSpriteIndex++;
        }

        public void SetClip(string name)
        {
            _currentState = _states[name];
            _currentSpriteIndex = 0;
            _secondsPerFrame = 1f / _currentState.FrameRate;
            _nextFrameTime = Time.time + _secondsPerFrame;
            _isPlaying = true;
        }

        [System.Serializable]
        public class AnimationState
        {
            public string Name;
            public bool Loop;
            public bool AllowNext;
            public Sprite[] Sprites;
            public int FrameRate;
            public UnityEvent OnComplete;
        }
    }
}


