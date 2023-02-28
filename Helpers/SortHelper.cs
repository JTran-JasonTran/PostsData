using AngularProject.Dtos;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AngularProject.Helpers
{
    public static class SortHelper
    {
        public static List<PostDto> sortPost(List<PostDto> posts, string sortBy, string direction)
        {
            if(posts== null || posts.Count == 0) return null;
            sortBy = sortBy.Remove(1).ToUpper() + sortBy.Substring(1);
            var sortBypropertyInfo = typeof(PostDto).GetProperty(sortBy);
            if (direction == "asc")
                posts = posts.OrderBy(x => sortBypropertyInfo.GetValue(x, null)).ToList();
            else
                posts = posts.OrderByDescending(x => sortBypropertyInfo.GetValue(x, null)).ToList();
            return posts;
        }

    }
}
