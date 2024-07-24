﻿using Robust.Shared.GameStates;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;

namespace Content.Shared._RMC14.Projectiles;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
[Access(typeof(RMCProjectileSystem))]
public sealed partial class SpawnOnTerminateComponent : Component
{
    [DataField, AutoNetworkedField]
    public EntityCoordinates? Origin;

    [DataField(required: true), AutoNetworkedField]
    public EntProtoId Spawn;
}
