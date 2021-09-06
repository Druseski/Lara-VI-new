using Lara_VI.Data;
using Lara_VI.Entities;
using Lara_VI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lara_VI.Repositories
{
    public class InquiryHeaderRepository : GenericRepository<InquiryHeader>, IInquiryHeaderRepository
    {
        private readonly DataContext _dataContext;

        public InquiryHeaderRepository(DataContext dataContext):base(dataContext)
        {
            _dataContext = dataContext;
        }

        public void Update(InquiryHeader inquiryHeader)
        {
            _dataContext.InquiryHeader.Update(inquiryHeader);
        }
    }
}
