using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDo
{
    internal sealed class BindableProperty<T> : INotifyPropertyChanged
    {
        private T value;

        public event PropertyChangedEventHandler PropertyChanged;

        public BindableProperty(T value)
        {
            this.value = value;
        }

        public T Get() => value;

        public void Set(T value, [CallerMemberName] string callerName = null)
        {
            if (!Equals(this.value, value))
            {
                this.value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerName));
            }
        }
    }
}
