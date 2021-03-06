﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.UserInterface
{
    public struct CommandInfo
    {
        public string[] users;
        public Action callable;

        public CommandInfo(string[] users, Action callable)
        {

            this.users = users;
            this.callable = callable;
        }
    }
}
