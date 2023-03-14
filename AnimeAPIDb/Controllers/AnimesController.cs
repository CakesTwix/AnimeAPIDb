using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeAPIDb.Models;
using AnimeAPIDb.Services;

namespace AnimeAPIDb.Controllers
{
    public class AnimesController : Controller
    {
        private readonly AnimeService _animeService;

        public AnimesController(AnimeService animeService)
        {
            _animeService = animeService;
        }

        // GET: Animes
        public async Task<IActionResult> Index()
        {
            var animeList = await _animeService.GetAllAnimeAsync();
            return animeList is not null ?
                        View(animeList) :
                        Problem("Entity set 'AnimeContext.Animes'  is null.");
        }

        // GET: Animes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            Anime? anime = await _animeService.GetAnimeAsync(id, null);

            return anime is null ? NotFound() : View(anime);
        }

        // GET: Animes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codename,NameUa,NameEn,Desc,Type,Poster,AnilistId,KitsuId,MalId,ImdbId,Year")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                await _animeService.AddAnimeAsync(anime);
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        // GET: Animes/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var anime = await _animeService.GetAnimeAsync(id, null);

            return anime is null ? NotFound() : View(anime);
        }

        // POST: Animes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codename,NameUa,NameEn,Desc,Type,Poster,AnilistId,KitsuId,MalId,ImdbId,Year")] Anime anime)
        {
            if (id != anime.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _animeService.EditAnimeAsync(anime);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeExists(anime.Id))
                    return NotFound();
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(anime);
        }

        // GET: Animes/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return NotFound();

            var anime = await _animeService.GetAnimeAsync(id, null);

            return anime is null ? NotFound() : View(anime);
        }

        // POST: Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _animeService.GetAllAnimeAsync() is null)
                return Problem("Entity set 'AnimeContext.Animes'  is null.");

            await _animeService.DeleteAnimeAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool AnimeExists(int id)
        {
            return _animeService.GetAnimeAsync(id, null) is not null;
        }
    }
}
