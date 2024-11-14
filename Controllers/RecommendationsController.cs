using ECommerceApi.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RecommendationsController : ControllerBase
{
    private readonly RecommendationService _recommendationService;

    public RecommendationsController(RecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    [HttpGet("{userId}")]
public async Task<ActionResult<IEnumerable<Product>>> GetRecommendations(Guid userId)
{
    var recommendations = await _recommendationService.GetRecommendationsAsync(userId.ToString());
    
    if (recommendations == null || !recommendations.Any())
    {
        return NotFound(); // Retorna 404 se não houver recomendações
    }

    return Ok(recommendations);
}
}
