﻿using bloggie.web.Models.Domain;
using Bloggie.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bloggie.Web.Repositories
{
     

    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;
        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
          var existingBlogPost =  await bloggieDbContext.BlogPosts.FindAsync(id);
            if(existingBlogPost != null)
            {
                bloggieDbContext.BlogPosts.Remove(existingBlogPost);
                await bloggieDbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;

        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync();

        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
          return await bloggieDbContext.BlogPosts.Include(x=> x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlAsync(string urlHandle)
        {
            return await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.Include(x=>x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if(existingBlog!= null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();
                return existingBlog;

            }
            return null; 
        }
    }
}
