using Microsoft.AspNetCore.Components.Forms;

namespace _2Good2EatStore.Data.Interfaces
{
    public interface IFileService
    {
        Task<string> MoveFileToRoot(IBrowserFile file);
    }
}
