using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories;
using Lara_VI.Repositories.Interfaces;
using Lara_VI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Lara_VI.Services
{
    public class InquiryDetailsService : GenericService<InquiryDetail>, IInquiryDetailService
    {
        private readonly IInquiryDetailRepository _inquiryDetailRepository;
        
        public InquiryDetailsService(IInquiryDetailRepository inquiryDetailRepository
           ) : base(inquiryDetailRepository)
        {
            _inquiryDetailRepository = inquiryDetailRepository;
            
        }


        public void Update(InquiryDetail inquiryDetail)
        {
            _inquiryDetailRepository.Update(inquiryDetail);

        }
    }
}
