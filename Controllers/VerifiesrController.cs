using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using DemoVerify.Database;
using DemoVerify.DTOs;
using DemoVerify.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoVerify.Controllers
{
    [Route("api/verifiers")]
    [ApiController]
    public class VerifiesrController : ControllerBase
    {
        private readonly VerifyDataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VerifiesrController(VerifyDataContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult<List<Verifier>> Get()
        {
            var result = _context.Verifiers.ToList();
            return StatusCode(200, result);
        }

        [HttpGet]
        public ActionResult<Verifier> Get(int id)
        {
            var result = _context.Verifiers.Find(id);
            if(result == null)
                return StatusCode(400, new{message = "Not Foud your Verify"});    
            return StatusCode(200, result);
        }

        [HttpPost]
        public ActionResult Create([FromForm] VerifierRequest verifierRequest)
        {
            try
            {
                string uniqueFileName = UploadedFile(verifierRequest);

                Verifier verifier = new Verifier
                {
                    Status = "Unverified",
                    LinkImage = uniqueFileName
                };
                _context.Add(verifier);
                _context.SaveChanges();
                return StatusCode(201, new { message = "UpLoad Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut]
        public ActionResult Update(int id)
        {
            try
            {
                var item = _context.Verifiers.Find(id);
                if(item == null)
                    return StatusCode(400, new{message = "Not Foud your Verify"});    
                item.Status = "Verified";
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return StatusCode(200, new { message = "Verify Success" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        private string UploadedFile(VerifierRequest model)
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resource", "images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            string dbPath = null;
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return dbPath;
        }
    }
}