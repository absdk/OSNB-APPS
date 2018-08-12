using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels.DataTableViewModels
{
    public class PostTableModel
    {
        public string PostId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CreateDate { get; set; }

        public string UserName { get; set; }
    }
}