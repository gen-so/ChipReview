using BlazorApp.Shared;

namespace BlazorApp.Client
{

    /// <summary>
    /// Place where global variables are stored
    /// </summary>
    public class GlobalVariableManager
    {
        public Review? ReviewInFocus { get; set; }
    }
}
