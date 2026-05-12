using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BloomAdmin.Main.Common
{
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 属性值改变时调用此方法，触发 PropertyChanged 事件，通知 UI 更新绑定的属性值。
        /// </summary>
        /// <param name="propertyName"></param>
        public void DoNotify([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
