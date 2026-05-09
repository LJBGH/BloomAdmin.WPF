using System.Windows.Input;

namespace BloomAdmin.Main.Common
{
    /// <summary>
    ///  委托命令基类，提供了一个简单的实现，可以通过设置 DoExecute 和 DoCanExecute 来定义命令的行为。
    /// </summary>
    public class CommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// 执行命令前调用此方法，触发 DoCanExecute 委托，判断命令是否可以执行。
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return DoCanExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// 执行命令时调用此方法，触发 OnExecute 委托，执行具体的命令逻辑。
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            DoExecute?.Invoke(parameter);
        }

        public Action<object> DoExecute { get; set; }
        public Func<object, bool> DoCanExecute { get; set; }
    }
}
