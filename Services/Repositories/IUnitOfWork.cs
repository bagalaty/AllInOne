using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Repositories
{
    public interface IUnitOfWork
    {
        IHttpContextAccessor HttpContext { get; }

        IHostingEnvironment HostingEnvironment { get; }

        AllInOneRequestManager AllInOneRequestManager { get; }

        //QuestionBankRepository QuestionBankManager { get; }

        BookmarkRepository BookmarkManager { get; }

     
    }
}
