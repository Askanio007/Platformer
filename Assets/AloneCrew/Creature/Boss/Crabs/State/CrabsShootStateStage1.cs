using AloneCrew;
using AloneCrew.Components;
using UnityEngine;

public class CrabsShootStateStage1 : AbstractCrabsShootState
{
    protected override void UpdateStage(AbstractProjectileSpawner spawner)
    {
        spawner.Stage = 0;
    }
}
