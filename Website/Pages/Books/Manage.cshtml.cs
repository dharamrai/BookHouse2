using Domain.Models.ModelDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Website.Service.SerivceInterface;
using Website.WebModels;

namespace Website.Pages.Books
{
    public class ManageModel : PageModel
    {
        private readonly IApiService _apiService;
        private readonly IWebHostEnvironment _webhost;

        [BindProperty]
        public BooksDTO booksDTO { get; set; }

        [BindProperty]
        public bool IsNew => booksDTO == null;
        public ManageModel(IApiService apiService, IWebHostEnvironment webhost)
        {
            _apiService = apiService;
            _webhost = webhost;
        }

        [BindProperty]
        public int Id { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Id = HttpContext.Session.GetInt32("Id") ?? 0;

            if (Id > 0)
            {
                try
                {
                    var result = await _apiService.SendRequestAsync<WebApiResponse<BooksDTO>>(
                        new ApiRequest
                        {
                            Method = HttpMethod.Get,
                            Url = "Books/Get/"+Id
                        });

                    if (result != null && result.IsSuccess)
                    {
                        booksDTO = result.Result;
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
            }
            else
            {
                booksDTO = new BooksDTO();
            }

            HttpContext.Session.Remove("Id");
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            var fileUpload = Request.Form.Files.FirstOrDefault();

            if (fileUpload != null && fileUpload.Length > 0)
            {
                booksDTO.BookImage = await UploadPicture(fileUpload, booksDTO.BookImage);
            }

            if (!ModelState.IsValid)
            {
                TempData["Notification"] = "Invalid model State";
                return RedirectToPage("Manage");
            }
            else
            {
                try
                {
                    if (booksDTO.Id > 0)
                    {
                        var result = await _apiService.SendRequestAsync<WebApiResponse<string>>(
                        new ApiRequest
                        {
                            Method = HttpMethod.Put,
                            Data = booksDTO,
                            Url = "Books/Update"
                        });

                        if (result.IsSuccess)
                        {
                            TempData["Notification"] = "Record updated successfully";
                            return RedirectToPage("Index");
                        }
                        else
                        {
                            TempData["Notification"] = string.Join(", ", result.Errors);
                        }
                    }
                    else
                    {
                        var result = await _apiService.SendRequestAsync<WebApiResponse<string>>(
                       new ApiRequest
                       {
                           Method = HttpMethod.Post,
                           Data = booksDTO,
                           Url = "Books/Create"
                       });

                        if (result.IsSuccess)
                        {
                            TempData["Notification"] = "Record created successfully";
                            return RedirectToPage("Index");
                        }
                        else
                        {
                            TempData["Notification"] = string.Join(", ", result.Errors);
                        }
                    }
                        
                }
                catch (Exception ex)
                {
                    TempData["Notification"] = $"API Error: {ex.Message}";
                }
            }
            return RedirectToPage("Index");
        }

        public async Task<string> UploadPicture(IFormFile uploadFile, string OldPhoto)
        {
            if (uploadFile == null || uploadFile.Length == 0)
            {
                return OldPhoto;
            }

            string uploadFolder = Path.Combine(_webhost.WebRootPath, "BookImage");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string guid = Guid.NewGuid().ToString("N");
            string uniqueFileName = guid.Substring(0, 6) + "_" + uploadFile.FileName;
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var filestream = new FileStream(filePath, FileMode.Create))
            {
                await uploadFile.CopyToAsync(filestream);
            }

            if (OldPhoto != null)
            {
                string oldFilePath = Path.Combine(uploadFolder, OldPhoto);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            return uniqueFileName;

        }
    }
}
