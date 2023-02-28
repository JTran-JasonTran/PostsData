using AngularProject.Helpers;
using AngularProject.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace AngularProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public PostController() { }

        [HttpGet("searchData")]
        public IActionResult Get(string tags, string sortBy, string direction)
        { 
            List<string> searchTags = InputValidationHelpers.processTagsValue(tags);
            if (searchTags == null) return BadRequest(InputValidationHelpers.tagsRequired);

            sortBy = InputValidationHelpers.processSortByValue(sortBy);
            if (sortBy == null) return BadRequest(InputValidationHelpers.sortByInvalid);
            
            direction = InputValidationHelpers.processDirectionValue(direction);
            if(direction ==null) return BadRequest(InputValidationHelpers.directionInvalid);
         
            List<PostDto> posts = new List<PostDto>();
            for (int i = 0; i < searchTags.Count; i++)
            {
                var response = APIHelper.GetDataFromAPI(searchTags[i]);
                if(response != null)
                {
                    posts.AddRange(response.ToList());
                }
            }

            posts = SortHelper.sortPost(posts, sortBy, direction);

            return Ok(posts);
        }
    }
}
