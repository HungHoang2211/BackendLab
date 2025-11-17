using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Data;
using Lab2.Models;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIGameController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        protected ReponseApi _reponse;

        public APIGameController(ApplicationDbContext db)
        {
            _db = db;
            _reponse = new();
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
