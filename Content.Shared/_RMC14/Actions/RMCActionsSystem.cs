using Content.Shared.Actions;
using Content.Shared.Actions.Events;

namespace Content.Shared._RMC14.Actions;

public sealed class RMCActionsSystem : EntitySystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;

    private EntityQuery<ActionSharedCooldownComponent> _actionSharedCooldownQuery;

    public override void Initialize()
    {
        _actionSharedCooldownQuery = GetEntityQuery<ActionSharedCooldownComponent>();

        SubscribeLocalEvent<ActionSharedCooldownComponent, ActionPerformedEvent>(OnSharedCooldownPerformed);
    }

    private void OnSharedCooldownPerformed(Entity<ActionSharedCooldownComponent> ent, ref ActionPerformedEvent args)
    {
        if (ent.Comp.OnPerform)
            ActivateSharedCooldown((ent, ent), args.Performer);
    }

    public void ActivateSharedCooldown(Entity<ActionSharedCooldownComponent?> action, EntityUid performer)
    {
        if (!Resolve(action, ref action.Comp, false))
            return;

        if (action.Comp.Cooldown == TimeSpan.Zero)
            return;

        foreach (var (actionId, _) in _actions.GetActions(performer))
        {
            if (!_actionSharedCooldownQuery.TryComp(actionId, out var shared) ||
                !shared.Ids.Overlaps(action.Comp.Ids))
            {
                continue;
            }

            _actions.SetIfBiggerCooldown(actionId, action.Comp.Cooldown);
        }
    }
}
