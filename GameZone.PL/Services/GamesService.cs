
using GameZone.PL.Interfaces;

namespace GameZone.PL.Services;
  public class GamesService : IGamesService
    {
        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GamesService(IGenericRepository<Game> gameRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _gameRepository = gameRepository;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}/images/";
        }

        public IEnumerable<Game> GetAll()
        {
            return _gameRepository.GetAll();
        }

        public Game? GetById(int id)
        {
            return _gameRepository.GetById(id);
        }

        public async Task CreateAsync(CreateGameFormViewModel model)
        {
            var coverName = await SaveCoverAsync(model.Cover);

            var game = new Game
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CaregoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };

            _gameRepository.Add(game);
            await _gameRepository.SaveAsync();
        }

        public async Task<Game?> UpdateAsync(EditGameFormViewModel model)
        {
            var game = _gameRepository.GetById(model.Id);

            if (game == null)
                return null;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

            if (model.Cover != null)
            {
                var oldCover = game.Cover;
                game.Cover = await SaveCoverAsync(model.Cover);

                // Delete old cover
                var oldCoverPath = Path.Combine(_imagesPath, oldCover);
                if (File.Exists(oldCoverPath))
                {
                    File.Delete(oldCoverPath);
                }
            }

            _gameRepository.Update(game);
            await _gameRepository.SaveAsync();

            return game;
        }

        public async Task DeleteAsync(int id)
        {
            var game = _gameRepository.GetById(id);
            if (game != null)
            {
                var coverPath = Path.Combine(_imagesPath, game.Cover);
                if (File.Exists(coverPath))
                {
                    File.Delete(coverPath);
                }

                _gameRepository.Delete(game);
                await _gameRepository.SaveAsync();
            }
        }

        private async Task<string> SaveCoverAsync(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await cover.CopyToAsync(stream);
            }

            return coverName;
        }
    }