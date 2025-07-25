// SPDX-FileCopyrightText: 2023 Skye <22365940+Skyedra@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 sleepyyapril <123355664+sleepyyapril@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Content.Client.Gameplay;
using Content.Client.Info;
using Robust.Client.Input;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controllers;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Input;
using Robust.Shared.Input.Binding;

namespace Content.Client.UserInterface.Systems.Info;

public sealed class CloseAllWindowsUIController : UIController
{
    [Dependency] private readonly IInputManager _inputManager = default!;
    [Dependency] private readonly IUserInterfaceManager _uiManager = default!;

    public override void Initialize()
    {
        _inputManager.SetInputCommand(EngineKeyFunctions.WindowCloseAll,
            InputCmdHandler.FromDelegate(session => CloseAllWindows()));
    }

    private void CloseAllWindows()
    {
        foreach (var childControl in new List<Control>(_uiManager.WindowRoot.Children)) // Copy children list as it will be modified on Close()
        {
            if (childControl is BaseWindow)
            {
                ((BaseWindow) childControl).Close();
            }
        }
    }
}

