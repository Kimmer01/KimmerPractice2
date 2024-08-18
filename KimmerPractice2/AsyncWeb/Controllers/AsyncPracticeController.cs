using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AsyncWeb.Controllers;

[ApiController]
[Route("/api")]
public class AsyncPracticeController : Controller
{
    [HttpGet]
    [Route("/")]
    [Route("/api/[controller]")]
    public async Task<ActionResult> Index(CancellationToken cancellationToken)
    {
        await DownloadAsync(@"https://www/baidu.com", 100, cancellationToken);
        return View();
    }

    static async Task DownloadAsync(string url, int n, CancellationToken cancellationToken)
    {
        using (HttpClient client = new HttpClient())
        {
            for (int i = 0; i < n; i++)
            {
                string response = await client.GetStringAsync(url, cancellationToken);
                Debug.WriteLine($"Times: {i}. {response}");

                if (cancellationToken.IsCancellationRequested)
                {
                    Debug.WriteLine($"Task was cancelled. Times: {i}.");
                }
            }
        }

    }
}
