using AloneCrew;
using AloneCrew.Components;
using UnityEngine;

public class CrabsShootStateStage3 : AbstractCrabsShootState
{
    protected override void UpdateStage(AbstractProjectileSpawner spawner)
    {
        spawner.Stage = 2;
    }
}
