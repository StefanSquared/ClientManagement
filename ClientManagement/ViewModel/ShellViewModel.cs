using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ClientManagement.Data;
using ClientManagement.Data.Repositories;
using ClientManagement.Models;
using ClientManagement.Resources;
using ClientManagement.ViewModel.Base;
using Prism.Commands;

namespace ClientManagement.ViewModel
{

	public class ShellViewModel : ViewModelBase
	{
		public ShellViewModel(IRepository<Client> repo)
		{
			_repo = repo;
			GetData();
		}

		public string Title { get; set; } = "Client Management";
		public string ValidationMessage { get; set; } = "";

		public ObservableCollection<ClientDto> ClientsList;

		private readonly IRepository<Client> _repo;

		public DelegateCommand SaveCommand { get; set; }
		public DelegateCommand NewCommand { get; set; }
		public DelegateCommand DeleteCommand { get; set; }

		public DelegateCommand <ClientDto> SelectionChangedCommand { get; set; }

		private Client _selectedClient = new Client();
		public Client SelectedClient
		{
			get => _selectedClient;
			set
			{
				_selectedClient = value;
				OnPropertyChanged();
			}
		}

		private bool _isEditing = false;
		private bool _canEdit = true;
		public bool CanEdit
		{
			get => _canEdit;
			set
			{
				_canEdit = value;
				_isEditing = false;
				OnPropertyChanged();
			}
		}

		private bool _canDelete = false;

		public bool CanDelete
		{
			get => _canDelete;
			set
			{
				_canDelete = value;
				OnPropertyChanged();
			}
		}

		protected override void RegisterCollections()
		{
			ClientsList = new ObservableCollection<ClientDto>();
		}

		protected override void RegisterCommands()
		{
			SaveCommand = new DelegateCommand(Save);
			NewCommand = new DelegateCommand(New);
			DeleteCommand = new DelegateCommand(Delete);
			SelectionChangedCommand = new DelegateCommand<ClientDto>(SelectionChanged);
		}

		private void Delete()
		{
			_repo.Delete(SelectedClient.Id);
			var clientToDelete = ClientsList.Single(client => client.Id == SelectedClient.Id);
			ClientsList.Remove(clientToDelete);
		}

		private void SelectionChanged(ClientDto client)
		{
			if (client != null)
			{
				SelectedClient = _repo.Get(client.Id);
				CanDelete = true;
			}
			else
			{
				SelectedClient = new Client();
				CanDelete = false;
			}
			CanEdit = false;
		}

		private void New()
		{
			SelectedClient = new Client();
			CanEdit = true;
		}

		private void GetData()
		{
			var clients = _repo
				.Get()
				.Select(client => new ClientDto()
				{
					Id = client.Id,
					Name = client.Name,
					BirthDate = client.BirthDate
				})
				.ToList();
			ClientsList.AddRange(clients);
			OnPropertyChanged();
		}

		public void Save()
		{
			if (!CanEdit)
			{
				CanEdit = true;
				_isEditing = true;
			}
			else
			{
				// if editing existing client (has id) this will update the database, otherwise (id==null) add a new entry to the database
				try
				{
					_repo.Save(SelectedClient);
				}
				catch (Exception e)
				{
					ValidationMessage = e.Message;
					OnPropertyChanged();
					return;
				}

				if(!_isEditing)
					ClientsList.Add(new ClientDto(SelectedClient));
				else
				{
					var clientToUpdate = ClientsList.Single(client => client.Id == SelectedClient.Id);
					if (clientToUpdate != null)
					{
						var index = ClientsList.IndexOf(clientToUpdate);
						ClientDto updatedClient = new ClientDto(SelectedClient);
						ClientsList.RemoveAt(index);
						ClientsList.Insert(index, updatedClient);
					}
				}
				CanEdit = false;
			}
		}
	}
}