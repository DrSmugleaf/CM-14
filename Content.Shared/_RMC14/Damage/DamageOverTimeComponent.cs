﻿using Content.Shared.Chat.Prototypes;
using Content.Shared.Damage;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._RMC14.Damage;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
[Access(typeof(SharedRMCDamageableSystem))]
public sealed partial class DamageOverTimeComponent : Component
{
    [DataField(required: true), AutoNetworkedField]
    public DamageSpecifier Damage = new();

    [DataField, AutoNetworkedField]
    public TimeSpan DamageEvery = TimeSpan.FromSeconds(1);

    [DataField, AutoNetworkedField]
    public TimeSpan NextDamageAt;

    [DataField, AutoNetworkedField]
    public bool AffectsDead;

    [DataField, AutoNetworkedField]
    public bool AffectsInfectedNested;

    [DataField, AutoNetworkedField]
    public ProtoId<EmotePrototype>? Emote = "Scream";
}
