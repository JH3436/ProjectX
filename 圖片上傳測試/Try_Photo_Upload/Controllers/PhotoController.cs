using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Try_Photo_Upload.Models;

namespace Try_Photo_Upload.Controllers
{
	public class PhotoController : Controller
	{
		private readonly PhotoUploadDBContext _context;

		public PhotoController(PhotoUploadDBContext context)
		{
			_context = context;
		}

	public async Task<IActionResult> ShowPhotos(string photoName)
{
    if (string.IsNullOrEmpty(photoName))
    {
        return NotFound();
    }

    var photosWithSameName = await _context.Photos
        .Where(m => m.PhotoName == photoName)
        .ToListAsync();

    if (photosWithSameName == null || photosWithSameName.Count == 0)
    {
        return NotFound();
    }

    return View(photosWithSameName);
}



		// GET: Photo
		public async Task<IActionResult> Index()
		{
			return _context.Photos != null ?
						View(await _context.Photos.ToListAsync()) :
						Problem("Entity set 'PhotoUploadDBContext.Photos'  is null.");
		}

		// GET: Photo/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Photos == null)
			{
				return NotFound();
			}

			var photos = await _context.Photos
				.FirstOrDefaultAsync(m => m.PhotoId == id);
			if (photos == null)
			{
				return NotFound();
			}

			return View(photos);
		}

		// GET: Photo/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Photo/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PhotoId,PhotoName")] Photos photos, List<IFormFile> imageDataFiles)
		{
			if (imageDataFiles != null && imageDataFiles.Count > 0)
			{
				foreach (var imageDataFile in imageDataFiles)
				{
					if (imageDataFile != null && imageDataFile.Length > 0)
					{
						using (var stream = new MemoryStream())
						{
							await imageDataFile.CopyToAsync(stream);

							// 從表單中讀取照片名稱
							var photoName = Request.Form["PhotoName"];

							// 創建一個新的 Photos 對象，設置 PhotoName 和 ImageData，然後保存到資料庫
							var newPhoto = new Photos
							{
								PhotoName = photoName, // 你可以根據需要設置照片名稱
								ImageData = stream.ToArray()
							};

							// 將 photos 存入資料庫
							_context.Photos.Add(newPhoto);
							await _context.SaveChangesAsync();
						}
					}
				}

				return RedirectToAction(nameof(Index));
			}

			return View(photos);
		}



		// GET: Photo/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Photos == null)
			{
				return NotFound();
			}

			var photos = await _context.Photos.FindAsync(id);
			if (photos == null)
			{
				return NotFound();
			}
			return View(photos);
		}

		// POST: Photo/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("PhotoId,PhotoName,ImageData")] Photos photos)
		{
			if (id != photos.PhotoId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(photos);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PhotosExists(photos.PhotoId))
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
			return View(photos);
		}

		// GET: Photo/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Photos == null)
			{
				return NotFound();
			}

			var photos = await _context.Photos
				.FirstOrDefaultAsync(m => m.PhotoId == id);
			if (photos == null)
			{
				return NotFound();
			}

			return View(photos);
		}

		// POST: Photo/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Photos == null)
			{
				return Problem("Entity set 'PhotoUploadDBContext.Photos'  is null.");
			}
			var photos = await _context.Photos.FindAsync(id);
			if (photos != null)
			{
				_context.Photos.Remove(photos);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PhotosExists(int id)
		{
			return (_context.Photos?.Any(e => e.PhotoId == id)).GetValueOrDefault();
		}
	}
}
