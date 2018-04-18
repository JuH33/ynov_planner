using Microsoft.AspNetCore.Mvc;
using events_planner.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using events_planner.Services;
using Microsoft.AspNetCore.Authorization;
using events_planner.Deserializers;
using events_planner.Constants.Services;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;

namespace events_planner.Controllers {
    [Route("api/[controller]")]
    public class EventController : BaseController {
        private IEventServices Services { get; set; }

        public EventController(PlannerContext context,
                               IEventServices services) {
            Context = context;
            Services = services;
        }

        #region User CRUD

        /// <summary>
        /// Create a new Event
        /// </summary>
        /// <response code="401">User token is not permitted</response>
        /// <response code="500">if the credential given is not valid or DB update failed</response>
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] EventDeserializer eventFromRequest) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            Event eventModel;
            try {
                eventFromRequest.BindWithEventModel<Event>(out eventModel);
                Context.Event.Add(eventModel);
                Context.SaveChanges();
            } catch (DbUpdateException e) {
                return BadRequest(e.InnerException.Message);
            }

            return CreatedAtAction(nameof(Read), new { id = eventModel.Id }, eventModel);
        }

        /// <summary>
        /// Get Event By ID
        /// </summary>
        /// <response code="401">User/Admin token is not permitted</response>
        /// <response code="500">if the credential given is not valid or DB update failed</response>
        [HttpGet("{id}"), Authorize(Roles = "Admin, Student")]
        public async Task<IActionResult> Read(int id) {
            Event eventModel = await Context.Event
                                            .Include(ev => ev.Images)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(e => e.Id == id);

            Price price = await Context.Price
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(p => p.EventId == id && p.RoleId == CurrentUser.Role.Id);

            if (eventModel == null) { return NotFound(id); }

            return new ObjectResult(new { Event = eventModel, Price = price });
        }

        /// <summary>
        /// Update an event by ID
        /// </summary>
        /// <response code="401">User/Admin token is not permitted</response>
        /// <response code="500">if the credential given is not valid or DB update failed</response>
        [HttpPatch("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] EventUpdatableDeserializer eventFromRequest, int id) {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            Event eventModel = await Context.Event
                                            .FirstOrDefaultAsync((obj) => obj.Id == id);

            if (eventModel == null) return NotFound("Event not found");

            try {
                eventFromRequest.BindWithModel(ref eventModel);
                Context.Event.Update(eventModel);
                int i = await Context.SaveChangesAsync();
                if (i < 1)
                    throw new DbUpdateException("Nothing has been updated",
                                                innerException: new System.Exception());
            } catch (DbUpdateException e) {
                return BadRequest(e.InnerException.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete an Event
        /// </summary>
        /// <response code="401">User/Admin token is not permitted</response>
        /// <response code="500">if the credential given is not valid or DB update failed</response>
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id) {
            Event eventModel = await Context.Event.FirstOrDefaultAsync((obj) => obj.Id == id);
            if (eventModel == null) return NotFound("Event Not Found");

            Context.Event.Remove(eventModel);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Get Sub Operations

        /// <summary>
        /// Return a list of Events, parameters from and to can be used to select an interval of events
        /// </summary>
        /// <param name="order">0 = ASC, 1 = DESC</param>
        /// <response code="401">User/Admin token is not permitted</response>
        /// <response code="500">if the credential given is not valid or DB update failed</response>
        [HttpGet("list/{order}"), AllowAnonymous]
        public async Task<IActionResult> GetList(int order) {
            IQueryable<Event> query;
            Event[] events;

            string from = HttpContext.Request.Query["from"];
            string to = HttpContext.Request.Query["to"];
            string limit = HttpContext.Request.Query["limit"];
            bool loadImage = HttpContext.Request.Query["images"] == bool.TrueString;

            switch ((OrderBy)order) {
                case (OrderBy.ASC):
                    query = Context.Event.OrderBy((Event arg) => arg.StartAt);
                    break;
                case (OrderBy.DESC):
                default:
                    query = Context.Event.OrderByDescending((Event arg) => arg.StartAt);
                    break;
            }

            if (from != null)
                query = query.Where(cc => cc.OpenAt >= DateTime.Parse(from));
            if (to != null)
                query = query.Where(cc => cc.EndAt <= DateTime.Parse(to));
            if (loadImage)
                query = query.Include(ev => ev.Images);
            if (limit != null) {
                Match match = (new Regex("[0-9]+")).Match(limit);
                if (!String.IsNullOrEmpty(match.Value))
                    query = query.Take(int.Parse(match.Value));
            }

            try {
                events = await query.AsNoTracking().ToArrayAsync();
            } catch (Exception e) {
                return BadRequest(e.InnerException.Message);
            }

            return new ObjectResult(events);
        }

        #endregion

        #region Set Sub Operation 

        /// <summary>
        /// Add a Category the An Event
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <param name="eventId">The Event ID</param>
        /// <response code="401">User/Admin token is not permitted</response>
        /// <response code="404">if event or category has not been found</response>
        /// <response code="500">if the credential given is not valid or DB update failed</response>
        [HttpGet("{eventId}/category/{categoryId}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(int eventId, int categoryId) {

            if (Context.EventCategory.Any((EventCategory ec) => ec.CategoryId == categoryId && ec.EventId == eventId)) {
                return BadRequest("Bind already exist");
            }

            Event eventModel = await Context.Event
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync((Event arg) => arg.Id == eventId);

            Category category = await Context.Category
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync((Category arg) => arg.Id == categoryId);

            if (eventModel == null || category == null) { return NotFound(); }

            EventCategory eventCategory = new EventCategory() { Category = category, Event = eventModel };

            try {
                Context.EventCategory.Update(eventCategory);
                await Context.SaveChangesAsync();
            } catch (DbUpdateException e) {
                return BadRequest(e.InnerException.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a category from the Event
        /// </summary>
        /// <param name="categoryId">The category ID</param>
        /// <param name="eventId">Then Event ID</param>
        /// <response code="401">User/Admin token is not permitted</response>
        /// <response code="500">if the credential given is not valid or DB update failed</response>
        /// <response code="200">If category has been removed</response>
        [HttpDelete("{eventId}/category/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int eventId, int categoryId) {
            Event eventModel = await Context.Event.Include(inc => inc.EventCategory)
                                            .FirstOrDefaultAsync((Event ev) => ev.Id == eventId);

            if (eventModel == null ||
                eventModel.EventCategory == null ||
                !eventModel.EventCategory.Any(cc => cc.CategoryId == categoryId)) { return NotFound(); }

            EventCategory category = eventModel.EventCategory.FirstOrDefault(cc => cc.CategoryId == categoryId);

            try {
                Context.EventCategory.Remove(category);
                await Context.SaveChangesAsync();
            } catch (DbUpdateException e) {
                return BadRequest(e.InnerException.Message);
            }

            return new OkObjectResult("Category removed");
        }

        /// <summary>
        /// Add images to the server.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST
        ///     imagesData = [{
        ///         "fileName": "chaton-diarrhee.jpg",
        ///         "title": "mytitle",
        ///         "alt": "myalt"
        ///         },
        ///         {
        ///             "fileName": "myfilename",
        ///             "title": "mytitle",
        ///             "alt": "myalt"
        ///         }]
        ///
        /// </remarks>
        /// <param name="eventId"></param>
        /// <returns>200</returns>
        [HttpPost("{eventId}/upload/images"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadImages(int eventId,
                                                      [FromServices] IImageServices imageServices,
                                                      [FromForm] ImageUploadDeserializer imageCore) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var imagesData = imageCore.GetImagesData();

            Event @event = await Services.GetEventByIdAsync(eventId);
            if (@event == null) return NotFound("Event Not Found");

            IFormFileCollection files = HttpContext.Request.Form.Files;

            string baseFileName = @event.Id.ToString() + "_" +
                                        @event.CreatedAt.ToString("yyyyMMdd");

            try {
                Dictionary<string, string> urls = await imageServices.UploadImageAsync(files, baseFileName);
                List<Image> rangeUpdate = new List<Image>();

                foreach (var pair in urls) {
                    Image image = new Image() { Url = pair.Key, EventId = @event.Id };
                    var current = imagesData.FirstOrDefault((arg) => arg.FileName == pair.Value);

                    if (current != null) {
                        image.Alt = current.Alt;
                        image.Title = current.Title;
                    }

                    rangeUpdate.Add(image);
                }

                Context.Images.UpdateRange(rangeUpdate);
                await Context.SaveChangesAsync();
            } catch (Exception e) when (e is IOException ||
                                        e is DbUpdateException) {
                string message = (e.InnerException != null) ? e.InnerException.Message :
                                                               e.Message;
                return BadRequest(message);
            };

            return Ok();
        }

        #endregion
    }
}