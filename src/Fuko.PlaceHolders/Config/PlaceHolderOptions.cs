namespace Fuko.PlaceHolders.Config
{
    public class PlaceHolderOptions
    {
        /*
         * The beginning of a placeholder
         */
        public string Start { get; set; } = "{{";
        
        /*
         * The end of a placeholder
         */
        public string End { get; set; } = "}}";
        
        /*
         * Check for skipped placeholders and throw an Exception if one was missed
         */
        public bool Thorough { get; set; } = true;
    }
}