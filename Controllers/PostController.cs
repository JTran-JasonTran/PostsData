using AngularProject.Helpers;
using AngularProject.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using AngularProject.Models;
using Microsoft.Extensions.Configuration;

namespace AngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PostController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpGet("search")]
        public IActionResult Get(string tags, string sortBy, string direction)
        { 
            // Validation
            List<string> searchTags = InputValidationHelpers.processTagsValue(tags);
            if (searchTags == null) return BadRequest(InputValidationHelpers.tagsRequired);

            sortBy = InputValidationHelpers.processSortByValue(sortBy);
            if (sortBy == null) return BadRequest(InputValidationHelpers.sortByInvalid);
            
            direction = InputValidationHelpers.processDirectionValue(direction);
            if(direction ==null) return BadRequest(InputValidationHelpers.directionInvalid);
         
            // Call Api
            List<Post> posts = new List<Post>();
            string apiPath = _configuration.GetValue<string>("PostApi");
            for (int i = 0; i < searchTags.Count; i++)
            {
                var response = APIHelper.GetDataFromAPI(apiPath, searchTags[i]);
                if(response != null)
                {
                    posts.AddRange(response.ToList());
                }
            }

            List<PostDto> postDtosResult = new List<PostDto>();
            if (posts.Count > 0)
            {
                // Sort as request
                posts = SortHelper.sortPost(posts, sortBy, direction);

                // Convert Post to PostDto before turned
                foreach (var post in posts)
                {
                    var postDto = new PostDto()
                    {
                        Id = post.Id,
                        Author = post.Author,
                        Likes = post.Likes,
                        Popularity = post.Popularity,
                        Reads = post.Reads,
                        Tags = post.Tags,
                    };
                    postDtosResult.Add(postDto);
                }
            }

            return Ok(postDtosResult);
        }
    }
}
