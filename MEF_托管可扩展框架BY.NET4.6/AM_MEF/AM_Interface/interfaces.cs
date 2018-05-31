using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM_Interface
{
    /// <summary>
    /// 插件接口，需要继承插件才能被识别
    /// 要放在main能找到的地方
    /// </summary>
    public interface IToolsPlugin
    {
        void Run(object[] args);
        void Dispose();
        void ChangeLangvage(string TagLangvage);
    }
    
    /// <summary>
    /// 插件描述
    /// </summary>
    public interface IToolsInfo
    {
        string toolName { get; }
    }

    /// <summary>
    /// 设置插件名称
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SetToolInfo : Attribute, IToolsInfo
    {
        public string toolName { get; private set; }
        public SetToolInfo(string name) { toolName = name; }
    }
}
