using Application.Common.Models;
namespace Application.Faq;

public interface IFaqService
{
    IReadOnlyList<FaqItem> GetFaqItems();
}