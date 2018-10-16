
namespace I.M.S.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies ItemDetailMetadata as the class
    // that carries additional metadata for the ItemDetail class.
    [MetadataTypeAttribute(typeof(ItemDetail.ItemDetailMetadata))]
    public partial class ItemDetail
    {

        // This class allows you to attach custom attributes to properties
        // of the ItemDetail class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ItemDetailMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ItemDetailMetadata()
            {
            }

            public int ID { get; set; }

            public string ItemBarcode { get; set; }

            public Nullable<int> ItemID { get; set; }

            public string ItemName { get; set; }

            public Nullable<int> Qty { get; set; }

            public Nullable<DateTime> ScanDate { get; set; }
        }
    }
}
