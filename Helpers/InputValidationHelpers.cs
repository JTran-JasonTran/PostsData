using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;

namespace AngularProject.Helpers
{
    public static class InputValidationHelpers
    {
        private static List<string> sortByValidValues = new List<string>() { "id", "reads", "likes", "popularity" };
        private static List<string> directionValidValues = new List<string>() { "asc", "desc" };
        public static string tagsRequired = "tags parameter is required";
        public static string sortByInvalid = "sortBy parameter is invalid";
        public static string directionInvalid = "direction parameter is invalid";

        public static List<string> processTagsValue(string tags)
        {
 
            if (String.IsNullOrEmpty(tags))
                return null;

            List<string> searchTags = tags.Split(",").ToList();
            searchTags = searchTags.Select(s => s.ToLower()).Distinct().ToList();
            return searchTags;
        }
        public static string processSortByValue(string sortBy)
        {
            if(sortBy == null)
            {
                return "id";
            }
            else if (sortByValidValues.Contains(sortBy.ToLower()))       
            {
                return sortBy.ToLower();
            }
            else
            {
                return null;
            }
        }

        public static string processDirectionValue(string direction)
        {
            if (direction == null) 
            {
                return "asc";
            }
            else if (directionValidValues.Contains(direction.ToLower()))
            {
                return direction.ToLower();
            }
            else
            {
                return null;
            }                   
        }
    }
}
