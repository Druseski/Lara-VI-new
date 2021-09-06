using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories.Interfaces;
using Lara_VI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lara_VI.Services
{
    public class InquiryHeaderService : GenericService<InquiryHeader>, IInquiryHeaderService
    {
        private readonly IInquiryHeaderRepository _inquiryHeaderRepository;
        
        public InquiryHeaderService(IInquiryHeaderRepository inquiryHeaderRepository
          ) : base(inquiryHeaderRepository)
        {
            _inquiryHeaderRepository = inquiryHeaderRepository;
            
        }


        public void Update(InquiryHeader inquiryHeader)
        {
            _inquiryHeaderRepository.Update(inquiryHeader);

        }
    }
}
