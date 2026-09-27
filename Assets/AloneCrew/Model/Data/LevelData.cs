using System;
using System.Collections.Generic;
using System.Linq;
using AloneCrew.Model.Definitions;
using UnityEngine;

namespace AloneCrew.Model.Data
{
    public class LevelData
    {
        [SerializeField] public List<LevelProgress> _progress;

        public int GetLevel(StatId statId)
        {
            var progress = _progress.FirstOrDefault(x => x.Id == statId);
            return progress?.Level ?? 0;
        }

        public void LevelUp(StatId statId)
        {
            var progress = _progress.FirstOrDefault(x => x.Id == statId);
            if (progress == null)
                _progress.Add(new LevelProgress(statId, 1));
            else 
                progress.Level++;
        }
    }

    [Serializable]
    public class LevelProgress
    {
        public StatId Id;
        public int Level;

        public LevelProgress(StatId id, int level)
        {
            Id = id;
            Level = level;
        }
    }
}