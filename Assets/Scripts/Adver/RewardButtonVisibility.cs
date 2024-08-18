using System;
using UnityEngine;
using Zenject;

namespace Adver
{
    public class RewardButtonVisibility : IInitializable, IDisposable
    {
        private readonly int _initialRewCount;
        private int _rewCount;

        private readonly LevelSwitcher _levelSwitcher;
        private readonly SkipLevelView _skipLevelView;

        public RewardButtonVisibility(LevelSwitcher levelSwitcher, SkipLevelView skipLevelView, int initialRewCount = 1)
        {
            _levelSwitcher = levelSwitcher;
            _skipLevelView = skipLevelView;
            _initialRewCount = initialRewCount;
            _rewCount = initialRewCount;
        }


        public void Initialize()
        {
            _levelSwitcher.OnLevelUpdate += OnLevelUpdate;
            _skipLevelView.OnRewardRelease += SkipLevelViewOnOnRewardRelease;
        }

        public void Dispose()
        {
            _levelSwitcher.OnLevelUpdate -= OnLevelUpdate;
            _skipLevelView.OnRewardRelease -= SkipLevelViewOnOnRewardRelease;
        }

        private void OnLevelUpdate()
        {
            Debug.Log($"ON LEVEL UPDATE {_skipLevelView.gameObject.activeInHierarchy}");
            _rewCount = _initialRewCount;
            _skipLevelView.gameObject.SetActive(true);
            
            Debug.Log($"ON LEVEL UPDATE {_skipLevelView.gameObject.activeInHierarchy}");

        }

        private void SkipLevelViewOnOnRewardRelease()
        {
            _rewCount -= 1;
            if (_rewCount <= 0)
            {
                _skipLevelView.gameObject.SetActive(false);
            }
        }
    }
}