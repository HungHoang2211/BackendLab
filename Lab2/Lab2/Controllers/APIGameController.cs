using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Data;
using Lab2.Models;
using Microsoft.AspNetCore.Identity;
using Lab2.DTO;
using Microsoft.AspNetCore.Identity.Data;
using System.Linq.Expressions;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIGameController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        protected ReponseApi _reponse;

        private readonly UserManager<ApplicationUser> _userManager;
        public APIGameController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _reponse = new();
            _userManager = userManager;
        }

        [HttpPost("SaveResult")]
        public async Task<IActionResult> SaveResult(LevelResultDTO levelResult)
        {
            try
            {
                var levelResultSave = new LevelResult
                {
                    UserId = levelResult.UserId,
                    LevelId = levelResult.LevelId,
                    Score = levelResult.Score,
                    CompletionDate = DateOnly.FromDateTime(DateTime.Now)
                };
                await _db.LevelResults.AddAsync(levelResultSave);
                await _db.SaveChangesAsync();
                _reponse.IsSuccess = true;
                _reponse.Notification = "Lưu kết quả thành công";
                _reponse.Data = levelResultSave;
                return Ok(_reponse);
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Notification = "Lỗi";
                _reponse.Data = ex.Message;
                return BadRequest(_reponse);
            }
        }

        [HttpGet("GetAllQuestionGameByLevel/{levelId}")]
        public async Task<IActionResult> GetAllQuestionGameByLevel(int levelId)
        {
            try
            {
                var questionGame = await _db.Questions.Where(x => x.levelId == levelId).ToListAsync();
                _reponse.IsSuccess = true;
                _reponse.Notification = "Lấy dữ liệu thành công";
                _reponse.Data = questionGame;
                return Ok(_reponse);
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Notification = "Lỗi";
                _reponse.Data = ex.Message;
                return BadRequest(_reponse);
            }
        }

        

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = registerDTO.Email,
                    Email = registerDTO.Email,
                    Name = registerDTO.Name,
                    RegionId = registerDTO.RegionId,
                    Avatar = registerDTO.LinkAvatar
                };
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (result.Succeeded)
                {
                    _reponse.IsSuccess = true;
                    _reponse.Notification = "Đăng ký thành công";
                    _reponse.Data = user;
                    return Ok(_reponse);
                }
                else
                {
                    _reponse.IsSuccess = false;
                    _reponse.Notification = "Đăng ký thất bại";
                    _reponse.Data = result.Errors;
                    return BadRequest(_reponse);
                }
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Notification = "Lỗi";
                _reponse.Data = ex.Message;
                return BadRequest(_reponse);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var email = loginRequest.Email;
                var password = loginRequest.Password;

                var user = await _userManager.FindByEmailAsync(email);
                if (user != null && await _userManager.CheckPasswordAsync(user, password))
                {
                    _reponse.IsSuccess = true;
                    _reponse.Notification = "Đăng nhập thành công";
                    _reponse.Data = user;
                    return Ok(_reponse);
                }
                else
                {
                    _reponse.IsSuccess = false;
                    _reponse.Notification = "Đăng nhập thất bại";
                    _reponse.Data = "Email hoặc mật khẩu không đúng";
                    return BadRequest(_reponse);
                }
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Notification = "Lỗi" + ex.Message;
                _reponse.Data = ex.Message;
                return BadRequest(_reponse);
            }
        }

        [HttpGet("GetAllGameLevel")]
        public async Task<IActionResult> GetAllGameLevel()
        {
            try
            {
                var gameLevel = await _db.GameLevels.ToListAsync();
                _reponse.IsSuccess = true;
                _reponse.Notification = "Lấy dữ liệu thành công";
                _reponse.Data = gameLevel;
                return Ok(_reponse);
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Notification = "Lỗi" + ex.Message;
                _reponse.Data = ex.Message;
                return BadRequest(_reponse);
            }
        }
        [HttpGet("GetAllQuestionGame")]
        public async Task<IActionResult> GetAllQuestionGame()
        {
            try
            {
                var questions = await _db.Questions.ToListAsync();
                _reponse.IsSuccess = true;
                _reponse.Notification = "Lấy dữ liệu thành công";
                _reponse.Data = questions;
                return Ok(_reponse);
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Notification = "Lỗi" + ex.Message;
                _reponse.Data = ex.Message;
                return BadRequest(_reponse);
            }
        }
        [HttpGet("GetAllRegionGame")]
        public async Task<IActionResult> GetAllRegionGame()
        {
            try
            {
                var regions = await _db.Regions.ToListAsync();
                _reponse.IsSuccess = true;
                _reponse.Notification = "Lấy dữ liệu thành công";
                _reponse.Data = regions;
                return Ok(_reponse);
            }
            catch (Exception ex)
            {
                _reponse.IsSuccess = false;
                _reponse.Notification = "Lỗi" + ex.Message;
                _reponse.Data = ex.Message;
                return BadRequest(_reponse);
            }
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            Lab2.Models.Lab2 lab1 = new Lab2.Models.Lab2()
            {
                CourseName = "Web Programming",
                CourseCode = "WEBD6201",
                Name = "John Doe",
                StudentCode = "123456789",
                Class = "WEBD6201-01"
            };
            int status = 1;
            string message = "Data retrieved successfully";
            var data = new {status, message, lab1};
            return new JsonResult(data);
        }
    }
}
