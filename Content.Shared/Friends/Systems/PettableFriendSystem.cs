// SPDX-FileCopyrightText: 2024 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Content.Shared.Chemistry.Components;
using Content.Shared.Friends.Components;
using Content.Shared.Interaction.Events;
using Content.Shared.NPC.Components;
using Content.Shared.NPC.Systems;
using Content.Shared.Popups;
using Content.Shared.Timing;

namespace Content.Shared.Friends.Systems;

public sealed class PettableFriendSystem : EntitySystem
{
    [Dependency] private readonly NpcFactionSystem _factionException = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly UseDelaySystem _useDelay = default!;

    private EntityQuery<FactionExceptionComponent> _exceptionQuery;
    private EntityQuery<UseDelayComponent> _useDelayQuery;

    public override void Initialize()
    {
        base.Initialize();

        _exceptionQuery = GetEntityQuery<FactionExceptionComponent>();
        _useDelayQuery = GetEntityQuery<UseDelayComponent>();

        SubscribeLocalEvent<PettableFriendComponent, UseInHandEvent>(OnUseInHand);
        SubscribeLocalEvent<PettableFriendComponent, GotRehydratedEvent>(OnRehydrated);
    }

    private void OnUseInHand(Entity<PettableFriendComponent> ent, ref UseInHandEvent args)
    {
        var (uid, comp) = ent;
        var user = args.User;
        if (args.Handled || !_exceptionQuery.TryGetComponent(uid, out var exceptionComp))
            return;

        if (_useDelayQuery.TryGetComponent(uid, out var useDelay) && !_useDelay.TryResetDelay((uid, useDelay), true))
            return;

        var exception = (uid, exceptionComp);
        if (_factionException.IsIgnored(exception, user))
        {
            _popup.PopupClient(Loc.GetString(comp.FailureString, ("target", uid)), user, user);
            return;
        }

        // you have made a new friend :)
        _popup.PopupClient(Loc.GetString(comp.SuccessString, ("target", uid)), user, user);
        _factionException.IgnoreEntity(exception, user);
        args.Handled = true;
    }

    private void OnRehydrated(Entity<PettableFriendComponent> ent, ref GotRehydratedEvent args)
    {
        // can only pet before hydrating, after that the fish cannot be negotiated with
        if (!TryComp<FactionExceptionComponent>(ent, out var comp))
            return;

        _factionException.IgnoreEntities(args.Target, comp.Ignored);
    }
}
