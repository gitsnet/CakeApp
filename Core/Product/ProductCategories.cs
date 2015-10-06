using Core.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Product
{
    public partial class ProductCategories : BaseEntity
    {
        public long ProductCategoryID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual Products Products { get; set; }
    }
}
