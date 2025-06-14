using Domain.Models;
using Domain.Models.ModelDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Website.Service.SerivceInterface;
using Website.WebModels;

namespace Website.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IApiService _apiService;
        public List<BooksDTO> Books { get; set; } = new();
        public IndexModel(IApiService apiservice)
        {
            _apiService = apiservice;
        }
        [BindProperty]
        public int bookId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var result = await _apiService.SendRequestAsync<WebApiResponse<List<BooksDTO>>>(
                    new ApiRequest
                    {
                        Method = HttpMethod.Get,
                        Url = "Books/Get"
                    });

                if (result != null && result.IsSuccess)
                {
                    Books = result.Result;
                }
                else
                {
                    TempData["Error"] = result?.Errors != null
                        ? string.Join(", ", result.Errors)
                        : "Unknown error from API.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"API Error: {ex.Message}";
            }

            return Page();
        }


        public IActionResult OnPostEdit()
        {
            HttpContext.Session.SetInt32("Id", bookId);

            return RedirectToPage("Manage");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            try
            {
                var result = await _apiService.SendRequestAsync<WebApiResponse<string>>(
                    new ApiRequest
                    {
                        Method = HttpMethod.Delete,
                        Url = "Books/Delete/" + bookId
                    });

                if (result != null && result.IsSuccess)
                {
                    TempData["Notification"] = "Record Deleted Successfully";
                    return RedirectToPage("Index");
                }
                else
                {
                   
                    TempData["Error"] = result?.Errors != null
                        ? string.Join(", ", result.Errors)
                        : "Unknown error from API.";
                    return RedirectToPage("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"API Error: {ex.Message}";
                return RedirectToPage("Index");
            }
        }

    }
}
