using System.Windows;
using ClientManagement.Data.Repositories;
using Prism.Unity;
using Unity;

//using Microsoft.Practices.Unity;

namespace ClientManagement.Startup
{
	public class Bootstrapper:UnityBootstrapper
	{
		protected override DependencyObject CreateShell()
		{
			return Container.Resolve<Views.Shell>(); 
		}

		protected override void InitializeShell()
		{
			Application.Current.MainWindow.Show();
		}

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();
			Container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
		}
	}
}