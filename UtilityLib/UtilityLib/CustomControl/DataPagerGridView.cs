using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Collections;

namespace UtilityLib.CustomControl
{
    public class DataPagerGridView : GridView, IPageableItemContainer
    {

        public int MaximumRows
        {
            get { return this.PageSize; }
        }

        public void SetPageProperties(int startRowIndex, int maximumRows, bool databind)
        {
            int newPageIndex = (startRowIndex / maximumRows);

            this.PageSize = maximumRows;

            if (this.PageIndex != newPageIndex)
            {

                bool isCanceled = false;

                if (databind)
                {

                    //  create the event arguments and raise the event

                    GridViewPageEventArgs args = new GridViewPageEventArgs(newPageIndex);

                    this.OnPageIndexChanging(args);

                    isCanceled = args.Cancel;

                    newPageIndex = args.NewPageIndex;

                }

                //  if the event wasn't cancelled change the paging values

                if (!isCanceled)
                {

                    this.PageIndex = newPageIndex;

                    if (databind)

                        this.OnPageIndexChanged(EventArgs.Empty);

                }

                if (databind)

                    this.RequiresDataBinding = true;

            }

        }

        public int StartRowIndex
        {
            get { return this.PageSize * this.PageIndex; }
        }

        public event EventHandler<PageEventArgs> TotalRowCountAvailable;


        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {

            int rows = base.CreateChildControls(dataSource, dataBinding);

            //  if the paging feature is enabled, determine the total number of rows in the datasource

            if (this.AllowPaging)
            {

                //  if we are databinding, use the number of rows that were created, otherwise cast the datasource to an Collection and use that as the count

                int totalRowCount = dataBinding ? rows : ((ICollection)dataSource).Count;

                //  raise the row count available event

                IPageableItemContainer pageableItemContainer = this as IPageableItemContainer;

                this.OnTotalRowCountAvailable(new PageEventArgs(pageableItemContainer.StartRowIndex, pageableItemContainer.MaximumRows, totalRowCount));
                //  make sure the top and bottom pager rows are not visible

                if (this.TopPagerRow != null)

                    this.TopPagerRow.Visible = false;

                if (this.BottomPagerRow != null)

                    this.BottomPagerRow.Visible = false;

            }

            return rows;
        }

        protected virtual void OnTotalRowCountAvailable(PageEventArgs e)
        {
            EventHandler<PageEventArgs> handler = this.TotalRowCountAvailable;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}
