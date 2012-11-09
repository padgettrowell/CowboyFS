using System;
using System.Web.Mvc;
using CowboyFS.Web.UI.Models;
using CowboyFS.Web.UI.Services;
using CowboyFS.Web.UI.ViewModels;
using FileResult = CowboyFS.Web.UI.Models.FileResult;

namespace CowboyFS.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private FileRepository _repository;

        public HomeController()
        {
            _repository = new FileRepository();
        }

        public ActionResult Index(int? library, string relPath = "")
        {
            var libaries = _repository.FetchLibraries();

            Library selectedLibrary;
            
            // if we don't have a valid library, render the libary list
            if (!_repository.TryGetLibrary(library, out selectedLibrary))
                return View("Libraries", _repository.FetchLibraries());


           // render the results for the specified path, relative to the specified library
            return View(new BrowseViewModel(selectedLibrary, relPath, _repository.FetchResultsForPath(selectedLibrary, relPath)));

        }

    }
}
