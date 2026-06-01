using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBot
{
    internal class User
    {
        public string Name { get; set; }
        public int MessageCount { get; private set; }

        public User(string name)
        {
            Name = name;
            MessageCount = 0;
        }

        
        public void IncrementMessageCount()
        {
            MessageCount++;
        }
    }
}

