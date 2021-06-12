using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindForkCodingTest.Data;
using MindForkCodingTest.EntityModel;
using MindForkCodingTest.Model;
using Newtonsoft.Json;

namespace MindForkCodingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext context;

        public BlogController(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll([FromForm]DataTable dt)
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var blogList = new List<BlogVM>();

            #region data format
            var blogResult = await context.Blog.Include(item => item.Comments).ToListAsync();
            foreach (var item in blogResult)
            {
                var blog = new BlogVM();
                blog.Name = item.Name;
                blog.PostedBy = item.PostedBy;
                blog.Date = item.Date;
                int numberOfComment = 0;
                foreach (var comment in item.Comments)
                {
                    var commentVM = new CommentVM()
                    {
                        Id = comment.Id,
                        BlogId = comment.BlogId,
                        CommenterName = comment.CommenterName,
                        Date = comment.Date,
                        LikeDislike = "Like ("+comment.Like.ToString() + ") Dislike ("+comment.Dislike.ToString()+")",
                    };
                    ++numberOfComment;
                    blog.Comments.Add(commentVM);
                }
                blog.NumberOfComment = numberOfComment.ToString() + " Comments";
                blogList.Add(blog);
            }
            #endregion
            

            #region Filtering table data
            // searching 
            if (searchValue != null)
            {
                try
                {
                    var filterBrandList = blogList.Where(
                        x => x.Name.ToLower().Contains(searchValue) ||
                        x.PostedBy.ToLower().Contains(searchValue) ||
                        x.Date.ToLower().Contains(searchValue)).ToList();
                    blogList = filterBrandList;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            #endregion

            var lists = blogList.OrderByDescending(x => x.Id).ToList();

            //total number of rows count     
            recordsTotal = lists.Count();

            //Paging     
            var data = lists.Skip(skip).Take(pageSize).ToList();

            //Returning Json Data    
            return new JsonResult(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
        }
    }
}
