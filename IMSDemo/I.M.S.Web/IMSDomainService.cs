
namespace I.M.S.Web
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


    // Implements application logic using the IMSEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public class IMSDomainService : LinqToEntitiesDomainService<IMSEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'ItemDetails' query.
        public IQueryable<ItemDetail> GetItemDetails()
        {
            return this.ObjectContext.ItemDetails;
        }

        public IQueryable<Employee> GetEmployees()
        {
            return this.ObjectContext.Employees;
        }
        public IQueryable<Location> GetLocations()
        {
            return this.ObjectContext.Locations;
        }

        [RequiresAuthentication]
        public void UpdateItemDetail(ItemDetail current)
        {
            this.ObjectContext.ItemDetails.AttachAsModified(current, this.ChangeSet.GetOriginal(current));
        }

        public IQueryable<GraphItem> GetGraphItems()
        {
            var itemDetails = (from id in this.ObjectContext.ItemDetails
                               group id by new { id.ItemID, id.ItemName } into itemGroup
                               select new GraphItem(){ ItemID = itemGroup.Key.ItemID, ItemName = itemGroup.Key.ItemName, Qty = itemGroup.Count() });
            return itemDetails;
        }
    }

    

    
}


