using System;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using JNotes.Models;
using JNotes.ViewModels;
using JNotes.Web;

namespace JNotes.Views
{
	public partial class TreeViewPage : Page
	{
		
	
		public TreeViewPage()
		{
			InitializeComponent();
			Loaded += TreeViewPage_Loaded;
		}


		private void TreeViewPage_Loaded(object sender, RoutedEventArgs e)
		{
			
		}

	

		// Executes when the user navigates to this page.
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		private void btnSearch_Click(object sender, RoutedEventArgs e)
		{
			
		}

	

		private void btnAddNode_Click(object sender, RoutedEventArgs e)
		{
		//	_treevm.AddFolder(null);
	
		}

	

		private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			//if (tv.SelectedItem == null)
			//{
			//    btnRemoveNode.IsEnabled = false;
			//    btnAddChilds.IsEnabled = false;
			//    return;
			//}

			//var item = (TreeFolder)tv.SelectedItem;
			//btnRemoveNode.IsEnabled = item.SubFolders.Count <= 0;
			//btnAddChilds.IsEnabled = true;
		}

		private void btnAddChilds_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			//if (tv.SelectedItem == null)
			//    return;

			//_treevm.AddFolder((TreeFolder)tv.SelectedItem);

		}
	}
}