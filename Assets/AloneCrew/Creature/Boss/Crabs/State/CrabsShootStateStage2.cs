using AloneCrew;
using AloneCrew.Components;
using UnityEngine;

public class CrabsShootStateStage2 : AbstractCrabsShootState
{
    protected override void UpdateStage(AbstractProjectileSpawner spawner)
    {
        spawner.Stage = 1;
    }
}
