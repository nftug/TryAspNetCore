#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public SelectList Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Genre { get; set; }

        public async Task OnGetAsync()
        {
            // メインテーブルを生成
            Movie = await GetMovieListAsync(_context, Search, Genre);

            // ジャンル一覧を生成
            Genres = await GetGenreSelectListAsync(_context);
        }

        public async Task<List<Movie>> GetMovieListAsync(RazorPagesMovieContext context, string search, string genre)
        {
            // データ一覧を生成
            var movies = context.Movie as IQueryable<Movie>;

            if (!string.IsNullOrEmpty(search))
            {
                movies = movies.Where(x =>
                    x.Title.ToLower().Contains(search.ToLower())
                );
            }
            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(x => x.Genre == genre);
            }

            movies = movies.OrderByDescending(item => item.CreateDate);
            return await movies.ToListAsync();
        }

        public async Task<SelectList> GetGenreSelectListAsync(RazorPagesMovieContext context)
        {
            // ジャンル一覧を生成
            var genreList = await context.Movie.OrderBy(x => x.Genre)
                                                .Select(x => x.Genre)
                                                .Distinct()
                                                .ToListAsync();
            return new SelectList(genreList);
        }
    }
}
