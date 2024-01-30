using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.DTO.Comment;
using SocialMediaApp.Interfaces;
using SocialMediaApp.Mappers;

namespace SocialMediaApp.Controllers
{

    [Route("api/comment")]
    [ApiController]
    public class CommentController(ICommentRepository commentRepo, IStockRepository stockRepo) : ControllerBase
    {

        private readonly ICommentRepository _commentRepo = commentRepo;
        private readonly IStockRepository _stockRepo = stockRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(s=> s.ToCommentDto()).ToList();

            return Ok(commentsDto);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
             var comment =  await this._commentRepo.GetByIdAsync(Id);  
            
              if(comment == null)
            {
                return NotFound("Comment doesn't exist");
            }
              return Ok(comment.ToCommentDto());   
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto body)
        { 
                var stock = await this._stockRepo.StockExists(stockId);
            
                if(!stock)
                {
                    return BadRequest("Stock does not Exist");
                }

                var commentModel = body.ToCommentFromCreate(stockId);
                await _commentRepo.CreateAsync(commentModel);
                return CreatedAtAction(nameof(GetById), commentModel.ToCommentDto());

        }


    }
}
