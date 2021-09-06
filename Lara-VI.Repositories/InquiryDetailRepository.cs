using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lara_VI.Repositories
{
    public class InquiryDetailRepository : GenericRepository<InquiryDetail>, IInquiryDetailRepository
    {
        private readonly DataContext _dataContext;

        public InquiryDetailRepository(DataContext dataContext):base(dataContext)
        {
            _dataContext = dataContext;
        }

        public void Update(InquiryDetail inquiryDetail)
        {
            _dataContext.InquiryDetail.Update(inquiryDetail);
        }
    }
}
