using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Obstacles
{
    public class BonusObstacle : BaseObstacle
    {
        [Header("Bonus Animation Setting")]
        [SerializeField, Min(1)] private int blinkCount = 5;
        [SerializeField, Range(0.1f, 1f)] private float blinkDuration = 0.2f;
        [SerializeField, Range(1f, 2f)] private float scaleMultiplier = 1.2f;
        [SerializeField, Range(0f, 1f)] private float blinkFadeValue = 0.4f;
        [SerializeField, Min(0.1f)] private float finalFadeDuration = 2f;

        private Sequence _sequence; 
        private Vector3 originalScale;
        private Vector3 scaledUp;

        private void Awake()
        {
            originalScale = transform.localScale;
            scaledUp = originalScale * scaleMultiplier;
        }

        private void OnDisable()
        {
            _sequence.Kill();
        }


        public override void InitObstacle(Vector2 position, float force, float gravityScale, ObstacleType type)
        {
            
            base.InitObstacle(position, force, gravityScale, type);
            
            _sequence = DOTween.Sequence();

            for (int i = 0; i < blinkCount; i++)
            {
                _sequence.Append(spriteRenderer.DOFade(blinkFadeValue, blinkDuration))
                    .Join(transform.DOScale(scaledUp, blinkDuration))
                    .Append(spriteRenderer.DOFade(1, blinkDuration))
                    .Join(transform.DOScale(originalScale, blinkDuration));
            }

            _sequence.Append(spriteRenderer.DOFade(0, finalFadeDuration))
                .Join(transform.DOScale(Vector3.zero, finalFadeDuration))
                .OnComplete(() =>
                {
                     gameObject.SetActive(false);
                });

            _sequence.Play();            
        }
    }
}