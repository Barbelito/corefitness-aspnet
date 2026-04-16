using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Application.Common.Models;
using Application.Faq;

public sealed class FaqService : IFaqService
{
    private readonly string _filePath;

    public FaqService(IHostEnvironment env)
    {
        _filePath = Path.Combine(env.ContentRootPath, "Data", "faqs.json");
    }

    public IReadOnlyList<FaqItem> GetFaqItems()
    {
        if (!File.Exists(_filePath))
            return [];

        var json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<FaqItem>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        ) ?? [];
    }
}