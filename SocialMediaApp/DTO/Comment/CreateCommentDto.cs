﻿using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.DTO.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "Title must be 5 characters")]
        [MaxLength(100,ErrorMessage = "Title cannot be over 100 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(200, ErrorMessage = "Content cannot be over 200 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
