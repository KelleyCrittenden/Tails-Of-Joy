﻿using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Tails_Of_Joy.Models;
using Tails_Of_Joy.Repositories;

namespace Tails_Of_Joy.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IPostRepository _postRepository;
       
        public PostController(IPostRepository postRepository,
            IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _postRepository = postRepository;
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postRepository.GetAllPosts());
        }

        // Displaying details of one Post
        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _postRepository.GetPostById(id);
            if (post != null)
            {
                NotFound();
            }
            return Ok(post);
        }

        [HttpGet("User/{id}")]
        public IActionResult GetbyUser(int id)
        {
            return Ok(_postRepository.GetUserPostsById(id));
        }


        [HttpPost]
        public IActionResult Add(Post post)
        {
            var currentUserProfile = GetCurrentUserProfile();
            if (currentUserProfile == null)
            {
                return Unauthorized();
            }
            _postRepository.Add(post);
            return CreatedAtAction("Get", new { id = post.Id }, post);
        }


        [HttpPut("edit/{id}")]
        public IActionResult Put(int id, Post post)
        {
            var currentUserProfile = GetCurrentUserProfile();
            if (currentUserProfile == null)
            {
                return Unauthorized();
            }
            if (id != post.Id)
            {
                return BadRequest();

            }
            _postRepository.UpdatePost(post);
            return NoContent();
        }



        [HttpPut("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var currentUserProfile = GetCurrentUserProfile();
            if (currentUserProfile == null)
            {
                return Unauthorized();
            }
            _postRepository.DeletePost(id);
            return NoContent();
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }

    }
}