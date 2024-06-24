using Microsoft.AspNetCore.Components.Forms;

namespace _2Good2EatStore.Data.Interfaces
{
    public interface IFileUploadUtilityService
    {
        Task<string> MoveFileToRoot(IBrowserFile file);
    }
}
