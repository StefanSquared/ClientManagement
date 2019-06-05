using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClientManagement.Annotations;

namespace ClientManagement.ViewModel.Base
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		protected virtual void RegisterCommands() { }
		protected virtual void RegisterCollections() { }

		protected ViewModelBase()
		{
			RegisterCommands();
			RegisterCollections();
		}


		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}