using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels.DataTableViewModels
{
    public class CommentTableModel
    {
        public string CommentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public string PostId { get; set; }
    }
}