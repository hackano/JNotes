using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Input;
using JNotes.Classes;
using JNotes.Models;
using JNotes.Web;

namespace JNotes.ViewModels
{
	public class TreeVm : ViewModelBase
	{


		#region Command

		public ICommand AddFolderCommand { get; set; }
		public ICommand DeleteFolderCommand { get; set; }
		public ICommand SelectedCommand { get; set; }

		private void InitCommands()
		{
			DeleteFolderCommand = new DelegateCommand(Delete, CanDelete);
			AddFolderCommand = new DelegateCommand(Add, CanAdd);
			SelectedCommand = new DelegateCommand(Select, CanSelect);
		}


		/// <summary>
		/// Selected command
		/// </summary>

		private bool CanSelect(object param)
		{
			return true;
		}
		public void Select(object param)
		{
			if (param == null)
			{
				_selectedfolder = null;
				return;
			}

			_selectedfolder = (TreeFolder)param;
		
			System.Diagnostics.Debug.WriteLine(_selectedfolder.Title);
		}


		/// <summary>
		/// Delete command
		/// </summary>
		
		private bool CanDelete(object param)
		{
			return true;
		}
		public void Delete(object param)
		{
			if(param==null)
				return;
			RemoveFolder((TreeFolder)param);
		}

		/// <summary>
		/// Add command
		/// </summary>
		
		private bool CanAdd(object param)
		{
			return true;
		}
		public void Add(object param)
		{
			if (param == null)
				AddFolder(null);
				else
				AddFolder((TreeFolder)param);
		}

		


		#endregion

		private TreeFolder _selectedfolder;
		private readonly jTreeServiceDomain _treeService;
		private ObservableCollection<TreeFolder> _treefolders;

		public TreeVm()
		{

			if (IsDesignTime())
				return;
	
		
			_treefolders = new ObservableCollection<TreeFolder>();
			_treeService = new jTreeServiceDomain();

			LoadOperation<TreeData> loadoperation = _treeService.Load(_treeService.GetTeamHierarchyQuery());
			loadoperation.Completed += (s, ee) => AddNodes();
			InitCommands();

			

		}

		public bool IsDesignTime()
		{
			return DesignerProperties.GetIsInDesignMode(Application.Current.RootVisual);
		}	

		public ObservableCollection<TreeFolder> TreeFolders
		{
			get { return _treefolders; }
			set
			{
				_treefolders = value;
				OnPropertyChanged("TreeFolders");
			}
		}


		private void AddNodes()
		{
			if (_treeService.TreeDatas != null)
				foreach (TreeData t in _treeService.TreeDatas)
				{
					TreeFolder newitem = TreeFolder(t);
					_treefolders.Add(newitem);
				}
		}

		private ObservableCollection<TreeFolder> AddChilds(EntityCollection<TreeData> treedata)
		{
			if (treedata.Count == 0)
				return null;

			var childs = new ObservableCollection<TreeFolder>();

			foreach (TreeData t in treedata)
			{
				TreeFolder newitem = TreeFolder(t);
				childs.Add(newitem);
			}

			return childs;
		}

		private TreeFolder TreeFolder(TreeData t)
		{
			TreeFolder newitem;
			return newitem = new TreeFolder
				{
					Title = t.Title,
					Text = t.Text,
					Id = t.Id,
					Parent = t.Parent,
					IsExpanded = t.isExpanded,
					SubFolders = AddChilds(t.ChildNodes),
					Data = t
				};
			return newitem;
		}

		public void AddFolder(TreeFolder parent)
		{
			if (parent == null)
				_treefolders.Add(AddNewSubFolder(null));
			else
			{
				parent.SubFolders.Add(AddNewSubFolder(parent.Id));
				parent.IsExpanded = true;
			}
		}


		private TreeFolder AddNewSubFolder(Guid? pId)
		{
			var newitem = new TreeFolder
				{
					Title = "[New Item]",
					Id = Guid.NewGuid(),
					Parent = pId
				};

			var treedata = new TreeData
				{
					Title = newitem.Title,
					Id = newitem.Id,
					Parent = newitem.Parent
				};

			_treeService.TreeDatas.Add(treedata);
			_treeService.SubmitChanges();

			newitem.Data = treedata;

			return newitem;
		}


		


		public void RemoveFolder(TreeFolder item)
		{
			if (item == null)
				return;



			if (item.SubFolders.Count > 0)
				return;


			if (item.Data != null)
			{
				_treeService.TreeDatas.Remove(item.Data);
				_treeService.SubmitChanges();
				item.Title = "[~Removed]"; // Om Titeln innehåller denna text så kommer Ui:t att dölja raden i treeview.
				// Dett är en workorund för att inte hel treeviewn uppdtareas när subfolders raderas.
				// I TreeViewPage.xaml triggas en GotoStateAction när titeln ändras till [~Removed]

				
				foreach (TreeFolder treeFolder in TreeFolders)
				{
				
					if (treeFolder == item)
					{
						treeFolder.Title = "[~Removed]";
						TreeFolders.Remove(item);
						
						
						return;
					}
					if (FindAndRemoveItem(item, treeFolder.SubFolders))
						return;
				}
			}
		}

		private bool FindAndRemoveItem(TreeFolder item, ObservableCollection<TreeFolder> subFolders)
		{
	
			foreach (TreeFolder treeFolder in subFolders)
			{
				if (treeFolder == item)
				{
					treeFolder.Title = "[~Removed]";
				    subFolders.Remove(item);
					return true;
				}

				if (FindAndRemoveItem(treeFolder, treeFolder.SubFolders))
					return true;
			}

			return false;
		}
	}
}