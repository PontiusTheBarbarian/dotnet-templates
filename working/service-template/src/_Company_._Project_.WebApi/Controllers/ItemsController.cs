// <copyright file="ItemsController.cs" company="_Company_.">
// Copyright (c) _Company_.. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.Net;
using _Company_._Project_.WebApi.Modules.FeatureManagement;
using Audit.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Swashbuckle.AspNetCore.Annotations;

#pragma warning disable SA1300 // Element should begin with upper-case letter
namespace _Company_._Project_.WebApi.Controllers
#pragma warning restore SA1300 // Element should begin with upper-case letter
{
	/// <summary>
	/// Items controller.
	/// </summary>
	[Authorize]
	[ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[AuditApi(
		EventTypeName = "{controller}/{action} ({verb})",
		IncludeResponseBody = true,
		IncludeRequestBody = true,
		IncludeModelState = true)]
	public class ItemsController : ControllerBase
	{
		/// <summary>
		/// Get endpoint.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <returns><see cref="string"/>.</returns>
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
		public string Get([FromRoute] int? id)
		{
			return $"Id: {id}";
		}

		/// <summary>
		/// Get endpoint.
		/// </summary>
		/// <param name="id">Id.</param>
		/// <param name="name">Name.</param>
		/// <returns><see cref="string"/>.</returns>
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
			[Required][FromBody] string name)
		{
			return $"Id: {id}, name: {name}";
		}
	}
}
