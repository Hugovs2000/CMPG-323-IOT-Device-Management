using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;
using System.Xaml.Permissions;
using System.Security.Policy;
using Zone = DeviceManagement_WebApp.Models.Zone;
using Microsoft.AspNetCore.Authorization;

namespace DeviceManagement_WebApp.Controllers
{
    [Authorize]
    public class ZonesController : Controller
    {
        private readonly IZonesRepository _zonesRepository;
        public ZonesController(IZonesRepository zonesRepository)
        {
            _zonesRepository = zonesRepository;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            return View(_zonesRepository.GetAll());
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zonesRepository.GetById(id);
            if (zone == null)
            {
                return NotFound();
            }

            return View(_zonesRepository.GetById(id));
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated,Device")] Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            _zonesRepository.Add(zone);
            _zonesRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (_zonesRepository.GetById(id) == null)
            {
                return NotFound();
            }

            return View(_zonesRepository.GetById(id));
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _zonesRepository.Remove(_zonesRepository.GetById(id));
            _zonesRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var zone = _zonesRepository.GetById(id);
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated,Device")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }
            try
            {
                _zonesRepository.Update(zone);
                _zonesRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (id == null)
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
    }
}
