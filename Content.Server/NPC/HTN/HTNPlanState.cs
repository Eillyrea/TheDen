// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

namespace Content.Server.NPC.HTN;

[Flags]
public enum HTNPlanState : byte
{
    TaskFinished = 1 << 0,

    PlanFinished = 1 << 1,
}
