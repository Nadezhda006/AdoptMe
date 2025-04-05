using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdoptMe.DTOs
{
    public class CreateVisitDTO:VisitDTO
    {
        public List<SelectListItem> Pets { get; set; }
        public List<SelectListItem> Vets { get; set; }
    }
}
