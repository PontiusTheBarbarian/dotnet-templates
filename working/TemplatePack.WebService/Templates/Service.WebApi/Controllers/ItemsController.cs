using System.ComponentModel.DataAnnotations;
using System.Net;
using Company.WebApi.Modules.FeatureManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Swashbuckle.AspNetCore.Annotations;
#if (IncludeAuditing)
using Audit.WebApi;
#endif

namespace Company.WebApi.Controllers
{
	[Authorize]
	[ApiController]
    [ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
#if (IncludeAuditing)
	[AuditApi(EventTypeName = "{controller}/{action} ({verb})",
	 IncludeResponseBody = true,
	  IncludeRequestBody = true,
	   IncludeModelState = true)]
#endif
	public class ItemsController : ControllerBase
	{
		[HttpGet]
		[AllowAnonymous]
		[Route("{id:int?}")]
		[FeatureGate(MyFeatureFlags.FeatureA)]
		[SwaggerOperation(
			Summary = "GET Items/s",
			Description = "Get an item/s",
			Tags = new[] { "Items" })]
		[SwaggerResponse(
			(int)HttpStatusCode.OK,
		 	Type = typeof(string))]
		[SwaggerResponse(
			(int)HttpStatusCode.NotFound,
		 	Type = typeof(string))]
		public string Get([FromRoute]int? id)
		{
			return $"Id: {id}";
		}


		[HttpPost]
		[AllowAnonymous]
		[Route("{id:int}")]
		[SwaggerOperation(
			Summary = "POST an Item",
			Description = "POST an item",
			Tags = new[] { "Items" })]
		[SwaggerResponse(
			(int)HttpStatusCode.Created,
		 	Type = typeof(string))]
		[SwaggerResponse(
			(int)HttpStatusCode.BadRequest,
		 	Type = typeof(string))]
		[FeatureGate(MyFeatureFlags.FeatureB)]
		public string Post(
			[FromRoute][Required] int id,
			[Required][FromBody]string name)
		{
			return $"Id: {id}, name: {name}";
		}
	}
}
