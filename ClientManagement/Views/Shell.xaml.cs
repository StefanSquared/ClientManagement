using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ClientManagement.Models;
using ClientManagement.Resources;
using ClientManagement.ViewModel;

namespace ClientManagement.Views
{
	/// <summary>
	/// Interaction logic for Shell.xaml
	/// </summary>
	public partial class Shell : Window
	{
		private ShellViewModel viewModelRef;
		public Shell(ShellViewModel viewModel)
		{
			InitializeComponent();
			DataContext = viewModel;
			lvClients.ItemsSource = viewModel.ClientsList;
			viewModelRef = viewModel;
		}

		private GridViewColumnHeader listViewSortCol = null;
		private SortAdorner listViewSortAdorner = null;
		public void ColumnHeader_Click(object sender, RoutedEventArgs e)
		{
			GridViewColumnHeader column = (sender as GridViewColumnHeader);
			string sortBy = column.Tag.ToString();
			if (listViewSortCol != null)
			{
				AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
				lvClients.Items.SortDescriptions.Clear();
			}

			ListSortDirection newDir = ListSortDirection.Ascending;
			if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
				newDir = ListSortDirection.Descending;

			listViewSortCol = column;
			listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
			AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
			lvClients.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));

			if (sortBy.Equals("Name"))
			{
				if (newDir == ListSortDirection.Descending)
				{
					Sort(viewModelRef.ClientsList, (a, b) => { return a.Name.CompareTo(b.Name); });
				}
				else
				{
					Sort(viewModelRef.ClientsList, (a, b) => { return b.Name.CompareTo(a.Name); });
				}
			}
			else
			{
				if (newDir == ListSortDirection.Descending)
				{
					Sort(viewModelRef.ClientsList, (a, b) => { return a.BirthDate.CompareTo(b.BirthDate); });
				}
				else
				{
					Sort(viewModelRef.ClientsList, (a, b) => { return a.BirthDate.CompareTo(b.BirthDate); });
				}
			}
			
		}
		public static void Sort<T>(ObservableCollection<T> collection, Comparison<T> comparison)
		{
			var sortableList = new List<T>(collection);
			sortableList.Sort(comparison);

			for (int i = 0; i < sortableList.Count; i++)
			{
				collection.Move(collection.IndexOf(sortableList[i]), i);
			}
		}

	}
}
