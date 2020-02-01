using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fuko.PlaceHolders.Config;

namespace Fuko.PlaceHolders
{
    public class PlaceHolders : IPlaceholders
    {
        private readonly PlaceHolderOptions _options;

        public PlaceHolders(PlaceHolderOptions options)
        {
            _options = options;
        }

        public async Task<string> ParseAsync(string template, IDictionary<string, string> placeholders)
        {
            foreach(KeyValuePair<string, string> entry in placeholders)
            {
                // Replace the placeholders
                template = template.Replace($"{_options.Start}{entry.Key}{_options.End}", entry.Value);
            }
            
            /*
             * Catch the placeholders that have been skipped
             */
            CatchSkippedPlaceholders(template);
            
            /*
             * Return the parsed template
             */
            return await Task.FromResult(template);
        }

        protected void CatchSkippedPlaceholders(string template)
        {
            if (_options.Thorough)
            {
                Regex regex = new Regex(@"" + Regex.Escape(_options.Start) + "(.*)" + Regex.Escape(_options.End),
                    RegexOptions.Singleline);
                Match match = regex.Match(template);
                
                if(match.Success) throw new Exception($"Could not find a replacement for {match.Value}");
            }
        }
    }
}
