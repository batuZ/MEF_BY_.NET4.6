
namespace MEF_Interface
{
    //共用接口
    public interface WriteTool
    {
        string toolName { get; }
        void WriteRun();
    }

    public interface ReadTool
    {
        string toolName { get; }
        void ReadRun();
    }
}
