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
              
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepo.GetAllAsync();
            var commentsDto = comments.Select(s=> s.ToCommentDto()).ToList();

            return Ok(commentsDto);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment =  await this._commentRepo.GetByIdAsync(Id);  
            
              if(comment == null)
            {
                return NotFound("Comment doesn't exist");
            }
              return Ok(comment.ToCommentDto());   
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto bodyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await this._stockRepo.StockExists(stockId);
            
                if(!stock)
                {
                    return BadRequest("Stock does not Exist");
                }

                var commentModel = bodyDto.ToCommentFromCreate(stockId);
                await _commentRepo.CreateAsync(commentModel);
                return CreatedAtAction(nameof(GetById), new { id = commentModel.Id}, commentModel.ToCommentDto());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto bodyDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
           
            var comment = await this._commentRepo.UpdateAsync(id, bodyDto.ToCommentFromUpdate());

            if(comment == null) return NotFound();  
            
            return Ok(comment.ToCommentDto());
        }


        [HttpDelete("{Id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await this._commentRepo.DeleteAsync(Id);
            if( comment == null)
            {
                return NotFound("Comment doesn't exist");
            }
            return Ok(comment.ToCommentDto());      
        }

    
    }
}
