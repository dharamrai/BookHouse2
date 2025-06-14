using Domain.Models.ModelDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Website.Service.SerivceInterface;
using Website.WebModels;

namespace Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IApiService _apiService;
        public IndexModel(ILogger<IndexModel> logger, IApiService apiservice)
        {
            _logger = logger;
            _apiService = apiservice;
        }

        [BindProperty]
        public List<BooksDTO> modelVm { get; set; } = new List<BooksDTO>();
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
                    modelVm = result.Result;
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
    }
}
