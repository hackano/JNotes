
using System.Data.Objects.DataClasses;

namespace JNotes.Web
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Data;
	using System.Linq;
	using System.ServiceModel.DomainServices.EntityFramework;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;


	// Implements application logic using the TreeEntities2 context.
	// TODO: Add your application logic to these methods or in additional methods.
	// TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
	// Also consider adding roles to restrict access as appropriate.
	// [RequiresAuthentication]
	[EnableClientAccess()]
	public class jTreeServiceDomain : LinqToEntitiesDomainService<TreeEntities2>
	{

		// TODO:
		// Consider constraining the results of your query method.  If you need additional input you can
		// add parameters to this method or create additional query methods with different names.
		// To support paging you will need to add ordering to the 'TreeDatas' query.
		//public IQueryable<TreeData> GetTreeDatas()
		//{
		//    return this.ObjectContext.TreeDatas;
		//}


		[Association("TreeData_TreeData", "Id", "Parent")]
		public EntityCollection<TreeData> ChildNodes;
		public IQueryable<TreeData> GetTeamHierarchy()
		{
			return this.ObjectContext.TreeDatas.OrderBy(e => e.Title);

		}

		public void InsertTreeData(TreeData treeData)
		{
			if ((treeData.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(treeData, EntityState.Added);
			}
			else
			{
				this.ObjectContext.TreeDatas.AddObject(treeData);
			}
		}

		public void UpdateTreeData(TreeData currentTreeData)
		{
			this.ObjectContext.TreeDatas.AttachAsModified(currentTreeData, this.ChangeSet.GetOriginal(currentTreeData));
		}

		
		public void DeleteTreeData(TreeData treeData)
		{
			if ((treeData.EntityState != EntityState.Detached))
			{
				this.ObjectContext.ObjectStateManager.ChangeObjectState(treeData, EntityState.Deleted);
			}
			else
			{
				this.ObjectContext.TreeDatas.Attach(treeData);
				this.ObjectContext.TreeDatas.DeleteObject(treeData);
			}
		}
	}
}


