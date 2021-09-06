using System;
using System.Collections.Generic;
using System.Text;

namespace Lara_VI.Entities.ViewModels
{
    public class InquiryViewModel
    {
        public InquiryHeader InquiryHeader { get; set; }
        public IEnumerable<InquiryDetail> InquiryDetail { get; set; }
    }
}
