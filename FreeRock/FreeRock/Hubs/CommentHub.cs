using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeRock.Data;
using FreeRock.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FreeRock.Hubs
{
    public class CommentHub<T> : Hub where T: class, ICommentable<T>
    {
        private readonly ApplicationDbContext _context;

        public CommentHub(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task SendMessage(string user, string message, int id)
        {
            DbSet<T> collection = (DbSet<T>) _context.GetType().GetProperties()
                                 .FirstOrDefault(x => x.PropertyType == typeof(DbSet<T>))
                                 .GetValue(_context);
            T commentableObj = collection.FirstOrDefault(x => x.ID == id);

            AddCommentary(commentableObj, message, user);
            
            collection.Update(commentableObj);
            await _context.SaveChangesAsync();

            DateTime date = DateTime.Now;
            await Clients.All.SendAsync("ReceiveMessage", user, message, date.ToString());
        }


        public void AddCommentary(T commentableObj, string NewCommentary, string UserName)
        {
            User user = _context.Users.FirstOrDefault(x => x.UserName == UserName);

            Comment<T> commentary = new Comment<T>
            {
                Text = NewCommentary,
                Date = DateTime.Now,
                Author = user,
                CommentableObj = commentableObj
            };
            
            commentableObj.Comments.Add(commentary);
        }
    }
}
