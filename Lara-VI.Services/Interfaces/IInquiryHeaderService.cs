
using Lara_VI.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lara_VI.Services.Interfaces
{
    public interface IInquiryHeaderService : IGenereicService<InquiryHeader>
    {
        void Update(InquiryHeader inquiryHeader);
    }
}
