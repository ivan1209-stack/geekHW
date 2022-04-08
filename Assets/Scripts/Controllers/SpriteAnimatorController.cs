using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class SpriteAnimatorController : IDisposable
    {
        private SpriteAnimatorConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimatorController(SpriteAnimatorConfig config)
        {
            _config = config;
        }
        private sealed class Animation
        {
            public AnimState Track;
            public List<Sprite> Sprites;
            public bool Loop;
            public float Speed = 10f;
            public float Counter;
            public bool Sleep;

            public void Update()
            {
                if (Sleep) return;
                Counter += Time.deltaTime * Speed;

                if (Loop)
                {
                    while (Counter > Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                }
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count;
                    Sleep = true;
                }
            }
        }

        public void StartAnimation(SpriteRenderer sprite, AnimState track, bool loop, float speed)
        {
            if (_activeAnimations.TryGetValue(sprite, out var animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Counter = 0;
                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0;
                }
            }
            else
            {
                _activeAnimations.Add(sprite, new Animation()
                {
                    Track = track,
                    Speed = speed,
                    Loop = loop,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites
                });
            }
        }
        
        public void StopAnimation(SpriteRenderer sprite)
        {
            if (_activeAnimations.ContainsKey(sprite))
            {
                _activeAnimations.Remove(sprite);
            }
        }

        public void Update()
        {
            foreach (var animation in _activeAnimations)
            {
                animation.Value.Update();
                if (animation.Value.Counter < animation.Value.Sprites.Count)
                {
                    animation.Key.sprite = animation.Value.Sprites[(int) animation.Value.Counter];
                }
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}