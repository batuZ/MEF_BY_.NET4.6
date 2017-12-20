using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF_Interface
{
    //共用接口
    public interface WriteTool
    {
        string toolName { get; }
        void Run();
    }
}
