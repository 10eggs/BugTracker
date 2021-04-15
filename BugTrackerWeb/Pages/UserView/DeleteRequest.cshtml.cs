using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Persistance.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTrackerWeb.Pages.UserView
{
    public class DeleteRequestModel : PageModel
    {
        private readonly IRequestPersistance _rp;
        public DeleteRequestModel(IRequestPersistance rp)
        {
            _rp = rp;
        }

        public async Task<JsonResult> OnPostDelete(int id)
        {
                var reqToDelete = await _rp.GetByIdAsync(id);
                if (reqToDelete == null)
                {
                    return new JsonResult(new { success = false, message = "Error while Deleting" });

                }
                await _rp.DeleteAsync(reqToDelete);
                return new JsonResult(new { success = true, message = "Delete successful" });

        }

    }
}
