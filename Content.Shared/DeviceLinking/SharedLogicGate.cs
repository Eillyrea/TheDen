// SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2025 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later AND MIT

using Robust.Shared.Serialization;

namespace Content.Shared.DeviceLinking;


/// <summary>
/// Types of logic gates that can be used, determines how the output port is set.
/// </summary>
[Serializable, NetSerializable]
public enum LogicGate : byte
{
    Or,
    And,
    Xor,
    Nor,
    Nand,
    Xnor
}

/// <summary>
/// Tells clients which logic gate layer to draw.
/// </summary>
[Serializable, NetSerializable]
public enum LogicGateVisuals : byte
{
    Gate
}

/// <summary>
/// Sprite layer for the logic gate.
/// </summary>
[Serializable, NetSerializable]
public enum LogicGateLayers : byte
{
    Gate,
    InputA,
    InputB,
    Output
}

/// <summary>
/// The possible states of a logic-capable signal.
/// Stored in network payload data of device network messages.
/// </summary>
[Serializable, NetSerializable]
public enum SignalState : byte
{
    Momentary, // Instantaneous pulse high, compatibility behavior
    Low,
    High
}
