﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeNotice.Data;
using PracticeNotice.Models;

namespace PracticeNotice.Controllers
{
    public class NoticesController : Controller
    {
        private readonly PracticeNoticeContext _context;
        private IWebHostEnvironment Environment;
        public NoticesController(PracticeNoticeContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            Environment  = _environment;
        }

        // GET: Notices
        public async Task<IActionResult> Index(string SearchType, string SearchString, int? page)
        {
            ViewBag.SearchType = SearchType;
            ViewBag.SearchString = SearchString;

            var notices = from n in _context.Notice
                          select n;

            if (!string.IsNullOrEmpty(SearchString))
            {
                switch (SearchType)
                {
                    case "Name":
                        notices = notices.Where(s => s.Name!.Contains(SearchString));
                        break;
                    case "Subject":
                        notices = notices.Where(s => s.Subject!.Contains(SearchString));
                        break;
                    default:
                        notices = notices.Where(s => s.Name!.Contains(SearchString) || s.Subject.Contains(SearchString));
                        break;
                }
            }
            else {
                page = 1;
            }
            //order by 
            notices = notices.OrderByDescending(s => s.RegDate);

            //int pageSize = 3;
            int pageSize = notices.Count();
            int pageNumber = (page ?? 1);

            return View(await notices.ToListAsync());
        }

        // GET: Notices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notice = await _context.Notice
                .FirstOrDefaultAsync(m => m.Idx == id);
            if (notice == null)
            {
                return NotFound();
            }

            return View(notice);
        }

        // GET: Notices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice notice)
        {
            string newFileName = string.Empty;
            if (notice.File != null)
            {
                if (notice.File.Length > 0)
                {
                    string UploadPath = Path.Combine(this.Environment.WebRootPath, "file");
                    if (!Directory.Exists(UploadPath))
                        Directory.CreateDirectory(UploadPath);

                    AppDomain.CurrentDomain.SetData("UploadPath", UploadPath);

                    string fileExt = Path.GetExtension(notice.File.FileName);
                    long fileSize = notice.File.Length;
                    newFileName = System.Guid.NewGuid().ToString() + fileExt;
                    UploadPath = Path.Combine(AppDomain.CurrentDomain.GetData("UploadPath").ToString(), newFileName);
                    using (var stream = new FileStream(UploadPath, FileMode.Create))
                    {
                        await notice.File.CopyToAsync(stream);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                notice.FileName = newFileName;
                notice.RegDate = DateTime.Now;

                _context.Add(notice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notice);
        }

        // GET: Notices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notice = await _context.Notice.FindAsync(id);
            if (notice == null)
            {
                return NotFound();
            }
            return View(notice);
        }

        // POST: Notices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Notice notice)
        {
            if (id != notice.Idx)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string newFileName = string.Empty;

                    if (notice.File != null)
                    {
                        if (notice.File.Length > 0)
                        {
                            string beforeFileName = _context
                                                    .Notice
                                                    .Where(n => n.Idx == id)
                                                    .Select(n => n.FileName)
                                                    .SingleOrDefault();


                            //이전 파일 삭제
                            if (beforeFileName != "")
                            { 
                                string beforePath = Path.Combine(this.Environment.WebRootPath, "file", beforeFileName);
                                System.IO.File.Delete(beforePath);
                            }
                            string UploadPath = Path.Combine(this.Environment.WebRootPath, "file");
                            if (!Directory.Exists(UploadPath))
                                Directory.CreateDirectory(UploadPath);

                            AppDomain.CurrentDomain.SetData("UploadPath", UploadPath);

                            string fileExt = Path.GetExtension(notice.File.FileName);
                            long fileSize = notice.File.Length;
                            newFileName = System.Guid.NewGuid().ToString() + fileExt;
                            UploadPath = Path.Combine(AppDomain.CurrentDomain.GetData("UploadPath").ToString(), newFileName);
                            using (var stream = new FileStream(UploadPath, FileMode.Create))
                            {
                                await notice.File.CopyToAsync(stream);
                            }
                        }
                    }

                    notice.FileName = newFileName;
                    notice.RegDate = DateTime.Now;
                    _context.Update(notice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticeExists(notice.Idx))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(notice);
        }

        // POST: Notices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string beforeFileName = _context
                                    .Notice
                                    .Where(n => n.Idx == id)
                                    .Select(n => n.FileName)
                                    .SingleOrDefault();

            //이전 파일 삭제
            if (beforeFileName != "")
            {
                string beforePath = Path.Combine(this.Environment.WebRootPath, "file", beforeFileName);
                System.IO.File.Delete(beforePath);
            }

            var notice = await _context.Notice.FindAsync(id);
            _context.Notice.Remove(notice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticeExists(int id)
        {
            return _context.Notice.Any(e => e.Idx == id);
        }
    }
}
