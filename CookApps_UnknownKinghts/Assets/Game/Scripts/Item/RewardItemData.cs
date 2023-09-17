using System;
using UnityEngine;

namespace InGame.ForItem.ForReward
{
    [Serializable]
    public class RewardItemData
    {
        public ERewardType Type;
        public int         Value;
        public Sprite      StarSprite;
        public Sprite      FrameSprite;
        public Sprite      IconSprite;
    }
}