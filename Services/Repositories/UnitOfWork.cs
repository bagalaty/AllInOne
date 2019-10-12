using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        public UnitOfWork(IHttpContextAccessor context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
            allInOneRequestManager = new AllInOneRequestManager(_context, _env);
            bookmarkRepository = new BookmarkRepository(allInOneRequestManager);
        }
        readonly IHttpContextAccessor _context;
        readonly IHostingEnvironment _env;
        public IHttpContextAccessor HttpContext => _context;

        public IHostingEnvironment HostingEnvironment => _env;

        public AllInOneRequestManager AllInOneRequestManager => allInOneRequestManager ?? new AllInOneRequestManager(_context, _env);

        AllInOneRequestManager allInOneRequestManager;
        BookmarkRepository bookmarkRepository;

        public BookmarkRepository BookmarkManager => bookmarkRepository ?? new BookmarkRepository(allInOneRequestManager);


    }
}
