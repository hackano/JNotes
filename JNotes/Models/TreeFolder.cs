using System;
using System.Collections.ObjectModel;
using JNotes.Web;
using ReactiveUI;

namespace JNotes.Models
{
	public class TreeFolder : ViewModelBase
	{
		#region Internal

		private bool _candelete;
		private object _hosted;
		private Guid _id;
		private System.Nullable<bool> _isexpanded;
		private System.Nullable<bool> _isselected;
		private TreeFolder _owner;
		private System.Nullable<Guid> _parent;
		private ObservableCollection<TreeFolder> _subFolders;
		private int _subfolderscount;
		private string _text;
		private string _title;
	

		#endregion

		#region Properties

		public TreeData Data { get; set; }



		public System.Nullable<bool> isSelected
		{
			get { return _isselected; }
			set
			{
				_isselected = value;
				OnPropertyChanged("IsSelected");
			}
		}

		public bool CanDelete
		{
			get { return _candelete; }
			set
			{
				_candelete = value;
				OnPropertyChanged("CanDelete");
			}
		}


		public TreeFolder Owner
		{
			get { return _owner; }
			set
			{
				_owner = value;
				OnPropertyChanged("Owner");
			}
		}


		public ObservableCollection<TreeFolder> SubFolders
		{
			get { return _subFolders ?? (_subFolders = new ObservableCollection<TreeFolder>()); }
			set
			{
				_subFolders = value;
				OnPropertyChanged("SubFolders");
			}
		}

		public object Hosted
		{
			get { return _hosted; }
			set
			{
				_hosted = value;
				OnPropertyChanged("Hosted");
			}
		}

		public int SubFoldersCount
		{
			get { return _subfolderscount; }
			set
			{
				_subfolderscount = value;
				OnPropertyChanged("SubFoldersCount");
			}
		}

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				OnPropertyChanged("Title");
			}
		}

		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				OnPropertyChanged("Text");
			}
		}

		public Guid Id
		{
			get { return _id; }
			set
			{
				_id = value;
				OnPropertyChanged("Id");
			}
		}

		public System.Nullable<Guid> Parent
		{
			get { return _parent; }
			set
			{
				_parent = value;
				OnPropertyChanged("Parent");
			}
		}

		public System.Nullable<bool> IsExpanded
		{
			get { return _isexpanded; }
			set
			{
				_isexpanded = value;
				OnPropertyChanged("IsExpanded");
			}
		}

		#endregion

		//internal bool Save(TreeDataLinkModelDataContext dc, string text)
		//{
		//    if (DataGenerator.SaveText(dc, Id, text))
		//    {
		//        Text = text;
		//        return true;
		//    }

		//    return false;
		//}

		//internal bool HasChild(TreeDataLinkModelDataContext dc)
		//{
		//    return DataGenerator.HasChild(dc, Id);
		//}
	}
}