using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    interface IConsoleUI
    {
        void registerCommands(Dictionary<string, Action> commandsMap);
    }
}
