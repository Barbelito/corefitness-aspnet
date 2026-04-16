using Application.Faq;
using Microsoft.AspNetCore.Mvc;

public class FaqController : Controller
{
    private readonly IFaqService _faqService;

    public FaqController(IFaqService faqService)
    {
        _faqService = faqService;
    }

    public IActionResult Index()
    {
        var faqs = _faqService.GetFaqItems();
        return View(faqs);
    }
}