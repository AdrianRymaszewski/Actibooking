﻿using Actibooking.Data;
using Actibooking.Data.Repository;
using Actibooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Actibooking.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actibooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;


        private readonly OrganizationManager _organizationManager;
        public OrganizationsController(IUnitOfWork uow, OrganizationManager organizationManager)
        {
            _uow = uow;
            _organizationManager = organizationManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _organizationManager.CheckIfEmpty();
            return Ok(organizations);                                      
        }

        [HttpGet("{organizationId}")]
        public async Task<IActionResult> GetOrganization(int organizationId)
        {
            var organization = await _organizationManager.FindOrganzation(organizationId);
            return Ok(organization);            
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrganization(NewOrganizationDTO newOrganizationDTO)
        {
            await _organizationManager.MapOrganization(newOrganizationDTO);
            return Ok("Organization created");
        }

        [HttpDelete("{organizationId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrganization(int organizationId)
        {
            await _uow.OrganizationRepo.DeleteAsync(organizationId);
            await _uow.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrganization(UpdateOrganizationDTO updateOrganizationDTO)
        {
            await _organizationManager.ConvertOrganization(updateOrganizationDTO);
            return Ok("Organization Updated");
        }

    }
}
