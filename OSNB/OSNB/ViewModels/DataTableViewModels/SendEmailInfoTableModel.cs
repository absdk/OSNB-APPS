using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSNB.ViewModels.DataTableViewModels
{
    public class SendEmailInfoTableModel
    {
        public string SendEmailInfoId { get; set; }
        public string SenderName { get; set; }
        public string SenderContactNo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string MemberViewModelId { get; set; }
        public string MemberName { get; set; }
    }
}