using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface IEmailServices
    {
        string SendEmail(string mailTo, string subject, string body);
    }
}
