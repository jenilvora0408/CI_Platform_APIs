using Common.Utils.Models;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    public interface IBaseController<T, D> where T : class
    {
        Task<IActionResult> Create(T dto);

        Task<IActionResult> Delete(D id);

        Task<IActionResult> Update(D id, T dto);

        Task<IActionResult> GetById(D id);
    }
}
