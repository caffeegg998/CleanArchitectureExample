using CleanArchitectureExample.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SparePartStockAPI.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoleController:ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleMgr;
            _userManager = userManager;
        }
        [HttpGet("getRoles")]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles;
                return Ok(roles);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {

                bool adminRoleExists = await _roleManager.RoleExistsAsync(name);
                if (!adminRoleExists)
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                    if (!result.Succeeded)
                        return BadRequest("Tạo Roles thất bại");
                }
                else
                {
                    return BadRequest("Role này đã được khởi tạo!");
                }
            }
            return Ok("Tạo Role thành công");
        }

        //Update Role
        [HttpGet("getUsersByRoleId")]
        public async Task<IActionResult> FindUsersByRoleId(string id)
        {
            //IdentityRole role = await _roleManager.FindByIdAsync(id);
            //List<IdentityUser> members = new List<IdentityUser>();
            //List<IdentityUser> nonMembers = new List<IdentityUser>();
            //foreach (IdentityUser identityUser in _userManager.Users)
            //{
            //    var list = await _userManager.IsInRoleAsync(identityUser, role.Name) ? members : nonMembers;
            //    list.Add(identityUser);
            //}
            //return Ok(new RoleEdit
            //{
            //    Role = role,
            //    Members = members,
            //    NonMembers = nonMembers
            //});
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound("Không tìm thấy vai trò");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            var allUsers = await _userManager.Users.ToListAsync();

            var members = usersInRole.ToList();
            var nonMembers = allUsers.Except(members).ToList();

            return Ok(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost("updateRoleToManyUsers")]
        public async Task<IActionResult> Update([FromForm] RoleModification model)
        {
            IdentityResult result;

            IdentityRole role = await _roleManager.FindByNameAsync(model.RoleName);

            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    IdentityUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            BadRequest(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    IdentityUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            BadRequest(result);
                    }
                }
            }

            return Ok(await FindUsersByRoleId(role.Id));

        }
    }
}
