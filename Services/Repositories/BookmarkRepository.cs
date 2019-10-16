using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Repositories
{
    public class BookmarkRepository : Repository<Bookmark>, IRepository<Bookmark>
    {
        public BookmarkRepository(AllInOneRequestManager allInOneRequestsManager) : base(allInOneRequestsManager)
        {

        }
        public BaseModel Create(Bookmark model)
        {
            var request = AllInOneRequestManager.CreateAllInOneRequest("CreateBookmark", true);
            request.Body = model;
            var response = request.GetResponse();
            return new BaseModel
            {
                //Message = response.message,
                //MessageType = response.HasError ? Models.Enums.MessageType.error : Models.Enums.MessageType.success
            };
        }

        public bool Delete(string id)
        {
            return Delete("DeleteBook", id);
        }

        public List<Bookmark> GetAll()
        {
            var list = GetAll<Bookmark>("GetAllBookmark");
            list.Sort();
            return list;
        }

        public Bookmark GetById(string id)
        {
            return GetById("GetBookmarkById", id);
        }

        public BaseModel Update(Bookmark model)
        {
            return Update("UpdateBookmark", model, new List<string> { model.Id.ToString() });
        }
    }
}
