﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tails_Of_Joy.Models;
using Tails_Of_Joy.Repositories;

namespace Tails_Of_Joy.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet("{firebaseUserId}")]
        public IActionResult GetByFirebaseUserId(string firebaseUserId)
        {
            var userProfile = _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }

        [HttpPost]
        public IActionResult Register(UserProfile userProfile)
        {
            // All newly registered users start out as a "user" user type (i.e. they are not admins)
            userProfile.UserTypeId = UserType.USER_ID;
            _userProfileRepository.Add(userProfile);
            return CreatedAtAction(
                nameof(GetByFirebaseUserId), new { firebaseUserId = userProfile.FirebaseUserId }, userProfile);
        }

        [HttpGet("q={id}")]
        public IActionResult Get(int id)
        {
            var userProfile = _userProfileRepository.GetUserProfileById(id);
            //if (userProfile != null)
            //{
            //    NotFound();
            //}
            return Ok(userProfile);
        }

        [HttpPut("edit/{id}")]
        public IActionResult Put(int id, UserProfile userProfile)
        {
            //var currentUserProfile = GetCurrentUserProfile();
            //if (currentUserProfile == null)
            //{
            //    return Unauthorized();
            //}
            _userProfileRepository.UpdateUserProfile(userProfile);
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
            _userProfileRepository.DeleteUserProfile(id);
            return NoContent();
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}