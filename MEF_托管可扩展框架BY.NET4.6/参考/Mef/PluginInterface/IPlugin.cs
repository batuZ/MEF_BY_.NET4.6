using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MefDemo
{
    public interface IPlugin
    {
        string Text { get; } 
        void Do(); 
    }
}
