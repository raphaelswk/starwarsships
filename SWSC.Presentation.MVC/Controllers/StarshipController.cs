using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SWSC.Business.Impl;
using SWSC.Entities;
using SWSC.Presentation.MVC.Mappers;
using SWSC.Presentation.MVC.ViewModels;

namespace SWSC.Presentation.MVC.Controllers
{
    public class StarshipController : Controller
    {
        // GET: Starship/Index
        public async Task<ActionResult> Index(decimal distance)
        {
            try
            {
                var starships = await new StarshipBL().GetAllStarshipsAsync(distance);

                var starshipsVM = SWSCMapper.Map<IEnumerable<Starship>, IEnumerable<StarshipVM>>(starships);

                ViewBag.Distance = distance;

                return await Task.FromResult(View(starshipsVM));
            }
            catch
            {
                return await Task.FromResult(RedirectToAction(nameof(GetDistance)));
            }
        }

        // GET: Starship/GetDistance
        public async Task<ActionResult> GetDistance() => await Task.FromResult(View());
        
        // POST: Starship/GetDistance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetDistance(DistanceVM distanceVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await Task.FromResult(RedirectToAction(nameof(Index), new { distance = distanceVM.MGLT }));
                }
                return await Task.FromResult(View());
            }
            catch (ArgumentNullException e)
            {
                ModelState.AddModelError(nameof(distanceVM.MGLT), e.Message);
                return await Task.FromResult(View(distanceVM));
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError(nameof(distanceVM.MGLT), e.Message);
                return await Task.FromResult(View(distanceVM));
            }
            catch (Exception)
            {
                return await Task.FromResult(View());
            }
        }
    }
}
